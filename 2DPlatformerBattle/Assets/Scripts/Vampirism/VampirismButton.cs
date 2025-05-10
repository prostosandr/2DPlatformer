using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VampirismButton : MonoBehaviour
{
    private Button _button;

    public event Action OnClicked;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        OnClicked?.Invoke();
    }
    
    public void SetInteractable(bool IsInteracted)
    {
        _button.interactable = IsInteracted;
    }
}
