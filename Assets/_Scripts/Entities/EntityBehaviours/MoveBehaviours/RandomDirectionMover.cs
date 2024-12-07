using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDirectionMover : IMover
{
    public event Action Moved;
    public event Action Stopped;

    private CharacterController _characterController;
    private float _speed;

    private Vector3 _currentDirection;
    private float _timeToChangeDirection;
    private float _currentTime = 0;

    private const float DeadZone = .1f;

    public RandomDirectionMover(CharacterController characterController, float speed, ObstacleChecker obstacleChecker, float timeToChangeDirection)
    {
        _characterController = characterController;
        _speed = speed;
        ObstacleChecker = obstacleChecker;
        _timeToChangeDirection = timeToChangeDirection;

        _currentDirection = GetRandomDirection();
    }

    public ObstacleChecker ObstacleChecker { get; private set; }

    public void ProcessMove(Vector3 direction)
    {
        if (ObstacleChecker.IsTouches() == false)
            _currentDirection.y += Physics.gravity.y;

        if (direction.sqrMagnitude < DeadZone * DeadZone)
        {
            Stopped?.Invoke();
            return;
        }

        _currentTime += Time.deltaTime;

        if (_currentTime >= _timeToChangeDirection)
        {
            _currentDirection = GetRandomDirection();
            _currentTime = 0;
        }

        _characterController.Move(Time.deltaTime * _speed * _currentDirection.normalized);
        _characterController.transform.rotation = Quaternion.LookRotation(new Vector3(_currentDirection.x,0,_currentDirection.z));
        Moved?.Invoke();
    }

    private Vector3 GetRandomDirection() => new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
}