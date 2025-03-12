using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerBomb))]
[RequireComponent(typeof(InfoCube))]

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _waitTime = 0.1f;
    [SerializeField, Range(0, 100)] private int _numbersOfCubes;

    [SerializeField] private List<Transform> _spawnPoints;

    private ObjectPool<Cube> _pool;

    private WaitForSeconds _wait;

    public event System.Action<Cube> RelesedCube;

    private void Awake()
    {
        _ = new InfoCube(this, Text);

        _wait = new WaitForSeconds(_waitTime);

        _pool = new ObjectPool<Cube>(Prefab, _numbersOfCubes);

        Changer = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        _pool.ChangedCountCreateObjects += ShowCountInstantiatedObjects;
        _pool.ChangedCountActive += ShowCountActiveObjects;
    }

    private void OnDisable()
    {
        _pool.ChangedCountCreateObjects -= ShowCountInstantiatedObjects;
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
            ShowCountGetObjects();

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

        cube.ChangeBoolForCube();
    }
}
