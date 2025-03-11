using UnityEngine;

public class MoneySpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    public Transform[] Points => _points;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _points = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _points[i] = transform.GetChild(i);
    }
#endif
}