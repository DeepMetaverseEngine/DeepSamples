using DeepCore;
using System;
using System.Collections.Generic;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;

public class ItemListener : ClientPackageListener, IItemShowAccesser
{
    public ItemListener(CommonBag bag, bool mergeOutOfStack, int size = 0) : base(bag, mergeOutOfStack, size)
    {
    }

    public Predicate<ItemData> OnMatch;
    public Comparison<ItemData> OnCompare;

    public Action<ItemUpdateAction> OnItemUpdateAction;


    readonly HashMap<int, ItemShow> mItMap = new HashMap<int, ItemShow>();
    readonly HashMap<int, ItemShow> mEmptyItemShows = new HashMap<int, ItemShow>();

    //TODO OnStop和OnStart的时候清理ItemShow的逻辑
    private readonly List<int> mDirty = new List<int>();

    private readonly List<int> mSelectDirty = new List<int>();

    public ItemData GetItemData(ItemShow it)
    {
        foreach (var entry in mItMap)
        {
            if (entry.Value == it)
            {
                return GetItemData(entry.Key);
            }
        }
        return null;
    }

    protected override void Disposing()
    {
        base.Disposing();
        OnItemUpdateAction = null;
        OnMatch = null;
        OnCompare = null;
    }

    public ItemData GetItemData(int index)
    {
        return AllSlots[index].Item as ItemData;
    }

    public ItemShow FindFirstFilled()
    {
        foreach (var slot in AllSlots)
        {
            if (!slot.IsNull)
            {
                return GetShowAt(slot.Index);
            }
        }
        return null;
    }

    public ItemShow GetShowAt(int index)
    {
        var it = mItMap.Get(index);
        if (it != null) return it;
        if (index < Size && !AllSlots[index].IsNull)
        {
            var item = (ItemData) AllSlots[index].Item;
            it = ItemShow.Create(item);
            mItMap.Add(index, it);
            it.Index = index;
            var empty = mEmptyItemShows.RemoveByKey(index);
            if (empty != null)
            {
                empty.ToCache();
            }
        }
        else
        {
            it = mEmptyItemShows.Get(index);
            if (it == null)
            {
                it = ItemShow.Create();
                mEmptyItemShows.Add(index, it);
                it.Index = index;
            }
            it.Status = index < Size ? QuadItemShow.ItemStatus.Unlock : QuadItemShow.ItemStatus.Lock;
        }

        return it;
    }

    public Action<int> OnFilledSizeChange { get; set; }

