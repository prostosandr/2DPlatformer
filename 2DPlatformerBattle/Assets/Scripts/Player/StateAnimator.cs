using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StateAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public enum States
    {
        Idle = 0,
        Walk = 1,
        Jump = 2
    }

    public void SetIdleAnimation()
    {
        SetAnimation((int)States.Idle);
    }

    public void SetWalkAnimation()
    {
        SetAnimation((int)States.Walk);
    }

    public void SetJumpAnimation()
    {
        SetAnimation((int)States.Jump);
    }

    private void SetAnimation(int value)
    {
        _animator.SetInteger(PlayerAnimatorData.Params.State, value);
    }
}