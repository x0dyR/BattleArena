using System;

public class KillEnemyCondition : IWinCondition
{
    public event Action Won;

    private ObservableList<Enemy> _enemies = new();

    private int _killToWin = 0;
    private int _diedCount = 0;

    public KillEnemyCondition(ObservableList<Enemy> enemies, int killToWin)
    {
        _enemies = enemies;
        _killToWin = killToWin;
    }

    public void Start() => _enemies.Added += OnAdded;

    public void End() => _enemies.Added -= OnAdded;

    private void OnAdded(Enemy enemy) => enemy.Died += OnDied;

    private void OnDied(IDamageable damageable)
    {   
        _diedCount++;
        damageable.Died -= OnDied;

        if (_diedCount >= _killToWin)
            Won?.Invoke();
    }
}
