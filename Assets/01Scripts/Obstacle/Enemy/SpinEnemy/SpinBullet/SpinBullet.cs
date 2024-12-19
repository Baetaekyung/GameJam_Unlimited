using UnityEngine;

public class SpinBullet : MonoBehaviour
{
    private BallController _player;

    [SerializeField] private float Speed = 10f; // 총알의 속도
    [SerializeField] private float Lifetime = 5f; // 총알의 생명 시간

    private Vector3 _direction;

    private void Awake()
    {
        _player = FindObjectOfType<BallController>();
    }

    private void Start()
    {
        // 총알의 방향은 Spin 적이 추적 중인 플레이어 방향으로 설정
        _direction = (_player.transform.position - transform.position).normalized;

        // 총알이 특정 시간 후 제거되도록 설정
        Destroy(gameObject, Lifetime);
    }

    private void Update()
    {
        // 총알이 지정된 방향으로 이동
        transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (collision.TryGetComponent(out BallController player))
        {
            player.Dead();
        }
    }
}
