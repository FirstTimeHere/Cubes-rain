using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class Bomb : GeneralObject
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;


    public event Action<Bomb> Released;

    private void Start()
    {
        StartCoroutine(GetAlphaChange());
    }

    private IEnumerator GetAlphaChange()
    {
        float startAlpha = Mesh.material.color.a;

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
    }

    public void StartCorutins()
    {
        StartCoroutine(GetAlphaChange());
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
        Released?.Invoke(this);
    }

    private void ChangeAlpha(float alpha)
    {
        Color color = Mesh.material.color;
        color.a = alpha;
        Mesh.material.color = color;
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