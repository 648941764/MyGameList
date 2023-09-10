using System.Dynamic;
using System.Collections.Generic;
using System;

public sealed partial class EventParam
{
    public EventName eventName;
    private readonly Queue<object> r_Parmas = new Queue<object>();

    public EventParam() { }
    public EventParam(EventName eventName) => this.eventName = eventName;

    public int ParamCount => r_Parmas.Count;

    public T Get<T>()
    {
        return (T)r_Parmas.Dequeue();
    }

    public EventParam Push<T>(T param)
    {
        r_Parmas.Enqueue(param);
        return this;
    }

    public void Clear()
    {
        eventName = EventName.Nothing;
        r_Parmas.Clear();
    }
}

public static class ParamPool
{
    public static readonly ObjectPool<EventParam> sr_ParamPool = new ObjectPool<EventParam>(200, default, _ => _.Clear());

    public static EventParam Get(EventName eventName = EventName.Nothing)
    {
        EventParam param = sr_ParamPool.Get();
        param.eventName = eventName;
        return param;
    }

    public static void Release(EventParam param) => sr_ParamPool.Release(param);
}