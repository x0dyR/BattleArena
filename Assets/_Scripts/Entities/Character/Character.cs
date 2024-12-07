using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour, IDamageable
{
    public event Action TookDamage;
    public event Action<IDamageable> Died;

    private Health _health;

    private InputSystem _input;

    private Vector3 _inputDirection;

    private IShooter _shooter;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public ObstacleChecker ObstacleChecker { get; private set; }

    [field: SerializeField] public float Speed { get; private set; }

    [field: SerializeField] public Transform Muzzle { get; private set; }

    [field: SerializeField] public int Maxhealth { get; private set; }

    [field: SerializeField] public int CurrentHealth { get; private set; }

    public IMover Mover { get; private set; }

    public bool IsInitialized { get; private set; }

    private void Update()
    {
        if (IsInitialized == false)
            throw new ArgumentNullException("Character is not Initialized");

        if (_health.IsDead)
            return;

        _inputDirection = _input.ReadMoveDirection().normalized;

        Mover.ProcessMove(_inputDirection);

        if (_input.IsLeftMouseButtonDown())
            _shooter.Shoot();
    }

    private void OnDisable()
    {
        _health.CurrentHealth.Changed -= OnTookDamage;
        _health.Died -= OnDied;
    }

    public void Initialize(InputSystem input, IMover mover, IShooter shooter, Health health)
    {
        _input = input;
        Mover = mover;
        _shooter = shooter;
        _health = health;

        CurrentHealth = _health.CurrentHealth.Value;

        _health.CurrentHealth.Changed += OnTookDamage;
        _health.Died += OnDied;

        IsInitialized = new object[] { _input, Mover, _shooter, _health }.All(s => s != null);
    }
    
    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    private void OnDied()
    {
        Died?.Invoke(this);

        gameObject.SetActive(false);
    }

    private void OnTookDamage(int obj)
    {
        TookDamage?.Invoke();
        CurrentHealth = _health.CurrentHealth.Value;
    }
}
