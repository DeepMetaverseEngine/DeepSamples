using System;
using DeepCore.GUI.Data;
using UnityEngine;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUI;

public class ItemList : ItemContainer
{
    public HZScrollPan mPan;
    private int mColumn;
    private HZCanvas mTemplateCanvas;

    private Vector2 mCvsSize;

    public ItemList(Vector2 panSize, Vector2 itemSize, int col)
    {
        Size2D = panSize;
        mColumn = col;

        mPan = HZScrollPan.CreateScrollPan(new UEScrollPanMeta()
        {
            EnableScrollH = false,
            EnableScrollV = true,
            UserData = HZScrollPan.Mode.Grid.ToString(),
            Visible = true,
            EnableElasticity = true,
            EnableChilds = true,
            Enable = false,
        });

        AddChild(mPan);
        mPan.Size2D = panSize;

        mItemSize = itemSize;

        float each = panSize.x / col;

        mCvsSize = new Vector2(each, each);
        mTemplateCanvas = HZCanvas.CreateCanvas(new UECanvasMeta()
        {
            Visible = true,
            Width = mCvsSize.x,
            Height = mCvsSize.y,
            Y = -10000,
            EnableChilds = true
        });
        AddChild(mTemplateCanvas);
    }

    protected override DisplayNode GetDisplayNodeAt(int index)
    {
        return mPan.ContainerPanel.FindChildAs<DisplayNode>(node => node.UserTag == index && node.Visible, false);
    }

    public void Init(IItemShowAccesser assesser, int count)
    {
        mAccesser = assesser;
        var rows = count / mColumn;
        if (count % mColumn != 0)
        {
            rows++;
        }
        mPan.Initialize(mCvsSize.x, mCvsSize.y, rows, mColumn, mTemplateCanvas, ScrollPanNodeInit);
    }


    private void ScrollPanNodeInit(int gx, int gy, DisplayNode displayNode)
    {
        var index = gy * mColumn + gx;
        displayNode.UserTag = index;
        UpdateAt(index, displayNode);
    }
     
}