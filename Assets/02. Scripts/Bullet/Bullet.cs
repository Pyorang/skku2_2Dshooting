using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed;

    [Header("이동")]
    public float StartSpeed = 1;
    public float MaxSpeed = 7;
    public float ZeroToMaxSpeedTime = 1.2f;

    private void Start()
    {
        _bulletSpeed = StartSpeed;
    }
    private void Update()
    {
        transform.position += Vector3.up * _bulletSpeed * Time.deltaTime;
        IncreaseBulletSpeed();
    }

    private void IncreaseBulletSpeed()
    {
        if (_bulletSpeed < MaxSpeed)
        {
            _bulletSpeed += ((MaxSpeed - StartSpeed) / ZeroToMaxSpeedTime) * Time.deltaTime;
            _bulletSpeed = Mathf.Min(_bulletSpeed, MaxSpeed);
        }
    }
}
