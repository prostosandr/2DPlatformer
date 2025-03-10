using System.Collections;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private EnemyPoints _enemyPoints;
    [SerializeField] private Transform[] _pathPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _closeDistance;
    [SerializeField] private int _pointNumber;

    private float _distance;
    private Vector3 _nextPoint;

    private void Start()
    {
        _nextPoint = _pathPoints[_pointNumber].transform.position;
        transform.position = _nextPoint;

        StartCoroutine(Moving());
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = _enemyPoints.transform.childCount;
        _pathPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _pathPoints[i] = _enemyPoints.transform.GetChild(i);
    }
#endif

    private IEnumerator Moving()
    {
        while (enabled)
        {
            _distance = (_nextPoint - transform.position).magnitude;

            if (_distance > _closeDistance)
                Move();
            else
                _nextPoint = GetNextPosition();

            yield return null;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPoint, _speed * Time.deltaTime);
    }

    private Vector3 GetNextPosition()
    {
        int minNubmer = 0;
        int one = 1;

        if (_pointNumber < _pathPoints.Length - one)
            _pointNumber++;
        else
            _pointNumber = minNubmer;

        return _pathPoints[_pointNumber].transform.position;
    }
}