using DeepCore;
using System;
using System.Collections.Generic;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;

public abstract class ClientPackageListener : Disposable, IPackageListener
{
    public CommonBag Package { get; private set; }
    private bool mLastStartSort;

    protected PackageSlot[] mSlots;

    public PackageSlot[] AllSlots
    {
        get { return mSlots; }
    }

    public ItemData[] AllItems
    {
        get
        {
            var ret = new List<ItemData>();
            for (var i = 0; i < mSlots.Length; i++)
            {
                if (!mSlots[i].IsNull)
                {
                    ret.Add((ItemData) mSlots[i].Item);
                }
            }
            return ret.ToArray();
        }
    }

    public int ItemCount
    {
        get
        {
            var count = 0;
            for (var i = 0; i < mSlots.Length; i++)
            {
                if (!mSlots[i].IsNull)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public IPackageItem this[int index]
    {
        get { return index < mSlots.Length ? mSlots[index].Item : null; }
    }

    public int EmptySoltCount
    {
        get
        {
            var count = 0;
            foreach (var slot in AllSlots)
            {
                if (slot.IsNull)
                {
                    count++;
                }
            }
            return count;
        }
    }

    protected ClientPackageListener(CommonBag bag, bool mergeOutOfStack, int size)
    {
        MergerOutOfMaxStack = mergeOutOfStack;
        AutoResize = size <= 0;

        if (size <= 0)
        {
            AutoResize = true;
            mSlots = new PackageSlot[0];
        }
        else
        {
            Resize(size);
        }
        Package = bag;
        Package.AddListener(this);
    }

    public bool KeepOrder { get; private set; }
    public bool AutoResize { get; private set; }
    public bool MergerOutOfMaxStack { get; private set; }

    public int Size
    {
        get { return mSlots.Length; }
    }

    public bool IsRunning { get; private set; }

    public abstract bool Match(ItemData item);

    protected virtual void UpdateCount(PackageSlot slot, uint from)
    {
        var item = (ItemData) slot.Item;
        if (MergerOutOfMaxStack)
        {
            var added = slot.Item.Count - from;
            for (var index = 0; index < mSlots.Length; index++)
            {
                var subItem = mSlots[index].Item;
                if (subItem == null || subItem.TemplateID != slot.Item.TemplateID || !item.CompareAttribute(subItem))
                {
                    continue;
                }

                subItem.Count += added;
                if (subItem.Count > 0)
                {
                    Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.UpdateCount, Index = index});
                }
                else
                {
                    mSlots[index].Item = null;
                    if (KeepOrder)
                    {
                        Start(mLastStartSort, KeepOrder);
                    }
                    else
                    {
                        Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.Remove, Index = index});
                    }
                }
                break;
            }
        }
        else
        {
            var index = GetLogicIndex(slot.Index);
            mSlots[index].Item.Count = item.Count;
            Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.UpdateCount, Index = index});
        }
    }

    public virtual void OnUpdatePackageAction(BasePackage package, ICollection<ItemUpdateAction> acts)
    {
        if (!IsRunning)
        {
            return;
        }

        foreach (var act in acts)
        {
            switch (act.Type)
            {
                case ItemUpdateAction.ActionType.Init:
                    Start(mLastStartSort, KeepOrder);
                    break;
                case ItemUpdateAction.ActionType.Add:
                    AddItem(new PackageSlot() {Index = act.Index, Item = package.GetItemAt<ItemData>(act.Index).Clone() as ItemData });
                    break;
                case ItemUpdateAction.ActionType.Remove:
                    RemoveItem(new PackageSlot() {Index = act.Index, Item = act.Param as ItemData});
                    break;
                case ItemUpdateAction.ActionType.UpdateCount:
                    UpdateCount(new PackageSlot() {Index = act.Index, Item = package.GetItemAt<ItemData>(act.Index).Clone() as ItemData }, (uint) act.Param);
                    break;
                case ItemUpdateAction.ActionType.UpdateAttribute:
                    UpdateItemAttribute(new PackageSlot() {Index = act.Index});
                    break;
                case ItemUpdateAction.ActionType.ChangeSize:
                    UpdateSize();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }


    /// <summary>
    /// 更新通知
    /// </summary>
    /// <param name="act"></param>
    protected abstract void Notify(ItemUpdateAction act);

    protected override void Disposing()
    {
        Package.RemoveListener(this);
    }

    protected void Resize(int size)
    {
        var lastSize = mSlots == null ? 0 : mSlots.Length;
        Array.Resize(ref mSlots, size);
        for (var i = mSlots.Length - 1; i >= 0; i--)
        {
            if (mSlots[i].Index == 0)
            {
                mSlots[i].Index = i;
            }
        }
        if (lastSize != 0 && !AutoResize)
        {
            var act = new ItemUpdateAction {Type = ItemUpdateAction.ActionType.ChangeSize, Param = lastSize};
            Notify(act);

        }
    }

    protected virtual void AddItem(PackageSlot slot)
    {
        if (MergerOutOfMaxStack)
        {
            for (var i = 0; i < mSlots.Length; i++)
            {
                var subItem = mSlots[i].Item as ItemData;
                if (subItem != null && subItem.TemplateID == slot.Item.TemplateID && subItem.CompareAttribute(slot.Item))
                {
                    subItem.Count += slot.Item.Count;
                    var act = new ItemUpdateAction {Type = ItemUpdateAction.ActionType.UpdateCount, Index = i};
                    Notify(act);
                    return;
                }
            }
        }

        if (KeepOrder)
        {
            Start(mLastStartSort, KeepOrder);
        }
        else
        {
            if (mLastStartSort)
            {
                for (var i = 0; i < mSlots.Length; i++)
                {
                    if (mSlots[i].IsNull)
                    {
                        mSlots[i].Item = slot.Item;
                        var act = new ItemUpdateAction { Index = i, Type = ItemUpdateAction.ActionType.Add };
                        if (!MergerOutOfMaxStack)
                        {
                            mIndexMap.Add(i, slot.Index);
                        }
                        Notify(act);
                        return;
                    }
                }
                if (AutoResize)
                {
                    Resize(mSlots.Length + 1);
                    mSlots[mSlots.Length - 1].Item = slot.Item;
                    var act = new ItemUpdateAction { Index = mSlots.Length - 1, Type = ItemUpdateAction.ActionType.Add };
                    if (!MergerOutOfMaxStack)
                    {
                        mIndexMap.Add(mSlots.Length - 1, slot.Index);
                    }
                    Notify(act);
                }
            }
            else
            {
                if (AutoResize && mSlots.Length <= slot.Index)
                {
                    Resize(slot.Index + 1);
                }
                mSlots[slot.Index] = slot;
                var act = new ItemUpdateAction { Index = slot.Index, Type = ItemUpdateAction.ActionType.Add };
                Notify(act);
            }
        }
    }

    protected virtual void UpdateItemAttribute(PackageSlot slot)
    {
        var item = Package.GetItemAt<ItemData>(slot.Index);
        if (mLastStartSort)
        {
            for (var i = 0; i < mSlots.Length; i++)
            {
                var old = mSlots[i].Item as ItemData;
                if (old != null && old.ID == item.ID)
                {
                    mSlots[i].Item = item;
                    var act = new ItemUpdateAction {Index = i, Type = ItemUpdateAction.ActionType.UpdateAttribute};
                    Notify(act);
                    break;
                }
            }
        }
        else
        {
            mSlots[slot.Index].Item = item;
            var act = new ItemUpdateAction {Index = slot.Index, Type = ItemUpdateAction.ActionType.UpdateAttribute};
            Notify(act);
        }
    }

    protected virtual void UpdateSize()
    {
        
    }

    protected virtual void RemoveItem(PackageSlot slot)
    {
        var item = (ItemData) slot.Item;
        if (MergerOutOfMaxStack)
        {
            for (var index = 0; index < mSlots.Length; index++)
            {
                var subItem = mSlots[index].Item;
                if (subItem == null || subItem.TemplateID != slot.Item.TemplateID || !item.CompareAttribute(subItem))
                {
                    continue;
                }
                if (subItem.Count >= item.Count)
                {
                    subItem.Count -= slot.Item.Count;
                }

                if (subItem.Count > 0)
                {
                    Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.UpdateCount, Index = index});
                }
                else
                {
                    if (KeepOrder)
                    {
                        Start(mLastStartSort, KeepOrder);
                    }
                    else
                    {
                        mSlots[index].Item = null;
                        Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.Remove, Index = index});
                    }
                }
                break;
            }
        }
        else
        {
            if (KeepOrder)
            {
                Start(mLastStartSort, KeepOrder);
            }
            else
            {
                var index = GetLogicIndex(slot.Index);
                mSlots[index].Item = null;
                mIndexMap.Remove(index);
                Notify(new ItemUpdateAction() {Type = ItemUpdateAction.ActionType.Remove, Index = index, Param = slot.Item});
            }
        }
    }


    private void Clean(bool notify)
    {
        mIndexMap.Clear();
        //clean
        for (var i = 0; i < mSlots.Length; i++)
        {
            mSlots[i].Item = null;
        }

        if (notify)
        {
            Notify(new ItemUpdateAction(ItemUpdateAction.ActionType.Init));
        }
    }

    protected HashMap<int, int> mIndexMap = new HashMap<int, int>();

    public void Start(bool sort, bool keepOrder)
    {
        IsRunning = true;
        KeepOrder = keepOrder;
        mLastStartSort = sort;
        Debugger.Log("start " + sort + " " + keepOrder + " " + MergerOutOfMaxStack + " " + GetHashCode());
        Clean(false);
        PackageSlot[] slots;
        if (MergerOutOfMaxStack)
        {
            var ret = new List<PackageSlot>();
            foreach (var pair in Package.MergerableList)
            {
                var firstIndex = pair.Value[0];
                var item = Package.GetItemAt<ItemData>(firstIndex);
                if (Match(item))
                {
                    var newItem = (IPackageItem) item.Clone();
                    //合并整个list
                    for (var i = 1; i < pair.Value.Count; i++)
                    {
                        newItem.Count += Package[pair.Value[i]].Count;
                    }
                    ret.Add(new PackageSlot {Index = firstIndex, Item = newItem});
                }
            }
            slots = ret.ToArray();
        }
        else
        {
            slots = Package.FindSlotAs<ItemData>(Match);
            for (var i = 0; i < slots.Length; i++)
            {
                slots[i].Item = (IPackageItem) slots[i].Item.Clone();
            }
        }

        if (slots.Length == 0)
        {
            Notify(new ItemUpdateAction(ItemUpdateAction.ActionType.Init));
            if (AutoResize)
            {
                Resize(0);
            }
            return;
        }

        if (sort)
        {
            Array.Sort(slots, CompareSlot);
            for (var i = 0; i < slots.Length; i++)
            {
                if (!slots[i].IsNull)
                {
                    mIndexMap.Add(i, slots[i].Index);
                }
                slots[i].Index = i;
            }
            if (AutoResize)
            {
                Resize(slots.Length);
            }
        }
        else
        {
            var maxIndex = 0;
            foreach (var slot in slots)
            {
                if (!slot.IsNull)
                {
                    mIndexMap.Add(slot.Index, slot.Index);
                }
                maxIndex = Math.Max(maxIndex, slot.Index);
            }
            if (AutoResize)
            {
                Resize(maxIndex + 1);
            }
        }


        for (var i = 0; i < slots.Length; i++)
        {
            mSlots[slots[i].Index].Item = slots[i].Item;
        }

        Notify(new ItemUpdateAction(ItemUpdateAction.ActionType.Init));
    }

    public virtual void Stop(bool clean)
    {
        Debugger.Log("stop " + GetHashCode());
        IsRunning = false;
        if (clean)
        {
            Clean(true);
        }
    }

    public int GetSourceIndex(int index)
    {
        int ret;
        if (mIndexMap.TryGetValue(index, out ret))
        {
            return ret;
        }
        if (!mLastStartSort)
        {
            return index;
        }
        return -1;
    }

    public int GetLogicIndex(int sourceIndex)
    {
        foreach (var entry in mIndexMap)
        {
            if (entry.Value == sourceIndex)
            {
                return entry.Key;
            }
        }
        if (!mLastStartSort)
        {
            return sourceIndex;
        }
        return -1;
    }

    protected virtual int CompareSlot(PackageSlot t1, PackageSlot t2)
    {
        return t1.Index.CompareTo(t2.Index);
    }

    public ItemData[] FindItemAs(Predicate<ItemData> handler)
    {
        var ret = new List<ItemData>();
        foreach (var slot in mSlots)
        {
            var it = slot.Item as ItemData;
            if (it != null && handler(it))
            {
                ret.Add(it);
            }
        }
        return ret.ToArray();
    }

    public ItemData FindFirstItemAs(Predicate<ItemData> handler)
    {
        foreach (var slot in mSlots)
        {
            var it = slot.Item as ItemData;
            if (it != null && handler(it))
            {
                return it;
            }
        }
        return null;
    }


    public PackageSlot[] FindSlotAs(Predicate<ItemData> handler)
    {
        var ret = new List<PackageSlot>();
        foreach (var slot in mSlots)
        {
            var it = slot.Item as ItemData;
            if (it != null && handler(it))
            {
                ret.Add(slot);
            }
        }
        return ret.ToArray();
    }

    public PackageSlot FindFirstSlotAs(Predicate<ItemData> handler)
    {
        foreach (var slot in mSlots)
        {
            var it = slot.Item as ItemData;
            if (it != null && handler(it))
            {
                return slot;
            }
        }
        return PackageSlot.InvaildSlot;
    }
}