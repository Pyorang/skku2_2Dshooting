using UnityEngine;

public abstract class Item : MonoBehaviour
{
    abstract protected void ProcessItemEffect(Collider2D collision);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProcessItemEffect(collision);
            Destroy(gameObject);
        }
    }
}
