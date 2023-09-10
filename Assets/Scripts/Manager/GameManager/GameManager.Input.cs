using UnityEngine;

public partial class GameManager
{
    private EventParam _keyDownEventParam = new EventParam(EventName.KeyDownInput);
    private EventParam _keyEventParam = new EventParam(EventName.KeyInput);
    private EventParam _keyUpEventParam = new EventParam(EventName.KeyUpInput);
    private KeyCode _current = KeyCode.None;

    private void _UpdateInput()
    {
        while (++_current <= KeyCode.Joystick8Button19)
        {
            if (Input.GetKeyDown(_current))
            {
                _keyDownEventParam.Push(_current);
                _Broadcast(_keyDownEventParam);
            }

            if (Input.GetKey(_current))
            {
                _keyEventParam.Push(_current);
                _Broadcast(_keyEventParam);
            }

            if (Input.GetKeyUp(_current))
            {
                _keyUpEventParam.Push(_current);
                _Broadcast(_keyUpEventParam);
            }
        }

        _current = KeyCode.None;
    }
}