using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float moveSpeed = 3f; // �̵� �ӵ�
    [SerializeField] protected float detectionRange = 10f; // �÷��̾� ���� �Ÿ�

    protected BallController _player; // �÷��̾� ��ġ ����

    [SerializeField] private Material dissolveMaterial; // ������ ȿ���� ���� �⺻ ��Ƽ����

    private readonly int dissolveHeight = Shader.PropertyToID("_DissolveHeight");
    private float dissolveValue = 1f; // ������ �ʱⰪ

    protected float _distanceToPlayer;
    protected bool isDead;
    protected bool isDestroy = false;

    protected Material newMaterial; // ���ο� ��Ƽ����

    protected virtual void Start()
    {
        // �÷��̾� ã��
        _player = FindObjectOfType<BallController>();

        // ���ο� ��Ƽ���� ���� �� ����
        newMaterial = new Material(dissolveMaterial);
        ApplyNewMaterial();
    }

    private void ApplyNewMaterial()
    {
        // �� ������Ʈ�� �������� ���ο� ��Ƽ���� ����
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }

    protected virtual void Update()
    {
        // �÷��̾���� �Ÿ� ���
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (!IsPlayerInRange())
        {
            // �÷��̾ ���� ������ ��� ����� ����
        }
    }

    protected virtual bool IsPlayerInRange()
    {
        return _distanceToPlayer <= detectionRange;
    }

    protected virtual void RotateTowardsPlayer()
    {
        // �����͸� �÷��̾� �������� ���߱�
        Vector3 upDirection = (_player.transform.position - transform.position).normalized;
        transform.up = upDirection; // �����͸� �÷��̾� �������� ����
    }

    public void Dead()
    {
        if (isDead) return; // �̹� �׾����� ó�� ����
        isDead = true;

        // ������ ó�� �ڷ�ƾ ����
        StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine()
    {
        while (dissolveValue > -0.5f)
        {
            dissolveValue -= Time.deltaTime; // ������ �� ����
            newMaterial.SetFloat(dissolveHeight, dissolveValue); // ���ο� ��Ƽ���� �� ����

            yield return null;
        }

        isDestroy = true;
    }

    private void OnDrawGizmos()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
