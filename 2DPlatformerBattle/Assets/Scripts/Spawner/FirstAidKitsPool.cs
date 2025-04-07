using UnityEngine;
using UnityEngine.Pool;

public class FirstAidKitsPool : MonoBehaviour
{
    [SerializeField] private FirstAidKit _prefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private int _numberOfFirstAidKits;

    private ObjectPool<FirstAidKit> _pool;

    public int NumberOfFirstAidKit => _numberOfFirstAidKits;
    public int PoolCapacity => _poolCapacity;

    private void Awake()
    {
        _pool = new ObjectPool<FirstAidKit>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (enemy) => SubscribeToFirstAidKit(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public FirstAidKit GetFirstAidKit()
    {
        _numberOfFirstAidKits++;

        return _pool.Get();
    }

    private void SubscribeToFirstAidKit(FirstAidKit firstAidKit)
    {
        firstAidKit.Deactivated += ReleaseMoney;
    }

    private void ReleaseMoney(FirstAidKit firstAidKit)
    {
        _numberOfFirstAidKits--;

        firstAidKit.Deactivated -= ReleaseMoney;
        _pool.Release(firstAidKit);
    }
}