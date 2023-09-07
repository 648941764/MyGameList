using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Start()
    {
        InputManager.Instance.ToString();
        EventManager.Instance.AddListener(_ =>
        {
            Debug.Log(_.Get<KeyCode>().ToString());
        });
    }

    protected override void Update()
    {
        UpdateTime();
    }

    const float ONE_MILLI_SEC = 0.001f;
    const int ONE_SEC = 1;

    /// <summary> 游戏时间, 毫秒，从APP第一运行开始记录，会保存 /// </summary>
    private int _gameTime;
    private float _elapsed;

    void UpdateTime()
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1毫秒
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
        }
    }
}