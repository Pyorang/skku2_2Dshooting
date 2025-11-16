using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("중앙이 아닌 경우의 데미지 배율")]
    public float NonCenterHitboxDamageMultiplier = 0.8f;

    [Header("스탯")]
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _knockBackPower = 1;
    private float _startHealth;

    [Header("아이템 드랍 비율")]
    [SerializeField] private int _itemDropRate = 50;


    [Header("폭발 효과 프리팹")]
    [SerializeField] private GameObject _explosionPrefab;

    [Header("몬스터 점수")]
    [SerializeField] private int _monsterPoint = 100;

    private static readonly Color s_hitColor = Color.red;
    private static readonly WaitForSeconds s_changeColorTime = new WaitForSeconds(0.1f);
    private SpriteRenderer _spriteRenderer;
    private Sprite _firstSprite;

    private void Awake()
    {
        _startHealth = _health;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _firstSprite = _spriteRenderer.sprite;
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        _spriteRenderer.size = Vector2.one;
        StopAllCoroutines();
    }

    private void Init()
    {
        _spriteRenderer.sprite = _firstSprite;
        _spriteRenderer.color = Color.white;
        _spriteRenderer.size = Vector2.one;
        _health = _startHealth;
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if(gameObject.activeSelf)
        {
            StartCoroutine(HitColorChanged());
        }

        if(_health <= 0 )
        {
            DropItem();
            MakeExplosionEffect();
            AudioManager.Instance.PlaySound("Explosion", AudioType.SFX);

            ScoreManager.Instance.AddScore(_monsterPoint);

            CameraShaker.Instance.StartShake();
            gameObject.SetActive(false);
        }
    }

    public void ProcessInstantDeath()
    {
        Hit(_health);
    }

    private void MakeExplosionEffect()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    public void KnockBack()
    {
        transform.position += Vector3.up * _knockBackPower;
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

            gameObject.SetActive(false);
        }
    }
    public void DropItem()
    {
        if (UnityEngine.Random.Range(1, 101) >= _itemDropRate)
        {
            ItemSpawn itemSpawn = gameObject.GetComponent<ItemSpawn>();
            if (itemSpawn != null)
            {
                GameObject item = Instantiate(itemSpawn.GetRandomItem());
                item.transform.position = transform.position; 
            }
        }
    }

    private IEnumerator HitColorChanged()
    {
        _spriteRenderer.color = s_hitColor;
        yield return s_changeColorTime;
        _spriteRenderer.color = Color.white;
    }
}
