using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private LootSpawnPoints _moneySpawnPoints;
    [SerializeField] private MoneyPool _moneyPool;
    [SerializeField] private FirstAidKitsPool _firstAidKitsPool;
    [SerializeField] private float _spawnTime;

    private Transform[] _spawnPoints;

    private void Awake()
    {
        _spawnPoints = _moneySpawnPoints.GetPoints();
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
                SpawnMoney();
            else if (_firstAidKitsPool.NumberOfFirstAidKit < _firstAidKitsPool.PoolCapacity)
                SpawnFirstAidKit();

            yield return wait;
        }
    }

    private void SpawnMoney()
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

    private void SpawnFirstAidKit()
    {
        int minNumber = 0;

        Transform spawnPoint = _spawnPoints[Random.Range(minNumber, _spawnPoints.Length)];
        Collider2D hit = Physics2D.OverlapPoint(new Vector2(spawnPoint.position.x, spawnPoint.position.y));

        if (hit == null)
        {
            FirstAidKit firstAidKit = _firstAidKitsPool.GetFirstAidKit();
            firstAidKit.gameObject.SetActive(true);
            firstAidKit.transform.position = spawnPoint.position;
        }
    }
}