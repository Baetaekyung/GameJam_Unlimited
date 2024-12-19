using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPoolManager", menuName = "SO/Pooling/ObjectPoolManager")]
public class ObjectPoolManagerSO : ScriptableObject
{
    [System.Serializable]
    public class ObjectPool
    {
        public string poolName;
        public GameObject prefab;
        public int initialSize;
        
        [HideInInspector]
        public Queue<GameObject> objects = new();
    }

    public Transform containerTrm;
    public List<ObjectPool> pools = new();

    private Dictionary<string, ObjectPool> _poolDictionary;

    public void Initialize()
    {
        _poolDictionary = new Dictionary<string, ObjectPool>();

        foreach (var pool in pools)
        {
            pool.objects = new Queue<GameObject>();
            _poolDictionary[pool.poolName] = pool;

            for (var i = 0; i < pool.initialSize; ++i)
            {
                var go = Instantiate(pool.prefab, containerTrm);
                go.SetActive(false);
                pool.objects.Enqueue(go);
            }
        }
    }

    public GameObject Spawn(string poolName, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(poolName, out var pool))
        {
            Debug.Log(($"Pool with name {poolName} does not exist"));
            return null;
        }

        GameObject go;

        go = pool.objects.Count > 0 ? pool.objects.Dequeue() : Instantiate(pool.prefab);
        
        go.transform.SetParent(null);
        go.SetActive(true);
        go.transform.SetPositionAndRotation(position, rotation);

        if (go.TryGetComponent(out IPoolable poolObject))
            poolObject.OnSpawn();

        return go;
    }

    public void Despawn(string poolName, GameObject go)
    {
        if (!_poolDictionary.ContainsKey(poolName))
        {
            Debug.Log(($"Pool with name {poolName} does not exist"));
            return;
        }
        
        go.transform.SetParent(containerTrm);
        go.SetActive(false);

        if (go.TryGetComponent(out IPoolable poolObj))
            poolObj.OnDespawn();
        
        _poolDictionary[poolName].objects.Enqueue(go);
    }
}
