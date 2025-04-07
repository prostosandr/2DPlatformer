using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(StateAnimator), typeof(CombatParameters))]
public class Player : MonoBehaviour
{
    private const int Zero = 0;

    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private StateAnimator _stateAnimator;
    private CombatParameters _combatParameters;
    private PlayerLootCollector _playerLootCollector;

    public int Damage => _combatParameters.Damage;

    private void OnEnable()
    {
        _inputReader.IsJumping += Jump;
        _playerLootCollector.Healed += Heal;
    }

    private void OnDisable()
    {
        _inputReader.IsJumping -= Jump;
        _playerLootCollector.Healed -= Heal;
    }

    public enum States
    {
        Idle = 0,
        Walk = 1,
        Jump = 2
    }

    private void Awake()
    {
        _groundDetector = GetComponent<GroundDetector>();
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _stateAnimator = GetComponent<StateAnimator>();
        _combatParameters = GetComponent<CombatParameters>();
        _playerLootCollector = GetComponent<PlayerLootCollector>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != Zero)
        {
            _mover.Move(_inputReader.Direction);
            _stateAnimator.SetAnimation((int)States.Walk);
        }

        if (_groundDetector.IsGround && _inputReader.Direction == Zero)
            _stateAnimator.SetAnimation((int)States.Idle);

        if (_groundDetector.IsGround == false)
            _stateAnimator.SetAnimation((int)States.Jump);
    }

    public void TakeDamage(int damage)
    {
        _combatParameters.TakeDamage(damage);
    }

    public void Heal(int value)
    {
        _combatParameters.TakeHeal(value);
    }

    private void Jump(bool isJump)
    {
        if (_groundDetector.IsGround)
            _mover.Jump();
    }
}