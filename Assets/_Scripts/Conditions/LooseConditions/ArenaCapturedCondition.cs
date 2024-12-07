using System;
using UnityEngine;

public class ArenaCapturedCondition : ILooseCondition
{
    public event Action Lost;

    private EnemySpawner _spawner;

    private int _enemyCountToCapture;
    private int _currentCount;

    public ArenaCapturedCondition(EnemySpawner spawner, int enemyToCapture)
    {
        _spawner = spawner;
        _enemyCountToCapture = enemyToCapture;

        _currentCount = 0;
    }

    public void Start() => _spawner.Enemies.Added += OnAdded;

    public void End() => _spawner.Enemies.Added -= OnAdded;

    private void OnAdded(Enemy enemy)
    {
        _currentCount++;
        enemy.Died += OnDied;

        if (_currentCount >= _enemyCountToCapture)
            Lost?.Invoke();
    }

    private void OnDied(IDamageable enemy)
    {
        _currentCount--;
        enemy.Died -= OnDied;
    }
}
