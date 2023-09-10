using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInSettingUI : SelectionUI
{

    private TextMeshProUGUI _titleText;
    protected override void AttachComponents()
    {
        selectionButton = selectionObject.GetComponent<Button>();
        _titleText = selectionObject.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    protected override string PrefabPath()
    {
        return "UI/SelectionButton";
    }

    protected override void InitUIData()
    {
        _titleText.color = Color.blue;
        _titleText.text = selectionData.title;
    }
}

public class GameInSetting : MonoBehaviour
{
    private SelectionList _selectionList;
    [SerializeField] private Transform _gameSettingParent;
    [SerializeField] private Transform _selection;

    private void Awake()
    {
        InitGameSetting();
    }

    public void InitGameSetting()
    {
        _selectionList = new SelectionList(_gameSettingParent);
        GameInSettingUI sound = new GameInSettingUI();
        sound.Instantiate(new SelectionData()
        {
            title = "音效设置",
            titleColor = Color.white,
            onSelectedAction = () =>
            {
                Debug.Log("打开音效设置");
                //此处还没有添加音效功能，游戏做完再添加
            }
        });
        _selectionList.AddSelection(sound);

        GameInSettingUI quit = new GameInSettingUI();
        quit.Instantiate(new SelectionData()
        {
            title = "返回主页面",
            titleColor = Color.white,
            onSelectedAction = () =>
            {
                Debug.Log("返回主页面");
               _gameSettingParent.gameObject.SetActive(false);
                _selection.gameObject.SetActive(true);
            }
        });
        _selectionList.AddSelection(quit);
    }
}
