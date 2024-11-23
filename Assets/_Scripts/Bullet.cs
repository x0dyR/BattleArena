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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
