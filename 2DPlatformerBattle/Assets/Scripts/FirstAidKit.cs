using System;
using UnityEngine;

public class FirstAidKit : LootParent
{
    [SerializeField] private int _valueHeal;

    public event Action<FirstAidKit> Deactivated;

    public int ValueHeal => _valueHeal;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}