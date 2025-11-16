using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject[] _enemySpawners;

    private static BossSpawner s_Instance;
    public static BossSpawner Instance => s_Instance;

    private bool _spawnedBoss = false;

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
    }

    public void SpawnBoss()
    {
       if(!_spawnedBoss)
       {
            DeactivateEnemySpawners();
            Instantiate(_bossPrefab, transform.position, Quaternion.identity);
            _spawnedBoss = true;
       }
    }

    public void ActivateEnemySpawners()
    {
        foreach(GameObject enemySpawner in _enemySpawners)
        {
            enemySpawner.SetActive(true);
        }
    }

    public void DeactivateEnemySpawners()
    {
        foreach (GameObject enemySpawner in _enemySpawners)
        {
            enemySpawner.SetActive(false);
        }
    }
}
