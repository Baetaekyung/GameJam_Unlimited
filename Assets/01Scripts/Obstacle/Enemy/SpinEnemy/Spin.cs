using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Spin : Enemy
{

    [SerializeField] private float _chaseTime = 10f; // ���� �ð�
    private float _chaseTimer = 0f; // ��� �ð�
    private bool _isChasing = false; // ���� ����

    private bool _isAddedToManager = false; // �Ŵ����� �߰��Ǿ����� ���� Ȯ��

    [SerializeField] private float _shootInterval = 2f; // �Ѿ� �߻� ����

    private float _lastShootTime = 0f; // ������ �Ѿ� �߻� �ð�


    protected override void Start()
    {
        base.Start();

        _lastShootTime = Time.time + _shootInterval; // ù �߻� ����
    }

    protected override void Update()
    {
        base.Update();

        if (!_isAddedToManager && IsPlayerInRange())
        {
            AddToManager(); // �÷��̾ ���� ���� ���� �� �Ŵ����� �߰�
        }

        if (_isChasing)
        {
            _chaseTimer += Time.deltaTime;

            if (_chaseTimer >= _chaseTime)
            {
                _isChasing = false; // ���� ����

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
            _isAddedToManager = true; // �ߺ� �߰� X
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

            // z��
            transform.RotateAround(_player.transform.position, Vector3.forward, SpinEnemyManager.Instance.RotationSpeed * Time.deltaTime);

            // �Ѿ� �߻�
            if (Time.time - _lastShootTime >= _shootInterval)
            {
                Debug.Log("���");
                ShootAtPlayer();
                _lastShootTime = Time.time; // ������ �߻� �ð� ����
            }
        }
    }

    // �Ѿ� �߻� �޼���
    private void ShootAtPlayer()
    {
        Instantiate(SpinEnemyManager.Instance.BulletPrefab, transform.position, Quaternion.identity);
    }
}
