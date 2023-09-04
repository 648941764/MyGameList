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
            throw new System.NullReferenceException("实例化SelectionBase未加载到prefab，路径：" + PrefabResourcesPath());
        }
        selectionObject = Object.Instantiate(prefab, parent).transform;
        AttachComponents();
        InitUIEvent();
        InitUIData();
    }

    public void Clear() { Object.DestroyImmediate(selectionObject.gameObject); }
}

#region 开始界面选择项

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
    public override string Title => "选择游戏";

    public override Color TitleColor => new Color(0f, 1f, 0f);

    /// <summary>
    /// 打开游戏列表
    /// </summary>
    protected override void OnSelected() { Debug.Log("打开选择游戏界面"); }
}

/// <summary>
/// 这个你自己加
/// </summary>
public class SettingSelection : MainSelection
{
    public override string Title => "设置";
    /// <summary>
    /// 打开游戏列表
    /// </summary>
    protected override void OnSelected() { }
}

public class QuitGameSelection : MainSelection
{
    public override string Title => "退出";

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
