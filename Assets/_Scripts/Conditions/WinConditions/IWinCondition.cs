using System;

public interface IWinCondition
{
    event Action Won;
    void Start();

    void End();
}
