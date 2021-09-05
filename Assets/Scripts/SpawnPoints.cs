using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : BoardUnit
{
    [SerializeField]
    private Transform [] _points;

    public Transform GetRandomSpawnPoint()
    {
        return _points [Random.Range (0, _points.Length)];
    }

}

