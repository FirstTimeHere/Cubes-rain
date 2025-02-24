using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    protected T Prefab;

    public ColorChanger Changer { get; protected set; }


    public event Action<Spawner<T>> ChangedText;

    [field: SerializeField] protected TextMeshProUGUI Text;

    private int AllObjects { get; set; }

    public int ActiveObjects { get; protected set; }

    public int InstantiateObjects { get; protected set; }

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

    public void ShowCountAllObjects(int count)
    {
        AllObjects = count;
        ChangedText?.Invoke(this);
    }

    public void ShowCountActiveObjects(int count)
    {
        ActiveObjects = count;
        ChangedText?.Invoke(this);
    }

    public void ShowCountAllCreatedObjects(int count)
    {
        InstantiateObjects = count;
        ChangedText?.Invoke(this);
    }
}
