using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("중앙이 아닌 경우의 데미지 배율")]
    public float NonCenterHitboxDamageMultiplier = 0.8f;

    [Header("스탯")]
    public float Speed = 3;
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _knockBackPower = 1;

    private void Update()
    {
        Move();
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if(_health <= 0 )
        {
            Destroy(gameObject);
        }
    }

    public void KnockBack()
    {
        transform.position += Vector3.up * _knockBackPower;
    }

    private void Move()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
    }

    public float DamageMultiplierByHitbox(float bulletXLocation)
    {
        float distance = Mathf.Abs(bulletXLocation - transform.position.x);
        float distanceRatio = distance / (transform.localScale.x / 2f);
        if (distanceRatio > 0.5f)
            return NonCenterHitboxDamageMultiplier;
        else
            return 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Player playerStat = collision.gameObject.GetComponent<Player>();

            // NOTE : CompareTag 메소드로 검사하였기 때문에
            // playerStat이 null일 가능성은 거의 없지만, 안전하게 처리
            if (playerStat != null)
            {
                playerStat.Hit(1);
            }

            Destroy(gameObject);
        }
    }
}
