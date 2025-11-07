using System.Net;
using UnityEngine;

public class ItemMove : MovementComponent
{
    [Header("플레이어에게 날라오기 전까지의 시간")]
    [SerializeField] private const float _startChaseTime = 2.0f;
    private float _timeElapsed = 0f;

    private GameObject _player;

    // NOTE : 베지어 곡선 관련 속성들
    private const float _bazzierBandOffset = 0.5f;
    private float _chaseTimeElapsed = 1.1f;
    private float _chaseDuration = 1f;
    private bool _leftMove;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _leftMove = (Random.value > 0.5f);
    }

    protected override void Move()
    {
        if(_timeElapsed <= _startChaseTime)
        {
            _timeElapsed += Time.deltaTime;
            transform.position += Vector3.down * _speed * Time.deltaTime;
            return;
        }

        if(_chaseTimeElapsed <= _chaseDuration)
        {
            _chaseTimeElapsed += Time.deltaTime * _speed;
            transform.position = new Vector3(BazzierPosition(_startPosition, _endPosition, _chaseTimeElapsed).x,
                BazzierPosition(_startPosition, _endPosition, _chaseTimeElapsed).y, 0);
        }
        else
        {
            _chaseTimeElapsed = 0;
            _startPosition = transform.position;
            if(_player != null)
            {
                _endPosition = _player.transform.position;
            }
            else
            {
                _endPosition = transform.position;
            }
            _chaseDuration = 1 / Mathf.Sqrt(Mathf.Pow(_endPosition.x - _startPosition.x, 2)
                + Mathf.Pow(_endPosition.y - _startPosition.y, 2));
        }
    }

    private Vector2 BazzierPosition(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        Vector2 distance = new Vector2(endPosition.x - startPosition.x, endPosition.y - startPosition.y);

        Vector2 startPos = new Vector2(startPosition.x, startPosition.y);
        Vector2 endPos = new Vector2(endPosition.x, endPosition.y);
        Vector2 centerPoint = Vector2.Lerp(startPos, endPos, 0.5f);

        Vector2 perpVector90 = _leftMove ?
            new Vector2(-distance.y, distance.x) :
            new Vector2(distance.y, -distance.x);

        float offsetMagnitude = distance.magnitude * _bazzierBandOffset;

        Vector2 normalizedPerp = perpVector90.normalized;
        Vector2 midPoint = centerPoint + (normalizedPerp * offsetMagnitude);

        Vector2 lerpedPoint1 = Vector2.Lerp(startPos, midPoint, duration);
        Vector2 lerpedPoint2 = Vector2.Lerp(midPoint, endPos, duration);

        return Vector2.Lerp(lerpedPoint1, lerpedPoint2, duration);
    }
}
