using UnityEngine;
using System.Collections.Generic;
using System;

public partial class GameManager : MonoSingleton<GameManager>
{
    float dt;

    protected override void Start()
    {
        base.Start();
        ModelManager.Instance.InstantiateModel();
    }

    protected override void Update()
    {
        GameUpdate();
    }

    private void GameUpdate()
    {
        dt = Time.deltaTime;
        _UpdateInput();
        _UpdateTime();
        CharacterManager.Instance.CharacterUpdate(dt);
    }

    private void _Broadcast(EventParam param)
    {
        EventManager.Instance.Broadcast(param, false);
    }
}