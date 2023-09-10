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
    [SerializeField] private GameObject _gameList;
    [SerializeField] private GameObject _gameSetting;

    private void Awake()
    {
        InitSelections();
        _gameList.SetActive(false);
        _gameSetting.SetActive(false);
    }

    private void InitSelections()
    {
        selectionList = new SelectionList(_selectionParent);

        InitSelectionUI choose = new InitSelectionUI();
        choose.Instantiate(new SelectionData()
        {
            title = "选择游戏", titleColor = new Color(0f, 1f, 0f), onSelectedAction = () =>
            {
                Debug.Log("打开选择游戏界面");
                _gameList.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        });
        selectionList.AddSelection(choose);

        InitSelectionUI setting = new InitSelectionUI();
        setting.Instantiate(new SelectionData() { title = "设置", onSelectedAction = () =>
            { 
                Debug.Log("打开选择设置界面");
                transform.gameObject.SetActive(false);
                _gameSetting.SetActive(true);
            }
        });
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
