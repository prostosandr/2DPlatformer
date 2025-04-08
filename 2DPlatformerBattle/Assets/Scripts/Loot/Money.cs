using System;

public class Money : ILootParent
{
    public event Action<Money> Deactivated;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}