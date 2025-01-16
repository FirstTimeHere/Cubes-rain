using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private int _lifeTimer = 0;

    private WaitForSecondsRealtime _wait;

    private Material _material;

    private Color _color;


    private void Awake()
    {
        _lifeTimer = RandomLifeTimer();
        _material = GetComponent<Renderer>().material;
        _color = _material.color;
    }

    private void Start()
    {
        StartCoroutine(GetLifeTime(_lifeTimer));
    }

    private void Explode()
    {
        foreach (Rigidbody hit in GetExlodableObjects())
        {
            hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
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

            _color.a = Mathf.Lerp(0f, 1f, _lifeTimer * Time.deltaTime);
            _material.color = _color;

            yield return _wait;
        }

        Explode();
    }

    private int RandomLifeTimer()
    {
        int minRandom = 2;
        int maxRandom = 5;

        return Random.Range(minRandom, maxRandom);
    }
}