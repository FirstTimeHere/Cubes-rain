using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CustomObjectPool<T> where T : Component
{
    private T _prefab;

    private ObjectPool<T> _poolObject;

    public CustomObjectPool(T prefabObject, int maxNumberObjects, int minNumberObjects = 10)
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

    //public Bomb GetBomb(Transform transform, SpawnerBomb spawner)
    //{
    //    Bomb bomb;

    //    int randomTime = GetRandomTime();

    //    bomb = _poolBomb.Get();

    //    bomb.ReturnDefaultAlpha();

    //    bomb.GetSpawner(spawner);

    //    bomb.transform.position = transform.position;

    //    bomb.ChangeLifeTimer(randomTime);

    //    return bomb;
    //}

    public T GetObject(Spawner<T> spawner)
    {
        T @object;

        @object = _poolObject.Get();
        spawner.Spawn(@object);

        return @object;
    }

    public int ShowCountAllObjects()
    {
        int objectCount = _poolObject.CountAll;
        return objectCount;
    }

    public int ShowCountActiveObjects()
    {
        int objectCount = _poolObject.CountActive;
        return objectCount;
    }

    public int ShowCountInactiveObjects()
    {
        int objectCount = _poolObject.CountInactive;
        return objectCount;
    }

    public void Release(T @object)
    {
        _poolObject.Release(@object);
    }

    private void OnDestroyObject(T @object)
    {
        Object.Destroy(@object);
    }

    private T CreateObject()
    {
        T @object = Object.Instantiate(_prefab);

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

}
