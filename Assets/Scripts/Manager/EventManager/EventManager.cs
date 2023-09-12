using System.Collections.Generic;
using System;
using System.Reflection;
using System.Diagnostics;

public delegate void EventHandler(EventParam param);//定义委托

/// <summary> 事件处理器 /// </summary>
public sealed class EventManager : Singleton<EventManager>
{
    private EventHandler r_Handler;
    private readonly HashSet<EventHandler> r_Handlers = new HashSet<EventHandler>();

    public void AddListener(EventHandler handler)//添加事件
    {
        if (r_Handlers.Contains(handler))//检查方法或者事件是否已经添加到列表里面了
        {
            UnityEngine.Debug.LogWarningFormat("事件添加：<color=#FFFFFF>{0}</color>，重复!", handler.GetMethodInfo().Name);
            return;
        }
        r_Handler += handler;
        r_Handlers.Add(handler);
    }

    public void RemoveListener(EventHandler handler)//移除监听事件
    {
        if (!r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("事件不存在：<color=#FFFFFF>{0}</color>，不存在!", handler.GetMethodInfo().Name);
            return;
        }
        r_Handler -= handler;
        r_Handlers.Remove(handler);
    }

    public void Broadcast(EventParam param, bool returnToPool = true)//用于触发事件
    {
        r_Handler?.Invoke(param);
        if (returnToPool) { ParamPool.Release(param); }
    }
}