using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 쿨타임")]
    private float _currentSpawnCoolTime;
    [SerializeField] private float _minSpawnCoolTime = 1f;
    [SerializeField] private float _maxSpawnCoolTime = 3f;

    [Header("Enemy 프리팹")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _chasingEnemyPrefab;

    [Header("스폰 확률")]
    public int EnemySpawnRate = 70;
    public int ChasingEnemySpawnRate = 30;

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
            Instantiate(SelectRandomEnemy(), transform);
        }
    }

    private GameObject SelectRandomEnemy()
    {
        int randomNumber = Random.Range(1, 101);

        randomNumber -= EnemySpawnRate;

        if(randomNumber < 0)
        {
            return _enemyPrefab;
        }

        randomNumber -= ChasingEnemySpawnRate;

        if (randomNumber - ChasingEnemySpawnRate <0)
        {
            return _chasingEnemyPrefab;
        }

        return _enemyPrefab;
    }
}
