using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float moveSpeed = 3f; // 이동 속도
    [SerializeField] protected float detectionRange = 10f; // 플레이어 감지 거리

    protected BallController _player; // 플레이어 위치 참조

    [SerializeField] private Material dissolveMaterial; // 디졸브 효과를 위한 기본 머티리얼

    private readonly int dissolveHeight = Shader.PropertyToID("_DissolveHeight");
    private float dissolveValue = 1f; // 디졸브 초기값

    protected float _distanceToPlayer;
    protected bool isDead;
    protected bool isDestroy = false;

    protected Material newMaterial; // 새로운 머티리얼

    protected virtual void Start()
    {
        // 플레이어 찾기
        _player = FindObjectOfType<BallController>();

        // 새로운 머티리얼 생성 및 적용
        newMaterial = new Material(dissolveMaterial);
        ApplyNewMaterial();
    }

    private void ApplyNewMaterial()
    {
        // 이 오브젝트의 렌더러에 새로운 머티리얼 적용
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }

    protected virtual void Update()
    {
        // 플레이어와의 거리 계산
        _distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (!IsPlayerInRange())
        {
            // 플레이어가 감지 범위를 벗어난 경우의 로직
        }
    }

    protected virtual bool IsPlayerInRange()
    {
        return _distanceToPlayer <= detectionRange;
    }

    protected virtual void RotateTowardsPlayer()
    {
        // 업벡터를 플레이어 방향으로 맞추기
        Vector3 upDirection = (_player.transform.position - transform.position).normalized;
        transform.up = upDirection; // 업벡터를 플레이어 방향으로 설정
    }

    public void Dead()
    {
        if (isDead) return; // 이미 죽었으면 처리 중지
        isDead = true;

        // 디졸브 처리 코루틴 시작
        StartCoroutine(DeadRoutine());
    }

    private IEnumerator DeadRoutine()
    {
        while (dissolveValue > -0.5f)
        {
            dissolveValue -= Time.deltaTime; // 디졸브 값 감소
            newMaterial.SetFloat(dissolveHeight, dissolveValue); // 새로운 머티리얼에 값 적용

            yield return null;
        }

        isDestroy = true;
    }

    private void OnDrawGizmos()
    {
        // 감지 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
