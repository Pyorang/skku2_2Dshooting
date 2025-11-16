using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("스탯")]
    [SerializeField] private float _health = 100f;

    [Header("폭발 효과 프리팹")]
    [SerializeField] private GameObject _explosionPrefab;

    [Header("몬스터 점수")]
    [SerializeField] private int _monsterPoint = 100;

    private static readonly Color s_hitColor = new Color(255, 0, 0, 255);
    private static readonly WaitForSeconds s_changeColorTime = new WaitForSeconds(0.1f);
    private SpriteRenderer _spriteRenderer;
    private float _startMoveTime = 3f;
    private float _timeElapsed;
    private float _speed = 1f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if(_timeElapsed <= _startMoveTime)
        {
            transform.position += Vector3.down * Time.deltaTime * _speed;
        }
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if (gameObject.activeSelf)
        {
            StartCoroutine(HitColorChanged());
        }

        if (_health <= 0)
        {
            MakeExplosionEffect();
            AudioManager.Instance.PlaySound("Explosion", AudioType.SFX);

            ScoreManager.Instance.AddScore(_monsterPoint);

            CameraShaker.Instance.StartShake();
            gameObject.SetActive(false);
        }
    }

    private void MakeExplosionEffect()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerStat = collision.gameObject.GetComponent<Player>();

            if (playerStat != null)
            {
                playerStat.Hit(1);
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
