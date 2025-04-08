using System;
using UnityEngine;

public class FirstAidKit : ILootParent
{
    [SerializeField] private int _valueHeal;

    public event Action<FirstAidKit> Deactivated;

    public int ValueHeal => _valueHeal;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}