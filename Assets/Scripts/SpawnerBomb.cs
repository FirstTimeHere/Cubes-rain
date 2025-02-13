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
        _pool.ChangedCountInactive += ShowCountInactiveObjects;

        _bomb.Released += ReleaseObjectPool;
    }

    private void OnDisable()
    {
        _spawnerCube.RelesedCube -= CreateBomb;

        _pool.ChangedCountAll -= ShowCountAllObjects;
        _pool.ChangedCountActive -= ShowCountActiveObjects;
        _pool.ChangedCountInactive -= ShowCountInactiveObjects;

        _bomb.Released -= ReleaseObjectPool;
    }

    private void CreateBomb(Cube cube)
    {
        _transformCubePosition = cube.transform;
        _pool.GetObject(this);
    }

    public override void Spawn(Bomb @object)
    {
        int randomTime = RandomTime();

        @object.transform.position = _transformCubePosition.position;

        @object.ReturnDefaultAlpha();

        @object.ChangeLifeTimer(randomTime);
    }

    private void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
    }
}
