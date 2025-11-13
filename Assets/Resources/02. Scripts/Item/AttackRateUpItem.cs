using UnityEngine;

public class AttackRateUpItem : Item
{
    [SerializeField] private float _ratio = 0.20f;

    protected override void ProcessItemEffect(Collider2D collsion)
    {
        PlayerFire playerFire = collsion.GetComponent<PlayerFire>();
        playerFire.IncreaseFiringRate(_ratio);
    }
}
