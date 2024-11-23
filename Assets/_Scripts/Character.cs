using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Character : MonoBehaviour, IDamageable
{
    public event Action<Vector3> StartedMoving;

    public event Action TookDamage;
    public event Action Died;

    [SerializeField] private Collider _collider;

    private Health _health;

    private InputSystem _input;

    private Vector3 _inputDirection;
    private IMover _mover;

    private IShooter _shooter;

    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public Transform Muzzle { get; private set; }

    [field: SerializeField] public int Maxhealth { get; private set; }

    [field:SerializeField] public int CurrentHealth { get; private set; }

    public bool IsInitialized { get; private set; }

    public void Initialize(InputSystem input, IMover mover, IShooter shooter,Health health)
    {
        _input = input;
        _mover = mover;
        _shooter = shooter;

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
    }

    private void OnTookDamage()
    {
        TookDamage?.Invoke();
        CurrentHealth = _health.CurrentHealth;
        Debug.Log("АЙАЙАЙА");
    }

    private void Update()
    {
        if (IsInitialized == false)
            throw new ArgumentNullException("Character is not Initialized");

        _inputDirection = _input.ReadMoveDirection().normalized;

        _mover.ProcessMove(_inputDirection);

        if (_inputDirection.sqrMagnitude > Mathf.Deg2Rad * Mathf.Deg2Rad)
            StartedMoving?.Invoke(_inputDirection);


        if (_input.IsLeftMouseButtonDown())
            _shooter.Shoot();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
