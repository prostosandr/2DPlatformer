using Unity.VisualScripting;
using UnityEngine;

public abstract class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _offset;

    protected float MaxValue { get; private set; }

    private void Start()
    {
        MaxValue = _health.MaxHealth;

        transform.position = (Vector2)_target.position + _offset;

        UpdateDrawing(_health.CurrentHealth);
    }

    private void Update()
    {
        transform.position = (Vector2)_target.position + _offset;
    }

    private void OnEnable()
    {
        _health.HealthChanged += UpdateDrawing;
        _health.Destroyed += DeleteObject;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= UpdateDrawing;
        _health.Destroyed -= DeleteObject;
    }

    public abstract void UpdateDrawing(float currentValue);

    private void DeleteObject()
    {
        Destroy(gameObject);
    }
}
