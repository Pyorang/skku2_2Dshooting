using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _currentExistTime = 0;
    private float _destroyTime = 3f;

    [Header("이동")]
    [SerializeField] private float _bombSpeed = 5f;

    private void Update()
    {
        _currentExistTime += Time.deltaTime;

        if (_currentExistTime >= _destroyTime)
        {
            Destroy(gameObject);
        }

        transform.position += Vector3.up * _bombSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.ProcessInstantDeath();
            }
        }
    }
}
