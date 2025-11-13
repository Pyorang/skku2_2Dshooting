using UnityEngine;

public abstract class MovementComponent : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] protected float _speed = 3f;
    private void Update()
    {
        Move();
    }

    protected abstract void Move();
}
