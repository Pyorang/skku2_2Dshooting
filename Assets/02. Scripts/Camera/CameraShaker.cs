using System;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Camera _camera;
    private bool _isShaking = false;
    private Vector3 _startPosition;
    private Vector3 _shakeVector;

    [Header("진동 설정값")]
    private float _currentDuration = 0;
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _vibrateMultiplier = 1.0f;

    private void Start()
    {
        _camera = Camera.main;
        _startPosition = _camera.transform.position;
        Enemy.OnDie += StartShake;
    }

    private void Update()
    {
        if( _isShaking )
        {
            _currentDuration += Time.deltaTime;
            if( _currentDuration >= _shakeDuration )
            {
                StopShake();
                return;
            }
            _shakeVector = _startPosition + Vector3.right * _vibrateMultiplier * UnityEngine.Random.Range(-1f, 1f);
            _camera.transform.position = _shakeVector;
        }
    }

    public void StartShake()
    {
        if(_isShaking)
        {
            StopShake();
        }

        _isShaking=true;
    }

    public void StopShake()
    {
        _isShaking = false;
        _camera.transform.position = _startPosition;
        _currentDuration = 0;
    }
}
