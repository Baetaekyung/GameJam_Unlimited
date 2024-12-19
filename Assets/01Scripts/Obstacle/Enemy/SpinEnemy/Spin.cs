using UnityEngine;

public class Spin : Enemy
{
    private SpinEnemyManager _manager; // �Ŵ��� ����

    protected override void Start()
    {
        base.Start();
        _manager = FindObjectOfType<SpinEnemyManager>();

        if (_manager != null)
        {
            _manager.AddEnemy(this);
        }
    }

    protected override void Update()
    {
        base.Update();

        FollowPlayer();
    }

    //private void OnDestroy()
    //{
    //    if (_manager != null)
    //    {
    //        _manager.RemoveEnemy(this);
    //    }
    //}

    protected override bool IsPlayerInRange() { return base.IsPlayerInRange(); }
    public void FollowPlayer()
    {
        if (IsPlayerInRange())
        {
            Debug.Log("���� �ȿ� �ֳ�");
        }
    }
}
