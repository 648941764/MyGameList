using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitSelection : MonoBehaviour
{
    private SelectionList<MainSelection> selectionList;
    [SerializeField]
    private Transform _selectionParent;

    private void Awake()
    {
        InitSelections();
    }

    private void InitSelections()
    {
        selectionList = new SelectionList<MainSelection>(_selectionParent);
        selectionList.AddSelection(new ChooseGameSelection());
        selectionList.AddSelection(new SettingSelection());
        selectionList.AddSelection(new QuitGameSelection());
    }
}
