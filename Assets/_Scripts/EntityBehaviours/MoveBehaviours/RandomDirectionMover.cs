using System.Collections;
using UnityEngine;

public class RandomDirectionMover : IMover
{
    private Transform _transform;

    private float _speed;

    private MonoBehaviour _context;

    private float _timeToChangeDirection;

    private Vector3 _currentDirection;

    public RandomDirectionMover(MonoBehaviour context,Transform transform, float speed,float timeToChangeDirection)
    {
        _context = context;

        _timeToChangeDirection = timeToChangeDirection;

        _transform = transform;
        _speed = speed;
    }

    public void ProcessMove(Vector3 direction)
    {
        if (direction.sqrMagnitude < Mathf.Deg2Rad * Mathf.Deg2Rad)
            return;

        _context.StartCoroutine(ChangeDirectionEvery(_timeToChangeDirection));

        _transform.position += _speed * Time.deltaTime * _currentDirection.normalized;
    }


    private IEnumerator ChangeDirectionEvery(float time)
    {
        _currentDirection = GetDirection();
        yield return new WaitForSeconds(time);
    }

    private Vector3 GetDirection()
     => new(Random.Range(0, 1), _transform.position.y, Random.Range(0, 1));
}
