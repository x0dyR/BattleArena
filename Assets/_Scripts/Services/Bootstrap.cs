using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Transform _charcaterSpawnPoint;
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private CharacterView _characterViewPrefab;

    private Character _characterInstance;

    private InputSystem _input;
    private IMover _characterMover;
    private IShooter _characterShooter;
    private Health _characterHealth;

    [SerializeField] private Enemy _enemmyPrefab;
    [SerializeField] private List<Transform> _enemySpawnPoints;

    private EnemyFactory _enemyFactory;
    private EnemySpawner _enemySpawner;
    [SerializeField] private int _spawnCooldown;

    [SerializeField] private WinConditions _winConditions;
    [SerializeField] private LooseConditions _looseConditions;

    private IWinCondition _winCondition;
    private ILooseCondition _looseCondition;

    [SerializeField, Range(1, 10)] private int _maxEnemyAtArena;
    [SerializeField, Range(1, 10)] private int _killToWin;
    [SerializeField, Range(5, 7)] private int _enemyToCapture;
    [SerializeField, Range(1, 100)] private float _timeToSurvive;

    private bool _isUnsubbed;

    private void Awake()
    {
        _input = new InputSystem();

        if (TryGetCharacter(out _characterInstance))
        {
            BindCameraTo(_characterInstance);
            GetCharacterView(_characterInstance);
        }

        _enemyFactory = new EnemyFactory(_enemmyPrefab);
        _enemySpawner = new EnemySpawner(this, _enemyFactory, _enemySpawnPoints, _spawnCooldown, _maxEnemyAtArena);

        _winCondition = _winConditions switch
        {
            WinConditions.SurviveForTime => new SurviveForTimeCondition(this, _timeToSurvive, _characterInstance),
            WinConditions.KillEnemy => new KillEnemyCondition(_enemySpawner.Enemies, _killToWin),
            _ => throw new ArgumentException("No suitable condition"),
        };

        _looseCondition = _looseConditions switch
        {
            LooseConditions.CharacterDied => new CharacterDiedCondition(_characterInstance),
            LooseConditions.ArenaCaptured => new ArenaCapturedCondition(_enemySpawner, _enemyToCapture),
            _ => throw new ArgumentException("No suitable condition"),
        };


        _winCondition.Start();
        _looseCondition.Start();

        _enemySpawner.StartSpawn();
        _winCondition.Won += OnWon;
        _looseCondition.Lost += OnLost;
    }

    private void OnDisable()
    {
        if (_isUnsubbed == false)
        {
            _winCondition.Won -= OnWon;
            _looseCondition.Lost -= OnLost;
        }

        _winCondition.End();
        _looseCondition.End();
    }

    private bool TryGetCharacter(out Character character)
    {
        character = Instantiate(_characterPrefab, _charcaterSpawnPoint.position, Quaternion.identity, null);

        _characterMover = new CharacterControllerMover(character.CharacterController, character.Speed, character.ObstacleChecker);
        _characterShooter = new BulletShooter(_bulletPrefab, character.Muzzle, 10);
        _characterHealth = new Health(character.Maxhealth, character.Maxhealth);

        character.Initialize(_input, _characterMover, _characterShooter, _characterHealth);

        return character.IsInitialized;
    }

    private void BindCameraTo(Character character) => _virtualCamera.Follow = character.transform;

    private CharacterView GetCharacterView(Character character)
    {
        CharacterView view = Instantiate(_characterViewPrefab, character.transform.position, character.transform.rotation, character.transform);
        view.Initialize(character);
        return view;
    }

    private void OnLost() => EndGame("Lost");

    private void OnWon() => EndGame("Won");

    private void EndGame(string message)
    {
        _isUnsubbed = true;

        _winCondition.Won -= OnWon;
        _looseCondition.Lost -= OnLost;

        _winCondition.End();
        _looseCondition.End();

        _enemySpawner.StopSpawn();
        Debug.Log(message);
    }
}
