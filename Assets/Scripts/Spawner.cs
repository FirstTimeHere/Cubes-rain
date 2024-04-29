using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Cube _cube;

    [SerializeField] private float _waitTime = 0.1f;

    private void Start()
    {
        StartCoroutine(GetCubes(_waitTime));
    }

    private void Spawn()
    {
        int spawnRandom = Random.Range(0, _spawnPoints.Count);
        var tempSpawnPoint = _spawnPoints[spawnRandom];

        Cube cube = Instantiate(_cube);
        cube.transform.position = tempSpawnPoint.position;
    }

    private IEnumerator GetCubes(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (true)
        {
            Spawn();
            yield return wait;
        }
    }
}
