using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}
