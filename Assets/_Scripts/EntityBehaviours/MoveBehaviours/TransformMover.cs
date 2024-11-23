using UnityEngine;

public class TransformMover : IMover
{
    private const float DeadZone = .1f;

    private Transform _transform;
    private float _speed;

    public TransformMover(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public void ProcessMove(Vector3 direction)
    {
        if (direction.sqrMagnitude < DeadZone * DeadZone)
            return;

        _transform.position += _speed * Time.deltaTime * direction;
    }
}
