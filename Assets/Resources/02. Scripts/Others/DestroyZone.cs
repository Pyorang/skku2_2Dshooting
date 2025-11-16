using Unity.VisualScripting;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") || collision.CompareTag("Enemy"))
        {
            if(collision.GetComponent<BossShoot>() == null)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
