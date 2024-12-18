using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] protected float moveSpeed = 3f; // 이동 속도
    [SerializeField] protected float detectionRange = 10f; // 플레이어 감지 거리

    protected BallController _player; // 플레이어 위치 참조

    protected float _distanceToPlayer;

    protected virtual void Start()
    {
        // 플레이어 찾기
        _player = FindObjectOfType<BallController>();
    }

    protected virtual void Update()
    {
        // 플레이어와의 거리 계산
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
        // 감지 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
