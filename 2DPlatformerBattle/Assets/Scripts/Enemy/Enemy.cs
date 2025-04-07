using UnityEngine;

[RequireComponent(typeof(EnemyPatrolLogic), typeof(EnemyMover), typeof(EnemyRaycast))]
[RequireComponent(typeof(CombatParameters), typeof(PlayerDetector))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _closeDistanceToPlayer;

    private PlayerDetector _playerDetector;
    private EnemyPatrolLogic _patrolLogic;
    private EnemyMover _enemyMover;
    private EnemyRaycast _enemyRaycast;
    private CombatParameters _combatParameters;
    private Player _player;

    private void Awake()
    {
        _patrolLogic = GetComponent<EnemyPatrolLogic>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyRaycast = GetComponent<EnemyRaycast>();
        _combatParameters = GetComponent<CombatParameters>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _enemyRaycast.CanSeePlayer += MoveToPlayer;
        _playerDetector.PlayerEntered += DetectPlayer;
    }

    private void OnDisable()
    {
        _enemyRaycast.CanSeePlayer -= MoveToPlayer;
        _playerDetector.PlayerEntered -= DetectPlayer;
    }

    private void Update()
    {
        if (_patrolLogic.DistanceToNextPoint > _patrolLogic.CloseDistance && _playerDetector.IsPlayer == false)
            _enemyMover.Move(_patrolLogic.NextPoint);
        else if (_playerDetector.IsPlayer == false)
            _patrolLogic.ReceiveNextPoint();

        if (_enemyRaycast.DistanceToPlayer <= _closeDistanceToPlayer)
            Battle();
    }

    private void DetectPlayer(Player player)
    {
        _player = player;

        _enemyRaycast.DetectPlayer(_player);
    }

    private void MoveToPlayer(Transform playerTransform)
    {
        _enemyMover.Move(playerTransform.position);
    }

    private void Battle()
    {
        if (_player != null)
        {
            _combatParameters.TakeDamage(_player.Damage);

            _player.TakeDamage(_combatParameters.Damage);
        }
    }
}