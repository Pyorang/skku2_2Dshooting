using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private int[] _itemSpawnRatios;

    public GameObject GetRandomItem()
    {
        int randomNumber = Random.Range(1, 101);

        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            randomNumber -= _itemSpawnRatios[i];

            if (randomNumber <= 0)
            {
                return _itemPrefabs[i];
            }
        }

        return _itemPrefabs[0];
    }
}
