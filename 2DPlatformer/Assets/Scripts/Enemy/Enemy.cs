using UnityEngine;

[RequireComponent(typeof(PatrolLogic), typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    private PatrolLogic _patrolLogic;
    private EnemyMover _enemyMover;

    private void Awake()
    {
        _patrolLogic = GetComponent<PatrolLogic>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void Update()
    {
        if (_patrolLogic.DistanceToNextPoint > _patrolLogic.CloseDistance)
            _enemyMover.Move(_patrolLogic.NextPoint);
        else
            _patrolLogic.ReceiveNextPoint();
    }
}