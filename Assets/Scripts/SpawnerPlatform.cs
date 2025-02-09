using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPlatform : Spawner<Platform>
{
    [SerializeField] private List<Platform> _platforms;
    [SerializeField] private List<Transform> _spawnPointsPlatform;

    [SerializeField] private Platform _basePlatform;
    [SerializeField] private Platform _invisiblePlatform;

    [SerializeField] private Transform _spawnPointBasePlatform;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        List<Transform> tempSpawnPoints = new List<Transform>();

        Prefab = _invisiblePlatform;
        Create();

        foreach (Transform spawnPoint in _spawnPointsPlatform)
        {
            tempSpawnPoints.Add(spawnPoint);
        }

        Prefab = _basePlatform;
        Create();

        for (int i = 0; i < _platforms.Count; i++)
        {
            int randomsSpawnPoint = Random.Range(0, tempSpawnPoints.Count);

            _platforms[i].transform.position = tempSpawnPoints[randomsSpawnPoint].transform.position;
            tempSpawnPoints.RemoveAt(randomsSpawnPoint);

            Prefab = _platforms[i];
            Create();
        }
    }
}
