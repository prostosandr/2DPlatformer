using UnityEngine;

[RequireComponent(typeof(EnemyPatrolLogic), typeof(EnemyMover), typeof(PlayerDefiner))]
[RequireComponent(typeof(Damager), typeof(PlayerDetector), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _closeDistanceToPlayer;

    private PlayerDetector _playerDetector;
    private EnemyPatrolLogic _patrolLogic;
    private EnemyMover _enemyMover;
    private PlayerDefiner _enemyRaycast;
    private Health _health;
    private Damager _damager;
    private Player _player;

    private void Awake()
    {
        _patrolLogic = GetComponent<EnemyPatrolLogic>();
        _enemyMover = GetComponent<EnemyMover>();
        _enemyRaycast = GetComponent<PlayerDefiner>();
        _playerDetector = GetComponent<PlayerDetector>();
        _health = GetComponent<Health>();
        _damager = GetComponent<Damager>();
    }

    private void OnEnable()
    {
        _enemyRaycast.CanSeePlayer += MoveToPlayer;
        _playerDetector.PlayerEntered += DetectPlayer;
        _health.Destroyed += DeleteObject;
    }

    private void OnDisable()
    {
        _enemyRaycast.CanSeePlayer -= MoveToPlayer;
        _playerDetector.PlayerEntered -= DetectPlayer;
        _health.Destroyed -= DeleteObject;
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

    public void DeleteObject()
    {
        Destroy(gameObject);
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
            _health.TakeDamage(_player.Damage);

            _player.TakeDamage(_damager.Damage);
        }
    } 
}