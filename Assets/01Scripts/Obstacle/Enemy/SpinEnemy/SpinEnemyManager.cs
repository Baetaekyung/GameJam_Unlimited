using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpinEnemyManager : MonoSingleton<SpinEnemyManager>
{
    private Transform _player; // 플레이어의 Transform
    public float RotationRadius = 10f; // 회전 반지름
    public float RotationSpeed = 30f; // 회전 속도

    [SerializeField] private List<Spin> _enemies = new List<Spin>(); // SpinEnemy들을 관리할 리스트
    private Dictionary<Spin, bool> _smoothTransitionFlags = new Dictionary<Spin, bool>(); // 궤도 진입 상태 플래그
    public float SmoothTransitionDuration = 0.5f; // 궤도로 부드럽게 이동하는 시간
    private float _currentAngle = 0f; // 회전 각도

    public SpinBullet BulletPrefab; // 총알 프리팹


    private bool _isSpeedIncreased = false; // 속도 증가 상태
    private float _increasedSpeed = 0f; // 증가된 속도
    private float _originalSpeed; // 원래 속도

    public float SpeedRestoreDuration = 3f; // 속도가 원래 속도로 돌아가는 시간

    private float _fadeTimer = 0;

    public int speedMultiplier = 3;

    protected override void Awake()
    {
        if (_player == null)
            _player = FindObjectOfType<BallController>().transform; // 플레이어 탐색

        _originalSpeed = RotationSpeed; // 원래 속도 저장
    }

    private void Update()
    {
        UpdateEnemyPositions();

        // 속도 증가 후 점차적으로 원래 속도로 줄어들게
        if (_isSpeedIncreased)
        {
            _fadeTimer += Time.deltaTime;

            RotationSpeed = Mathf.Lerp(_increasedSpeed, _originalSpeed, _fadeTimer / SpeedRestoreDuration);
            if (RotationSpeed <= _originalSpeed + 0.1f) // 원래 속도에 근접하면 속도 복원 종료
            {
                RotationSpeed = _originalSpeed;
                _isSpeedIncreased = false;
            }
        }
    }

    // 새로운 적 추가
    public void AddEnemy(Spin enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);

            // 부드러운 이동 상태 초기화
            _smoothTransitionFlags[enemy] = true;

            // 플레이어의 자식으로 설정하고, 회전 궤도에 위치
            enemy.transform.SetParent(_player);

            IncreaseSpeed();
        }
    }

    // 적 제거
    public void RemoveEnemy(Spin enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
            _smoothTransitionFlags.Remove(enemy);

            Destroy(enemy.gameObject);
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
            Spin enemy = _enemies[i];
            float angle = _currentAngle + (angleStep * i); // 각 적의 각도 계산
            Vector3 targetPosition = CalculateOrbitPosition(angle);

            if (_smoothTransitionFlags[enemy]) // 부드러운 궤도 진입 중인지 확인
            {
                SmoothTransitionToOrbit(enemy, targetPosition);
            }
            else
            {
                // 이미 궤도에 진입한 경우 위치 갱신
                enemy.transform.localPosition = targetPosition;
            }
        }

        // 회전 각도 업데이트
        _currentAngle += RotationSpeed * Time.deltaTime;
        if (_currentAngle >= 360f) _currentAngle -= 360f;
    }

    private Vector3 CalculateOrbitPosition(float angle)
    {
        // 플레이어를 기준으로 회전 궤도의 좌표 계산
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * RotationRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * RotationRadius;

        return new Vector3(x, y, 0f); // z 값은 고정 (플레이어와 동일한 z값 유지)
    }

    private void SmoothTransitionToOrbit(Spin enemy, Vector3 targetPosition)
    {
        // 부드럽게 이동
        enemy.transform.localPosition = Vector3.Lerp(
            enemy.transform.localPosition,
            targetPosition,
            Time.deltaTime / SmoothTransitionDuration
        );

        // 목표 위치에 도달하면 부드러운 이동 종료
        if (Vector3.Distance(enemy.transform.localPosition, targetPosition) < 0.1f)
        {
            _smoothTransitionFlags[enemy] = false; // 궤도 진입 완료
        }
    }

    // 모든 적의 속도를 3배로 증가시키는 메서드
    public void IncreaseSpeed()
    {
        if (!_isSpeedIncreased)
        {
            _increasedSpeed = RotationSpeed * speedMultiplier; // 속도 3배 증가
            RotationSpeed = _increasedSpeed;
            _fadeTimer = 0;
            _isSpeedIncreased = true;
        }
    }
}
