using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Cube _cube;
    [SerializeField] private Platform _platform;

    [SerializeField] private float _waitTime = 0.1f;
    [SerializeField, Range(0, 100)] private int _numbersOfCubes;

    private List<Cube> _cubes = new List<Cube>();

    private void Start()
    {
        CreateCubes(_numbersOfCubes);
        StartCoroutine(GetCubes(_waitTime));
    }

    private void Spawn()
    {
        int spawnRandom = Random.Range(0, _spawnPoints.Count);
        int spawnRandomCube = Random.Range(0, _cubes.Count);

        var tempSpawnPoint = _spawnPoints[spawnRandom];

        if (_cubes[spawnRandomCube].gameObject.activeSelf == false)
        {
            _cubes[spawnRandomCube].gameObject.SetActive(true);
            _cubes[spawnRandomCube].transform.position = tempSpawnPoint.position;
            _cubes[spawnRandomCube].ChangeLifeTimer(GetRandomTime());
        }
    }

    private void CreateCubes(int numbersOfCubes)
    {
        for (int i = 0; i < numbersOfCubes; i++)
        {
            _cubes.Add(Instantiate(_cube));
        }

        for (int i = 0; i < _cubes.Count; i++)
        {
            _cubes[i].GetPlatform(_platform);
        }
    }

    private IEnumerator GetCubes(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
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
