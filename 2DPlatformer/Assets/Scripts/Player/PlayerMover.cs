using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const int Zero = 0;

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
        int one = 1;

        if (direction > Zero)
            transform.localScale = new Vector2(one, one);
        else if (direction < Zero)
            transform.localScale = new Vector2(-one, one);
    }
}