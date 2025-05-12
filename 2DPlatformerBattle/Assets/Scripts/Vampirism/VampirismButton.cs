using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class VampirismButton : MonoBehaviour
{
    private Button _button;

    public event Action Clicked;

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
        Clicked?.Invoke();
    }
    
    public void EnableInteractable()
    {
        _button.interactable = true;
    }

    public void DisableInteractable()
    {
        _button.interactable = false;
    }
}
