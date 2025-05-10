using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothAbilityBar : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;

    private Slider _slider;
    private Coroutine _coroutine;
    private float _maxValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = _maxValue;
    }

    public void UpdateDrawing(float currentValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValueSmooth(currentValue));
    }

    private IEnumerator ChangeValueSmooth(float currentValue)
    {
        float currentSliderValue = _slider.value;

        float elapsed = 0f;

        while (Mathf.Approximately(_slider.value, currentValue) == false)
        {
            elapsed += Time.deltaTime;

            float thridParameter = Mathf.Clamp01(elapsed / _smoothDuration);

            _slider.value = Mathf.Lerp(currentSliderValue, currentValue / _maxValue, thridParameter);

            yield return null;
        }

        _coroutine = null;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }
}
