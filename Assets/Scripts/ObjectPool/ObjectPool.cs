using System.Collections.Generic;
using System;

public class ObjectPool<T> where T : new()
{
    private readonly uint r_MaxCount;
    private readonly Action<T> r_OnGet, r_OnRelease;
    private readonly Stack<T> r_Pool = new Stack<T>();

    public ObjectPool(uint maxCount = 512, Action<T> onGet = default, Action<T> onRelease = default) => (r_MaxCount, r_OnGet, r_OnRelease) = (maxCount, onGet, onRelease);

    public T Get()
    {
        T @object = r_Pool.Count > 0 ? r_Pool.Pop() : new T();
        r_OnGet?.Invoke(@object);
        return @object;
    }

    public void Release(T @object)
    {
        if (r_Pool.Count == r_MaxCount) { return; }
        r_OnRelease?.Invoke(@object);
        r_Pool.Push(@object);
    }
}