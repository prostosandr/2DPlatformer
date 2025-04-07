using System;

public class Money : LootParent
{
    public event Action<Money> Deactivated;

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}