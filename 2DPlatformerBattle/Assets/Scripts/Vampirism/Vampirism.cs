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
    private Enemy _currentEnemy;
    private float _minDistance;

    public event Action<int> Vampirized;

    private void Awake()
    {
        _bar.SetMaxValue(_timeAbility);

        _enemys = new HashSet<Enemy>();
        _minDistance = 100000000f;
    }

    private void OnEnable()
    {
        _abilityButton.OnClicked += StartAbility;
        _enemyDetector.IsEnemy += SetEnemys;
    }

    private void OnDisable()
    {
        _abilityButton.OnClicked -= StartAbility;
        _enemyDetector.IsEnemy -= SetEnemys;
    }

    public void SetPositionEnemyDetector(Transform position)
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

        _minDistance = 1000000000000f;

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
        if (_enemys.Count != 0)
        {
            foreach (Enemy enemy in _enemys)
            {
                float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (currentDistance < _minDistance)
                {
                    _minDistance = currentDistance;
                    _currentEnemy = enemy;
                }

                if (enemy == _currentEnemy)
                {
                    enemy.TakeDamage(_damage);
                    Vampirized?.Invoke(_damage);

                    if (enemy == null)
                        break;
                }
            }
        }
    }
}

