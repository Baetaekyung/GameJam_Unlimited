using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Spin : Enemy
{

    [SerializeField] private float _chaseTime = 10f; // 추적 시간
    private float _chaseTimer = 0f; // 경과 시간
    private bool _isChasing = false; // 추적 여부

    private bool _isAddedToManager = false; // 매니저에 추가되었는지 여부 확인

    [SerializeField] private float _shootInterval = 2f; // 총알 발사 간격

    private float _lastShootTime = 0f; // 마지막 총알 발사 시간


    protected override void Start()
    {
        base.Start();

        _lastShootTime = Time.time + _shootInterval; // 첫 발사 지연
    }

    protected override void Update()
    {
        base.Update();

        if (!_isAddedToManager && IsPlayerInRange())
        {
            AddToManager(); // 플레이어가 범위 내에 있을 때 매니저에 추가
        }

        if (_isChasing)
        {
            _chaseTimer += Time.deltaTime;

            if (_chaseTimer >= _chaseTime)
            {
                _isChasing = false; // 추적 종료

                SpinEnemyManager.Instance.RemoveEnemy(this);
            }
            else
            {
                FollowPlayer();
            }
        }
    }

    private void AddToManager()
    {
        if (!_isAddedToManager)
        {
            SpinEnemyManager.Instance.AddEnemy(this);
            _isAddedToManager = true; // 중복 추가 X
            _isChasing = true;
        }
    }

    protected override void RotateTowardsPlayer()
    {
        base.RotateTowardsPlayer();
    }

    protected override bool IsPlayerInRange()
    {
        return base.IsPlayerInRange();

    }

    public void FollowPlayer()
    {
        if (IsPlayerInRange())
        {
            RotateTowardsPlayer();

            // z축
            transform.RotateAround(_player.transform.position, Vector3.forward, SpinEnemyManager.Instance.RotationSpeed * Time.deltaTime);

            // 총알 발사
            if (Time.time - _lastShootTime >= _shootInterval)
            {
                Debug.Log("쏘기");
                ShootAtPlayer();
                _lastShootTime = Time.time; // 마지막 발사 시간 갱신
            }
        }
    }

    // 총알 발사 메서드
    private void ShootAtPlayer()
    {
        Instantiate(SpinEnemyManager.Instance.BulletPrefab, transform.position, Quaternion.identity);
    }
}
