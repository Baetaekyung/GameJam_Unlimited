using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float moveSpeed = 3f; // �̵� �ӵ�
    [SerializeField] protected float detectionRange = 10f; // �÷��̾� ���� �Ÿ�

    protected BallController _player; // �÷��̾� ��ġ ����

    protected float _distanceToPlayer;

    protected virtual void Start()
    {
        // �÷��̾� ã��
        _player = FindObjectOfType<BallController>();
    }

    protected virtual void Update()
    {
        // �÷��̾���� �Ÿ� ���
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
    }
    protected virtual bool IsPlayerInRange()
    {
        if (_distanceToPlayer <= detectionRange)
            return true;

        return false;
    }

    private void OnDrawGizmos()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
