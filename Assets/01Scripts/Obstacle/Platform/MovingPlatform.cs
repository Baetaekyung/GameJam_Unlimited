using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private bool _isHorizontal = false;    // �¿�� ������ �Ŵ�?
    [SerializeField] private bool _isVertical = false;      // ���Ϸ� ������ �Ŵ�?
    [SerializeField] private float _moveDistance = 5f;      // �̵� �Ÿ�
    [SerializeField] private float _moveSpeed = 2f;         // �̵� �ӵ�
    [SerializeField] private float _waitTime = 1f;          // ���� �ð�

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

    private IEnumerator MoveHorizontalRoutine()
    {
        float targetX = _startPosition.x + _moveDistance;
        float initialX = _startPosition.x;

        while (true)
        {
            // ���ʿ��� ���������� �̵�
            while (transform.position.x < targetX)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ����
            yield return new WaitForSeconds(_waitTime);

            // �����ʿ��� �������� �̵�
            while (transform.position.x > initialX)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(initialX, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ����
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private IEnumerator MoveVerticalRoutine()
    {
        float targetY = _startPosition.y + _moveDistance;
        float initialY = _startPosition.y;

        while (true)
        {
            // �Ʒ����� ���� �̵�
            while (transform.position.y < targetY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ����
            yield return new WaitForSeconds(_waitTime);

            // ������ �Ʒ��� �̵�
            while (transform.position.y > initialY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, initialY, transform.position.z), _moveSpeed * Time.deltaTime);
                yield return null;
            }

            // ��� ����
            yield return new WaitForSeconds(_waitTime);
        }
    }
}
