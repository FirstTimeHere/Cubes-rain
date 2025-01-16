using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerBombs))]

public class SpawnerCubs : Spawner<Cube>
{
    [SerializeField] private Cube _cube;

    [SerializeField] private float _waitTime = 0.1f;
    [SerializeField, Range(0, 100)] private int _numbersOfCubes;

    [SerializeField] private List<Transform> _spawnPoints;

    private List<Cube> _cubs = new List<Cube>();

    private WaitForSeconds _wait;

    private SpawnerBombs _spawnerBombs;

    private void Awake()
    {
        _spawnerBombs = GetComponent<SpawnerBombs>();

        _wait = new WaitForSeconds(_waitTime);

        Prefab = _cube;

        for (int i = 0; i < _numbersOfCubes; i++)
        {
            _cubs.Add(Create());
        }
    }

    private void Start()
    {
        StartCoroutine(GetCubes());
    }

    private void Spawn()
    {
        int spawnRandom = Random.Range(0, _spawnPoints.Count);
        int spawnRandomCube = Random.Range(0, _cubs.Count);

        var tempSpawnPoint = _spawnPoints[spawnRandom];

        if (_cubs[spawnRandomCube].gameObject.activeSelf == false)
        {
            _cubs[spawnRandomCube].gameObject.SetActive(true);
            _cubs[spawnRandomCube].transform.position = tempSpawnPoint.position;
            _cubs[spawnRandomCube].GetSpawner(_spawnerBombs);
            _cubs[spawnRandomCube].ChangeLifeTimer(GetRandomTime());
        }
    }

    private IEnumerator GetCubes()
    {
        while (enabled)
        {
            Spawn();
            yield return _wait;
        }
    }

    private int GetRandomTime()
    {
        int minRandom = 2;
        int maxRandom = 6;
        int randomTime = Random.Range(minRandom, maxRandom);

        return randomTime;
    }
}
