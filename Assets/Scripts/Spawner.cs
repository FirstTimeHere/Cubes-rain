using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T Prefab;

    public Action Created;
    public int AllObjecctsCreate {  get; private set; }
    protected virtual T Create()
    {
        T @object = Instantiate(Prefab);
        AllObjecctsCreate++;
        Created?.Invoke();

        return @object;
    }
}
