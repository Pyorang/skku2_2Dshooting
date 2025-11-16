using System.Collections;
using UnityEngine;

public enum AttackType
{
    StraightFire = 0,
    ZigZagFire = 1
}

public class BossShoot : MonoBehaviour
{
    [SerializeField] private GameObject _enemyBullet;

    private void Start()
    {
        StartCoroutine(StartRandomAttack());
    }

    public void StartFiring(AttackType attackType)
    {
        StartCoroutine(attackType.ToString());
    }

    public void StopFiring(AttackType attackType)
    {
        StopCoroutine(attackType.ToString());
    }

    private IEnumerator StraightFire()
    {
        float attackRate = 0.5f;
        int count = 10;
        int oneLineBulletCount = 5;

        for(int i = 0; i < count; i++)
        {
            int xDifference = -2;

            for(int j = 0; j < oneLineBulletCount; j++)
            {
                GameObject enemyBullet = Instantiate(_enemyBullet, transform.position, Quaternion.identity);
                enemyBullet.transform.position += Vector3.right * xDifference;
                xDifference++;
            }

            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator ZigZagFire()
    {
        float attackRate = 0.5f;
        int count = 10;
        int xDifference = -2;

        for (int i = 0; i< count; i++)
        {
            GameObject enemyBullet = Instantiate(_enemyBullet, transform.position, Quaternion.identity);
            enemyBullet.transform.position += Vector3.right * xDifference;
            xDifference++;
            yield return new WaitForSeconds(attackRate);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject enemyBullet = Instantiate(_enemyBullet, transform.position, Quaternion.identity);
            enemyBullet.transform.position += Vector3.right * xDifference;
            xDifference--;
            yield return new WaitForSeconds(attackRate);
        }
    }

    private IEnumerator StartRandomAttack()
    {
        while (true)
        {
            int attackCount = System.Enum.GetValues(typeof(AttackType)).Length;
            int randomIndex = Random.Range(0, attackCount);
            AttackType randomAttack = (AttackType)randomIndex;
            StartFiring(randomAttack);

            yield return new WaitForSeconds(3f);

            StopFiring(randomAttack);

            yield return new WaitForSeconds(3f);
        }
    }
}
