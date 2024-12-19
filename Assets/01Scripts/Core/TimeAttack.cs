using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAttack : MonoBehaviour
{
    [SerializeField] private float _remainingTime;

    private float _remainTimer;

    [SerializeField] private TextMeshProUGUI _time;

    private BallController _player;

    private void Awake()
    {
        _player = FindObjectOfType<BallController>();

        _remainTimer = _remainingTime;
        _time.text = $"{_remainingTime}";
    }

    private void Update()
    {
        if (_player.IsDead)
        {
            _time.text = "0";
            return;
        }

        // Clamp
        _remainTimer = Mathf.Clamp(_remainTimer - Time.deltaTime, 0, _remainingTime);

        _time.text = $"{_remainTimer:F2}"; // 소수점 2자리
    }

    private void Start()
    {
        StartCoroutine(RemainingRoutine());
    }

    private IEnumerator RemainingRoutine()
    {
        yield return new WaitForSeconds(_remainingTime);

        _player.Dead();
    }
}
