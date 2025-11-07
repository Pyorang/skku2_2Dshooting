using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _enemyBullet;
    [SerializeField] private float _reloadingTime;
    private float _timeElapsed = 0;

    private void Update()
    {
        _timeElapsed += Time.deltaTime;
        if( _timeElapsed >= _reloadingTime )
        {
            _timeElapsed = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_enemyBullet);
        bullet.transform.position = gameObject.transform.position;
    }
}
