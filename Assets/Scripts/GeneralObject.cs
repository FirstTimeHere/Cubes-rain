using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralObject : MonoBehaviour
{
    public WaitForSeconds Wait { get; protected set; }

    public Coroutine Coroutine { get; protected set; }

    public int LifeTimer {  get; protected set; }
    public float WaitTime { get; private set; } = 1f;

    protected virtual void StartCorutine() { }
    protected virtual void StopCorutine(Coroutine coroutine) { }

    public virtual void ChangeLifeTimer(int time)
    {
        LifeTimer = time;
    }
}
