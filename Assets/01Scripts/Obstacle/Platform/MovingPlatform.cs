using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool _isHorizontal = false;    // 좌우로 움직일 거니?
    [SerializeField] private bool _isVertical = false;      // 상하로 움직일 거니?
    [SerializeField] private float _moveDistance = 5f;      // 이동 거리
    [SerializeField] private float _moveSpeed = 2f;         // 이동 속도
    [SerializeField] private float _waitTime = 1f;          // 멈출 시간

    [SerializeField] private bool _isRight = true;          // 오른쪽으로만 움직일지 여부

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Vector3 cubeSize = transform.localScale; // 사각형 크기를 오브젝트 스케일과 동일하게 설정

        // 목표 지점을 계산
        float rightX = _startPosition.x + _moveDistance;
        float leftX = _startPosition.x - _moveDistance;
        float topY = _startPosition.y + _moveDistance;
        float bottomY = _startPosition.y - _moveDistance;

        // 수평 이동일 때 목표 지점 표시
        if (_isHorizontal)
        {
            if (_isRight)
            {
                Gizmos.DrawCube(new Vector3(rightX, _startPosition.y, 0f), cubeSize);
            }
            else
            {
                Gizmos.DrawCube(new Vector3(leftX, _startPosition.y, 0f), cubeSize);
            }

            // 시작 위치도 표시
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(_startPosition, cubeSize);
        }

        // 수직 이동일 때 목표 지점 표시
        if (_isVertical)
        {
            if (_isRight)
            {
                Gizmos.DrawCube(new Vector3(_startPosition.x, topY, 0f), cubeSize);
            }
            else
            {
                Gizmos.DrawCube(new Vector3(_startPosition.x, bottomY, 0f), cubeSize);
            }

            // 시작 위치도 표시
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(_startPosition, cubeSize);
        }
    }

    private IEnumerator MoveHorizontalRoutine()
    {
        float rightX = _startPosition.x + _moveDistance; // 오른쪽 끝
        float leftX = _startPosition.x - _moveDistance;  // 왼쪽 끝

        while (true)
        {
            if (_isRight)
            {
                // 원점 ↔ 오른쪽 이동
                while (transform.position.x < rightX)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(rightX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);

                while (transform.position.x > _startPosition.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_startPosition.x, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);
            }
            else
            {
                // 원점 ↔ 왼쪽 이동
                while (transform.position.x > leftX)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(leftX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);

                while (transform.position.x < _startPosition.x)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(_startPosition.x, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);
            }
        }
    }

    private IEnumerator MoveVerticalRoutine()
    {
        float topY = _startPosition.y + _moveDistance;   // 위쪽 끝
        float bottomY = _startPosition.y - _moveDistance; // 아래쪽 끝

        while (true)
        {
            if (_isRight)
            {
                // 원점 ↔ 위쪽 이동
                while (transform.position.y < topY)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, topY, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);

                while (transform.position.y > _startPosition.y)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _startPosition.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);
            }
            else
            {
                // 원점 ↔ 아래쪽 이동
                while (transform.position.y > bottomY)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, bottomY, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);

                while (transform.position.y < _startPosition.y)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _startPosition.y, transform.position.z), _moveSpeed * Time.deltaTime);
                    yield return null;
                }

                yield return new WaitForSeconds(_waitTime);
            }
        }
    }
}
