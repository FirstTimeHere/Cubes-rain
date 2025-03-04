using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Bomb : GeneralObject
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private int _lifeTimerForColor;

    private Coroutine _corutineColor;

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
        //StartCoroutine(GetLifeTime(WaitTime));
        StartCoroutine(GetAlphaChange());
    }

    private IEnumerator GetLifeTime(float delay)
    {
        Wait = new WaitForSeconds(delay);

        while (LifeTimer > 0)
        {
            LifeTimer--;
            yield return Wait;
        }

        Explode();
    }

    private IEnumerator GetAlphaChange()
    {
        float startAlpha = _material.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < LifeTimer)
        {
            float currentAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / LifeTimer);
            ChangeAlpha(currentAlpha);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        Explode();
    }

    public override void ChangeLifeTimer(int time)
    {
        LifeTimer = time;
        _lifeTimerForColor = time;
    }

    public void StartCorutins()
    {
        //StartCoroutine(GetLifeTime(WaitTime));
        StartCoroutine(GetAlphaChange());
    }

    protected override void StartCorutine()
    {
        //Coroutine = StartCoroutine(GetLifeTime(WaitTime));
        _corutineColor=StartCoroutine(GetAlphaChange());
    }

    protected override void StopCorutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(Coroutine);
            Coroutine = null;
        }
    }

    private void Explode()
    {
        foreach (Rigidbody hit in GetExlodableObjects())
        {
            hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        StopCorutine(Coroutine);
        StopCorutine(_corutineColor);
        Released?.Invoke(this);
    }

    private void ChangeAlpha(float alpha)
    {
        _color.a = alpha;
        _material.color = _color;
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
}