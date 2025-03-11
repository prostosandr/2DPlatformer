using UnityEngine;

public class MoneyCollector : MonoBehaviour
{
    public Money money;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Money money))
            money.Deactivate();
    }
}
