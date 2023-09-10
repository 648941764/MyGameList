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
            title = "��Ч����",
            titleColor = Color.white,
            onSelectedAction = () =>
            {
                Debug.Log("����Ч����");
                //�˴���û�������Ч���ܣ���Ϸ���������
            }
        });
        _selectionList.AddSelection(sound);

        GameInSettingUI quit = new GameInSettingUI();
        quit.Instantiate(new SelectionData()
        {
            title = "������ҳ��",
            titleColor = Color.white,
            onSelectedAction = () =>
            {
                Debug.Log("������ҳ��");
               _gameSettingParent.gameObject.SetActive(false);
                _selection.gameObject.SetActive(true);
            }
        });
        _selectionList.AddSelection(quit);
    }
}
