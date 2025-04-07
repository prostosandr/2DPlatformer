using System;
using UnityEngine;

public class EnemyRaycast : MonoBehaviour
{
    private float _distanceToPlayer;

    public event Action<Transform> CanSeePlayer;

    public float DistanceToPlayer => _distanceToPlayer;

    public void DetectPlayer(Player player)
    {
        Vector2 rayDirection = player.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection);

        _distanceToPlayer = (player.transform.position - transform.position).sqrMagnitude;

        if (hit.collider != null)
        {
            if (hit.transform == player.transform)
            {
                CanSeePlayer?.Invoke(player.transform);
            }
        }
    }
}
