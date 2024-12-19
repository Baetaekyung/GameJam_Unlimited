using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMaker : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private ObjectPoolManagerSO _poolManagerSO;
    
    [SerializeField] private float _shootDelay;
    [SerializeField] private float _shootPower;

    private void Start()
    {
        StartCoroutine(nameof(MakeBullet));
    }

    private IEnumerator MakeBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shootDelay);
            GameObject go = _poolManagerSO.Spawn("Bullets", _bulletSpawnPoint.position, transform.rotation);
            var bullet = go.GetComponent<Bullet>();
            
            bullet.RbCompo.AddForce(transform.right * _shootPower, ForceMode2D.Impulse);
        }
    }
}
