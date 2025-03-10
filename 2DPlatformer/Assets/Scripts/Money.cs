using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action<Money> Deactivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerMover>(out _))
            Deactivated?.Invoke(this);
    }
}