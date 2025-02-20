using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private SpawnerCube _spawnerCube;

    private CustomObjectPool<Bomb> _pool;

    private Transform _transformCubePosition;

    private int _maxCountBomb = 15;

    private void Awake()
    {
        _pool = new CustomObjectPool<Bomb>(_bomb, _maxCountBomb);
    }

    private void OnEnable()
    {
        _spawnerCube.RelesedCube += CreateBomb;

        _pool.ChangedCountAll += ShowCountAllObjects;
        _pool.ChangedCountActive += ShowCountActiveObjects;
        _pool.ChangedCountCreateObjects += ShowCountAllCreatedObjects;
    }

    private void OnDisable()
    {
        _spawnerCube.RelesedCube -= CreateBomb;

        _pool.ChangedCountAll -= ShowCountAllObjects;
        _pool.ChangedCountActive -= ShowCountActiveObjects;
        _pool.ChangedCountCreateObjects -= ShowCountAllCreatedObjects;
    }

    private void CreateBomb(Cube cube)
    {
        _transformCubePosition = cube.transform;
        var bomb = _pool.GetObject(this);
        bomb.Released += ReleaseObjectPool;
    }

    public override void Spawn(Bomb @object)
    {
        int randomTime = GetRandomTime();

        @object.transform.position = _transformCubePosition.position;

        @object.ReturnDefaultAlpha();

        @object.ChangeLifeTimer(randomTime);
    }

    private void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
        bomb.Released -= ReleaseObjectPool;
    }
}
