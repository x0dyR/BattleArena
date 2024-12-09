using System;

public class ArenaCapturedCondition : ILooseCondition
{
    public event Action Lost;

    private EnemySpawner _spawner;

    private int _enemyCountToCapture;
    private int _currentCount = 0; 

    public ArenaCapturedCondition(EnemySpawner spawner, int enemyToCapture)
    {
        _spawner = spawner;
        _enemyCountToCapture = enemyToCapture;
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
