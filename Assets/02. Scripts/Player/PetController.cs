using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private static readonly float _timeDifference = 0.3f;
    private float _timeElapsed = 0;

    [Header("펫 오브젝트")]
    [SerializeField] private GameObject _pet;

    private Queue<Vector3> _locationList = new Queue<Vector3>();
    private Vector3 _prevPosition;
    private int deltaFrame = 0;

    private void Update()
    {
        if(_prevPosition != transform.position)
        {
            _locationList.Enqueue(transform.position);
            _prevPosition = transform.position;
        }

        _timeElapsed += Time.deltaTime;

        if (_timeElapsed < _timeDifference)
        {
            deltaFrame++;
            return;
        }

        if(_pet != null)
        {
            if(deltaFrame < _locationList.Count)
            {
                _pet.transform.position = _locationList.Dequeue();
            }
        }
    }
}
