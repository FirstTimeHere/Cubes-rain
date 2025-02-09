using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CustomObjectPool
{
    private Cube _cube;
    private Bomb _bomb;

    private ObjectPool<Cube> _poolCube;
    private ObjectPool<Bomb> _poolBomb;

    public CustomObjectPool(Cube prefabCube, int maxNumbersCube, int minNumbersCube = 10)
    {
        if (minNumbersCube > maxNumbersCube)
        {
            maxNumbersCube = minNumbersCube;
        }

        _cube = prefabCube;
        _poolCube = new ObjectPool<Cube>(CreateCube, 
            OnGetCubeFromPool, OnReleasedPool, OnDestroyCube, 
            true, minNumbersCube, maxNumbersCube);
    }

    public CustomObjectPool(Bomb prefabBomb, int maxNumbersCube, int minNumbersCube = 10)
    {
        if (minNumbersCube > maxNumbersCube)
        {
            maxNumbersCube = minNumbersCube;
        }

        _bomb = prefabBomb;
        _poolBomb = new ObjectPool<Bomb>(CreateBomb,
            OnGetBombFromPool, OnReleasedBombFromPool, OnDestroyBomb,
            true, minNumbersCube, maxNumbersCube);
    }

    private void OnDestroyBomb(Bomb bomb)
    {
        Object.Destroy(bomb);
    }

    private void OnReleasedBombFromPool(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }

    private void OnGetBombFromPool(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
    }

    private Bomb CreateBomb()
    {
        Bomb bomb = Object.Instantiate(_bomb);
        return bomb;
    }

    public Bomb GetBomb(Transform transform, SpawnerBomb spawner)
    {
        Bomb bomb;

        int randomTime = GetRandomTime();

        bomb = _poolBomb.Get();

        bomb.ReturnDefaultAlpha();

        bomb.GetSpawner(spawner);

        bomb.transform.position = transform.position;

        bomb.ChangeLifeTimer(randomTime);

        return bomb;
    }

    public Cube GetCube(List<Transform> spawnPoints,  SpawnerCube spawner)
    {
        Cube cube;

        int randomTime=GetRandomTime();
        int spawnRandom = Random.Range(0, spawnPoints.Count);

        var tempSpawnPoint = spawnPoints[spawnRandom];

        cube = _poolCube.Get();

        cube.GetComponent<MeshRenderer>().material.color = Color.white;

        cube.ReturnColor();

        cube.GetSpawner(spawner);

        cube.transform.position = tempSpawnPoint.position;

        cube.ChangeLifeTimer(randomTime);

        return cube;
    }

    public int ShowCountAllCubes()
    {
        int objectCount = _poolCube.CountAll;
        return objectCount;
    }

    public int ShowCountActiveCubes()
    {
        int objectCount = _poolCube.CountActive;
        return objectCount;
    }

    public int ShowCountInactiveCubes()
    {
        int objectCount = _poolCube.CountInactive;
        return objectCount;
    }

    public int ShowCountAllBombs()
    {
        int objectCount = _poolBomb.CountAll;
        return objectCount;
    }

    public int ShowCountActiveBombs()
    {
        int objectCount = _poolBomb.CountActive;
        return objectCount;
    }

    public int ShowCountInactiveBombs()
    {
        int objectCount = _poolBomb.CountInactive;
        return objectCount;
    }

    public void Release(Cube cube)
    {
        _poolCube.Release(cube);
    }

    public void Release(Bomb bomb)
    {
        _poolBomb.Release(bomb);
    }

    private void OnDestroyCube(Cube cube)
    {
        Object.Destroy(cube);
    }

    private Cube CreateCube()
    {
        Cube cube = Object.Instantiate(_cube);

        return cube;
    }

    private void OnGetCubeFromPool(Cube cube)
    {
        cube.gameObject.SetActive(true);
    }

    private void OnReleasedPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;
        int randomTime = Random.Range(minRandom, maxRandom);

        return randomTime;
    }
}
