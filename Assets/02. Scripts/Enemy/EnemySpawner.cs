using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 쿨타임")]
    private float _currentSpawnCoolTime;
    [SerializeField] private float _minSpawnCoolTime = 1f;
    [SerializeField] private float _maxSpawnCoolTime = 3f;

    [Header("Enemy 프리팹")]
    [SerializeField] private GameObject _enemyPrfab;

    private void Start()
    {
        _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
    }
    private void Update()
    {
        _currentSpawnCoolTime -= Time.deltaTime;

        if(_currentSpawnCoolTime <= 0)
        {
            _currentSpawnCoolTime = Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
            Instantiate(_enemyPrfab, transform);
        }
    }
}
