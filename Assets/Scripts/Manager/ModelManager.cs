using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>, IPersistent
{
    private readonly Dictionary<Type, GameModel> _models = new Dictionary<Type, GameModel>();

    public void InstantiateModel()//实例化模型
    {
        AddModel(new PickupModel());
    }

    public void AddModel(GameModel model)// 添加模型
    {
        Type type = model.GetType();
        if (_models.ContainsKey(type))
        {
            Debug.LogFormat("{0}模块已经添加", type.Name);
            return;
        }
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

    public T GetModel<T>(GameModel model) where T : GameModel
    {
        return (T)_models[model.GetType()];
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