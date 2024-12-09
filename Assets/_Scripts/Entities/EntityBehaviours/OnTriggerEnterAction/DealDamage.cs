using UnityEngine;

public class DealDamage : IOnTriggerEnterAction
{
    private int _damage;

    public DealDamage(int damage)
    {
        _damage = damage;
    }

    public void Action(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable) && (collider.TryGetComponent(out Enemy _) == false))
            damageable.TakeDamage(_damage);
    }
}
