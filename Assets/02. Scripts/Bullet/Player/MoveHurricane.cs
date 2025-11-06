using System;
using UnityEngine;

public class MoveHurricane : MonoBehaviour
{
    [Header("각 속도")]
    public float MoveSpeed = 5f;

    [Header("반지름 커지는 속도")]
    public float RadiusIncreaseSpeed = 2f;

    private float _currentRadius = 0;

    //원운동
    private Vector3 _centerPosition;
    private float _angle = 0f;

    private void Start()
    {
        _centerPosition = transform.position;
    }
    private void Update()
    {
        _currentRadius += Time.deltaTime * RadiusIncreaseSpeed;

        Move();
    }

    private void Move()
    {
        _angle += MoveSpeed * Time.deltaTime;

        if (_angle < 360)
        {
            float radian = Mathf.Deg2Rad * _angle;
            float x = _currentRadius * Mathf.Sin(radian);
            float y = _currentRadius * Mathf.Cos(radian);
            transform.position = _centerPosition + new Vector3(x, y);
        }
        else
        {
            _angle -= 360f;
        }
    }
}
