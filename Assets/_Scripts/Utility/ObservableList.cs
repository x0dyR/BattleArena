using System;
using System.Collections.Generic;

public class ObservableList<T>
{
    public event Action<T> Added;
    public event Action<T> Removed;

    private List<T> _list = new();

    public IEnumerable<T> List => _list;

    public void Add(T item)
    {
        if (_list.Contains(item))
            throw new ArgumentException($"{nameof(item)} already exists");

        _list.Add(item);
        Added?.Invoke(item);
    }

    public void Remove(T item)
    {
        if (_list.Contains(item) == false)
            throw new AggregateException($"{nameof(item)} does not exist");

        _list.Remove(item);
        Removed?.Invoke(item);
    }

    public void ForEach(Action<T> action) => _list.ForEach(action);

    public bool Contains(T item) => _list.Contains(item);

    public int Count => _list.Count;
}
