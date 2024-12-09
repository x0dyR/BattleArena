using System;

public class Health
{
    public event Action Died;

    private ReactiveValue<int> _maxHealth;
    private ReactiveValue<int> _currentHealth;

    public Health(int maxHealth, int currentHealth)
    {
        _maxHealth = new ReactiveValue<int>(maxHealth);
        _currentHealth = new ReactiveValue<int>(currentHealth);
    }

    public IReadOnlyReactiveVlue<int> MaxHealth => _maxHealth;

    public IReadOnlyReactiveVlue<int> CurrentHealth => _currentHealth;

    public bool IsDead => _currentHealth.Value < 0;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException("Damage below zero");

        _currentHealth.Value -= damage;

        if (_currentHealth.Value < 0)
        {
            _currentHealth.Value = 0;
            Died?.Invoke();
        }
    }
}
