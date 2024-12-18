using System.Collections.Generic;
using UnityEngine;

public class SpinEnemyManager : MonoBehaviour
{
    [SerializeField] private Transform _player; // 플레이어의 Transform
    [SerializeField] private float _rotationRadius = 5f; // 회전 반지름
    [SerializeField] private float _rotationSpeed = 30f; // 회전 속도

    private List<Spin> _enemies = new List<Spin>(); // SpinEnemy들을 관리할 리스트
    private float _currentAngle = 0f; // 회전 각도

    private void Awake()
    {
        if (_player == null)
            _player = FindObjectOfType<BallController>().transform;
    }

    private void Update()
    {
        UpdateEnemyPositions();
    }

    // 새로운 적 추가
    public void AddEnemy(Spin enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
            UpdateEnemyPositions();
        }
    }

    // 적 제거
    public void RemoveEnemy(Spin enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
            UpdateEnemyPositions();
        }
    }

    // 적들의 위치 업데이트
    private void UpdateEnemyPositions()
    {
        int enemyCount = _enemies.Count;
        if (enemyCount == 0) return;

        float angleStep = 360f / enemyCount; // 각 적의 간격 각도

        for (int i = 0; i < enemyCount; i++)
        {
            float angle = _currentAngle + (angleStep * i);
            float x = _player.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * _rotationRadius;
            float z = _player.position.z + Mathf.Sin(angle * Mathf.Deg2Rad) * _rotationRadius;

            _enemies[i].transform.position = new Vector3(x, _player.position.y, z);
        }

        // 회전 각도 업데이트
        _currentAngle += _rotationSpeed * Time.deltaTime;
        if (_currentAngle >= 360f) _currentAngle -= 360f;
    }
}
