using UnityEngine;
using UnityEngine.Pool;

public class MoneyPool : MonoBehaviour
{
    [SerializeField] private Money _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _numberOfMoney;

    private ObjectPool<Money> _pool;

    public int NumberOMoney => _numberOfMoney;
    public int PoolCapacity => _poolCapacity;

    private void Awake()
    {
        _pool = new ObjectPool<Money>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => SubscribeToMoney(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public Money GetMoney()
    {
        _numberOfMoney++;

        return _pool.Get();
    }

    private void SubscribeToMoney(Money money)
    {
        money.Deactivated += ReleaseMoney;
    }

    private void ReleaseMoney(Money money)
    {
        _numberOfMoney--;

        money.Deactivated -= ReleaseMoney;
        _pool.Release(money);
    }
}