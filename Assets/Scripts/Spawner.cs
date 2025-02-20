using System;
using TMPro;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component
{
    protected T Prefab;

    public event Action ChangedText;

    public virtual int AllObjects { get; protected set; }

    public virtual int ActiveObjects { get; protected set; }

    public virtual int InstantiateObjects { get; protected set; }

    protected virtual T Create()
    {
        T @object = Instantiate(Prefab);

        return @object;
    }

    public virtual void Spawn(T @object) { }

    public int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;
        int randomTime = UnityEngine.Random.Range(minRandom, maxRandom);

        return randomTime;
    }

    private void ShowInfo(TextMeshProUGUI text)
    {
        text.text = $"Сколько раз вызван Instantiate: {InstantiateObjects}\n" +
            $"Всего объектов/вызовов GET: {AllObjects}\n" +
            $"Активных объектов: {ActiveObjects}\n\n";
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

    protected void ShowCountAllCreatedObjects(int count)
    {
        InstantiateObjects = count;
        ChangedText?.Invoke();
    }


}
