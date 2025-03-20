using UnityEngine;

[RequireComponent(typeof(GroundDetector), typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(StateAnimator))]
public class Player : MonoBehaviour
{
    private const int Zero = 0;

    private GroundDetector _groundDetector;
    private InputReader _inputReader;
    private PlayerMover _mover;
    private StateAnimator _stateAnimator;

    private void OnEnable()
    {
        _inputReader.IsJumping += Jump;
    }

    private void OnDisable()
    {
        _inputReader.IsJumping -= Jump;
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

    private void Jump(bool isJump)
    {
        _mover.Jump();
    }
}