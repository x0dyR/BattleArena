using Cinemachine;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Transform _charcaterSpawnPoint;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Character _characterPrefab;

    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Enemy _enemmyPrefab;

    [SerializeField] private Transform _enemySpawnPoint;

    private Character _characterInstance;

    private InputSystem _input;

    private IMover _characterMover;

    private IShooter _characterShooter;

    private Health _characterHealth;

    private IMover _enemyMover;

    private IOnTriggerEnterAction _onTriggerEnterAction;

    private Health _enemyHealth;

    private void Awake()
    {
        _input = new InputSystem();

        if (TryGetCharacter(out _characterInstance))
            BindCameraTo(_characterInstance);

        TryGetEnemy(out Enemy _);
    }

    private bool TryGetCharacter(out Character character)
    {
        character = Instantiate(_characterPrefab, _charcaterSpawnPoint.position, Quaternion.identity, null);

        _characterMover = new TransformMover(character.transform, character.Speed);
        _characterShooter = new BulletShooter(_bulletPrefab, character.Muzzle, 10);

        _characterHealth = new Health(character.Maxhealth, character.Maxhealth);

        character.Initialize(_input, _characterMover, _characterShooter, _characterHealth);

        if (character.IsInitialized)
            return true;
        else
            return false;
    }

    private void BindCameraTo(Character character)
    {
        _virtualCamera.Follow = character.transform;
    }

    private bool TryGetEnemy(out Enemy enemy)
    {
        enemy = Instantiate(_enemmyPrefab, _enemySpawnPoint.position, Quaternion.identity, null);

        _enemyMover = new RandomDirectionMover(this,enemy.transform, enemy.Speed,1);

        _onTriggerEnterAction = new DealDamage(enemy.Damage);

        _enemyHealth = new Health(enemy.MaxHealth, enemy.MaxHealth);

        enemy.Initialize(_enemyMover, _onTriggerEnterAction, _enemyHealth);

        if (enemy.IsInitialized)
            return true;
        else
            return false;
    }
}
