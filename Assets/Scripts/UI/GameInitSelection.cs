using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʼ����ѡ��UI
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
        choose.Instantiate(new SelectionData() { title = "ѡ����Ϸ", titleColor = new Color(0f, 1f, 0f), onSelectedAction = () => Debug.Log("��ѡ����Ϸ����") });
        selectionList.AddSelection(choose);

        InitSelectionUI setting = new InitSelectionUI();
        setting.Instantiate(new SelectionData() { title = "����", onSelectedAction = () => Debug.Log("��ѡ�����ý���") });
        selectionList.AddSelection(setting);

        InitSelectionUI quit = new InitSelectionUI();
        quit.Instantiate(new SelectionData()
        {
            title = "�˳�",
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
