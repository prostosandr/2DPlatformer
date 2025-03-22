using UnityEngine;

[RequireComponent(typeof(EnemyPatrolLogic), typeof(EnemyMover), typeof(EnemyRaycast))]
[RequireComponent(typeof(CombatParameters))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _closeDistanceToPlayer;

    private EnemyPatrolLogic _patrolLogic;
    private EnemyMover _enemyMover;
    private EnemyRaycast _enemyRaycast;
    private CombatParameters _combatParameters;

    private void Awake()
    {
        _patrolLogic = GetComponent<EnemyPatrolLogic>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyRaycast = GetComponent<EnemyRaycast>();
        _combatParameters = GetComponent<CombatParameters>();
    }

    private void OnEnable()
    {
        _enemyRaycast.CanSeePlayer += MoveToPlayer;
    }

    private void OnDisable()
    {
        _enemyRaycast.CanSeePlayer -= MoveToPlayer;
    }

    private void Update()
    {
        if (_patrolLogic.DistanceToNextPoint > _patrolLogic.CloseDistance && _enemyRaycast.IsPlayer == false)
            _enemyMover.Move(_patrolLogic.NextPoint);
        else if (_enemyRaycast.IsPlayer == false)
            _patrolLogic.ReceiveNextPoint();

        if (_enemyRaycast.DistanceToPlayer <= _closeDistanceToPlayer)
            Battle();
    }

    private void MoveToPlayer(Transform playerTransform)
    {
        _enemyMover.Move(playerTransform.position);
    }

    private void Battle()
    {
        _combatParameters.TakeDamage(_enemyRaycast.Player.Damage);

        _enemyRaycast.Player.TakeDamage(_combatParameters.Damage);
    }
}