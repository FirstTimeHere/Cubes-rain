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
    }

    private void OnDisable()
    {
        _spawnerCube.RelesedCube -= CreateBomb;
    }

    private void Update()
    {
        AllObjects = _pool.ShowCountAllObjects();
        ActiveObjects = _pool.ShowCountActiveObjects();
        InactiveObjects = _pool.ShowCountInactiveObjects();
    }

    private void CreateBomb(Transform transform)
    {
        _transformCubePosition = transform;
    }

    public override void Spawn(Bomb @object)
    {
        int randomTime = RandomTime();
        
        _pool.GetObject(this);

        @object.ReturnDefaultAlpha();

        @object.GetSpawner(this);

        @object.transform.position = _transformCubePosition.position;

        @object.ChangeLifeTimer(randomTime);
    }

    public void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
    }
}
