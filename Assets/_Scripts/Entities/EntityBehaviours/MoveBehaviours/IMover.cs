using System;
using UnityEngine;

public interface IMover
{
    event Action Moved;
    event Action Stopped;

    ObstacleChecker ObstacleChecker { get; }

    void ProcessMove(Vector3 direction);
}
