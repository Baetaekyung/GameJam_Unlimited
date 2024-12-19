using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpinEnemyManager : MonoSingleton<SpinEnemyManager>
{
    private Transform _player; // �÷��̾��� Transform
    public float RotationRadius = 10f; // ȸ�� ������
    public float RotationSpeed = 30f; // ȸ�� �ӵ�

    [SerializeField] private List<Spin> _enemies = new List<Spin>(); // SpinEnemy���� ������ ����Ʈ
    private Dictionary<Spin, bool> _smoothTransitionFlags = new Dictionary<Spin, bool>(); // �˵� ���� ���� �÷���
    public float SmoothTransitionDuration = 0.5f; // �˵��� �ε巴�� �̵��ϴ� �ð�
    private float _currentAngle = 0f; // ȸ�� ����

    public SpinBullet BulletPrefab; // �Ѿ� ������


    private bool _isSpeedIncreased = false; // �ӵ� ���� ����
    private float _increasedSpeed = 0f; // ������ �ӵ�
    private float _originalSpeed; // ���� �ӵ�

    public float SpeedRestoreDuration = 3f; // �ӵ��� ���� �ӵ��� ���ư��� �ð�

    private float _fadeTimer = 0;

    public int speedMultiplier = 3;

    protected override void Awake()
    {
        if (_player == null)
            _player = FindObjectOfType<BallController>().transform; // �÷��̾� Ž��

        _originalSpeed = RotationSpeed; // ���� �ӵ� ����
    }

    private void Update()
    {
        UpdateEnemyPositions();

        // �ӵ� ���� �� ���������� ���� �ӵ��� �پ���
        if (_isSpeedIncreased)
        {
            _fadeTimer += Time.deltaTime;

            RotationSpeed = Mathf.Lerp(_increasedSpeed, _originalSpeed, _fadeTimer / SpeedRestoreDuration);
            if (RotationSpeed <= _originalSpeed + 0.1f) // ���� �ӵ��� �����ϸ� �ӵ� ���� ����
            {
                RotationSpeed = _originalSpeed;
                _isSpeedIncreased = false;
            }
        }
    }

    // ���ο� �� �߰�
    public void AddEnemy(Spin enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);

            // �ε巯�� �̵� ���� �ʱ�ȭ
            _smoothTransitionFlags[enemy] = true;

            // �÷��̾��� �ڽ����� �����ϰ�, ȸ�� �˵��� ��ġ
            enemy.transform.SetParent(_player);

            IncreaseSpeed();
        }
    }

    // �� ����
    public void RemoveEnemy(Spin enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
            _smoothTransitionFlags.Remove(enemy);

            Destroy(enemy.gameObject);
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
            Spin enemy = _enemies[i];
            float angle = _currentAngle + (angleStep * i); // �� ���� ���� ���
            Vector3 targetPosition = CalculateOrbitPosition(angle);

            if (_smoothTransitionFlags[enemy]) // �ε巯�� �˵� ���� ������ Ȯ��
            {
                SmoothTransitionToOrbit(enemy, targetPosition);
            }
            else
            {
                // �̹� �˵��� ������ ��� ��ġ ����
                enemy.transform.localPosition = targetPosition;
            }
        }

        // ȸ�� ���� ������Ʈ
        _currentAngle += RotationSpeed * Time.deltaTime;
        if (_currentAngle >= 360f) _currentAngle -= 360f;
    }

    private Vector3 CalculateOrbitPosition(float angle)
    {
        // �÷��̾ �������� ȸ�� �˵��� ��ǥ ���
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * RotationRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * RotationRadius;

        return new Vector3(x, y, 0f); // z ���� ���� (�÷��̾�� ������ z�� ����)
    }

    private void SmoothTransitionToOrbit(Spin enemy, Vector3 targetPosition)
    {
        // �ε巴�� �̵�
        enemy.transform.localPosition = Vector3.Lerp(
            enemy.transform.localPosition,
            targetPosition,
            Time.deltaTime / SmoothTransitionDuration
        );

        // ��ǥ ��ġ�� �����ϸ� �ε巯�� �̵� ����
        if (Vector3.Distance(enemy.transform.localPosition, targetPosition) < 0.1f)
        {
            _smoothTransitionFlags[enemy] = false; // �˵� ���� �Ϸ�
        }
    }

    // ��� ���� �ӵ��� 3��� ������Ű�� �޼���
    public void IncreaseSpeed()
    {
        if (!_isSpeedIncreased)
        {
            _increasedSpeed = RotationSpeed * speedMultiplier; // �ӵ� 3�� ����
            RotationSpeed = _increasedSpeed;
            _fadeTimer = 0;
            _isSpeedIncreased = true;
        }
    }
}
