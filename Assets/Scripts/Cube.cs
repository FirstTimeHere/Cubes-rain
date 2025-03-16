using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : GeneralObject
{
    private bool _isTouchedPlatform = true;

    public event Action<Cube> Released;
    public event Action<Cube> TouchedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchedPlatform)
        {
            if (collision.collider.TryGetComponent(out Platform platform))
            {
                TouchedPlatform?.Invoke(this);
                _isTouchedPlatform = false;

                StartCorutine();
            }
        }
    }

    private IEnumerator GetLifeTimer(float delay)
    {
        Wait = new WaitForSeconds(delay);

        while (LifeTimer > 0)
        {
            LifeTimer--;

            yield return Wait;
        }

        StopCorutine(Coroutine);
        Released?.Invoke(this);
    }

    public void ChangeBoolForCube()
    {
        _isTouchedPlatform = true;
    }

    protected override void StartCorutine()
    {
        Coroutine = StartCoroutine(GetLifeTimer(WaitTime));
    }

    protected override void StopCorutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }
}
