using UnityEngine;
using Cinemachine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CollisionCameraShake : MonoBehaviour
{
    private BallController _player;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _noise;
    private float _shakeTimer;

    [SerializeField] private float _shakeDuration = 0.5f; // ��鸲 ���� �ð�
    [SerializeField] private float _shakeAmplitude = 2f;  // ��鸲 ����
    [SerializeField] private float _shakeFrequency = 2f;  // ��鸲 �ӵ�

    private void Awake()
    {
        _player = FindObjectOfType<BallController>();

        // Cinemachine Virtual Camera�� ������
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
            // �浹 �� ī�޶� ��鸲 Ȱ��ȭ
            _noise.m_AmplitudeGain = _shakeAmplitude;
            _noise.m_FrequencyGain = _shakeFrequency;

            // ī�޶� ��鸲 ������ ���� (�浹 ���⿡ �°� ����)
            _noise.m_AmplitudeGain = _shakeAmplitude;
            _noise.m_FrequencyGain = _shakeFrequency;

            // ��鸲 Ÿ�̸� ����
            _shakeTimer = _shakeDuration;
        }
    }

    private void Update()
    {
        // ��鸲 Ÿ�̸� ����
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;

            // Ÿ�̸Ӱ� ������ ��鸲 ��Ȱ��ȭ
            if (_shakeTimer <= 0f && _noise != null)
            {
                _noise.m_AmplitudeGain = 0f;
                _noise.m_FrequencyGain = 0f;
            }
        }
    }
}
