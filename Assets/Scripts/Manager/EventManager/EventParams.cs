using System.Dynamic;
using System.Collections.Generic;

public sealed class EventParam
{
    public EventName eventName;
    private readonly Queue<Any> r_Parmas = new Queue<Any>();

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