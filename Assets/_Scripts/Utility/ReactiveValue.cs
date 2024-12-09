using System;

public class ReactiveValue<T> : IReadOnlyReactiveVlue<T> where T : IComparable
{
    public event Action<T> Changed;

    private T _value;

    public ReactiveValue()
    {
        Value = default;
    }
    public ReactiveValue(T value)
    {
        Value = value;
    }

    public T Value
    {
        get => _value;
        set
        {
            if (_value.CompareTo(value) != 0)
            {
                _value = value;
                Changed?.Invoke(_value);
            }
        }
    }
}
