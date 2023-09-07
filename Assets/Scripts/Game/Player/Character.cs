using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    public abstract void PlayerUpdate(float dt);
    public virtual void EnrollEvents() { }
    public virtual void UnenrollEvents() { }
}