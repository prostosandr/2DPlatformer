using System;
using System.Collections;
using UnityEngine;

public class RendererAbilityBar : MonoBehaviour
{
    [SerializeField] private float _smoothDuration;

    private Coroutine _coroutine;

    public event Action<float> Rendered;

    public void UpdateValue(float value, float sliderValue, float maxValue)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValueSmooth(value, sliderValue, maxValue));
    }

    private IEnumerator ChangeValueSmooth(float currentValue, float sliderValue, float maxValue)
    {
        float elapsed = 0f;

        float currentSliderValue = sliderValue;

        while (Mathf.Approximately(sliderValue, currentValue) == false)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / _smoothDuration);

            sliderValue = Mathf.Lerp(currentSliderValue, currentValue / maxValue, progress);

            Rendered?.Invoke(sliderValue);

            yield return null;
        }

        _coroutine = null;
    }
}
