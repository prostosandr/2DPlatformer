using UnityEngine;

public class PatrolLogic : MonoBehaviour
{
    [SerializeField] private EnemyPathPoints _enemyPathPoints;
    [SerializeField] private Transform[] _currentPathPoints;
    [SerializeField] private float _closeDistance;
    [SerializeField] private int _pointNumber;

    private Vector2 _nextPoint;

    public float CloseDistance => _closeDistance;
    public Vector2 NextPoint => _nextPoint;
    public float Distance => Vector2.Distance(transform.position, _nextPoint);

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
        int minNubmer = 0;
        int one = 1;

        if (_pointNumber < _currentPathPoints.Length - one)
            _pointNumber++;
        else
            _pointNumber = minNubmer;

        _nextPoint = _currentPathPoints[_pointNumber].transform.position;
    }
}