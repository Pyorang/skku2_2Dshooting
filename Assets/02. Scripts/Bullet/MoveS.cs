using UnityEngine;

public class MoveS : MonoBehaviour
{
    public float HorizontalSpeed = 1f;

    [Header("S자 이동")]
    private float _leftTargetX;
    private float _rightTargetX;
    private bool _movingLeft = true;
    public float SMoveOffset = 1f;

    private void Start()
    {
        _leftTargetX = transform.position.x - SMoveOffset;
        _rightTargetX = transform.position.x + SMoveOffset;
    }

    private void Update()
    {
        MoveLikeS();
    }

    private void MoveLikeS()
    {
        if (_movingLeft)
        {
            transform.position += Vector3.left * HorizontalSpeed * Time.deltaTime;
            if (transform.position.x <= _leftTargetX)
            {
                transform.position = new Vector2(_leftTargetX, transform.position.y);
                _movingLeft = false;
            }
        }
        else
        {
            transform.position += Vector3.right * HorizontalSpeed * Time.deltaTime;
            if (transform.position.x >= _rightTargetX)
            {
                transform.position = new Vector2(_rightTargetX, transform.position.y);
                _movingLeft = true;
            }
        }
    }
}
