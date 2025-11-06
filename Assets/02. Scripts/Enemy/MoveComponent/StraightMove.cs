using UnityEngine;

public class StraightMove : MovementComponent
{
    protected override void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * _speed;
    }
}
