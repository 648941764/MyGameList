using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public sealed class SelectionList
{
    private readonly List<SelectionUI> r_Selections = new List<SelectionUI>();
    private readonly Transform r_SelectionParent;

    public Transform SelectionParent => r_SelectionParent;

    public SelectionList(Transform selectionParent)
    {
        r_SelectionParent = selectionParent.transform;
    }

    public void AddSelection(SelectionUI selection)
    {
        r_Selections.Add(selection);
        selection.SetParent(r_SelectionParent);
    }

    public void AddSelections(IList<SelectionUI> selections)
    {
        if (selections?.Count == 0) { return; }
        r_Selections.AddRange(selections);
        int i = -1;
        while (++i < selections.Count)
        {
            selections[i].SetParent(r_SelectionParent);
        }
    }

    public void ClearSelections()
    {
        int i = -1;
        while (++i < r_Selections.Count) { r_Selections[i].Clear(); }
        r_Selections.Clear();
    }
}
