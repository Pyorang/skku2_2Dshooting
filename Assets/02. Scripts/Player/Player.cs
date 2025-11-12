using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 3;
    [SerializeField] private AudioSource _itemGainSFX;
    [SerializeField] private AudioSource _deathSFX;

    public void PlayItemGain()
    {
        _itemGainSFX.Play();
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if(_health <= 0 )
        {
            _deathSFX.Play();
            ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
            scoreManager.Save();
            Destroy(gameObject);
        }
    }

    public void Heal(float healAmount)
    {
        _health += healAmount;
    }
}
