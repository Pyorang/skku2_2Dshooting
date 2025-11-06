using UnityEngine;

public class ChasingEnemy : Enemy
{
    // 목표 : 플레이어를 쫓아가는 적을 만들고 싶다.

    private void Update()
    {
        // 1. 플레이어의 위치를 구한다.
        GameObject PlayerObject = GameObject.FindWithTag("Player");

        // 2. 위치에 따라 방향을 구한다.
        Vector3 direction = (PlayerObject.transform.position - transform.position).normalized;

        // 3. 방향에 맞게 이동한다.
        transform.position += direction * Time.deltaTime * Speed;
    }
}
