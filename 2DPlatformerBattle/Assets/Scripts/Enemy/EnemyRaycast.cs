using System;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    [SerializeField] private Player _player;

    private bool _isPlayer;
    private float _distanceToPlayer;

    public bool IsPlayer => _isPlayer;
    public float DistanceToPlayer => _distanceToPlayer;
    public Player Player => _player;

    public event Action<Transform> CanSeePlayer;

    private void Update()
    {
        Vector2 rayDirection = _player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection);

        _distanceToPlayer = (_player.transform.position - transform.position).sqrMagnitude;

        if (hit.collider != null)
        {
            if (hit.transform == _player.transform)
            {
                _isPlayer = true;

                CanSeePlayer?.Invoke(_player.transform);
            }
            else
            {
                _isPlayer = false;
            }
        }
    }
}
