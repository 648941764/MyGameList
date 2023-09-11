using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ��Ϸ���̻��࣬����Ҫȥʵ��Update
/// </summary>
public abstract class GameFlow : MonoBehaviour
{
    private readonly HashSet<EventHandler> _enrolledHanders = new HashSet<EventHandler>();

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void OnEnable() { EnrollEvents(); }
    protected virtual void OnDisable() { UnenrollEvents(); }
    protected virtual void OnDestroy() { _enrolledHanders.Clear(); }
    public virtual void OnBegin() { }
    public virtual void OnEnd() { }
    public virtual void GameUpdate(float dt) { }

    #region Events����Ҫ��Awake������м���
    protected void EnrollEvents(EventHandler handler)
    {
        _enrolledHanders.Add(handler);
    }

    private void EnrollEvents()
    {
        if (_enrolledHanders.Count > 0)
        {
            foreach (EventHandler handler in _enrolledHanders)
            {
                EventManager.Instance.AddListener(handler);
            }
        }
    }

    private void UnenrollEvents()
    {
        if (_enrolledHanders.Count > 0)
        {
            foreach (EventHandler handler in _enrolledHanders)
            {
                EventManager.Instance.RemoveListener(handler);
            }
        }
    }

    protected void Broadcast(EventParam param)
    {
        EventManager.Instance.Broadcast(param);
    }

    #endregion
}