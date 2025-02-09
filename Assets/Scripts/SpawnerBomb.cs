using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Bomb _bomb;
    [SerializeField] private SpawnerCube _spawnerCube;

    private CustomObjectPool _pool;

    private int _maxCountBomb = 15;

    private void Awake()
    {
        _pool = new CustomObjectPool(_bomb, _maxCountBomb);
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
        AllObjects = _pool.ShowCountAllBombs();
        ActiveObjects = _pool.ShowCountActiveBombs();
        InactiveObjects = _pool.ShowCountInactiveBombs();
    }

    private void CreateBomb(Transform transform)
    {
        _pool.GetBomb(transform, this);
    }

    public void ReleaseObjectPool(Bomb bomb)
    {
        _pool.Release(bomb);
    }
}
