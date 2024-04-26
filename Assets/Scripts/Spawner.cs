using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Cube _cube;

    [SerializeField, Range(1, 10)] private int _numbersOfCubes;
}
