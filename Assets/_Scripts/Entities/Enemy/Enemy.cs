using System;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action TookDamage;
    public event Action<IDamageable> Died;

    private Health _health;
    private IMover _mover;

    [SerializeField] private TriggerReciever _triggerReciever;
    private IOnTriggerEnterAction _onTriggerEnter;

    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public ObstacleChecker ObstacleChecker { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int CurrentHealth { get; private set; }
    [field: SerializeField] public int TimeToChangeDirection { get; private set; }

    public bool IsInitialized { get; private set; }

    private void Update()
    {
        if (IsInitialized == false)
            throw new ArgumentNullException($"{name} is not Initialized");

        if (_health.IsDead)
        {
            _mover.ProcessMove(Vector3.zero);
            return;
        }

        _mover.ProcessMove(Vector3.one);
    }

    private void OnDisable()
    {
        _health.CurrentHealth.Changed -= OnHealthChanged;
        _health.Died -= OnDied;
        _triggerReciever.Triggered -= OnTriggered;
    }

    public void Initialize(IMover mover, IOnTriggerEnterAction onTriggerEnter, Health health)
    {
        _mover = mover;
        _onTriggerEnter = onTriggerEnter;

        _health = health;

        _health.CurrentHealth.Changed += OnHealthChanged;
        _health.Died += OnDied;

        _triggerReciever.Triggered += OnTriggered;

        _triggerReciever.Initiazlie(CharacterController.radius);

        CurrentHealth = _health.CurrentHealth.Value;

        IsInitialized = new object[] { _mover, _onTriggerEnter, _health, _triggerReciever }.All(s => s != null);
    }

    private void OnTriggered(Collider other) => _onTriggerEnter.Action(other);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);

    private void OnDied()
    {
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnHealthChanged(int obj)
    {
        TookDamage?.Invoke();
        CurrentHealth = _health.CurrentHealth.Value;
    }
}
