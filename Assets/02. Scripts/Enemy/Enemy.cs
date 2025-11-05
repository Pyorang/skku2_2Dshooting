using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("스탯")]
    public float Speed = 3;
    public float Health = 100f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStat playerStat = collision.gameObject.GetComponent<PlayerStat>();

            // NOTE : CompareTag 메소드로 검사하였기 때문에
            // playerStat이 null일 가능성은 거의 없지만, 안전하게 처리
            if (playerStat != null)
            {
                playerStat.Health -= 1;

                if(playerStat.Health <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }
}
