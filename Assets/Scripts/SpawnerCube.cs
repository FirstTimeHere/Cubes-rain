using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerBomb))]
[RequireComponent(typeof(ColorChanger))]

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private ColorChanger _changer;
    [SerializeField] private Cube _cube;

    [SerializeField] private float _waitTime = 0.1f;
    [SerializeField, Range(0, 100)] private int _numbersOfCubes;

    [SerializeField] private List<Transform> _spawnPoints;

    private CustomObjectPool<Cube> _pool;

    private WaitForSeconds _wait;

    private InfoText<Cube> _info;

    public event System.Action<Cube> RelesedCube;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);

        _pool = new CustomObjectPool<Cube>(_cube, _numbersOfCubes);

        _info = new InfoText<Cube>(this, Text);

        _changer = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _pool.ChangedCountCreateObjects += ShowCountAllCreatedObjects;
        _pool.ChangedCountAll += ShowCountAllObjects;
        _pool.ChangedCountActive += ShowCountActiveObjects;
    }

    private void OnDisable()
    {
        _pool.ChangedCountCreateObjects -= ShowCountAllCreatedObjects;
        _pool.ChangedCountAll -= ShowCountAllObjects;
        _pool.ChangedCountActive -= ShowCountActiveObjects;
    }

    private void Start()
    {
        StartCoroutine(GetCubes());
    }

    private IEnumerator GetCubes()
    {
        while (enabled)
        {
            var cube = _pool.GetObject(this);

            cube.Released += ReleaseObjectPool;

            cube.Released += _changer.GetDefaultColor;

            cube.TouchedPlatform += _changer.GetNewColorCube;

            yield return _wait;
        }
    }

    private void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);

        cube.Released -= _changer.GetDefaultColor;
        cube.Released -= ReleaseObjectPool;
        cube.TouchedPlatform -= _changer.GetNewColorCube;

        RelesedCube?.Invoke(cube);
    }

    public override void Spawn(Cube @object)
    {
        int randomTime = GetRandomTime();
        int spawnRandom = Random.Range(0, _spawnPoints.Count);

        var tempSpawnPoint = _spawnPoints[spawnRandom];

        @object.transform.position = tempSpawnPoint.position;

        @object.ChangeLifeTimer(randomTime);
    }
}
