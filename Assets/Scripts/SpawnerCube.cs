using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerBomb))]

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private Cube _cube;

    [SerializeField] private float _waitTime = 0.1f;
    [SerializeField, Range(0, 100)] private int _numbersOfCubes;

    [SerializeField] private List<Transform> _spawnPoints;

    private CustomObjectPool _pool;

    private WaitForSeconds _wait;

    public System.Action<Transform> RelesedCube;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);

        _pool = new CustomObjectPool(_cube, _numbersOfCubes);
    }

    private void Start()
    {
        StartCoroutine(GetCubes());
    }

    private void Update()
    {
        AllObjects = _pool.ShowCountAllCubes();
        ActiveObjects = _pool.ShowCountActiveCubes();
        InactiveObjects = _pool.ShowCountInactiveCubes();
    }

    public void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);
        RelesedCube?.Invoke(cube.transform);
    }

    private IEnumerator GetCubes()
    {
        while (enabled)
        {
            _pool.GetCube(_spawnPoints, this);

            yield return _wait;
        }
    }
}
