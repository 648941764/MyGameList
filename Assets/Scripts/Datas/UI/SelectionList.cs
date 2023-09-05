using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SelectionList<T> where T : SelectionBase
{
    private readonly List<T> r_Selections = new List<T>();
    private readonly Transform r_SelectionParent;

    public Transform SelectionParent => r_SelectionParent;

    public SelectionList(Transform selectionParent)
    {
        r_SelectionParent = selectionParent.transform;
    }

    public void AddSelection(T selection) 
    {
        selection.Instantiate(SelectionParent);
        r_Selections.Add(selection);
    }

    public void ClearSelections()
    {
        int i = -1;
        while (++i < r_Selections.Count) { r_Selections[i].Clear(); }
        r_Selections.Clear();
    }
}
