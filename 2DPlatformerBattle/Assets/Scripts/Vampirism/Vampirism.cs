using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private VampirismButton _abilityButton;
    [SerializeField] private AbilityBar _abilityBar;
    [SerializeField] private RendererAbilityBar _rendererBar;

    [SerializeField] private int _timeAbility;
    [SerializeField] private int _damage;

    private HashSet<Enemy> _enemys;

    public event Action<int> Vampirized;

    private void Awake()
    {
        _enemys = new HashSet<Enemy>();
    }

    private void Start()
    {
        _abilityBar.SetCurrentValue(_timeAbility);
    }

    private void OnEnable()
    {
        _abilityButton.Clicked += StartAbility;
        _enemyDetector.EnemysChanged += SetEnemys;
        _rendererBar.Rendered += SetAbilityBarValue;
    }

    private void OnDisable()
    {
        _abilityButton.Clicked -= StartAbility;
        _enemyDetector.EnemysChanged -= SetEnemys;
        _rendererBar.Rendered -= SetAbilityBarValue;
    }

    public void SetPositionEnemyDetector(Vector3 position)
    {
        _enemyDetector.SetPosition(position);
    }

    private void SetAbilityBarValue(float value)
    {
        _abilityBar.SetCurrentValue(value);
    }

    private void StartAbility()
    {
        _enemys.Clear();

        _enemyDetector.gameObject.SetActive(true);
        _abilityButton.DisableInteractable();

        StartCoroutine(Vampirize());
    }

    private IEnumerator Vampirize()
    {
        int oneSecond = 1;
        var wait = new WaitForSeconds(oneSecond);

        for (int i = _timeAbility; i >= 0; i--)
        {
            _rendererBar.UpdateValue(i, _abilityBar.Value, _timeAbility);

            DrinkBlood();

            yield return wait;
        }

        _enemyDetector.gameObject.SetActive(false);

        StartCoroutine(ReloadVampirism(wait));
    }

    private IEnumerator ReloadVampirism(WaitForSeconds wait)
    {
        for (int i = 0; i <= _timeAbility; i++)
        {
            _rendererBar.UpdateValue(i, _abilityBar.Value, _timeAbility);

            yield return wait;
        }

        _abilityButton.EnableInteractable();
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
            nearestEnemy.TakeDamage(GetCurrentDamage(nearestEnemy.CurrentHealth));
            Vampirized?.Invoke(GetCurrentDamage(nearestEnemy.CurrentHealth));
        }
    }

    private int GetCurrentDamage(int enemyCurrentHealth)
    {
        int newDamage = _damage;

        if (enemyCurrentHealth < _damage)
        {
            newDamage = enemyCurrentHealth;
        }

        return newDamage;
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

