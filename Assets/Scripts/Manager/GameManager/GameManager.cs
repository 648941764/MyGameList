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

    /// <summary> ��Ϸʱ��, ���룬��APP��һ���п�ʼ��¼���ᱣ�� /// </summary>
    private int _gameTime;
    private float _elapsed;

    private void _UpdateTime()//�����Ǹ�����Ϸ���е�ʱ��
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1����
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