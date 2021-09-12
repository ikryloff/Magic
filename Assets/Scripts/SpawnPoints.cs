using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform [] _points;

    public Transform GetRandomSpawnPoint()
    {
        return _points [Random.Range (0, _points.Length)];
    }

    public void SetPointPosition( int index, Vector3 pos )
    {
        _points [index].position = pos;
    }

}

