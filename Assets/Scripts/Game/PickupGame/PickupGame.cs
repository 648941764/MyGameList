using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PickupGame : Game
{
    [SerializeField]
    private Character[] _characters;

    public override void OnGameStart()
    {
        int i = -1;
        while (++i < _characters.Length)
        {
            _characters[i].OnGameStart();
        }
    }

    public override void OnGameEnd()
    {
        int i = -1;
        while (++i < _characters.Length)
        {
            _characters[i].OnGameEnd();
        }
    }
}
