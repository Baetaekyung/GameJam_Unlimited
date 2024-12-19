using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DetectEffect : MonoBehaviour, IPoolable
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private ObjectPoolManagerSO _poolManagerSO;

    public void OnParticleSystemStopped()
    {
        _poolManagerSO.Despawn("DetectEffect", gameObject);
        OnDespawn();
    }

    public void OnSpawn()
    {
        _effect.Play();
    }

    public void OnDespawn()
    {
        _effect.Stop();
    }
}
