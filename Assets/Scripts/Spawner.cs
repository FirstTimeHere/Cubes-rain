using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    protected T Prefab;

    protected virtual T Create()
    {
        T @object = Instantiate(Prefab);

        return @object;
    }
}
