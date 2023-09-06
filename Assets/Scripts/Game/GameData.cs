using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public abstract class GameData : IPersistent
{
    public abstract string GetSceneName();

    public void Load(JObject jsonObject)
    {
    }

    public void Save(JObject jsonObject)
    {
    }
}