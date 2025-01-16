using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlatforms : Spawner<Platform>
{
    [SerializeField] private List<Platform> _platdorms;
    [SerializeField] private List<Transform> _spawnPointsPlatform;

    [SerializeField] private Platform _basePlatform;

    [SerializeField] private Transform _spawnPointBasePlatform;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        List<Transform> tempSpawnPoints = new List<Transform>();

        foreach (Transform spawnPoint in _spawnPointsPlatform)
        {
            tempSpawnPoints.Add(spawnPoint);
        }

        Prefab = _basePlatform;
        Create();

        for (int i = 0; i < _platdorms.Count; i++)
        {
            int randomsSpawnPoin = Random.Range(0, tempSpawnPoints.Count);

            _platdorms[i].transform.position = tempSpawnPoints[randomsSpawnPoin].transform.position;
            tempSpawnPoints.RemoveAt(randomsSpawnPoin);

            Prefab = _platdorms[i];
            Create();
        }
    }
}
