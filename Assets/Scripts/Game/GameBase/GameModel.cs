using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class GameModel : IPersistent// 游戏模型的基类
{
    private readonly HashSet<EventHandler> _enrolledHanders = new HashSet<EventHandler>();

    public abstract string GetSceneName();

    public virtual void Load(JObject jsonObject)
    {
    }

    public virtual void Save(JObject jsonObject)
    {
    }

    #region Enroll Events

    public void EnrollEvents()
    {
        if (_enrolledHanders.Count > 0)
        {
            foreach (EventHandler handler in _enrolledHanders)
            {
                EventManager.Instance.AddListener(handler);
            }
        }
    }

    public void UnenrollEvents()
    {
        if (_enrolledHanders.Count > 0)
        {
            foreach (EventHandler handler in _enrolledHanders)
            {
                EventManager.Instance.RemoveListener(handler);
            }
        }
    }

    protected void EnrollEvents(EventHandler handler)
    {
        _enrolledHanders.Add(handler);
    }

    protected void UnenrollEvents(EventHandler handler)
    {
        _enrolledHanders.Remove(handler);
    }

    #endregion

    public virtual void Dispose()
    {
        _enrolledHanders.Clear();
    }
}