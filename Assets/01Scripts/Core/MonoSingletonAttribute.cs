using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MonoSingletonAttribute : Attribute
{
    public readonly SingletonFlag Flag;

    public MonoSingletonAttribute(SingletonFlag flag)
    {
        this.Flag = flag;
    }
}

[Flags]
public enum SingletonFlag
{
    None = 0,
    DontDestroyOnLoad = 1 << 0
}
