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

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _mover.Jump();

        if (_groundDetector.IsGround == false)
            _stateAnimator.SetAnimation((int)States.Jump);
    }
}