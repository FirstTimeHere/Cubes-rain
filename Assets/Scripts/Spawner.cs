using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    protected T Prefab;

    public event Action ChangedText;

    public virtual int AllObjects { get; protected set; }

    public virtual int ActiveObjects { get; protected set; }

    public virtual int InactiveObjects { get; protected set; }

    protected virtual T Create()
    {
        T @object = Instantiate(Prefab);

        return @object;
    }

    public virtual void Spawn(T @object) { }

    public virtual int RandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;
        int randomTime = UnityEngine.Random.Range(minRandom, maxRandom);

        return randomTime;
    }

    protected void ShowCountAllObjects(int count)
    {
        AllObjects = count;
        ChangedText?.Invoke();
    }

    protected void ShowCountActiveObjects(int count)
    {
        ActiveObjects = count;
        ChangedText?.Invoke();
    }

    protected void ShowCountInactiveObjects(int count)
    {
        InactiveObjects = count;
        ChangedText?.Invoke();
    }
}
