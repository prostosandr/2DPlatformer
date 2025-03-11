using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const KeyCode W = KeyCode.W;

    private bool _isJump;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(W))
            _isJump = true;
    }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;

        return localValue;
    }
}
