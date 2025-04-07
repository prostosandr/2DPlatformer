using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private int _minGroundCollisions;

    private int _groundCollisions;

    public bool IsGround => _groundCollisions > _minGroundCollisions;

    private void Start()
    {
        _groundCollisions = _minGroundCollisions;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
            _groundCollisions++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _groundCollisions--;

            if (_groundCollisions < _minGroundCollisions)
            {
                _groundCollisions = _minGroundCollisions;
            }
        }
    }
}
