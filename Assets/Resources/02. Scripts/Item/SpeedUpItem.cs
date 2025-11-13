using UnityEngine;

public class SpeedUpItem : Item
{
    [SerializeField] private int _speed = 1;

    protected override void ProcessItemEffect(Collider2D collision)
    {
        PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
        playerMove.SpeedUp(_speed);
    }
}
