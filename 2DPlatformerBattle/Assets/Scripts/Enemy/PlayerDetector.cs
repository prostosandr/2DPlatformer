using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private int _minPlayerCollision;

    private Player _player;
    private int _playerCollisions;
    private bool _isPlayer;

    public event Action<Player> PlayerEntered;

    public bool IsPlayer => _isPlayer;

    private void Start()
    {
        _playerCollisions = _minPlayerCollision;
    }

    private void Update()
    {
        if (_playerCollisions > _minPlayerCollision && _player != null)
        {
            _isPlayer = true;
            PlayerEntered?.Invoke(_player);
        }
        else
        {
            _isPlayer = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out _player))
        {
            _playerCollisions++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out _player))
        {
            _playerCollisions--;

            if (_playerCollisions < _minPlayerCollision)
            {
                _playerCollisions = _minPlayerCollision;
            }
        }
    }
}
