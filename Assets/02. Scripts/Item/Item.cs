using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private GameObject _gainEffect;
    abstract protected void ProcessItemEffect(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProcessItemEffect(collision);
            collision.GetComponent<Player>().PlayItemGain();
            Instantiate(_gainEffect, collision.transform);
            Destroy(gameObject);
        }
    }
}
