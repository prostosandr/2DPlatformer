using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : HealthBar
{
    [SerializeField] private float _smoothDuration;

    private Slider _slider;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public override void UpdateDrawing(float currentValue)
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

            _slider.value = Mathf.Lerp(currentSliderValue, currentValue / MaxValue, thridParameter);

            yield return null;
        }

        _coroutine = null;
    }
}