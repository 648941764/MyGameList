using System.Collections.Generic;
using System;

public class ObjectPool<T> where T : new()
{
    private readonly int r_MaxCount;
    private readonly Action<T> r_OnGet, r_OnRelease, r_OnOutOfBound;
    private readonly Stack<T> r_Pool = new Stack<T>();

    public int currentCount => r_Pool.Count;
    public int maxCount => r_MaxCount;

    public ObjectPool(
        int maxCount = 512,
        Action<T> onGet = default,
        Action<T> onRelease = default,
        Action<T> onOutOfBound = default
        ) 
        => (r_MaxCount, r_OnGet, r_OnRelease, r_OnOutOfBound) = (maxCount, onGet, onRelease, onOutOfBound);

    public T Get()
    {
        T @object = r_Pool.Count > 0 ? r_Pool.Pop() : new T();
        r_OnGet?.Invoke(@object);
        return @object;
    }

    public void Release(T @object)
    {
        if (r_Pool.Count == r_MaxCount) 
        {
            r_OnOutOfBound?.Invoke(@object);
            return;
        }
        r_OnRelease?.Invoke(@object);
        r_Pool.Push(@object);
    }
}