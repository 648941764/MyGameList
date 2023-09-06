using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SelectionUI
{
    protected SelectionData selectionData;
    protected Transform selectionObject;
    protected Button selectionButton;

    protected abstract void AttachComponents();
    protected virtual void RegisterUIEvents() { selectionButton.onClick.AddListener(OnSelectedHandle); }
    protected virtual void InitUIData() { }

    public void SetParent(Transform parent)
    {
        selectionObject.SetParent(parent);
    }

    protected virtual void OnSelectedHandle()
    {
        selectionData?.onSelectedAction?.Invoke();
    }

    protected abstract string PrefabPath();

    /// <summary>
    /// ������������GameObject
    /// </summary>
    /// <param name="selectionData"></param>
    /// <returns></returns>
    /// <exception cref="System.NullReferenceException"></exception>
    public SelectionUI Instantiate(SelectionData selectionData)
    {
        this.selectionData = selectionData;
        GameObject prefab = Resources.Load<GameObject>(PrefabPath());
        if (prefab == null)
        {
            throw new System.NullReferenceException("ʵ����SelectionUIδ���ص�prefab��·����" + PrefabPath());
        }
        selectionObject = Object.Instantiate(prefab).transform;
        AttachComponents();
        RegisterUIEvents();
        InitUIData();
        return this;
    }

    public void Clear() { Object.DestroyImmediate(selectionObject.gameObject); }
}