using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private float _itemSpawnInterval; //아이템 생성 간격
    private float _lastTryTime;

    private void Start()
    {
        Instantiate(_itemPrefab, transform.position, Quaternion.identity).transform.SetParent(transform);
    }

    private void Update()
    {
        if (_lastTryTime > _itemSpawnInterval)
        {
            if (transform.childCount == 0)
            {
                Instantiate(_itemPrefab, transform.position, Quaternion.identity).transform.SetParent(transform);
            }
            _lastTryTime = 0f;
        }
        else
        {
            _lastTryTime += Time.deltaTime;
        }
    }
}
