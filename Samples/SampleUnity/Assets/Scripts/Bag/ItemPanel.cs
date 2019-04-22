
using UnityEngine;
using DeepCore.GUI.Data;
using DeepCore;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;

public class ItemPanel : ItemContainer
{
    public HashMap<int, DisplayNode> mShowMap = new HashMap<int, DisplayNode>(5);

    public ItemPanel(Vector2 nodeSize)
    {
        mItemSize = nodeSize;
    }

    public void AddLogicNode(int index, DisplayNode node)
    {
        mShowMap.Add(index, node);
    }

    public void AddNode(int index, Vector2 pos)
    {
        var cvs = new UECanvas();
        cvs.Size2D = mItemSize;
        cvs.Position2D = pos;
        AddChild(cvs);
        AddLogicNode(index, cvs);
    }


    public void Init(IItemShowAccesser assesser)
    {
        mAccesser = assesser;
        foreach (var entry in mShowMap)
        {
            UpdateAt(entry.Key, entry.Value);
        }
    }

    protected override DisplayNode GetDisplayNodeAt(int index)
    {
        return mShowMap.Get(index);
    }
}