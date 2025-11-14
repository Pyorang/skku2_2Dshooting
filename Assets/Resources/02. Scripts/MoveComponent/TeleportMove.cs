using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TeleportMove : MovementComponent
{
    [SerializeField] private float _movingDuration = 5f;
    private bool _isMoving = true;
    private float _timeElapsed = 0;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Init()
    {
        _isMoving = true;
        _timeElapsed = 0;

        _animator.Play("Idle", 0, 0f);
    }

    protected override void Move()
    {
        if (_isMoving)
        {
            _timeElapsed += Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime * _speed;
            if (_timeElapsed >= _movingDuration)
            {
                _isMoving = false;
                _timeElapsed = 0;
                StartCoroutine(Teleport());
            }
        }
    }

    private IEnumerator Teleport()
    {
        _animator.SetTrigger("FadeOut");
        yield return null;

        float fadeOutTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(fadeOutTime);

        MovePosition();

        _animator.SetTrigger("FadeIn");
        yield return null;

        float fadeInTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(fadeInTime);

        _isMoving = true;
    }

    private void MovePosition()
    {
        transform.position = new Vector3(transform.position.x + Random.Range(-1, 2), transform.position.y, 0);
    }
}