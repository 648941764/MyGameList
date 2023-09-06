using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Update()
    {
        UpdateTime();
    }

    const float ONE_MILLI_SEC = 0.001f;
    const int ONE_SEC = 1;

    /// <summary> ��Ϸʱ��, ���룬��APP��һ���п�ʼ��¼���ᱣ�� /// </summary>
    private int _gameTime;
    private float _elapsed;

    void UpdateTime()
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1����
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
        }
    }
}