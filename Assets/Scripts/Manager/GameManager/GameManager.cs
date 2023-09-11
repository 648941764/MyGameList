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
        CharacterManager.Instance.CharacterUpdate(dt);
    }

    const float ONE_MILLI_SEC = 0.001f;
    const int ONE_SEC = 1;

    /// <summary> 游戏时间, 毫秒，从APP第一运行开始记录，会保存 /// </summary>
    private int _gameTime;
    private float _elapsed;

    private void _UpdateTime()//可能是跟踪游戏进行的时间
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1毫秒
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
        }
    }

    private void _Broadcast(EventParam param)
    {
        EventManager.Instance.Broadcast(param, false);
    }
}