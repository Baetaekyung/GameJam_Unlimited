using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMaker : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private float _shootDelay;

    private void Start()
    {
        StartCoroutine(nameof(MakeBullet));
    }

    private IEnumerator MakeBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(_shootDelay);
            Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
        }
    }
}
