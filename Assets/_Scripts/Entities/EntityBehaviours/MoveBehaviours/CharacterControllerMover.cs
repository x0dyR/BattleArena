using System;
using UnityEngine;

public class CharacterControllerMover : IMover
{
    public event Action Moved;
    public event Action Stopped;

    private const float DeadZone = .1f;

    private CharacterController _characterController;
    private float _speed;

    public CharacterControllerMover(CharacterController characterController, float speed, ObstacleChecker obstacleChecker)
    {
        _characterController = characterController;
        _speed = speed;
        ObstacleChecker = obstacleChecker;
    }

    public ObstacleChecker ObstacleChecker { get; private set; }

    public void ProcessMove(Vector3 direction)
    {
        if (ObstacleChecker.IsTouches() == false)
            direction.y += Physics.gravity.y;

        if (direction.sqrMagnitude < DeadZone * DeadZone)
        {
            Stopped?.Invoke();
            return;
        }

        _characterController.Move(Time.deltaTime * _speed * direction.normalized);
        Moved?.Invoke();
        RotateTo(direction);
    }

    private void RotateTo(Vector3 direction)
    {
        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);

        if (lookDirection.sqrMagnitude > DeadZone * DeadZone)
            _characterController.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
    }
}
