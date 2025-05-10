using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Slider _instanceHealthSlider;
    [SerializeField] private Slider _smoothHealthSlider;
    [SerializeField] private float _smoothSpeed;

    private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Awake()
    {
        _currentHealth = _health.CurrentHealth;
        _maxHealth = _health.MaxHealth;
    }

    private void Start()
    {
        _instanceHealthSlider.maxValue = _maxHealth;
        _smoothHealthSlider.maxValue = _maxHealth;

        UpdateUI();
    }

    private void Update()
    {
        _currentHealth = _health.CurrentHealth;

        if (_smoothHealthSlider.value != _currentHealth)
            _smoothHealthSlider.value = Mathf.MoveTowards(_smoothHealthSlider.value, _currentHealth, _smoothSpeed * Time.deltaTime);

        UpdateUI();
    }

    private void UpdateUI()
    {
        _healthText.text = $"{_currentHealth} / {_maxHealth}";

        _instanceHealthSlider.value = _currentHealth;
    }
}
