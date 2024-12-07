using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    private MonoBehaviour _context;
    private EnemyFactory _enemyFactory;
    private List<Transform> _spawnPoints;
    private int _spawnCooldown;
    private int _maxEnemyCount;

    private Coroutine _spawnCoroutine;

    public EnemySpawner(MonoBehaviour context, EnemyFactory enemy, List<Transform> spawnPoints, int spawnCooldown,int maxEnemyCount)
    {
        _context = context;
        _enemyFactory = enemy;
        _spawnPoints = spawnPoints;
        _spawnCooldown = spawnCooldown;
        _maxEnemyCount = maxEnemyCount;
    }

    public ObservableList<Enemy> Enemies { get; private set; } = new();

    public void StartSpawn() => _spawnCoroutine = _context.StartCoroutine(SpawnWithCooldown(_spawnCooldown));

    public void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            _context.StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnWithCooldown(int cooldown)
    {
        while (Enemies.Count < _maxEnemyCount)
        {
            Vector3 randomPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
            Enemy enemy = _enemyFactory.Get(randomPosition);
            Enemies.Add(enemy);

            enemy.Died += OnDied;

            yield return new WaitForSeconds(cooldown);
        }
    }

    private void OnDied(IDamageable obj)
    {
        obj.Died -= OnDied;
        Enemies.Remove(obj as Enemy);
    }
}
