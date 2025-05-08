using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDamagePlayer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private int _damage;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(DealDamage);

        _buttonText.text = $"Нанести {_damage} урона";
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(DealDamage);
    }

    private void DealDamage()
    {
        _health.TakeDamage(_damage);
    }
}
