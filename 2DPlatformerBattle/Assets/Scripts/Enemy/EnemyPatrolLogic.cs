using UnityEngine;

public class EnemyPatrolLogic : MonoBehaviour
{
    [SerializeField] private EnemyPathPoints _enemyPathPoints;
    [SerializeField] private Transform[] _currentPathPoints;
    [SerializeField] private float _closeDistance;
    [SerializeField] private int _pointNumber;

    private Vector3 _nextPoint;

    public float CloseDistance => _closeDistance;
    public Vector3 NextPoint => _nextPoint;
    public float DistanceToNextPoint => (_nextPoint - transform.position).sqrMagnitude;

    private void Awake()
    {
        _nextPoint = _currentPathPoints[_pointNumber].transform.position;
        transform.position = _nextPoint;
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _enemyPathPoints.transform.childCount;
        _currentPathPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _currentPathPoints[i] = _enemyPathPoints.transform.GetChild(i);
    }
#endif

    public void ReceiveNextPoint()
    {
        _nextPoint = _currentPathPoints[_pointNumber++ % _currentPathPoints.Length].transform.position;
    }
}