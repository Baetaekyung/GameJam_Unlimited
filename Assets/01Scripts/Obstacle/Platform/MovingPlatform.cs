using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool _isHorizontal = false;    // �¿�� ������ �Ŵ�?
    [SerializeField] private bool _isVertical = false;      // ���Ϸ� ������ �Ŵ�?
    [SerializeField] private float _moveDistance = 5f;      // �̵� �Ÿ�
    [SerializeField] private float _moveSpeed = 2f;         // �̵� �ӵ�
    [SerializeField] private float _waitTime = 1f;          // ���� �ð�

    [SerializeField] private bool _isRight = true;          // ���������θ� �������� ����

    private Vector3 _startPosition; // ���� ��ġ

    private void Start()
    {
        _startPosition = transform.position;  // ���� ��ġ ����

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
        Vector3 cubeSize = transform.localScale; // �簢�� ũ�⸦ ������Ʈ �����ϰ� �����ϰ� ����

        // ��ǥ ������ ���
        float rightX = _startPosition.x + _moveDistance;
        float leftX = _startPosition.x - _moveDistance;
        float topY = _startPosition.y + _moveDistance;
        float bottomY = _startPosition.y - _moveDistance;

        // ���� �̵��� �� ��ǥ ���� ǥ��
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

            // ���� ��ġ�� ǥ��
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(_startPosition, cubeSize);
        }

        // ���� �̵��� �� ��ǥ ���� ǥ��
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

            // ���� ��ġ�� ǥ��
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(_startPosition, cubeSize);
        }
    }

    private IEnumerator MoveHorizontalRoutine()
    {
        float rightX = _startPosition.x + _moveDistance; // ������ ��
        float leftX = _startPosition.x - _moveDistance;  // ���� ��

        while (true)
        {
            if (_isRight)
            {
                // ���� �� ������ �̵�
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
                // ���� �� ���� �̵�
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
        float topY = _startPosition.y + _moveDistance;   // ���� ��
        float bottomY = _startPosition.y - _moveDistance; // �Ʒ��� ��

        while (true)
        {
            if (_isRight)
            {
                // ���� �� ���� �̵�
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
                // ���� �� �Ʒ��� �̵�
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
