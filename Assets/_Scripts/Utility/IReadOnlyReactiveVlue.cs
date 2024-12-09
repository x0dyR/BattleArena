using System;

public interface IReadOnlyReactiveVlue<T> where T : IComparable
{
    event Action<T> Changed;

    T Value { get; }
}