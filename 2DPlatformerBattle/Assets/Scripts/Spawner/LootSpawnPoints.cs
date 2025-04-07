using UnityEngine;
using System.Linq;

public class LootSpawnPoints : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    public Transform[] GetPoints()
    {
        return _points.ToArray();
    }

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