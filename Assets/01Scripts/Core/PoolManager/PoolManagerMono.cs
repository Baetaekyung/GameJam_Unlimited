using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManagerMono : MonoBehaviour
{
    [SerializeField, Tooltip("생성한 물체의 부모")] private Transform _containerTrm;
    [SerializeField] private ObjectPoolManagerSO _objectPoolManagerSO;
    
    private void Awake()
    {
        _objectPoolManagerSO.containerTrm = _containerTrm; //오브젝트 생성 위치 결정
        
        _objectPoolManagerSO.Initialize();
    }
}
