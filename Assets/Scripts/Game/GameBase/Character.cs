using UnityEngine;
using System.Collections.Generic;

public abstract class Character : GameFlow
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public override void Begin()
    {
        CharacterManager.Instance.Add(this);
    }

    public override void Pause(bool pause)
    {
        if (pause)
        {
            CharacterManager.Instance.Del(this);
        }
        else
        {
            CharacterManager.Instance.Add(this);
        }
    }

    public override void Over()
    {
        CharacterManager.Instance.Del(this);
    }
}