using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class SelectionBase
{
    protected Transform selectionObject;
    protected Button selectionButton;

    public abstract string Title { get; }
    public virtual Color TitleColor => new Color(0f, 0f, 0f);

    protected abstract void OnSelected();
    protected abstract void AttachComponents();
    protected abstract void InitUIEvent();
    protected virtual void InitUIData() { }
    public abstract string PrefabResourcesPath();

    public void Instantiate(Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(PrefabResourcesPath());
        if (prefab == null)
        {
            throw new System.NullReferenceException("ʵ����SelectionBaseδ���ص�prefab��·����" + PrefabResourcesPath());
        }
        selectionObject = Object.Instantiate(prefab, parent).transform;
        AttachComponents();
        InitUIEvent();
        InitUIData();
    }

    public void Clear() { Object.DestroyImmediate(selectionObject.gameObject); }
}

#region ��ʼ����ѡ����

public abstract class MainSelection : SelectionBase
{
    private TextMeshProUGUI _titleText;

    protected override void AttachComponents()
    {
        selectionButton = selectionObject.GetComponent<Button>();
        _titleText = selectionObject.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    protected override void InitUIEvent()
    {
        selectionButton.onClick.AddListener(OnSelected);
    }

    protected override void InitUIData()
    {
        _titleText.color = TitleColor;
        _titleText.text = Title;
    }

    public override string PrefabResourcesPath()
    {
        return "UI/SelectionButton";
    }
}

public class ChooseGameSelection : MainSelection
{
    public override string Title => "ѡ����Ϸ";

    public override Color TitleColor => new Color(0f, 1f, 0f);

    /// <summary>
    /// ����Ϸ�б�
    /// </summary>
    protected override void OnSelected() { Debug.Log("��ѡ����Ϸ����"); }
}

/// <summary>
/// ������Լ���
/// </summary>
public class SettingSelection : MainSelection
{
    public override string Title => "����";
    /// <summary>
    /// ����Ϸ�б�
    /// </summary>
    protected override void OnSelected() { }
}

public class QuitGameSelection : MainSelection
{
    public override string Title => "�˳�";

    protected override void OnSelected()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

#endregion
}
