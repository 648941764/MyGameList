using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面选项UI
/// </summary>
public class InitSelectionUI : SelectionUI
{
    private TextMeshProUGUI _titleText;

    protected override void AttachComponents()
    {
        selectionButton = selectionObject.GetComponent<Button>();
        _titleText = selectionObject.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    protected override void InitUIData()
    {
        _titleText.color = selectionData.titleColor;
        _titleText.text = selectionData.title;
    }

    protected override string PrefabPath()
    {
        return "UI/SelectionButton";
    }
}

public class GameInitSelection : MonoBehaviour
{
    private SelectionList selectionList;
    [SerializeField] private Transform _selectionParent;

    private void Awake()
    {
        InitSelections();
    }

    private void InitSelections()
    {
        selectionList = new SelectionList(_selectionParent);

        InitSelectionUI choose = new InitSelectionUI();
        choose.Instantiate(new SelectionData() { title = "选择游戏", titleColor = new Color(0f, 1f, 0f), onSelectedAction = () => Debug.Log("打开选择游戏界面") });
        selectionList.AddSelection(choose);

        InitSelectionUI setting = new InitSelectionUI();
        setting.Instantiate(new SelectionData() { title = "设置", onSelectedAction = () => Debug.Log("打开选择设置界面") });
        selectionList.AddSelection(setting);

        InitSelectionUI quit = new InitSelectionUI();
        quit.Instantiate(new SelectionData()
        {
            title = "退出",
            onSelectedAction = () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        });
        selectionList.AddSelection(quit);
    }
}
