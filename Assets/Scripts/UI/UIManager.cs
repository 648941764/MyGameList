using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] GameInitSelection initSelection;
    [SerializeField] InGameList inGameList;
    [SerializeField] GameInSetting gameInSetting;
}
