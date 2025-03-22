using UnityEngine;

public class CombatParameters : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _maxHealth;

    public int Damage => _damage;

    private void Update()
    {
        if(_health == _minHealth)
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if(_health < _minHealth)
            _health = _minHealth;
    }

    public void Heal(int value)
    {
        _health += value;

        if(_health > _maxHealth)
            _health = _maxHealth;
    }
}
