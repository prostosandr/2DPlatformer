using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;

    public int CurrentHealth => _health;
    public int MaxHealth => _maxHealth;

    public event Action Destroyed;
    public event Action<float> HealthChanged;

    public void TakeHeal(int value)
    {
        if (value >= 0)
        {
            _health += value;

            if (_health > _maxHealth)
                _health = _maxHealth;

            HealthChanged?.Invoke(_health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage >= 0)
        {
            _health -= damage;

            if (_health < _minHealth)
            {
                _health = _minHealth;
                Destroyed?.Invoke();
            }

            HealthChanged?.Invoke(_health);
        }
    }
}
