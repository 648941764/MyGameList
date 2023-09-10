using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 游戏流程基类，都不要去实现Update
/// </summary>
public abstract class GameFlow : MonoBehaviour
{
    private readonly HashSet<EventHandler> _enrolledHanders = new HashSet<EventHandler>();

    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void OnEnable() { EnrollEvents(); }
    protected virtual void OnDisable() { UnenrollEvents(); }
    protected virtual void OnDestroy() { _enrolledHanders.Clear(); }
    public virtual void OnGameStart() { }
    public virtual void OnGameEnd() { }
    public virtual void GameUpdate(float dt) { }

    #region Events，需要再Awake里面进行监听
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

    #endregion
}