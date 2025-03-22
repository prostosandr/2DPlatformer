using System;
using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private int _valueHeal;

    public int ValueHeal => _valueHeal;

    public event Action<FirstAidKit> Deactivated;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}