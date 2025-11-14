using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyFactory : MonoBehaviour
{
    [Header("Enemy 프리팹")]
    [SerializeField] private GameObject[] _enemyPrefabs;

    [Header("풀링")]
    public int EnemyPoolSize = 30;
    public int ChasingEnemyPoolSize = 30;
    public int TeleportEnemyPoolSize = 30;
    public GameObject[] _enemyObjectPool;
    public GameObject[] _chasingEnemyObjectPool;
    public GameObject[] _teleportEnemyObjectPool;

    private static EnemyFactory s_Instance;
    public static EnemyFactory Instance => s_Instance;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PoolInit();
    }

    private void PoolInit()
    {
        _enemyObjectPool = new GameObject[EnemyPoolSize];
        _chasingEnemyObjectPool = new GameObject[ChasingEnemyPoolSize];
        _teleportEnemyObjectPool = new GameObject[TeleportEnemyPoolSize];

        for (int i = 0; i < EnemyPoolSize; i++)
        {
            GameObject enemyObject = Instantiate(_enemyPrefabs[0], transform);

            _enemyObjectPool[i] = enemyObject;
            enemyObject.SetActive(false);
        }

        for (int i = 0; i < ChasingEnemyPoolSize; i++)
        {
            GameObject _chasingEnemy = Instantiate(_enemyPrefabs[1], transform);

            _chasingEnemyObjectPool[i] = _chasingEnemy;
            _chasingEnemy.SetActive(false);
        }

        for (int i = 0; i < TeleportEnemyPoolSize; i++)
        {
            GameObject teleportEnemy = Instantiate(_enemyPrefabs[2], transform);

            _teleportEnemyObjectPool[i] = teleportEnemy;
            teleportEnemy.SetActive(false);
        }
    }

    public GameObject MakeEnemy(Vector3 position)
    {
        for (int i = 0; i < EnemyPoolSize; i++)
        {
            GameObject enemyObject = _enemyObjectPool[i];

            if (enemyObject.activeSelf == false)
            {
                enemyObject.transform.position = position;
                enemyObject.SetActive(true);

                return enemyObject;
            }
        }

        return IncreaseEnemyPoolSize();
    }

    public GameObject MakeChasingEnemy(Vector3 position)
    {
        for (int i = 0; i < ChasingEnemyPoolSize; i++)
        {
            GameObject chasingEnemyObject = _chasingEnemyObjectPool[i];

            if (chasingEnemyObject.activeSelf == false)
            {
                chasingEnemyObject.transform.position = position;
                chasingEnemyObject.SetActive(true);

                return chasingEnemyObject;
            }
        }

        return IncreaseChasingEnemyPoolSize();
    }

    public GameObject MakeTeleportEnemy(Vector3 position)
    {
        for (int i = 0; i < TeleportEnemyPoolSize; i++)
        {
            GameObject teleportEnemyObject = _teleportEnemyObjectPool[i];

            if (teleportEnemyObject.activeSelf == false)
            {
                teleportEnemyObject.transform.position = position;
                teleportEnemyObject.SetActive(true);

                return teleportEnemyObject;
            }
        }

        return IncreaseTeleportEnemyPoolSize();
    }

    private GameObject IncreaseEnemyPoolSize()
    {
        int newPoolSize = (int)(EnemyPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];

        for (int i = 0; i < EnemyPoolSize; i++)
        {
            newPool[i] = _enemyObjectPool[i];
        }

        for (int i = EnemyPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(_enemyPrefabs[0], transform);
            newPool[i].SetActive(false);
        }

        _enemyObjectPool = newPool;
        EnemyPoolSize = newPoolSize;

        _enemyObjectPool[EnemyPoolSize - 1].SetActive(true);
        return _enemyObjectPool[EnemyPoolSize - 1];
    }

    private GameObject IncreaseChasingEnemyPoolSize()
    {
        int newPoolSize = (int)(ChasingEnemyPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];

        for (int i = 0; i < ChasingEnemyPoolSize; i++)
        {
            newPool[i] = _chasingEnemyObjectPool[i];
        }

        for (int i = EnemyPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(_enemyPrefabs[1], transform);
            newPool[i].SetActive(false);
        }

        _chasingEnemyObjectPool = newPool;
        ChasingEnemyPoolSize = newPoolSize;

        _chasingEnemyObjectPool[ChasingEnemyPoolSize - 1].SetActive(true);
        return _chasingEnemyObjectPool[ChasingEnemyPoolSize - 1];
    }

    private GameObject IncreaseTeleportEnemyPoolSize()
    {
        int newPoolSize = (int)(TeleportEnemyPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];

        for (int i = 0; i < TeleportEnemyPoolSize; i++)
        {
            newPool[i] = _teleportEnemyObjectPool[i];
        }

        for (int i = TeleportEnemyPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(_enemyPrefabs[2], transform);
            newPool[i].SetActive(false);
        }

        _teleportEnemyObjectPool = newPool;
        TeleportEnemyPoolSize = newPoolSize;

        _teleportEnemyObjectPool[TeleportEnemyPoolSize - 1].SetActive(true);
        return _teleportEnemyObjectPool[TeleportEnemyPoolSize - 1];
    }
}