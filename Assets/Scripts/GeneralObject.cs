using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour
{
    public WaitForSeconds Wait { get; protected set; }

    public Coroutine Coroutine { get; protected set; }

    public MeshRenderer Mesh { get; private set; }


    public int LifeTimer {  get; protected set; }
    public float WaitTime { get; private set; } = 1f;

    private void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
    }

    protected virtual void StartCorutine() { }
    protected virtual void StopCorutine(Coroutine coroutine) { }

    public virtual void ChangeLifeTimer(int time)
    {
        LifeTimer = time;
    }
}
