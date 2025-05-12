using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AbilityBar : MonoBehaviour
{
    private Slider _bar;

    public float Value => _bar.value;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }

    public void SetCurrentValue(float value)
    {
        _bar.value = value;
    }
}
