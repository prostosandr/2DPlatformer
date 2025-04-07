using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocityX, _jumpForce);
    }

    public void Move(float direction)
    {
        _rigidbody.linearVelocity = new Vector2(_speedX * direction, _rigidbody.linearVelocityY);

        TurnTorwards(direction);
    }

    private void TurnTorwards(float direction)
    {
        Vector2 rightLook = new(1, 1);
        Vector2 leftLook = new(-1, 1);

        if (direction > 0)
            transform.localScale = rightLook;
        else if (direction < 0)
            transform.localScale = leftLook;
    }
}