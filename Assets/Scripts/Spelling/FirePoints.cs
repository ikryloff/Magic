using UnityEngine;

public class FirePoints : MonoBehaviour
{
    [SerializeField]
    private Transform [] _points;

    public Transform GetRandomPoint()
    {
        return _points [Random.Range (0, _points.Length)];
    }
}
