using Excalibur.Physical;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class CharacterManager : Singleton<CharacterManager>
{
    private readonly List<Character> _characters = new List<Character>();
    private readonly List<Character> _addList = new List<Character>();
    private readonly List<Character> _delList = new List<Character>();

    private int _current;

    public void CharacterUpdate(float dt)
    {
        if (_addList.Count > 0)
        {
            _characters.AddRange(_addList);
            _addList.Clear();
        }

        if (_delList.Count > 0)
        {
            _current = -1;
            while (++_current < _delList.Count)
            {
                int index = _characters.IndexOf(_delList[_current]);
                _characters[index] = _characters[_characters.Count - 1];
                _characters.RemoveAt(_characters.Count - 1);
            }
            _delList.Clear();
        }

        if (_characters.Count == 0) { return; }

        _current = -1;
        while (++_current < _characters.Count)
        {
            _characters[_current].GameUpdate(dt);
        }
    }

    public void Add(Character character)
    {
        _addList.Add(character);
    }

    public void Del(Character character)
    {
        _addList.Add(character);
    }
}