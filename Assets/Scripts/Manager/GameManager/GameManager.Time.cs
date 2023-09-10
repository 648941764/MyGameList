using UnityEngine;

public partial class GameManager
{
    const float ONE_MILLI_SEC = 0.001f;
    const int ONE_SEC = 1;

    /// <summary> ��Ϸʱ��, ���룬��APP��һ���п�ʼ��¼���ᱣ�� /// </summary>
    private int _gameTime;
    private float _elapsed;

    private void _UpdateTime()
    {
        _elapsed += Time.deltaTime;
        while (_elapsed >= ONE_MILLI_SEC) // 1����
        {
            _elapsed -= ONE_MILLI_SEC;
            _gameTime += ONE_SEC;
        }
    }
}