using UnityEngine;

public class SpinBullet : MonoBehaviour
{
    private BallController _player;

    [SerializeField] private float Speed = 10f; // �Ѿ��� �ӵ�
    [SerializeField] private float Lifetime = 5f; // �Ѿ��� ���� �ð�

    private Vector3 _direction;

    private void Awake()
    {
        _player = FindObjectOfType<BallController>();
    }

    private void Start()
    {
        // �Ѿ��� ������ Spin ���� ���� ���� �÷��̾� �������� ����
        _direction = (_player.transform.position - transform.position).normalized;

        // �Ѿ��� Ư�� �ð� �� ���ŵǵ��� ����
        Destroy(gameObject, Lifetime);
    }

    private void Update()
    {
        // �Ѿ��� ������ �������� �̵�
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
