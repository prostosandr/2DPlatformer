using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private HashSet<Enemy> _enemys;

    public event Action<HashSet<Enemy>> EnemysChanged;

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

            EnemysChanged?.Invoke(new HashSet<Enemy>(_enemys));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemys.Remove(enemy);

            EnemysChanged?.Invoke(new HashSet<Enemy>(_enemys));
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
