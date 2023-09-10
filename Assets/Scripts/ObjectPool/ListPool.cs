using System.Collections.Generic;

public static class ListPool<T>
{
    private static readonly ObjectPool<List<T>> sr_Pool = new ObjectPool<List<T>>(512, default, _ => _.Clear());

    public static List<T> Get() => sr_Pool.Get();

    public static void Release(List<T> list) => sr_Pool.Release(list);
}