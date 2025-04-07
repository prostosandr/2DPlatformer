using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode Jump = KeyCode.W;

    public float Direction { get; private set; }

    public event Action<bool> IsJumping;

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(Jump))
            IsJumping?.Invoke(true);
    }
}
