using System;

public interface ILooseCondition
{
    event Action Lost;

    void Start();

    void End();
}
