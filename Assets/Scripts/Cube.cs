using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private bool _isColorChanged = true;

    private float _lifeTimer;

    private SpawnerCube _spawner;

    private WaitForSeconds _wait;

    public void ReturnColor()
    {
        _isColorChanged = !_isColorChanged;
    }

    public void GetSpawner(SpawnerCube spawner)
    {
        _spawner = spawner;
    }

    public void ChangeLifeTimer(int time)
    {
        _lifeTimer = time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            if (_isColorChanged == false)
            {
                _isColorChanged = true;
                platform.GetNewColorCube(this);
            }

            StartCoroutine(GetLifeTime(_lifeTimer));
        }
    }

    private IEnumerator GetLifeTime(float delay)
    {
        _wait = new WaitForSeconds(delay);

        while (_lifeTimer > 0)
        {
            _lifeTimer--;

            yield return _wait;
        }

        _spawner.ReleaseObjectPool(this);
    }
}
