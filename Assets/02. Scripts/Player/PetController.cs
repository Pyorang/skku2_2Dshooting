using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private static readonly float _startDelay = 0.3f;
    private float _timeElapsed = 0;

    [Header("펫 오브젝트")]
    [SerializeField] private Transform _petTransform;

    private Queue<Vector3> _locationList = new Queue<Vector3>();
    private Vector3 _prevPosition;
    private int _deltaFrame = 0;

    private void Update()
    {
        SaveLocation();
        WaitForStartDelay();
        ChangePetPosition();
    }

    private void SaveLocation()
    {
        if (_prevPosition != transform.position)
        {
            _locationList.Enqueue(transform.position);
            _prevPosition = transform.position;
        }
    }

    private void WaitForStartDelay()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed < _startDelay)
        {
            _deltaFrame++;
            return;
        }
    }

    private void ChangePetPosition()
    {
        if (_petTransform != null)
        {
            if (_deltaFrame < _locationList.Count)
            {
                _petTransform.transform.position = _locationList.Dequeue();
            }
        }
    }
}
