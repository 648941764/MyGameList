using System.Collections.Generic;

public static class EventParamPool
{
    private static readonly Queue<EventParam> r_Pool = new Queue<EventParam>();

    public static EventParam GetParam(EventName eventName = EventName.Nothing)
    {
        if (r_Pool.Count == 0) { return new EventParam(eventName); }
        EventParam p = r_Pool.Dequeue();
        p.eventName = eventName;
        return p;
    }

    public static void ReleaseParam(EventParam param)
    {
        param.Clear();
        r_Pool.Enqueue(param);
    }
}