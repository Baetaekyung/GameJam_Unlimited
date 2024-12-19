using System.Collections.Generic;
using UnityEngine;

public class SpinEnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _player; // �÷��̾��� Transform
    [SerializeField] private float _rotationRadius = 5f; // ȸ�� ������
    [SerializeField] private float _rotationSpeed = 30f; // ȸ�� �ӵ�

    private List<Spin> _enemies = new List<Spin>(); // SpinEnemy���� ������ ����Ʈ
    private float _currentAngle = 0f; // ȸ�� ����

    private void Awake()
    {
        if (_player == null)
            _player = FindObjectOfType<BallController>().transform;
    }

    private void Update()
    {
        UpdateEnemyPositions();
    }

    // ���ο� �� �߰�
    public void AddEnemy(Spin enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
            UpdateEnemyPositions();
        }
    }

    // �� ����
    public void RemoveEnemy(Spin enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
            UpdateEnemyPositions();
        }
    }

    // ������ ��ġ ������Ʈ
    private void UpdateEnemyPositions()
    {
        int enemyCount = _enemies.Count;
        if (enemyCount == 0) return;

        float angleStep = 360f / enemyCount; // �� ���� ���� ����

        for (int i = 0; i < enemyCount; i++)
        {
            float angle = _currentAngle + (angleStep * i);
            float x = _player.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * _rotationRadius;
            float z = _player.position.z + Mathf.Sin(angle * Mathf.Deg2Rad) * _rotationRadius;

            _enemies[i].transform.position = new Vector3(x, _player.position.y, z);
        }

        // ȸ�� ���� ������Ʈ
        _currentAngle += _rotationSpeed * Time.deltaTime;
        if (_currentAngle >= 360f) _currentAngle -= 360f;
    }
}
