using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using FancyScrollView;
using EasingCore;

public class BirthdayScrollView : FancyScrollView<BirthdayItemData, BirthdayContext>
{
    public int SelectIndex;
    public bool IsSelecting = false;

    [SerializeField] Scroller scroller = default;
    [SerializeField] GameObject cellPrefab = default;

    protected override GameObject CellPrefab => cellPrefab;

    protected override void Initialize()
    {
        base.Initialize();

        Context.OnCellClicked = SelectCell;

        scroller.OnValueChanged(UpdatePosition);
        scroller.OnSelectionChanged(UpdateSelection);
    }

    void Start()
    {
        scroller.OnValueChanged(base.UpdatePosition);
    }

    private void UpdateSelection(int index)
    {
        if (Context.SelectedIndex == index)
        {
            return;
        }

        Context.SelectedIndex = index;
        SelectIndex = index;
        IsSelecting = false;
        BirthdayManager.Instance.SetBtn_Done();
        Refresh();
    }

    public void UpdateData(IList<BirthdayItemData> items)
    {
        base.UpdateContents(items);
        scroller.SetTotalCount(items.Count);
    }

    public void SelectCell(int index)
    {
        if (index < 0 || index >= ItemsSource.Count || index == Context.SelectedIndex)
        {
            return;
        }

        UpdateSelection(index);
        scroller.ScrollTo(index, 0.35f, Ease.OutCubic);
    }
}