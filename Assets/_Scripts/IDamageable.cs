using System;

public interface IDamageable
{
    event Action TookDamage;
    event Action<IDamageable> Died;

    void TakeDamage(int damage);
}
