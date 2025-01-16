using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private bool _isColorChanged;

    private float _lifeTimer;

    private SpawnerBombs _spawnerBomb;

    private WaitForSeconds _wait;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            if (_isColorChanged == false)
            {
                _isColorChanged = true;
                gameObject.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
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

        _spawnerBomb.CreateBomb(transform);
        gameObject.SetActive(false);
    }

    public void GetSpawner(SpawnerBombs spawner)
    {
        _spawnerBomb = spawner;
    }

    public void ChangeLifeTimer(int time)
    {
        _lifeTimer = time;
    }
}
