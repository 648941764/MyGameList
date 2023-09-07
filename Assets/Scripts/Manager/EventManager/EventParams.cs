using System.Dynamic;
using System.Collections.Generic;

public sealed class EventParam
{
    public EventName eventName;
    private readonly Queue<Any> r_Parmas = new Queue<Any>();

    public EventParam() { }
    public EventParam(EventName eventName) => this.eventName = eventName;

    public dynamic Get()
    {
        return r_Parmas.Dequeue();
    }

    public EventParam Push(object param)
    {
        r_Parmas.Enqueue(new Any(param));
        return this;
    }

    public void Clear()
    {
        eventName = EventName.Nothing;
        r_Parmas.Clear();
    }
}

public struct Any
{
    public dynamic param;
    public Any(dynamic param) => this.param = param;
}

public static class ParamPool
{
    public static readonly ObjectPool<EventParam> sr_ParamPool = new ObjectPool<EventParam>(200, default, _ => _.Clear());

    public static EventParam Get(EventName eventName)
    {
        EventParam param = sr_ParamPool.Get();
        param.eventName = eventName;
        return param;
    }

    public static void Release(EventParam param) => sr_ParamPool.Release(param);
}