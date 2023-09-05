using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    const float ONE_MILLI_SEC = 0.001f;
    const int ONE_SEC = 1;

    /// <summary> 游戏时间, 毫秒，从APP第一运行开始记录，会保存 /// </summary>
    private int _gameTime;

    private float _elapsed;

    protected override void Update()
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1毫秒
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
        }
    }
}