using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int _speed = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            playerMove.SpeedUp(_speed);
            Destroy(gameObject);
        }
    }
}
