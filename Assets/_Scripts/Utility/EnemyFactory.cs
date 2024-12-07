using UnityEngine;

public class EnemyFactory
{
    private Enemy _enemyPrefab;

    private IOnTriggerEnterAction _triggerAction;
    private IMover _mover;
    private Health _health;

    public EnemyFactory(Enemy enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }

    public Enemy Get(Vector3 spawnPosition)
    {
        Enemy enemy = Object.Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, null);

        _triggerAction = new DealDamage(enemy.Damage);
        _mover = new RandomDirectionMover(enemy.CharacterController, enemy.Speed, enemy.ObstacleChecker,enemy.TimeToChangeDirection);
        _health = new(enemy.MaxHealth, enemy.MaxHealth);
        enemy.Initialize(_mover, _triggerAction, _health);

        return enemy;
    }
}
