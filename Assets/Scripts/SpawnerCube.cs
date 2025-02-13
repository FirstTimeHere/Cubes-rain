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

    public System.Action<Cube> RelesedCube;

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

        _cube.Released += ReleaseObjectPool;
    }

    private void OnDisable()
    {
        _pool.ChangedCountAll -= ShowCountAllObjects;
        _pool.ChangedCountActive -= ShowCountActiveObjects;
        _pool.ChangedCountInactive -= ShowCountInactiveObjects;

        _cube.Released -= ReleaseObjectPool;
    }

    private void Start()
    {
        StartCoroutine(GetCubes());
    }

    private void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);
        Debug.Log(cube.GetInstanceID());
        Debug.Log(_pool.ToString());
        RelesedCube?.Invoke(cube);
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
            _pool.GetObject(this);

            yield return _wait;
        }
    }
}
