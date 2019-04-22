using DeepCore.GUI.Data;
using UnityEngine;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUI;

//todo 重构一下，目前太混乱
public abstract class ItemContainer : HZCanvas
{
    protected IItemShowAccesser mAccesser;

    //点击
    public TouchClickHandle OnItemClick { get; set; }

    protected Vector2 mItemSize;
    private bool mDirtySelect;

    /// <summary>
    /// new, old
    /// </summary>
    public ItemSelectHandler OnItemSingleSelect;

    public ItemSelectHandler OnItemMultiSelect;

    public ItemInitHandler OnItemInit { get; set; }
    public bool ShowBackground { get; set; }

    public bool EnableSelect { get; set; }
    private bool mEnableMultiSelct;

    public bool EnableMultiSelect
    {
        get { return mEnableMultiSelct; }
        set
        {
            if (mEnableMultiSelct != value)
            {
                mEnableMultiSelct = value;
                CleanSelect();
            }
        }
    }

    public bool EnableEmptySelect { get; set; }

    protected abstract DisplayNode GetDisplayNodeAt(int index);

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (mAccesser != null)
        {
            var dirtySelect = mAccesser.GetAndCleanSelectDirty();
            if (dirtySelect != null)
            {
                if (!EnableMultiSelect)
                {
                    var it = mAccesser.GetShowAt(dirtySelect[0]);
                    if (it.IsEmpty)
                    {
                        if (OnItemSingleSelect != null)
                        {
                            OnItemSingleSelect.Invoke(null, mAccesser.FirstSelected);
                        }
                    }
                    else
                    {
                        Select(it);
                    }
                }
                else
                {
                    foreach (var ds in dirtySelect)
                    {
                        var it = mAccesser.GetShowAt(ds);
                        if (!it.IsEmpty)
                        {
                            it.IsSelected = true;
                        }
                        if (OnItemMultiSelect != null)
                        {
                            OnItemMultiSelect.Invoke(it, it);
                        }
                    }
                }
            }
            var dirty = mAccesser.GetAndCleanDirty();
            if (dirty != null)
            {
                foreach (var index in dirty)
                {
                    var displayNode = GetDisplayNodeAt(index);
                    if (displayNode != null)
                    {
                        UpdateAt(index, displayNode);
                    }
                }
            }
        }
    }


    protected virtual void UpdateAt(int index, DisplayNode displayNode)
    {
        if (mAccesser != null)
        {
            var it = mAccesser.GetShowAt(index);
            if (it.Parent != displayNode)
            {
                it.RemoveFromParent(false);

                //remove old
                var old = displayNode.FindChildAs<ItemShow>(t => true);
                if (old != null)
                {
                    old.RemoveFromParent(false);
                }
                displayNode.AddChild(it);
                it.Size2D = mItemSize;
                UIUtils.AdjustAnchor(ImageAnchor.C_C, displayNode, it, Vector2.zero);
                displayNode.UserTag = index;
                OnInitItemShow(it, index);
            }
        }
        else
        {
            var it = displayNode.GetChildAt(0) as ItemShow;
            if (it != null)
            {
                it.ToCache();
            }
        }
    }

    private void OnInitItemShow(ItemShow it, int index)
    {
        it.EnableTouch = true;
        
        it.TouchClick = It_PointerClick;
        if (OnItemInit != null)
        {
            OnItemInit.Invoke(it);
        }
        it.ShowBackground = ShowBackground;
    }

    public bool Select(int index)
    {
        var it = mAccesser.GetShowAt(index);
        return Select(it);
    }

    public bool SelectFirst()
    {
        var it = mAccesser.FindFirstFilled();
        return Select(it);
    }

    public bool Select(ItemShow it)
    {
        if (!EnableSelect || it == null)
        {
            return false;
        }

        if (EnableEmptySelect || !it.IsEmpty)
        {
            if (!EnableMultiSelect)
            {
                var selectItemshow = mAccesser.FirstSelected;
                if (selectItemshow == it)
                {
                    return false;
                }
                if (selectItemshow != null)
                {
                    selectItemshow.IsSelected = false;
                }
                it.IsSelected = true;
                if (OnItemSingleSelect != null)
                {
                    OnItemSingleSelect.Invoke(it, selectItemshow);
                }
            }
            else
            {
                it.IsSelected = !it.IsSelected;
                if (OnItemMultiSelect != null)
                {
                    OnItemMultiSelect.Invoke(it, it);
                }
            }
        }
        return EnableEmptySelect || !it.IsEmpty;
    }

    private void It_PointerClick(DisplayNode sender)
    {
        var it = (ItemShow) sender;
        if (OnItemClick != null)
        {
            OnItemClick.Invoke(it);
        }
        Select(it);
    }

    protected override void OnDisposeEvents()
    {
        base.OnDisposeEvents();
        OnItemClick = null;
        OnItemSingleSelect = null;
        OnItemInit = null;
        OnItemMultiSelect = null;
    }


    protected override void OnDispose()
    {
        base.OnDispose();
        mAccesser = null;
    }


    public void CleanSelect()
    {
        if (!EnableMultiSelect)
        {
            var selectItemshow = mAccesser.FirstSelected;
            if (selectItemshow != null)
            {
                selectItemshow.IsSelected = false;
            }

            if (OnItemSingleSelect != null)
            {
                OnItemSingleSelect.Invoke(null, selectItemshow);
            }
        }
        else
        {
            foreach (var show in mAccesser.AllSelected)
            {
                show.IsSelected = false;
                if (OnItemMultiSelect != null)
                {
                    OnItemMultiSelect.Invoke(show, show);
                }
            }
        }
        mAccesser.GetAndCleanSelectDirty();
    }
}