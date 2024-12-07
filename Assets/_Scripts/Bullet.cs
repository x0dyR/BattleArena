using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private int _damage;

    public void Launch(Vector3 direction)
    {
        _rigidbody.AddForce(direction, ForceMode.Impulse);
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IDamageable damageable) && collider.TryGetComponent(out Character _) == false)
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}