using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("능력치")]
    [SerializeField] private int _speed = 1;
    public float SpeedMultiplier = 1.2f;

    private bool _boostOn = false;
    private float _horiziontalInput = 0f;
    private float _verticalInput = 0f;

    private const float OriginSnapThresholdSq = 0.001f;

    private void Update()
    {
        SpeedControl();
        HandleInput();

        Vector2 direction = new Vector2(_horiziontalInput, _verticalInput).normalized;
        Vector2 displacement = _boostOn ? direction * SpeedMultiplier * _speed : direction * _speed;

        Vector2 newPosition = (Vector2)transform.position + displacement * Time.deltaTime;

        transform.position = CheckBoundary(newPosition);
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

    private void HandleInput()
    {
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

            if (newPosition.x < minX)
                newPosition.x = maxX;
            if (newPosition.x > maxX)
                newPosition.x = minX;
            newPosition.y = Mathf.Clamp(newPosition.y, minY, 0);
        }

        return newPosition;
    }

    public void SpeedUp(int value)
    {
        _speed += value;
    }
}
