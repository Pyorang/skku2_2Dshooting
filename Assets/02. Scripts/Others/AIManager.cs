using System.Collections.Generic;
using UnityEngine;

public struct DistanceInfo
{
    public GameObject Object;
    public float SqrDistance;

    public DistanceInfo(GameObject obj, float sqrDist)
    {
        Object = obj;
        SqrDistance = sqrDist;
    }
}

public class AIManager : MonoBehaviour
{
    private List<GameObject> _managingObjects = new List<GameObject>();
    private static AIManager _instance;
    public static AIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    void Start()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddObject(GameObject obj)
    {
        _managingObjects.Add(obj);
    }

    public void RemoveObject(GameObject obj)
    {
        _managingObjects.Remove(obj);
    }

    public List<GameObject> GetManagingObjects()
    {
        return _managingObjects;
    }
}
