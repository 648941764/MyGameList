using UnityEngine;
using System.Collections.Generic;

public abstract class Character : GameFlow
{
    protected override void OnEnable()
    {
        base.OnEnable();
        CharacterManager.Instance.Add(this);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CharacterManager.Instance.Del(this);
    }
}