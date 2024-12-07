using System;
using UnityEngine;

public interface IMover
{
    ObstacleChecker ObstacleChecker { get; }

    event Action Moved;
    event Action Stopped;

    void ProcessMove(Vector3 direction);
}
