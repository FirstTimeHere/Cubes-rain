using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _lifeTimer;

    private WaitForSeconds _wait;

    public event Action<Cube> Released;
    public event Action<Cube> TouchedPlatform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            TouchedPlatform?.Invoke(this);

            StartCoroutine(GetLifeTimer(_lifeTimer));
        }
    }

    private IEnumerator GetLifeTimer(float delay)
    {
        _wait = new WaitForSeconds(delay);

        while (_lifeTimer > 0)
        {
            _lifeTimer--;

            yield return _wait;
        }

        Released?.Invoke(this);
    }

    public void ChangeLifeTimer(int time)
    {
        _lifeTimer = time;
    }
}
