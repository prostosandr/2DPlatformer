using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;

    public int CurrentHealth => _health;
    public int MinHealth => _minHealth;
    public int MaxHealth => _maxHealth;

    public event Action Destroyed;

    public void TakeHeal(int value)
    {
        _health += value;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= _minHealth)
        {
            _health = _minHealth;
            Destroyed?.Invoke();
        }
    }
}
