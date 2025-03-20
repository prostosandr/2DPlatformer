using UnityEngine;

public class PlayerMoneyCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Money money))
            money.Deactivate();
    }
}
