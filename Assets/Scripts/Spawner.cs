using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T Prefab;

    public Action Created;

    public virtual int AllObjects { get; protected set; }

    public virtual int ActiveObjects { get; protected set; }

    public virtual int InactiveObjects { get; protected set; }

    protected virtual T Create()
    {
        T @object = Instantiate(Prefab);
        Created?.Invoke();

        return @object;
    }
}
