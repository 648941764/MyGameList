using System.Collections.Generic;
using System;
using System.Reflection;

public delegate void EventHandler(EventParam param);

/// <summary> �¼������� /// </summary>
public sealed class EventManager : Singleton<EventManager>
{
    private EventHandler r_Handler;
    private readonly HashSet<EventHandler> r_Handlers = new HashSet<EventHandler>();

    public void AddListener(EventHandler handler)
    {
        if (r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("�¼���ӣ�<color=#FFFFFF>{0}</color>���ظ�!", handler.GetMethodInfo().Name);
        }
        r_Handler += handler;
    }

    public void RemoveListener(EventHandler handler)
    {
        if (!r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("�¼������ڣ�<color=#FFFFFF>{0}</color>��������!", handler.GetMethodInfo().Name);
        }
        r_Handler -= handler;
    }

    public void Broadcast(EventParam param)
    {
        r_Handler.Invoke(param);
    }
}