using System;
using System.Collections;
using UnityEngine;

public class Crow : Enemy
{
    [SerializeField] private float minSpeed = 1f; // ��ǥ �ӵ� (���� �� ���� �ӵ�)
    [SerializeField] private float dashCooldown = 3f; // ���� �� �޽� �ð�
    [SerializeField] private float waitBeforeDash = 2f; // �뽬 �� ��� �ð�

    private Vector2 _dashPos; // �̵��ϰ��� �ϴ� ����
    private bool _isDashing = false; //�뽬���ΰ�?
    private float _cooldownTimer = 0f;
    private bool _isOnCooldown = false; // ��ٿ� ���ΰ�?
    private bool _isPreparingToDash = false;
    private float _preparationTimer = 0f;

    private float _halfwayDistance; // �� ���������� �Ÿ�

    private SpriteRenderer _spriteRenderer;
    private float _fadeTimer = 0f; // ���̵� Ÿ�̸�
    private bool _isFadingIn = false; // ���̵� ���� ����

    private Transform _child;


    private void Awake()
    {
        _child = transform.GetChild(0).transform;

        _spriteRenderer = _child.GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();

        if (_isDashing) Dash();
        else if (_isOnCooldown) Cooldown();
        else if (_isPreparingToDash) PrepareToDash();
        else IdleAndDetectPlayer();
    }

    private void IdleAndDetectPlayer()
    {
        if (IsPlayerInRange())
        {
            // ���� �غ� ���·� ��ȯ
            _isPreparingToDash = true;
            _preparationTimer = waitBeforeDash;
        }
    }

    private void PrepareToDash()
    {
        if (!IsPlayerInRange())
        {
            _isPreparingToDash = false;
            DisableSprite();
            return; // �뽬 �غ� ����ϰ� �Լ� ����
        }

        // �÷��̾ �ٶ󺸸� ���
        RotateTowardsPlayer();

        if (!_isFadingIn)
        {
            _fadeTimer = 0f;
            _isFadingIn = true;
        }

        // ���̵� ȿ�� ����
        ApplyFadeInEffect();

        _child.position = _player.transform.position;
        _child.localScale = new Vector3(_child.localScale.x, _distanceToPlayer * 2, _child.localScale.z);

        // ���� ��ǥ ���� ���
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _dashPos = (Vector2)transform.position + direction * (Vector2.Distance(transform.position, _player.transform.position) * 2f);

        // ��� �ð� ����
        _preparationTimer -= Time.deltaTime;

        // ��� �ð��� ������ �뽬 ����
        if (_preparationTimer <= 0)
        {
            DisableSprite();

            _isPreparingToDash = false;
            _isDashing = true;

            // �� ���������� �Ÿ� ���
            _halfwayDistance = Vector2.Distance(transform.position, _dashPos) / 2f;
        }
    }

    private void Dash()
    {
        // ��ǥ ��ġ�� �̵�
        transform.position = Vector2.MoveTowards(transform.position, _dashPos, moveSpeed * Time.deltaTime);

        // ���� ��ǥ ���������� �Ÿ�
        float distanceToTarget = Vector2.Distance(transform.position, _dashPos);

        // �� ������ ���� ��� �ӵ� ���������� ����
        if (distanceToTarget <= _halfwayDistance)
        {
            // �ӵ��� ���������� ��ǥ �ӵ�(minSpeed)�� ����
            moveSpeed = Mathf.Lerp(moveSpeed, minSpeed, Time.deltaTime * 2f);
        }

        // ��ǥ ������ �����ϸ� �뽬 ����
        if (distanceToTarget < 0.1f)
        {
            _isDashing = false;
            _isOnCooldown = true;
            _cooldownTimer = dashCooldown;
            DisableSprite();

            // �ӵ� �ʱ�ȭ
            moveSpeed = 15f;
        }
    }

    protected override void RotateTowardsPlayer()
    {
        base.RotateTowardsPlayer();
    }

    private void Cooldown()
    {
        DisableSprite();
        if (_cooldownTimer > 0)
        {
            // �޽� ��
            _cooldownTimer -= Time.deltaTime;
        }
        else
        {
            // �޽� ����
            _isOnCooldown = false;
        }
    }


    private void ApplyFadeInEffect()
    {
        Color color = _spriteRenderer.color;
        color.a = 0;
        _spriteRenderer.color = color;

        _fadeTimer += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, _fadeTimer / waitBeforeDash);

        // ���� ���� (���� ���� ����)
        color.a = alpha;
        _spriteRenderer.color = color;

        // ���̵尡 �����ٸ� �÷��� �ʱ�ȭ
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


}
