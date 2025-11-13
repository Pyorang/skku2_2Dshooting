using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [Header("총알 프리팹")]  // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject AssistBulletPrefab;

    private static BulletFactory s_Instance;
    public static BulletFactory Instance => s_Instance;

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


    public GameObject MakeBullet(Vector3 position)
    {
        return Instantiate(BulletPrefab, position, Quaternion.identity, transform);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(AssistBulletPrefab, position, Quaternion.identity, transform);
    }
}
