using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>, IPersistent
{
    private readonly Dictionary<Type, GameModel> _models = new Dictionary<Type, GameModel>();

    public void InstantiateModel()
    {
        AddModel<PickupModel>();
    }

    public void AddModel<T>() where T : GameModel, new()
    {
        Type type = typeof(T);
        if (_models.ContainsKey(type))
        {
            Debug.LogFormat("{0}模块已经添加", type.Name);
            return;
        }
        T model = new T();
        model.OnInstantiated();
        model.OnEstablished();
        _models.Add(type, model);
    }

    public void DelModel(GameModel model)
    {
        Type type = model.GetType();
        if (!_models.ContainsKey(type))
        {
            Debug.LogFormat("{0}模块移除不存在", type.Name);
            return;
        }
        _models.Remove(type);
    }

    public T GetModel<T>() where T : GameModel
    {
        return (T)_models[typeof(T)];
    }

    public void Load(JObject jsonObject)
    {
        foreach (GameModel model in _models.Values)
        {
            model.Load(jsonObject);
        }
    }

    public void Save(JObject jsonObject)
    {
        foreach (GameModel model in _models.Values)
        {
            model.Save(jsonObject);
        }
    }
}