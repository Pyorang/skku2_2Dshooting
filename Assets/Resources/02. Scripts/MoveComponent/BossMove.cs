using UnityEngine;

public class BossMove : MovementComponent
{
    private float _startMoveTime = 3f;
    private float _timeElapsed;

    private void OnDisable()
    {
        BossSpawner.Instance.ActivateEnemySpawners();
    }

    protected override void Move()
    {
        _timeElapsed += Time.deltaTime;
        if (_timeElapsed <= _startMoveTime)
        {
            transform.position += Vector3.down * Time.deltaTime * _speed;
        }
    }
}
