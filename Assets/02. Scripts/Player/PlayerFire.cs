using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스바를 누르면 총알을 만들어서 발사하고 싶다.

    // 필요 속성
    [Header("총알 프리팹")]  // 복사해올 총알 프리팹 게임 오브젝트
    public GameObject BulletPrefab;
    public GameObject AssistBulletPrefab;
    [SerializeField] private GameObject _bombPrefab;

    [Header("총구")]
    public Transform LeftFirePosition;
    public Transform RightFirePosition;
    [SerializeField] private Transform _bombFirePosition;

    [Header("보조 총구")]
    public Transform AssistLeftFirePosition;
    public Transform AssistRightFirePosition;

    [Header("재장전")]
    public bool _isReloading = false;
    public float ReloadingTime = 0.6f;
    private float _currentReloadingTime = 0f;

    [Header("폭탄")]
    [SerializeField] private KeyCode _bombKey = KeyCode.Alpha3;

    private bool _isAutoFire = true;

    private void Update()
    {
        ProcessBombInput();
        ChangeAutoMode();

        if(!_isReloading)
        {
            if (_isAutoFire)
            {
                Fire();
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Fire();
                }
            }
        }

        if(_isReloading)
        {
            Reload();
        }
    }

    private void ProcessBombInput()
    {
        if(Input.GetKeyDown(_bombKey))
        {
            Instantiate(_bombPrefab, _bombFirePosition.position, Quaternion.identity);
        }
    }

    private void ChangeAutoMode()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isAutoFire = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isAutoFire = false;
        }
    }

    public void Fire()
    {
        GameObject leftBullet = Instantiate(BulletPrefab);
        GameObject rightBullet = Instantiate(BulletPrefab);

        leftBullet.transform.position = LeftFirePosition.position;
        rightBullet.transform.position = RightFirePosition.position;

        AssistFire();

        _isReloading = true;
    }

    public void AssistFire()
    {
        if (!_isReloading)
        {
            GameObject leftAssistBullet = Instantiate(AssistBulletPrefab);
            GameObject rightAssistBullet = Instantiate(AssistBulletPrefab);

            leftAssistBullet.transform.position = AssistLeftFirePosition.position;
            rightAssistBullet.transform.position = AssistRightFirePosition.position;
        }
    }

    public void Reload()
    {
        _currentReloadingTime += Time.deltaTime;
        if (_currentReloadingTime >= ReloadingTime)
        {
            _isReloading = false;
            _currentReloadingTime = 0;
        }
    }

    public void IncreaseFiringRate(float ratio)
    {
        ReloadingTime *= (1 - ratio);
    }
}

