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

    private CustomObjectPool<Cube> _pool;

    private WaitForSeconds _wait;

    public event System.Action<Cube> RelesedCube;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);

        _pool = new CustomObjectPool<Cube>(_cube, _numbersOfCubes);
    }

    private void OnEnable()
    {
        _pool.ChangedCountAll += ShowCountAllObjects;
        _pool.ChangedCountActive += ShowCountActiveObjects;
        _pool.ChangedCountInactive += ShowCountInactiveObjects;
    }

    private void OnDisable()
    {
        _pool.ChangedCountAll -= ShowCountAllObjects;
        _pool.ChangedCountActive -= ShowCountActiveObjects;
        _pool.ChangedCountInactive -= ShowCountInactiveObjects;
    }

    private void Start()
    {
        StartCoroutine(GetCubes());
    }

    private void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);
        RelesedCube?.Invoke(cube);
        cube.Released -= ReleaseObjectPool;
    }

    public override void Spawn(Cube @object)
    {
        int randomTime = RandomTime();
        int spawnRandom = Random.Range(0, _spawnPoints.Count);

        var tempSpawnPoint = _spawnPoints[spawnRandom];

        @object.GetComponent<MeshRenderer>().material.color = Color.white;

        @object.ReturnColor();

        @object.transform.position = tempSpawnPoint.position;

        @object.ChangeLifeTimer(randomTime);
    }

    private IEnumerator GetCubes()
    {
        while (enabled)
        {
           var cube =_pool.GetObject(this);

            cube.Released += ReleaseObjectPool;

            yield return _wait;
        }
    }
}
