using System;
using UnityEngine;

public class PlayerLootCollector : MonoBehaviour
{
    public event Action<int> Healed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Money money))
        {
            money.Deactivate();
        }
        else if (collision.gameObject.TryGetComponent(out FirstAidKit firstAidKit))
        {
            firstAidKit.Deactivate();
            Healed?.Invoke(firstAidKit.ValueHeal);
        }
    }
}
