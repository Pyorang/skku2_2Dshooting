using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float _currentExistTime = 0;

    [Header("폭탄 생존 시간")]
    [SerializeField] private float _destroyTime = 3f;

    private void Start()
    {
        AudioManager.Instance.PlaySound("Ability", AudioType.SFX);
    }

    private void Update()
    {
        _currentExistTime += Time.deltaTime;

        if (_currentExistTime >= _destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.ProcessInstantDeath();
        }
    }
}
