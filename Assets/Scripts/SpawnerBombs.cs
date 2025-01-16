using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBombs : Spawner<Bomb>
{
   [SerializeField] private Bomb _bomb;

    public void CreateBomb(Transform transform)
    {
        Prefab = _bomb;
        Prefab.transform.position = transform.position;
        Create();
    }
}
