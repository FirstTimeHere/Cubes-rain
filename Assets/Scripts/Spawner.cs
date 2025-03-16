using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [field: SerializeField] protected T Prefab;

    public ColorChanger Changer { get; protected set; }

    public int AllObjects { get; private set; }

    public int ActiveObjects { get; private set; }

    public int InstantiateObjects { get; private set; }

    public int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 5;

        return UnityEngine.Random.Range(minRandom, ++maxRandom);
    }

    public void ShowCountGetObjects(int count)
    {
        AllObjects = count;
    }

    public void ShowCountActiveObjects(int count)
    {
        ActiveObjects = count;
    }

    public void ShowCountInstantiatedObjects(int count)
    {
        InstantiateObjects = count;
    }

    protected virtual T Create()
    {
        return Instantiate(Prefab);
    }

    protected virtual void Spawn(T @object) { }
}
