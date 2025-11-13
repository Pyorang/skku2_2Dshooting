using UnityEngine;

public class BezierBullet : MonoBehaviour
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

    [Header("베지어")]
    private float _currentDuration = 0;
    public float Duration;
    private bool _fliped;
    private Transform StartPoint;
    private Vector3 MidPoint1;
    private Vector3 MidPoint2;
    private Vector3 EndPoint;

    private void Start()
    {
        StartPoint = transform;
        MidPoint1 = transform.position + new Vector3(-2, 1, 0);
        MidPoint2 = transform.position + new Vector3(-1, 3, 0);
        EndPoint = transform.position + new Vector3(0, 20, 0);
        _bulletSpeed = StartSpeed;
        _fliped = Random.Range(0,2) == 0 ? false : true;

        if(_fliped )
        {
            MidPoint1 = new Vector3(-MidPoint1.x, MidPoint1.y, MidPoint1.z);
            MidPoint2 = new Vector3(-MidPoint2.x, MidPoint2.y, MidPoint2.z);
        }
    }
    private void Update()
    {
        if(_currentDuration <= 1f)
        {
            _currentDuration += (Time.deltaTime / Duration) * _bulletSpeed;
            transform.position = BazierMove();
        }
        IncreaseBulletSpeed();
    }

    private Vector3 BazierMove()
    {
        Vector3 a = Vector3.Lerp(StartPoint.position, MidPoint1, _currentDuration);
        Vector3 b = Vector3.Lerp(MidPoint1, MidPoint2, _currentDuration);
        Vector3 c = Vector3.Lerp(MidPoint2, EndPoint, _currentDuration);
        Vector3 d = Vector3.Lerp(a, b, _currentDuration);
        Vector3 e = Vector3.Lerp(b,c, _currentDuration);
        Vector3 result = Vector3.Lerp(d, e, _currentDuration);
        return result;
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

        if (randomNumber > CriticalHit)
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
            if (enemy != null)
            {
                enemy.Hit(Damage * enemy.DamageMultiplierByHitbox(transform.position.x));

                if (GetCriticalHit())
                {
                    enemy.KnockBack();
                }
            }

            Destroy(gameObject);
        }
    }
}
