using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private int _lifeTimer;
    private int _lifeTimerForColor;

    private WaitForSecondsRealtime _wait;

    private Material _material;

    private Color _color;

    public event Action<Bomb> Released;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _color = _material.color;
        _material.color = _color;
    }

    private void Start()
    {
        StartCoroutine(GetLifeTime(_lifeTimer));
        StartCoroutine(GetAlphaChange());
    }

    public void ChangeLifeTimer(int time)
    {
        _lifeTimer = time;
        _lifeTimerForColor = time;
    }

    public void ReturnDefaultAlpha()
    {
        _color.a = 1f;
    }

    private void Explode()
    {
        foreach (Rigidbody hit in GetExlodableObjects())
        {
            hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Released?.Invoke(this);
    }

    private List<Rigidbody> GetExlodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> explodedObjects = new List<Rigidbody>();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                explodedObjects.Add(hit.attachedRigidbody);
            }
        }

        return explodedObjects;
    }

    private IEnumerator GetLifeTime(float delay)
    {
        _wait = new WaitForSecondsRealtime(delay);

        while (_lifeTimer > 0)
        {
            _lifeTimer--;
            yield return _wait;
        }

        Explode();
    }

    private IEnumerator GetAlphaChange()
    {
        float startAlpha = _material.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < _lifeTimerForColor)
        {
            float currentAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / _lifeTimerForColor);
            ChangeAlpha(currentAlpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private void ChangeAlpha(float alpha)
    {
        _color.a = alpha;
        _material.color = _color;
    }
}