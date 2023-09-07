using UnityEngine;
using System;

public sealed class InputManager : MonoSingleton<InputManager>
{
    private KeyCode[] r_KeyCodes;
    EventParam eventParam;
    int i;

    protected override void OnAwake()
    {
        eventParam = ParamPool.Get(EventName.KeyboardInput);
        Array keyCodes = Enum.GetValues(typeof(KeyCode));
        r_KeyCodes = new KeyCode[keyCodes.Length];
        i = -1;
        foreach (KeyCode keyCode in keyCodes)
        {
            r_KeyCodes[++i] = keyCode;
        }
    }

    protected override void Update()
    {
        i = -1;
        while (++i < r_KeyCodes.Length)
        {
            if (Input.GetKey(r_KeyCodes[i]))
            {
                eventParam.Push(r_KeyCodes[i]);
                EventManager.Instance.Broadcast(eventParam, false);
            }
        }
    }
}