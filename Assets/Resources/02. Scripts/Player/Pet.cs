using UnityEngine;

public class Pet : MonoBehaviour
{
    [Header("플레이어 펫 발사 총알")]
    [SerializeField] private GameObject _petBulletPrefab;

    [Header("플레이어 펫 발사 주기")]
    [SerializeField] private float _reloadTime = 2.5f;
    private float _reloadTimeElapsed = 0;

    private void Update()
    {
        _reloadTimeElapsed += Time.deltaTime;

        if(_reloadTime <= _reloadTimeElapsed)
        {
            _reloadTimeElapsed = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(_petBulletPrefab, transform.position, Quaternion.identity);
    }
}
