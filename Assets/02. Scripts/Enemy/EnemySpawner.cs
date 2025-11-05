using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 쿨타임")]
    private float _currentSpawnCoolTime;
    public float _spawnCoolTime = 5f;

    [Header("Enemy 프리팹")]
    [SerializeField] private GameObject _enemyPrfab;

    private void Start()
    {
        _currentSpawnCoolTime = _spawnCoolTime;
    }
    private void Update()
    {
        _currentSpawnCoolTime -= Time.deltaTime;

        if(_currentSpawnCoolTime <= 0)
        {
            _currentSpawnCoolTime = _spawnCoolTime;
            Instantiate(_enemyPrfab, transform);
        }
    }
}
