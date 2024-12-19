using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    public Rigidbody2D RbCompo => GetComponent<Rigidbody2D>();
    [SerializeField] private ObjectPoolManagerSO _poolManagerSO;
    private float _lifeTime = 10f;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        
        if (_lifeTime <= 0)
        {
            _poolManagerSO.Despawn("Bullets", this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out BallController ball))
        {
            SoundManager.Instance.PlayerSFX(SfxType.BULLETHIT);
            ball.Dead();
        }
        _poolManagerSO.Despawn("Bullets", this.gameObject);
    }

    public void OnSpawn()
    { }

    public void OnDespawn()
    {
        _lifeTime = 10f;
    }
}
