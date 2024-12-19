using UnityEngine;

public class Crow : Enemy
{
    [SerializeField] private float minSpeed = 1f; // 목표 속도 (감속 후 최종 속도)
    [SerializeField] private float waitBeforeDash = 2f; // 대쉬 전 대기 시간

    private Vector2 _dashPos; // 이동하고자 하는 지점
    private bool _isDashing = false; // 대쉬 중인가?
    private bool _isPreparingToDash = false;
    private float _preparationTimer = 0f;

    private float _halfwayDistance; // 반 지점까지의 거리

    private SpriteRenderer _spriteRenderer;
    private float _fadeTimer = 0f; // 페이드 타이머
    private bool _isFadingIn = false; // 페이드 진행 여부

    private Transform _child;

    private PolygonCollider2D _collider;

    private void Awake()
    {
        _child = transform.GetChild(0).transform;
        _spriteRenderer = _child.GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    protected override void Update()
    {
        base.Update();

        if (_isDashing) Dash();
        else if (_isPreparingToDash) PrepareToDash();
        else IdleAndDetectPlayer();
    }

    private void IdleAndDetectPlayer()
    {
        if (IsPlayerInRange() && !_isDashing)
        {
            // 범위 내에 들어오면 대쉬 준비 시작
            _isPreparingToDash = true;
            _preparationTimer = waitBeforeDash;
        }
    }

    private void PrepareToDash()
    {
        if (_isDashing) return; // 대쉬가 이미 시작되었으면 대기하지 않음

        if (!IsPlayerInRange())
        {
            // 플레이어가 범위 밖으로 나가면 대쉬 준비 취소
            _isPreparingToDash = false;
            DisableSprite();
            return;
        }

        // 플레이어를 바라보며 대기
        RotateTowardsPlayer();

        if (!_isFadingIn)
        {
            _fadeTimer = 0f;
            _isFadingIn = true;
        }

        // 페이드 효과 적용
        ApplyFadeInEffect();

        _child.position = _player.transform.position;
        _child.localScale = new Vector3(_child.localScale.x, _distanceToPlayer * 2, _child.localScale.z);

        // 돌진 목표 지점 계산
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _dashPos = (Vector2)transform.position + direction * (Vector2.Distance(transform.position, _player.transform.position) * 2f);



        // 대기 시간 감소
        _preparationTimer -= Time.deltaTime;

        // 대기 시간이 끝나면 대쉬 시작
        if (_preparationTimer <= 0)
        {
            DisableSprite();
            _isPreparingToDash = false;
            _isDashing = true;

            // 반 지점까지의 거리 계산
            _halfwayDistance = Vector2.Distance(transform.position, _dashPos) / 2f;
        }
    }

    private void Dash()
    {
        // 목표 위치로 이동
        transform.position = Vector2.MoveTowards(transform.position, _dashPos, moveSpeed * Time.deltaTime);

        // 현재 목표 지점까지의 거리
        float distanceToTarget = Vector2.Distance(transform.position, _dashPos);

        // 반 지점을 지난 경우 속도 점진적으로 감소
        if (distanceToTarget <= _halfwayDistance)
        {
            // 속도를 점차적으로 목표 속도(minSpeed)로 감소
            moveSpeed = Mathf.Lerp(moveSpeed, minSpeed, Time.deltaTime * 2f);
        }

        // 목표 지점에 도달하면 대쉬 종료
        if (distanceToTarget < 0.1f)
        {
            _collider.enabled = false;
            Dead();
        }
    }

    private void ApplyFadeInEffect()
    {
        Color color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;

        _fadeTimer += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, _fadeTimer / waitBeforeDash);

        // 색상 변경 (알파 값만 조정)
        color.a = alpha;
        _spriteRenderer.color = color;

        // 페이드가 끝났다면 플래그 초기화
        if (_fadeTimer >= waitBeforeDash)
            _isFadingIn = false;
    }

    private void DisableSprite()
    {
        Color color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;

        _fadeTimer = 0;
    }

    protected override bool IsPlayerInRange() { return base.IsPlayerInRange(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BallController player))
        {
            player.Dead();
        }
    }
}
