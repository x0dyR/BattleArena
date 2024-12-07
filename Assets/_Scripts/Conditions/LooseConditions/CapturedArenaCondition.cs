using System;

public class CapturedArenaCondition : ILooseCondition
{
    public event Action Lost;

    private EnemySpawner _spawner;

    private int _enemyCountToCapture = 0;
    private int _currentCount = 0;

    public CapturedArenaCondition(EnemySpawner spawner, int enemyToCapture)
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
        enemy.Died -= OnDied;
        _currentCount--;
    }
}
