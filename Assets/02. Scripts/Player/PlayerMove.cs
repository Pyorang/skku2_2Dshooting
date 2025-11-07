using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("능력치")]
    [SerializeField] private int _speed = 1;
    [SerializeField] private float _approachAlertDist = 5;
    public float SpeedMultiplier = 1.2f;


    private float _detectDuration = 0;
    [SerializeField] private float _detectDelay = 0.1f;
    private bool _boostOn = false;
    private bool _isAutoPilot = true;
    private float _horiziontalInput = 0f;
    private float _verticalInput = 0f;
    private Vector2 _nextMove;

    private const float OriginSnapThresholdSq = 0.001f;

    private void Update()
    {
        AutoPilotControl();
        
        if(!_isAutoPilot )
        {
            HandleInput();

            Vector2 direction = new Vector2(_horiziontalInput, _verticalInput).normalized;
            Vector2 displacement = _boostOn ? direction * SpeedMultiplier * _speed : direction * _speed;

            Vector2 newPosition = (Vector2)transform.position + displacement * Time.deltaTime;
            transform.position = CheckBoundary(newPosition);
        }

        else
        {
            _detectDuration += Time.deltaTime;
            if(_detectDuration >= _detectDelay)
            {
                _detectDuration = 0;
                _nextMove = CalculateNextMovement() * _speed;
            }

            Vector2 newPosition = (Vector2) transform.position + _nextMove * Time.deltaTime;
            transform.position = CheckBoundary(newPosition);
        }
    }

    public void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _boostOn = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _boostOn = false;
        }
    }

    public void AutoPilotControl()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoPilot = true;
            _boostOn = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isAutoPilot = false;
        }
    }

    private void HandleInput()
    {
        SpeedControl();

        if (Input.GetKey(KeyCode.R))
        {
            Vector2 currentPosition = transform.position;

            if (currentPosition.sqrMagnitude < OriginSnapThresholdSq)
            {
                transform.position = Vector2.zero;
                _horiziontalInput = 0f;
                _verticalInput = 0f;
            }
            else
            {
                Vector2 directionToOrigin = -currentPosition.normalized;
                _horiziontalInput = directionToOrigin.x;
                _verticalInput = directionToOrigin.y;
            }
        }
        else
        {
            _horiziontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }
    }

    public Vector2 CheckBoundary(Vector2 newPosition)
    {
        Camera cam = Camera.main;

        if (cam != null)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.orthographicSize * cam.aspect;
            Vector3 camPos = cam.transform.position;

            float minX = camPos.x - halfWidth - (float)transform.localScale.x / 2;
            float maxX = camPos.x + halfWidth + (float)transform.localScale.x / 2;
            float minY = camPos.y - halfHeight;

            if(_isAutoPilot)
            {
                if (newPosition.x < minX + transform.localScale.x)
                    newPosition.x = minX + transform.localScale.x;
                if (newPosition.x > maxX - +transform.localScale.x)
                    newPosition.x = maxX - +transform.localScale.x;
            }
            else
            {
                if (newPosition.x < minX)
                    newPosition.x = maxX;
                if (newPosition.x > maxX)
                    newPosition.x = minX;
            }

            newPosition.y = Mathf.Clamp(newPosition.y, minY, 0);
        }

        return newPosition;
    }

    public void SpeedUp(int value)
    {
        _speed += value;
    }

    public DistanceInfo[] GetNearestObjects()
    {
        Vector3 playerPosition = transform.position;
        List<GameObject> managedObjects = AIManager.Instance.GetManagingObjects();

        List<DistanceInfo> distanceList = new List<DistanceInfo>(managedObjects.Count);

        for (int i = 0; i < managedObjects.Count; i++)
        {
            GameObject currentObject = managedObjects[i];

            if (currentObject == null) continue;

            float sqrDist = (currentObject.transform.position - playerPosition).sqrMagnitude;

            distanceList.Add(new DistanceInfo(currentObject, sqrDist));
        }

        int n = distanceList.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (distanceList[j].SqrDistance > distanceList[j + 1].SqrDistance)
                {
                    DistanceInfo temp = distanceList[j];
                    distanceList[j] = distanceList[j + 1];
                    distanceList[j + 1] = temp;
                }
            }
        }

        int countToReturn = Mathf.Min(3, distanceList.Count);
        DistanceInfo[] nearestObjectsInfo = new DistanceInfo[countToReturn];

        for (int i = 0; i < countToReturn; i++)
        {
            nearestObjectsInfo[i] = distanceList[i];

            if (nearestObjectsInfo[i].Object.CompareTag("Enemy"))
            {
                if (distanceList[i].SqrDistance < _approachAlertDist)
                {
                    nearestObjectsInfo[i].Object.GetComponent<AIManageableObject>().IsFriendly = false;
                }
            }
        }

        return nearestObjectsInfo;
    }

    public Vector3 CalculateNextMovement()
    {
        DistanceInfo[] nearestObjects = GetNearestObjects();

        Vector3 result = Vector3.zero;

        for(int i = 0; i< nearestObjects.Length; i++)
        {
            result += nearestObjects[i].Object.GetComponent<AIManageableObject>().IsFriendly 
                ? - (transform.position - nearestObjects[i].Object.transform.position) * (nearestObjects.Length - i) * 2
                : (transform.position - nearestObjects[i].Object.transform.position) * (nearestObjects.Length - i) * 2;
        }

        if(result == Vector3.zero)
            return Vector3.zero;

        return result.normalized;
    }
}
