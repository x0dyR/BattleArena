using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action TookDamage;
    public event Action Died;

    private Health _health;

    private IMover _mover;

    private IOnTriggerEnterAction _onTriggerEnter;

    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }

    [field: SerializeField] public int MaxHealth { get; private set; }

    public bool IsInitialized { get; private set; }

    public void Initialize(IMover mover, IOnTriggerEnterAction onTriggerEnter, Health health)
    {
        _mover = mover;
        _onTriggerEnter = onTriggerEnter;

        _health = health;

        _health.TookDamage += OnTookDamage;
        _health.Died += OnDied;

        IsInitialized = true;
    }
    private void OnDisable()
    {
        _health.TookDamage -= OnTookDamage;
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }

    private void OnTookDamage()
    {
        TookDamage?.Invoke();
        Debug.Log("АЙАЙАЙАЙАЙА");
    }

    private void Update()
    {
        if (IsInitialized == false)
            throw new ArgumentNullException($"{name} is not Initialized");

        _mover.ProcessMove(Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        _onTriggerEnter.Action(other);
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
