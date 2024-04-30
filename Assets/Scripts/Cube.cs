using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Platform))]

public class Cube : MonoBehaviour
{
    private Platform _platform;

    private bool _isColorChanged;

    private float _lifeTimer;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _platform.gameObject)
        {
            if (_isColorChanged == false)
            {
                _isColorChanged = true;
                gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }

            StartCoroutine(GetLifeTime(_lifeTimer));
        }
    }
    private IEnumerator GetLifeTime(float delay)
    {
        var wait = new WaitForSecondsRealtime(delay);

        while (_lifeTimer > 0)
        {
            _lifeTimer--;

            yield return wait;
        }
    }

    public void ChangeLifeTimer(int time)
    {
        _lifeTimer = time;
    }

    public void GetPlatform(Platform plane)
    {
        _platform = plane;
    }
}
