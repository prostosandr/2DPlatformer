using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MoneySpawnPoints _moneySpawnPoints;
    [SerializeField] private MoneyPool _moneyPool;
    [SerializeField] private float _spawnTime;

    private Transform[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = _moneySpawnPoints.Points;
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        var wait = new WaitForSeconds(_spawnTime);

        while (enabled)
        {
            if (_moneyPool.NumberOMoney < _moneyPool.PoolCapacity)
                Spawn();

            yield return wait;
        }
    }

    private void Spawn()
    {
        int minNumber = 0;

        Transform spawnPoint = _spawnPoints[Random.Range(minNumber, _spawnPoints.Length)];
        Collider2D hit = Physics2D.OverlapPoint(new Vector2(spawnPoint.position.x, spawnPoint.position.y));

        if (hit == null)
        {
            Money money = _moneyPool.GetMoney();
            money.gameObject.SetActive(true);
            money.transform.position = spawnPoint.position;
        }
    }
}