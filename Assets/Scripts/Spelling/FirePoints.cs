using UnityEngine;

public class FirePoints : MonoBehaviour
{
    [SerializeField]
    private Transform [] _points;

    public Transform GetRandomPoint()
    {
        return _points [Random.Range (0, _points.Length)];
    }

    public void SetPointPosition( int index, Vector3 pos )
    {
        _points [index].position = pos;
    }

    public int GetPointsCount()
    {
        return _points.Length;
    }
}
