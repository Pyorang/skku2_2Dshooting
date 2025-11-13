using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 3;

    public void PlayItemGain()
    {
        AudioManager.s_Instance.PlaySound("Item", AudioType.SFX);
    }

    public void Hit(float damage)
    {
        _health -= damage;

        if(_health <= 0 )
        {
            AudioManager.s_Instance.PlaySound("Explosion", AudioType.SFX);
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
