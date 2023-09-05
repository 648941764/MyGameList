using System;

public class Singleton<T> where T : class, new()
{
    private static readonly Lazy<T> _lazy = new Lazy<T>(() => new T());
    public static T Instance => _lazy.Value;
    protected Singleton() { }
}
