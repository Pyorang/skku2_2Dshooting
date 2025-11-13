using UnityEngine;

public enum EnemyType
{
    Enemy,
    ChasingEnemy,
    TeleportEnemy
}

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 쿨타임")]
    private float _currentSpawnCoolTime;
    [SerializeField] private float _minSpawnCoolTime = 1f;
    [SerializeField] private float _maxSpawnCoolTime = 3f;

    [Header("Enemy 프리팹")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("스폰 확률")]
    public int[] EnemySpawnRate;

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
            
            EnemyType enemyType = SelectRandomEnemy();
            
            switch(enemyType)
            {
                case EnemyType.ChasingEnemy:
                    EnemyFactory.Instance.MakeChasingEnemy(transform.position);
                    break;
                case EnemyType.TeleportEnemy:
                    EnemyFactory.Instance.MakeTeleportEnemy(transform.position);
                    break;
                default:
                    EnemyFactory.Instance.MakeEnemy(transform.position);
                    break;
            }
        }
    }

    private EnemyType SelectRandomEnemy()
    {
        int randomNumber = Random.Range(1, 101);
        int enemyType = 0;

        for(int i = 0; i < _enemyPrefabs.Length; i++)
        {
            randomNumber -= EnemySpawnRate[i];

            if(randomNumber <= 0)
            {
                break;
            }

            enemyType++;
        }

        return (EnemyType)enemyType;
    }
}
