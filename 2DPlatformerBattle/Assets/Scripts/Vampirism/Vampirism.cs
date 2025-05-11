using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private VampirismButton _abilityButton;
    [SerializeField] private SmoothAbilityBar _bar;
    [SerializeField] private int _timeAbility;
    [SerializeField] private int _damage;

    private Coroutine _coroutineActivated;
    private Coroutine _coroutineDeactivated;
    private HashSet<Enemy> _enemys;

    public event Action<int> Vampirized;

    private void Awake()
    {
        _bar.SetMaxValue(_timeAbility);

        _enemys = new HashSet<Enemy>();
    }

    private void OnEnable()
    {
        _abilityButton.OnClicked += StartAbility;
        _enemyDetector.EnemysChanged += SetEnemys;
    }

    private void OnDisable()
    {
        _abilityButton.OnClicked -= StartAbility;
        _enemyDetector.EnemysChanged -= SetEnemys;
    }

    public void SetPositionEnemyDetector(Vector3 position)
    {
        _enemyDetector.SetPosition(position);
    }

    private void StartAbility()
    {
        _enemys.Clear();

        _enemyDetector.gameObject.SetActive(true);
        _abilityButton.SetInteractable(false);

        _coroutineActivated = StartCoroutine(Vampirize());
    }

    private IEnumerator Vampirize()
    {
        int oneSecond = 1;
        var wait = new WaitForSeconds(oneSecond);

        for (int i = _timeAbility; i >= 0; i--)
        {
            _bar.UpdateDrawing(i);

            DrinkBlood();

            yield return wait;
        }

        _enemyDetector.gameObject.SetActive(false);

        _coroutineDeactivated = StartCoroutine(ReloadVampirism());

        StopCoroutine(_coroutineActivated);
    }

    private IEnumerator ReloadVampirism()
    {
        int oneSecond = 1;
        var wait = new WaitForSeconds(oneSecond);

        for (int i = 0; i <= _timeAbility; i++)
        {
            _bar.UpdateDrawing(i);

            yield return wait;
        }

        _abilityButton.SetInteractable(true);

        StopCoroutine(_coroutineDeactivated);
    }

    private void SetEnemys(HashSet<Enemy> enemys)
    {
        _enemys = enemys;
    }

    private void DrinkBlood()
    {
        Enemy nearestEnemy = GetNearestEnemy();

        if (nearestEnemy != null)
        {
            nearestEnemy.TakeDamage(_damage);
            Vampirized?.Invoke(_damage);
        }
    }

    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;

        float minDistance = Mathf.Infinity;

        foreach (Enemy enemy in _enemys)
        {
            if (enemy == null)
                continue;

            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }
}

