using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InfoBomb))]

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private SpawnerCube _spawnerCube;

    private ObjectPool<Bomb> _pool;

    private Transform _transformCubePosition;

    private int _maxCountBomb = 15;

    private void Awake()
    {
        _ = new InfoBomb(this, Text);

        _pool = new ObjectPool<Bomb>(_bomb, _maxCountBomb);

        Changer = GetComponent<ColorChanger>();
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
        var bomb = _pool.GetObject();
        Spawn(bomb);

        bomb.Released += ReleaseObjectPool;
    }

    protected override void Spawn(Bomb @object)
    {
        int randomTime = GetRandomTime();

        @object.Released += Changer.GetDefaultAlpha;


        @object.transform.position = _transformCubePosition.position;

        @object.ChangeLifeTimer(randomTime);

        @object.StartCorutins();
    }

    private void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
        bomb.Released -= ReleaseObjectPool;
        bomb.Released -= Changer.GetDefaultAlpha;
    }
}
