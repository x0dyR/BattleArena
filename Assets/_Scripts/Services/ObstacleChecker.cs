using UnityEngine;

public class ObstacleChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _collideMask;

    [SerializeField] private float _distanceToCheck;

    public bool IsTouches() => Physics.CheckSphere(transform.position, _distanceToCheck, _collideMask);

    private void OnDrawGizmos() => Gizmos.DrawSphere(transform.position, _distanceToCheck);
}
