using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform [] _points;

    public Transform GetSpawnPoint( int pos)
    {
        return _points [pos - 1];
    }

    public void SetPointPosition( int index, Vector3 pos )
    {
        Debug.Log (index);
        _points [index].position = pos;
    }

}

