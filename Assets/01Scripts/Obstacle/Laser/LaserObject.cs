using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;

    [SerializeField] private float _laserDuration;
    [SerializeField] private float _laserCoolTime;

    private void Start()
    {
        StartCoroutine(nameof(LaserRoutine));
    }

    private IEnumerator LaserRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_laserCoolTime);
            laserPrefab.SetActive(true);
            yield return new WaitForSeconds(_laserDuration);
            laserPrefab.SetActive(false);
        }
    }
}
