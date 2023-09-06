using System.Collections.Generic;
using System;
using System.Reflection;

public delegate void EventHandler(EventParam param);

/// <summary> 事件处理器 /// </summary>
public sealed class EventManager : Singleton<EventManager>
{
    private EventHandler r_Handler;
    private readonly HashSet<EventHandler> r_Handlers = new HashSet<EventHandler>();

    public void AddListener(EventHandler handler)
    {
        if (r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("事件添加：<color=#FFFFFF>{0}</color>，重复!", handler.GetMethodInfo().Name);
        }
        r_Handler += handler;
    }

    public void RemoveListener(EventHandler handler)
    {
        if (!r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("事件不存在：<color=#FFFFFF>{0}</color>，不存在!", handler.GetMethodInfo().Name);
        }
        r_Handler -= handler;
    }

    public void Broadcast(EventParam param)
    {
        r_Handler.Invoke(param);
    }
}