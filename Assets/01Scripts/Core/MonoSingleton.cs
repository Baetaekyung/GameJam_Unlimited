using System;
using System.Reflection;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static bool isShuttingDown { get; set; } = false;

    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (isShuttingDown) return null;
            if (_instance is null) RuntimeInitializer();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        CheckBindingFlags();
        
        Debug.LogWarning($"싱글톤이 2개 이상 씬에 존재함. 이름은 {typeof(T).Name}");
        Destroy(gameObject);
    }

    private void CheckBindingFlags()
    {
        var singletonAttribute = typeof(T).GetCustomAttribute<MonoSingletonAttribute>();
        var singletonFlag = singletonAttribute?.Flag ?? 0; //만약 플레그가 존재한다면 플레그의 수 아니면 0

        if (singletonFlag.HasFlag(SingletonFlag.DontDestroyOnLoad)) DontDestroyOnLoad(this);
        else return;
    }

    /// <summary>
    /// It executes when it is not exist in hierarchy but any method called singleton
    /// </summary>
    private static T RuntimeInitializer()
    {
        if (_instance is null)
        {
            _instance = (T)FindFirstObjectByType(typeof(T));
                
            if (_instance != null)
            {
                return _instance;
            }

            GameObject go = new(name: typeof(T).Name + "(Singleton)");
            go.AddComponent<T>();
            _instance = go.GetComponent<T>();
            Debug.Log($"{go.name} is Runtime Singleton!");
        }
        
        if (_instance is not null) return _instance;
        Debug.LogWarning($"{typeof(T).Name} is failed singleton");
        return null;
    }

    protected void OnDestroy()
    {
        Debug.Log($"{typeof(T).Name} is Destroyed!");
        _instance = null;
    }

    protected virtual void OnApplicationQuit()
    {
        isShuttingDown = true;
    }
}
