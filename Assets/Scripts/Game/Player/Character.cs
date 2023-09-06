using UnityEngine;

public abstract class Character
{
    protected Transform character;
    public virtual void Attach(Transform characterTransform)
    {
        character = characterTransform;
        AttachComponents();
        RegisterEvents();
    }

    public virtual void AttachComponents() { }
    public virtual void RegisterEvents() { }
    public abstract void PlayerUpdate(float dt);
}