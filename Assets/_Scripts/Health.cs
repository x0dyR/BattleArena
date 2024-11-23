using System;

public class Health
{
    public event Action TookDamage;
    public event Action Died;

    private int _maxHealth;

    private int _currentHealth;

    public Health(int maxHealth, int currentHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = currentHealth;
    }

    public int MaxHealth => _maxHealth;

    public int CurrentHealth => _currentHealth;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException("Damage below zero");

        _currentHealth -= damage;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Died?.Invoke();
        }

        TookDamage?.Invoke();
    }
}
