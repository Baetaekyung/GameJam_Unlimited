using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool _isHorizontal = false;    // 좌우로 움직일 거니?
    [SerializeField] private bool _isVertical = false;      // 상하로 움직일 거니?
    [SerializeField] private float _moveDistance = 5f;      // 이동 거리
    [SerializeField] private float _moveSpeed = 2f;         // 이동 속도
    [SerializeField] private float _waitTime = 1f;          // 멈출 시간

    private Vector3 _startPosition; // 시작 위치

    private void Start()
    {
        _startPosition = transform.position;  // 시작 위치 저장

        if (_isHorizontal)
        {
            StartCoroutine(MoveHorizontalRoutine());
        }

        if (_isVertical)
        {
            StartCoroutine(MoveVerticalRoutine());
        }
    }

    private IEnumerator MoveHorizontalRoutine()
    {
        float targetX = _startPosition.x + _moveDistance;
        float initialX = _startPosition.x;

        while (true)
        {
            // 왼쪽에서 오른쪽으로 이동
            while (transform.position.x < targetX)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 잠시 멈춤
            yield return new WaitForSeconds(_waitTime);

            // 오른쪽에서 왼쪽으로 이동
            while (transform.position.x > initialX)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(initialX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 잠시 멈춤
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private IEnumerator MoveVerticalRoutine()
    {
        float targetY = _startPosition.y + _moveDistance;
        float initialY = _startPosition.y;

        while (true)
        {
            // 아래에서 위로 이동
            while (transform.position.y < targetY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 잠시 멈춤
            yield return new WaitForSeconds(_waitTime);

            // 위에서 아래로 이동
            while (transform.position.y > initialY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialY, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // 잠시 멈춤
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
