using System;
using UnityEngine;

public class PlayerLootCollector : MonoBehaviour
{
    public event Action<int> Healed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LootParent>(out _))
        {
            Component[] components = collision.gameObject.GetComponentsInChildren<Component>();

            foreach (Component component in components)
            {
                if (component is Money money)
                {
                    money.Deactivate();
                }
                else if (component is FirstAidKit firstAidKit)
                {
                    firstAidKit.Deactivate();
                    Healed?.Invoke(firstAidKit.ValueHeal);
                }
            }
        }
    }
}
