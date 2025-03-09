using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    [field: SerializeField] protected TextMeshProUGUI Text;

    [field: SerializeField] protected T Prefab;

    public event Action<Spawner<T>> ChangedText;

    public ColorChanger Changer { get; protected set; }

    public int AllObjects { get; private set; }

    public int ActiveObjects { get; private set; }

    public int InstantiateObjects { get; private set; }

    protected virtual T Create()
    {
        return Instantiate(Prefab);
    }

    protected virtual void Spawn(T @object) { }

    public int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;

        return UnityEngine.Random.Range(minRandom, maxRandom);
    }

    public void ShowCountGetObjects()
    {
        AllObjects++;
        ChangedText?.Invoke(this);
    }

    public void ShowCountActiveObjects(int count)
    {
        ActiveObjects = count;
        ChangedText?.Invoke(this);
    }

    public void ShowCountInstantiatedObjects(int count)
    {
        InstantiateObjects = count;
        ChangedText?.Invoke(this);
    }
}
