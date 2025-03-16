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

    private MyObjectPool<Cube> _pool;
    private InfoCube _info;

    private WaitForSeconds _wait;

    private int _getObjectsCount = 0;

    public event System.Action<Cube> RelesedCube;

    private void Awake()
    {
        _info = GetComponent<InfoCube>();
        _wait = new WaitForSeconds(_waitTime);

        _pool = new MyObjectPool<Cube>(Prefab, _numbersOfCubes);

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
            ShowCountGetObjects(_getObjectsCount++);

            cube.Released += ReleaseObjectPool;

            cube.Released += Changer.GetDefaultColor;

            cube.TouchedPlatform += Changer.GetNewColorCube;

            _info.ShowText(this);

            yield return _wait;
        }
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

    private void ReleaseObjectPool(Cube cube)
    {
        _pool.Release(cube);

        cube.Released -= Changer.GetDefaultColor;
        cube.Released -= ReleaseObjectPool;
        cube.TouchedPlatform -= Changer.GetNewColorCube;

        _info.ShowText(this);

        RelesedCube?.Invoke(cube);
    }
}
