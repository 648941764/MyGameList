using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected Transform character;
    public virtual void RegisterEvents() { }
    public abstract void PlayerUpdate(float dt);
}