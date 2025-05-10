using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonHealPlayer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private int _valueHeal;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(DealHeal);

        _buttonText.text = $"Вылечить {_valueHeal} здоровья";
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(DealHeal);
    }

    private void DealHeal()
    {
        _health.TakeHeal(_valueHeal);
    }
}
