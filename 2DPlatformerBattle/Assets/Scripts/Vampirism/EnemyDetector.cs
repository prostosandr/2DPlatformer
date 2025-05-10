using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private HashSet<Enemy> _enemys;

    public event Action<HashSet<Enemy>> IsEnemy;

    private void Awake()
    {
        gameObject.SetActive(false);

        _enemys = new HashSet<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemys.Add(enemy);

            IsEnemy?.Invoke(_enemys);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemys.Remove(enemy);

            IsEnemy?.Invoke(_enemys);
        }
    }

    public void SetPosition(Transform position)
    {
        transform.position = position.position;
    }
}
