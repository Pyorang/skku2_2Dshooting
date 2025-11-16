using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject AssistBulletPrefab;

    private static BulletFactory s_Instance;
    public static BulletFactory Instance => s_Instance;

    [Header("풀링")]
    public int bulletPoolSize = 30;
    public int subBulletPoolSize = 50;
    public GameObject[] _bulletObjectPool;
    public GameObject[] _subBulletObjectPool;

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
        _bulletObjectPool = new GameObject[bulletPoolSize];
        _subBulletObjectPool = new GameObject[subBulletPoolSize];

        for(int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObject = Instantiate(BulletPrefab, transform);

            _bulletObjectPool[i] = bulletObject;
            bulletObject.SetActive(false);
        }

        for(int i =0; i< subBulletPoolSize; i++)
        {
            GameObject subBulletObject = Instantiate(AssistBulletPrefab, transform);

           _subBulletObjectPool[i] = subBulletObject;
            subBulletObject.SetActive(false);
        }
    }

    public GameObject MakeBullet(Vector3 position)
    {
        for(int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bulletObject = _bulletObjectPool[i];

            if(bulletObject.activeSelf == false)
            {
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);

                return bulletObject;
            }
        }

        return IncreaseBulletPoolSize();
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        for (int i = 0; i < subBulletPoolSize; i++)
        {
            GameObject subBulletObject = _subBulletObjectPool[i];

            if (subBulletObject.activeSelf == false)
            {
                subBulletObject.transform.position = position;
                subBulletObject.SetActive(true);

                return subBulletObject;
            }
        }

        return IncreaseSubBulletPoolSize();
    }

    private GameObject IncreaseBulletPoolSize()
    {
        int newPoolSize = (int)(bulletPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];
        
        for(int i = 0; i < bulletPoolSize; i++)
        {
            newPool[i] = _bulletObjectPool[i];
        }

        for(int i = bulletPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(BulletPrefab, transform);
            newPool[i].SetActive(false);
        }

        _bulletObjectPool = newPool;
        bulletPoolSize = newPoolSize;

        _bulletObjectPool[bulletPoolSize - 1].SetActive(true);
        return _bulletObjectPool[bulletPoolSize - 1];
    }

    private GameObject IncreaseSubBulletPoolSize()
    {
        int newPoolSize = (int)(subBulletPoolSize * 3 / 2);
        GameObject[] newPool = new GameObject[newPoolSize];

        for (int i = 0; i < subBulletPoolSize; i++)
        {
            newPool[i] = _subBulletObjectPool[i];
        }

        for (int i = subBulletPoolSize; i < newPoolSize; i++)
        {
            newPool[i] = Instantiate(AssistBulletPrefab, transform);
            newPool[i].SetActive(false);
        }

        _subBulletObjectPool = newPool;
        subBulletPoolSize = newPoolSize;

        _subBulletObjectPool[subBulletPoolSize - 1].SetActive(true);
        return _subBulletObjectPool[subBulletPoolSize - 1];
    }
}