    protected override void Notify(ItemUpdateAction act)
    {
        switch (act.Type)
        {
            case ItemUpdateAction.ActionType.Init:
                foreach (var entry in mItMap)
                {
                    mDirty.Add(entry.Key);
                }
                foreach (var entry in mEmptyItemShows)
                {
                    mDirty.Add(entry.Key);
                }
                Reset();
                break;
            case ItemUpdateAction.ActionType.Add:
                mDirty.Add(act.Index);
                if (OnFilledSizeChange != null)
                {
                    OnFilledSizeChange(Size - EmptySoltCount);
                }
                break;
            case ItemUpdateAction.ActionType.Remove:
                mDirty.Add(act.Index);
                var removeIt = mItMap.RemoveByKey(act.Index);
                if (removeIt != null)
                {
                    if (removeIt.IsSelected)
                    {
                        mSelectDirty.Add(act.Index);
                    }
                    removeIt.ToCache();
                }
                if (OnFilledSizeChange != null)
                {
                    OnFilledSizeChange(Size - EmptySoltCount);
                }
                UnityEngine.Debug.Log(" remove ---- " + act.Index);
                break;
            case ItemUpdateAction.ActionType.UpdateAttribute:
                var it = mItMap.Get(act.Index);
                if (it != null)
                {
                    it.IsBinded = !GetItemData(act.Index).CanTrade;
                }
                break;
            case ItemUpdateAction.ActionType.UpdateCount:
                it = mItMap.Get(act.Index);
                if (it != null)
                {
                    it.Num = GetItemData(act.Index).Count;
                }
                break;
            case ItemUpdateAction.ActionType.ChangeSize:
                for (var i = (int)act.Param; i < Size; i++)
                {
                    mDirty.Add(i);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void OnUpdatePackageAction(BasePackage package, ICollection<ItemUpdateAction> acts)
    {
        base.OnUpdatePackageAction(package, acts);

        if (OnItemUpdateAction != null && IsRunning)
        {
            foreach (var itemUpdateAction in acts)
            {
                OnItemUpdateAction.Invoke(itemUpdateAction);
            }
        }
    }

    protected override void AddItem(PackageSlot slot)
    {
        if (OnMatch == null || OnMatch.Invoke((ItemData) slot.Item))
        {
            base.AddItem(slot);
        }
    }

    protected override void UpdateCount(PackageSlot slot, uint from)
    {
        if (OnMatch == null || OnMatch.Invoke((ItemData) slot.Item))
        {
            base.UpdateCount(slot, from);
        }
    }

    protected override void UpdateItemAttribute(PackageSlot slot)
    {
        var item = Package.GetItemAt<ItemData>(slot.Index);
        if (OnMatch == null || OnMatch.Invoke(item))
        {
            base.UpdateItemAttribute(slot);
        }
    }

    protected override void UpdateSize()
    {
        base.UpdateSize();
        if (!AutoResize)
        {
            Resize(Package.Size);
        }
    }

    protected override void RemoveItem(PackageSlot slot)
    {
        if (OnMatch == null || OnMatch.Invoke((ItemData) slot.Item))
        {
            base.RemoveItem(slot);
        }
    }

    public int[] GetAndCleanDirty()
    {
        if (mDirty.Count > 0)
        {
            var ret = mDirty.ToArray();
            mDirty.Clear();
            return ret;
        }
        return null;
    }

    public int[] GetAndCleanSelectDirty()
    {
        if (mSelectDirty.Count > 0)
        {
            var ret = mSelectDirty.ToArray();
            mSelectDirty.Clear();
            return ret;
        }
        return null;
    }

    public override bool Match(ItemData item)
    {
        if (OnMatch != null)
        {
            return item != null && OnMatch(item as ItemData);
        }
        return true;
    }


    public void Reset()
    {
        foreach (var entry in mItMap)
        {
            entry.Value.ToCache();
        }
        foreach (var entry in mEmptyItemShows)
        {
            entry.Value.ToCache();
        }
        mItMap.Clear();
        mEmptyItemShows.Clear();
    }

    public override void Stop(bool clean)
    {
        base.Stop(clean);
        mDirty.Clear();
        mSelectDirty.Clear();
        Reset();
        
    }

    protected override int CompareSlot(PackageSlot t1, PackageSlot t2)
    {
        if (OnCompare != null && !t1.IsNull && !t2.IsNull)
        {
            return OnCompare(t1.Item as ItemData, t2.Item as ItemData);
        }
        return base.CompareSlot(t1, t2);
    }

    public ItemShow[] AllSelected
    {
        get
        {
            var ret = new List<ItemShow>();
            foreach (var entry in mItMap)
            {
                if (entry.Value.IsSelected)
                {
                    ret.Add(entry.Value);
                }
            }
            foreach (var entry in mEmptyItemShows)
            {
                if (entry.Value.IsSelected)
                {
                    ret.Add(entry.Value);
                }
            }
            return ret.ToArray();
        }
    }


    public ItemShow FirstSelected
    {
        get
        {
            foreach (var entry in mItMap)
            {
                if (entry.Value.IsSelected)
                {
                    return entry.Value;
                }
            }
            foreach (var entry in mEmptyItemShows)
            {
                if (entry.Value.IsSelected)
                {
                    return entry.Value;
                }
            }
            return null;
        }
    }
}