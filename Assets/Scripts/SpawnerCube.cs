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

    private ObjectPool<Cube> _pool;

    private WaitForSeconds _wait;

    private InfoText<Cube> _info;

    public event System.Action<Cube> RelesedCube;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitTime);

        _pool = new ObjectPool<Cube>(_cube, _numbersOfCubes);

        _info = new InfoText<Cube>(this, Text);

        Changer = GetComponent<ColorChanger>();
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
            var cube = _pool.GetObject();
            Spawn(cube);

            cube.Released += ReleaseObjectPool;

            cube.Released += Changer.GetDefaultColor;

            cube.TouchedPlatform += Changer.GetNewColorCube;

            yield return _wait;
        }
    }

    private void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);

        cube.Released -= Changer.GetDefaultColor;
        cube.Released -= ReleaseObjectPool;
        cube.TouchedPlatform -= Changer.GetNewColorCube;

        RelesedCube?.Invoke(cube);
    }

    protected override void Spawn(Cube cube)
    {
        int randomTime = GetRandomTime();
        int spawnRandom = Random.Range(0, _spawnPoints.Count);

        var tempSpawnPoint = _spawnPoints[spawnRandom];

        cube.transform.position = tempSpawnPoint.position;

        cube.ChangeLifeTimer(randomTime);
    }
}
