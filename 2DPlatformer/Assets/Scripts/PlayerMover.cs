using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string ConstJump = nameof(Jump);
    private const string State = "State";

    [SerializeField] private float _speed;
    [SerializeField] private float _rayDistance;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    public enum States
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Fall = 3
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsGrounded())
            SetAnimator(States.Idle);

        if (IsGrounded() && Input.GetButton(Horizontal))
            Move();

        if (IsGrounded() && Input.GetButton(ConstJump))
            Jump();
        else if (IsGrounded() == false)
            SetAnimator(States.Fall);
    }

    private void Move()
    {
        int zero = 0;
        int one = 1;

        float directionX = Input.GetAxis(Horizontal);
        _rigidbody.linearVelocity = new Vector2(directionX * _speed, _rigidbody.linearVelocity.y);

        SetAnimator(States.Walk);

        if (directionX > zero)
            transform.localScale = new Vector2(one, one);
        else if (directionX < zero)
            transform.localScale = new Vector2(-one, one);
    }

    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocityX, _jumpForce);

        SetAnimator(States.Jump);
    }

    private bool IsGrounded()
    {
        bool isGrounded = false;
        RaycastHit2D hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, _rayDistance);

        if (hit.collider != null && hit.collider.TryGetComponent<Ground>(out _))
            isGrounded = true;

        return isGrounded;
    }

    private void SetAnimator(States state)
    {
        _animator.SetInteger(State, (int)state);
    }    
}