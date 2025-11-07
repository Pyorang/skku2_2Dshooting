using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _healAmount = 1f;

    protected override void ProcessItemEffect(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        player.Heal(_healAmount);
    }
}
