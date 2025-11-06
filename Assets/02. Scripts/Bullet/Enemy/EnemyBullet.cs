using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float _bulletDamage = 1f;
    [SerializeField] private float _bulletSpeed = 1f;

    private void Update()
    {
        transform.position += Vector3.down * _bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Hit(_bulletDamage);
            Destroy(gameObject);
        }
    }
}
