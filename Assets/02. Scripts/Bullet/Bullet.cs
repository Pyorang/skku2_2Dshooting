using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("총알 데미지")]
    public float Damage = 60f;

    [Header("총알 크리티컬 확률")]
    public int CriticalHit = 50;

    private float _bulletSpeed;

    [Header("이동")]
    public float StartSpeed = 1;
    public float MaxSpeed = 7;
    public float ZeroToMaxSpeedTime = 1.2f;

    private void Start()
    {
        _bulletSpeed = StartSpeed;
    }
    private void Update()
    {
        transform.position += Vector3.up * _bulletSpeed * Time.deltaTime;
        IncreaseBulletSpeed();
    }

    private void IncreaseBulletSpeed()
    {
        if (_bulletSpeed < MaxSpeed)
        {
            _bulletSpeed += ((MaxSpeed - StartSpeed) / ZeroToMaxSpeedTime) * Time.deltaTime;
            _bulletSpeed = Mathf.Min(_bulletSpeed, MaxSpeed);
        }
    }

    private bool GetCriticalHit()
    {
        int randomNumber = Random.Range(1, 101);
        
        if(randomNumber > CriticalHit)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // NOTE : CompareTag 메소드로 검사하였기 때문에
            // enemy가 null일 가능성은 거의 없지만, 안전하게 처리
            if (enemy!= null)
            {
                enemy.Hit(Damage * enemy.DamageMultiplierByHitbox(transform.position.x));

                if(GetCriticalHit())
                {
                    enemy.KnockBack();
                }
            }

            Destroy(gameObject);
        }
    }
}
