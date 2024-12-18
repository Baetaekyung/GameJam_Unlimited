using UnityEngine;
using Cinemachine;

public class CollisionCameraShake : MonoBehaviour
{
    private BallController _player;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private float _shakeTimer;

    [SerializeField] private float _shakeDuration = 0.5f; // 흔들림 지속 시간
    [SerializeField] private float _shakeAmplitude = 2f;  // 흔들림 강도
    [SerializeField] private float _shakeFrequency = 2f;  // 흔들림 속도

    private void Awake()
    {
        _player = FindObjectOfType<BallController>();



        // Cinemachine Virtual Camera를 가져옴
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (_virtualCamera != null)
        {
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_noise != null)
        {
            Debug.Log("속도는 " + _player.RbCompo.velocity.magnitude);

            _noise.m_AmplitudeGain = _player.RbCompo.velocity.magnitude / 3;
            _noise.m_FrequencyGain = _player.RbCompo.velocity.magnitude / 3;

            // 흔들림 타이머 설정
            _shakeTimer = _shakeDuration;
        }
    }

    private void Update()
    {
        // 흔들림 타이머 감소
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            // 타이머가 끝나면 흔들림 비활성화
            if (_shakeTimer <= 0f && _noise != null)
            {
                _noise.m_AmplitudeGain = 0f;
                _noise.m_FrequencyGain = 0f;
            }
        }
    }
}
