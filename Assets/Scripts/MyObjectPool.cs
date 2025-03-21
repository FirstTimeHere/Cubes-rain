using System;
using UnityEngine;
using UnityEngine.Pool;

public class MyObjectPool<T> where T : Component
{
    private readonly T _prefab;

    private readonly ObjectPool<T> _poolObject;

    private int _instantiateCount = 0;

    public event Action<int> ChangedCountActive;
    public event Action<int> ChangedCountCreateObjects;

    public MyObjectPool(T prefabObject, int maxNumberObjects, int minNumberObjects = 10)
    {
        if (minNumberObjects > maxNumberObjects)
        {
            maxNumberObjects = minNumberObjects;
        }

        _prefab = prefabObject;
        _poolObject = new ObjectPool<T>(CreateObject,
            OnGetObjectFromPool, OnReleasedPool, OnDestroyObject,
            true, minNumberObjects, maxNumberObjects);
    }

    private void OnDestroyObject(T @object)
    {
        UnityEngine.Object.Destroy(@object);
    }

    private T CreateObject()
    {
        T @object = UnityEngine.Object.Instantiate(_prefab);

        ChangedCountCreateObjects?.Invoke(_instantiateCount++);
        return @object;
    }

    private void OnGetObjectFromPool(T @object)
    {
        @object.gameObject.SetActive(true);
    }

    private void OnReleasedPool(T @object)
    {
        @object.gameObject.SetActive(false);
    }

    public T GetObject()
    {
        T @object;

        @object = _poolObject.Get();

        ChangedCountActive?.Invoke(_poolObject.CountActive);

        return @object;
    }

    public void Release(T @object)
    {
        _poolObject.Release(@object);

        ChangedCountActive?.Invoke(_poolObject.CountActive);
    }
}
