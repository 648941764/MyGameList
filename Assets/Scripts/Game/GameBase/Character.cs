using UnityEngine;
using System.Collections.Generic;

public abstract class Character : GameFlow
{
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