using System.Collections.Generic;
using System;
using System.Reflection;
using System.Diagnostics;

public delegate void EventHandler(EventParam param);//����ί��

/// <summary> �¼������� /// </summary>
public sealed class EventManager : Singleton<EventManager>
{
    private EventHandler r_Handler;
    private readonly HashSet<EventHandler> r_Handlers = new HashSet<EventHandler>();

    public void AddListener(EventHandler handler)//����¼�
    {
        if (r_Handlers.Contains(handler))//��鷽�������¼��Ƿ��Ѿ���ӵ��б�������
        {
            UnityEngine.Debug.LogWarningFormat("�¼���ӣ�<color=#FFFFFF>{0}</color>���ظ�!", handler.GetMethodInfo().Name);
            return;
        }
        r_Handler += handler;
        r_Handlers.Add(handler);
    }

    public void RemoveListener(EventHandler handler)//�Ƴ������¼�
    {
        if (!r_Handlers.Contains(handler))
        {
            UnityEngine.Debug.LogWarningFormat("�¼������ڣ�<color=#FFFFFF>{0}</color>��������!", handler.GetMethodInfo().Name);
            return;
        }
        r_Handler -= handler;
        r_Handlers.Remove(handler);
    }

    public void Broadcast(EventParam param, bool returnToPool = true)//���ڴ����¼�
    {
        r_Handler?.Invoke(param);
        if (returnToPool) { ParamPool.Release(param); }
    }
}