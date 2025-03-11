using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public event Action<Money> Deactivated;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}