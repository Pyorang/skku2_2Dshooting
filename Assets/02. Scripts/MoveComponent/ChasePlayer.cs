using UnityEngine;

public class ChasePlayer : MovementComponent
{
    private Transform _playerTransform;
    private static readonly float s_rotateOffset = 90f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            _playerTransform = player.transform;
        }
    }

    protected override void Move()
    {
        if (_playerTransform == null) return;

        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;

        LookAtPlayer(direction);
    }

    private void LookAtPlayer(Vector3 direction)
    {
        float radian = Mathf.Atan2(direction.y, direction.x);
        float degree = radian * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, degree + s_rotateOffset);
    }
}
