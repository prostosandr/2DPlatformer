using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(StateAnimator), typeof(Health), typeof(Damager))]
[RequireComponent(typeof(Vampirism))]
public class Player : MonoBehaviour
{
    private const int Zero = 0;

    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private StateAnimator _stateAnimator;
    private PlayerLootCollector _playerLootCollector;
    private Health _health;
    private Damager _damager;
    private Vampirism _vampirism;

    public int Damage => _damager.Damage;

    private void OnEnable()
    {
        _inputReader.IsJumping += Jump;
        _playerLootCollector.Healed += TakeHeal;
        _health.Destroyed += DeleteObject;
        _vampirism.Vampirized += TakeHeal;
    }

    private void OnDisable()
    {
        _inputReader.IsJumping -= Jump;
        _playerLootCollector.Healed -= TakeHeal;
        _health.Destroyed -= DeleteObject;
        _vampirism.Vampirized -= TakeHeal;
    }

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _stateAnimator = GetComponent<StateAnimator>();
        _playerLootCollector = GetComponent<PlayerLootCollector>();
        _health = GetComponent<Health>();
        _damager = GetComponent<Damager>();
        _vampirism = GetComponent<Vampirism>();
    }

    private void Update()
    {
        _vampirism.SetPositionEnemyDetector(transform);
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != Zero)
        {
            _mover.Move(_inputReader.Direction);
            _stateAnimator.SetWalkAnimation();
        }

        if (_groundDetector.IsGround && _inputReader.Direction == Zero)
            _stateAnimator.SetIdleAnimation();

        if (_groundDetector.IsGround == false)
            _stateAnimator.SetJumpAnimation();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void TakeHeal(int value)
    {
        _health.TakeHeal(value);
    }

    private void DeleteObject()
    {
        Destroy(gameObject);
    }

    private void Jump(bool isJump)
    {
        if (_groundDetector.IsGround)
            _mover.Jump();
    }   
}