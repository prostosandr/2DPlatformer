using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StateAnimator : MonoBehaviour
{
    private const string State = "State";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAnimation(int value)
    {
        _animator.SetInteger(State, value);
    }
}