using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game : MonoBehaviour
{
    protected GameData gameData;
    protected abstract void Instantiate(GameData gameData);

    /// <summary> 游戏逻辑实现接口 /// </summary>
    /// <param name="dt">Time.deltaTime</param>
    protected abstract void GameUpdate(float dt);

    public abstract string GetSceneName();
}
