using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore;

namespace TLClient.Protocol.Modules.Package
{
    public abstract class BasePackage : Disposable
    {
        /// <summary>
        /// 不允许直接使用mDatas，用this[],Size, GetSlot、ForeachSlots、DeepCloneAllSlots、和查询接口中使用
        /// </summary>
        private PackageSlot[] mDatas = new PackageSlot[0];

        private readonly HashSet<IPackageListener> mListeners = new HashSet<IPackageListener>();
        protected readonly List<ItemUpdateAction> BatchActionList = new List<ItemUpdateAction>();
        public int MaxSize { get; set; }
        public bool AutoResize => MaxSize == 0;

        public int Size
        {
            get => mDatas.Length;
            set
            {
                if (MaxSize > 0 && value > MaxSize)
                {
                    throw new ArgumentOutOfRangeException("size > MaxSize");
                }

                var lastSize = mDatas.Length;
                if (mDatas == null)
                {
                    mDatas = new PackageSlot[value];
                }
                else
                {
                    if (value < mDatas.Length)
                    {
                        throw new ArgumentOutOfRangeException("size < oldsize");
                    }

                    Array.Resize(ref mDatas, value);
                }

                for (var i = mDatas.Length - 1; i >= 0; i--)
                {
                    if (mDatas[i].Index == 0)
                    {
                        mDatas[i].Index = i;
                    }
                    else
                    {
                        break;
                    }
                }

                OnSizeChange(lastSize);
            }
        }

        public IPackageItem this[int index]
        {
            get => index < mDatas.Length ? mDatas[index].Item : null;
            protected set => mDatas[index].Item = value;
        }

        public PackageSlot GetSlot(int index)
        {
            return mDatas[index];
        }

        public void ForeachSlots(Action<PackageSlot> act)
        {
            foreach (var slot in mDatas)
            {
                act.Invoke(slot);
            }
        }

        public PackageSlot[] DeepCloneAllSlots()
        {
            return mDatas.Select(item => item.Clone()).ToArray();
        }

        protected override void Disposing()
        {
            mListeners.Clear();
        }

        protected virtual void OnReset()
        {
            InitAllTemplateIndex();
        }

        public void AddListener(IPackageListener listener)
        {
            if (mListeners.Contains(listener))
            {
                return;
            }

            mListeners.Add(listener);
        }

        public void RemoveListener(IPackageListener listener)
        {
            mListeners.Remove(listener);
        }

        private int mBatchListen;

        public void BeginBatchListen()
        {
            if (mBatchListen < 0)
            {
                mBatchListen = 0;
            }

            mBatchListen = mBatchListen + 1;
        }


        public void EndBatchListen()
        {
            mBatchListen = mBatchListen - 1;
            if (mBatchListen == 0)
            {
                if (BatchActionList.Count > 0)
                {
                    foreach (var listener in mListeners)
                    {
                        listener.OnUpdatePackageAction(this, BatchActionList);
                    }

                    BatchActionList.Clear();
                }
            }
        }

        public string CurrentActionReason { get; private set; }

        public class ReasonDisposer : IDisposable
        {
            private readonly BasePackage mPackage;

            public ReasonDisposer(BasePackage p, string reason)
            {
                mPackage = p;
                mPackage.CurrentActionReason = reason;
            }

            public void Dispose()
            {
                mPackage.CurrentActionReason = null;
            }
        }

        private OutSizeLogicHandler mForceOutSizeHandler;


        public IDisposable CreateUpdateReason(string reason)
        {
            return new ReasonDisposer(this, reason);
        }

        private HashMap<string, RecoverSnap> mRecoverMap;

        public void CreateRecoverSnap(string key)
        {
            if (mRecoverMap == null)
            {
                mRecoverMap = new HashMap<string, RecoverSnap>();
            }

            var lastActionCode = BatchActionList.Count > 0 ? BatchActionList[BatchActionList.Count - 1].GetHashCode() : 0;
            var snap = new RecoverSnap {Key = key, StartActionCode = lastActionCode};
            mRecoverMap.Add(key, snap);
        }

        public void RemoveRecoverSnap(string key)
        {
            mRecoverMap.Remove(key);
        }

        public void ApplyRecoverSnap(string recoverKey)
        {
            var info = mRecoverMap?.RemoveByKey(recoverKey);

            if (info?.DiffSrcSlots != null)
            {
                foreach (var entry in info.DiffSrcSlots)
                {
                    mDatas[entry.Key] = entry.Value;
                }

                if (info.StartActionCode == 0) return;
                var removeStart = -1;
                for (var i = BatchActionList.Count - 1; i >= 0; i--)
                {
                    if (BatchActionList[i].GetHashCode() != info.StartActionCode) continue;
                    removeStart = i;
                    break;
                }

                if (removeStart >= 0)
                {
                    BatchActionList.RemoveRange(removeStart, BatchActionList.Count - removeStart);
                }
            }
        }

        public void ApplySlotDiff(PackageSlotDiff df)
        {
            switch (df.Op)
            {
                case PackageSlotDiff.Operator.Add:
                    AddItem(df.Slot.Index, df.Slot.Item);
                    break;
                case PackageSlotDiff.Operator.Increment:
                    IncrementCount(df.Slot.Index, df.Slot.Item.Count);
                    break;
                case PackageSlotDiff.Operator.Decrement:
                    DecrementCount(df.Slot.Index, df.Slot.Item.Count);
                    break;
                case PackageSlotDiff.Operator.Delete:
                    RemoveItem(df.Slot.Index);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ApplySlotDiff(PackageSlotDiff[] diffs)
        {
            foreach (var df in diffs)
            {
                ApplySlotDiff(df);
            }
        }

        public PackageSlotDiff[] DiffWithSlots(PackageSlot[] other)
        {
            if (mDatas.Length != other.Length)
            {
                throw new ArgumentException($"{mDatas.Length} != {other.Length}");
            }

            var diffList = new List<PackageSlotDiff>();
            for (var i = 0; i < Size; i++)
            {
                var diff = mDatas[i].Diff(other[i]);
                if (diff != null)
                {
                    diffList.Add(diff);
                }
            }

            return PackageSlotDiff.MergerTemplateDiffs(diffList.ToArray());
        }

        #region 条件检测

        protected bool CheckItemOut(IPackageItem item, uint count)
        {
            return item != null && count > item.MaxStackCount;
        }

        protected bool CheckOutOfSize(int index)
        {
            if (index >= Size)
            {
                if (AutoResize)
                {
                    Size = index + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        protected bool CheckItemExists(int index)
        {
            return this[index] != null;
        }

        #endregion

        /// <summary>
        /// 道具需要特定位置，返回-1表示自动分配
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int OnSelectSlot(IPackageItem item)
        {
            return -1;
        }

        #region 道具发生变更

        private void TrySaveRecoverSnap(PackageSlot slot)
        {
            if (mRecoverMap != null)
            {
                slot = slot.Clone();
                foreach (var entry in mRecoverMap)
                {
                    if (entry.Value.DiffSrcSlots == null)
                    {
                        entry.Value.DiffSrcSlots = new HashMap<int, PackageSlot>();
                    }

                    if (!entry.Value.DiffSrcSlots.ContainsKey(slot.Index))
                    {
                        entry.Value.DiffSrcSlots.Add(slot.Index, slot);
                    }
                }
            }
        }

        /// <summary>
        /// 通知一个ItemUpdateAction
        /// </summary>
        /// <param name="act"></param>
        private void NotifyUpdateAction(ref ItemUpdateAction act)
        {
            if (string.IsNullOrEmpty(act.Reason) && !string.IsNullOrEmpty(CurrentActionReason))
            {
                act.Reason = CurrentActionReason;
            }

            if (mBatchListen > 0)
            {
                BatchActionList.Add(act);
            }
            else
            {
                foreach (var listener in mListeners)
                {
                    listener.OnUpdatePackageAction(this, new[] {act});
                }
            }
        }

        /// <summary>
        /// 道具已添加
        /// </summary>
        /// <param name="index"></param>
        protected virtual void OnItemAdded(int index)
        {
            var templatePackageItem = this[index];
            if (templatePackageItem != null)
            {
                AddTempalteIndex(templatePackageItem.TemplateID, index);
            }

            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.Add, index);
            NotifyUpdateAction(ref act);
        }

        /// <summary>
        /// 道具已移除
        /// </summary>
        /// <param name="lastItem"></param>
        /// <param name="index"></param>
        protected virtual void OnItemRemoved(IPackageItem lastItem, int index)
        {
            var templatePackageItem = lastItem;
            if (templatePackageItem != null)
            {
                RemoveTemplateIndex(templatePackageItem.TemplateID, index);
            }

            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.Remove, index, lastItem);
            NotifyUpdateAction(ref act);
        }

        /// <summary>
        /// 道具数量发生改变
        /// </summary>
        /// <param name="index"></param>
        /// <param name="from"></param>
        protected virtual void OnItemCountChanged(int index, uint from)
        {
            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.UpdateCount, index, from);
            NotifyUpdateAction(ref act);
        }

        /// <summary>
        /// 道具属性发生改变
        /// </summary>
        /// <param name="index"></param>
        public virtual void OnItemAttributeUpdated(int index)
        {
            var item = GetItemAt<IPackageItem>(index);
            RemoveTemplateIndex(item.TemplateID, index);
            AddTempalteIndex(item.TemplateID, index);

            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.UpdateAttribute, index);
            NotifyUpdateAction(ref act);
        }

        protected virtual void OnSizeChange(int lastSize)
        {
            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.ChangeSize, -1, lastSize);
            NotifyUpdateAction(ref act);
        }

        #endregion

        #region 模板缓存

        /// <summary>
        ///     类别表，列表中的每一个列表都是可以合并的
        /// </summary>
        private readonly List<KeyValuePair<int, List<int>>> mMergerList = new List<KeyValuePair<int, List<int>>>();

        /// <summary>
        ///     模版索引，提升模版查询效率
        /// </summary>
        private readonly HashMap<int, List<int>> mTemplateMap = new HashMap<int, List<int>>();


        public HashMap<int, List<int>> AllTemplateIndexMap => new HashMap<int, List<int>>(mTemplateMap);

        public List<KeyValuePair<int, List<int>>> MergerableList => new List<KeyValuePair<int, List<int>>>(mMergerList);

        public int FindFirstTemplateItemIndex(int templateID)
        {
            var slot = FindFirstSlotAs<IPackageItem>(it => it.TemplateID == templateID);
            return slot.Index;
        }

        private void AddTempalteIndex(int id, int index)
        {
            List<int> list;
            if (!mTemplateMap.TryGetValue(id, out list))
            {
                list = new List<int>(1);
                mTemplateMap.Add(id, list);
            }

            list.Add(index);

            List<int> mergerList = null;
            foreach (var pair in mMergerList)
            {
                if (pair.Key != id) continue;
                var item = this[pair.Value[0]];
                if (GetItemAt<IPackageItem>(index).CompareAttribute(item))
                {
                    mergerList = pair.Value;
                    pair.Value.Add(index);
                    break;
                }
            }

            if (mergerList == null)
            {
                mMergerList.Add(new KeyValuePair<int, List<int>>(id, new List<int> {index}));
            }
        }

        private void RemoveTemplateIndex(int id, int index)
        {
            var list = mTemplateMap.Get(id);
            if (list != null)
            {
                list.Remove(index);
                if (list.Count == 0)
                {
                    mTemplateMap.Remove(id);
                }
            }

            for (var i = 0; i < mMergerList.Count; i++)
            {
                var pair = mMergerList[i];
                if (pair.Key != id || !pair.Value.Remove(index)) continue;
                if (pair.Value.Count == 0)
                {
                    mMergerList.RemoveAt(i);
                }

                break;
            }
        }

        protected void InitAllTemplateIndex()
        {
            mTemplateMap.Clear();
            mMergerList.Clear();
            foreach (var slot in mDatas)
            {
                var item = slot.Item;
                if (item != null)
                {
                    AddTempalteIndex(item.TemplateID, slot.Index);
                }
            }
        }

        #endregion

        #region 整理逻辑

        private readonly DateTime mLastPackUpTime = DateTime.MinValue;
        public float PackUpCoolDownSec { get; set; }
        public float PackUpPassTimeSec => Convert.ToSingle((DateTime.Now - mLastPackUpTime).TotalSeconds);

        public virtual bool CanPackUp()
        {
            return PackUpPassTimeSec > PackUpCoolDownSec;
        }

        public bool PackUp(IComparer<IPackageItem> comparer)
        {
            if (!CanPackUp())
            {
                return false;
            }

            //check mergerList
            //合并
            foreach (var pair in mMergerList)
            {
                for (var i = pair.Value.Count - 1; i >= 0; i--)
                {
                    var itemIndex = pair.Value[i];
                    var item = this[itemIndex];
                    for (var j = 0; j < pair.Value.Count; j++)
                    {
                        var mergerIndex = pair.Value[j];
                        var mergerItem = this[mergerIndex];

                        if (itemIndex == mergerIndex || mergerItem == null)
                        {
                            continue;
                        }

                        var added = mergerItem.MaxStackCount - mergerItem.Count;
                        if (added >= item.Count)
                        {
                            mergerItem.Count += item.Count;
                            item.Count = 0;
                            break;
                        }

                        item.Count -= added;
                        mergerItem.Count += added;
                    }

                    if (item.Count == 0)
                    {
                        this[itemIndex] = null;
                    }
                }
            }

            //排序
            Array.Sort(mDatas, (s1, s2) =>
            {
                if (s1.Item == null && s2.Item == null)
                {
                    return s1.Index.CompareTo(s2.Index);
                }

                if (s1.Item == null)
                {
                    return 1;
                }

                if (s2.Item == null)
                {
                    return -1;
                }

                return comparer?.Compare(s1.Item, s2.Item) ?? s1.Item.CompareTo(s2.Item);
            });

            for (var i = 0; i < mDatas.Length; i++)
            {
                mDatas[i].Index = i;
            }

            OnReset();
            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.Init) {Reason = nameof(PackUp)};
            NotifyUpdateAction(ref act);

            return true;
        }

        #endregion

        #region 查询接口

        public T GetItemAt<T>(int index) where T : class, IPackageItem
        {
            return index >= 0 && index < mDatas.Length ? mDatas[index].Item as T : null;
        }

        public List<int> EmptySlotIndexs
        {
            get
            {
                var emptySlots = new List<int>();
                foreach (var slot in mDatas)
                {
                    if (slot.Item == null)
                    {
                        emptySlots.Add(slot.Index);
                    }
                }

                return emptySlots;
            }
        }

        public PackageSlot[] AllSlots
        {
            get
            {
                var ret = new PackageSlot[mDatas.Length];
                Array.Copy(mDatas, ret, ret.Length);
                return ret;
            }
        }

        public int EmptySlotCount => AutoResize ? int.MaxValue : CountSlotAs(e => e.IsNull);
        public bool IsFull => EmptySlotCount == 0;

        public int NextEmptySlot(int start = 0)
        {
            for (var index = start; index < mDatas.Length; index++)
            {
                var slot = mDatas[index];
                if (slot.Item == null)
                {
                    return slot.Index;
                }
            }

            return -1;
        }

        public PackageSlot[] FindSlotAs<T>(Predicate<T> handler) where T : IPackageItem
        {
            var list = new List<PackageSlot>();
            foreach (var slot in mDatas)
            {
                if ((slot.Item is T) && handler((T) slot.Item))
                {
                    list.Add(slot);
                }
            }

            if (list.Count > 0)
            {
                return list.ToArray();
            }

            return new PackageSlot[0];
        }


        public ICollection<PackageSlot> FindSlotAs(Predicate<PackageSlot> handler)
        {
            var list = new List<PackageSlot>();
            foreach (var slot in mDatas)
            {
                if (handler(slot))
                {
                    list.Add(slot);
                }
            }

            return list;
        }

        public PackageSlot FindFirstSlotAs<T>(Predicate<T> handler) where T : IPackageItem
        {
            foreach (var slot in mDatas)
            {
                if ((slot.Item is T) && handler((T) slot.Item))
                {
                    return slot;
                }
            }

            return PackageSlot.InvaildSlot;
        }

        public ulong CountItemAs<T>(Predicate<T> handler) where T : IPackageItem
        {
            ulong ret = 0;
            foreach (var slot in mDatas)
            {
                if ((slot.Item is T) && handler((T) slot.Item))
                {
                    ret += slot.Item.Count;
                }
            }

            return ret;
        }

        public int CountSlotAs(Predicate<PackageSlot> handler)
        {
            int ret = 0;
            foreach (var slot in mDatas)
            {
                if (handler(slot))
                {
                    ret += 1;
                }
            }

            return ret;
        }

        public T[] FindItemAs<T>(Predicate<T> handler) where T : IPackageItem
        {
            var list = new List<T>();
            foreach (var slot in mDatas)
            {
                if ((slot.Item is T) && handler((T) slot.Item))
                {
                    list.Add((T) slot.Item);
                }
            }

            if (list.Count > 0)
            {
                return list.ToArray();
            }

            return new T[0];
        }

        public T FindFirstItemAs<T>(Predicate<T> handler) where T : IPackageItem
        {
            foreach (var slot in mDatas)
            {
                if (slot.Item is T && handler((T) slot.Item))
                {
                    return (T) slot.Item;
                }
            }

            return default(T);
        }

        public bool Enough(ICollection<CostCondition> conditions)
        {
            return CreateActualProduct(conditions, null).Result != ErrorCode.None;
        }

        public ulong Count(int templateid)
        {
            return CountItemAs<IPackageItem>(it => it.TemplateID == templateid);
        }

        public bool Enough(int templateid, ulong count)
        {
            var list = mTemplateMap.Get(templateid);
            if (list != null)
            {
                //check enough
                foreach (var i in list)
                {
                    if (count <= mDatas[i].Item.Count)
                    {
                        //count = 0;
                        return true;
                    }

                    count -= this[i].Count;
                }
            }

            return false;
        }

        #endregion


        #region 生产接口，会发生背包变化的接口

        /// <summary>
        /// 初始化所有slot
        /// </summary>
        /// <param name="slots"></param>
        /// <returns></returns>
        public virtual bool InitSlots(params PackageSlot[] slots)
        {
            for (var i = 0; i < mDatas.Length; i++)
            {
                mDatas[i].Item = null;
                mDatas[i].Index = i;
            }

            foreach (var slot in slots)
            {
                if (CheckOutOfSize(slot.Index))
                {
                    return false;
                }

                if (slot.Item != null && CheckItemOut(slot.Item, slot.Item.Count))
                {
                    return false;
                }
            }

            foreach (var slot in slots)
            {
                mDatas[slot.Index] = slot;
            }

            OnReset();
            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.Init) {Reason = "Init"};
            NotifyUpdateAction(ref act);

            return true;
        }

        public void Cleanup()
        {
            for (var i = 0; i < Size; i++)
            {
                this[i] = null;
            }

            OnReset();
            var act = new ItemUpdateAction(this, ItemUpdateAction.ActionType.Init) {Reason = "Cleanup"};
            NotifyUpdateAction(ref act);
        }

        public ErrorCode Cost(ICollection<CostCondition> conditions)
        {
            return Product(conditions, null);
        }

        public ErrorCode Cost(int templateid, uint count)
        {
            var snap = new TemplateItemSnap {TemplateID = templateid, Count = count};
            return Cost(new[] {snap.ToCostCondition()});
        }

        public ErrorCode TestAddItem(ICollection<IPackageItem> items)
        {
            var product = CreateActualProduct(null, items);
            if (SupportOutSize(product))
            {
                return ErrorCode.None;
            }

            return product.Result;
        }

        public ErrorCode AddItem(ICollection<IPackageItem> items)
        {
            var product = CreateActualProduct(null, items);
            return Product(product);
        }

        public ErrorCode Product(ICollection<CostCondition> conditions, ICollection<IPackageItem> items)
        {
            var product = CreateActualProduct(conditions, items);
            return Product(product);
        }

        public ErrorCode Product(ICollection<CostCondition> conditions, ICollection<IPackageItem> items, out List<IPackageItem> inItems, out List<IPackageItem> inItemsOutSize, out List<IPackageItem> outItems, out List<IPackageItem> outRemoved)
        {
            var product = CreateActualProduct(conditions, items);
            inItemsOutSize = product.OutOfSize ?? new List<IPackageItem>();
            outItems = new List<IPackageItem>();
            inItems = new List<IPackageItem>();
            outRemoved = new List<IPackageItem>();
            if (product.Added != null)
            {
                foreach (var slot in product.Added)
                {
                    inItems.Add((IPackageItem) slot.Item.Clone());
                }
            }

            if (product.UpdateCount != null)
            {
                foreach (var entry in product.UpdateCount)
                {
                    var last = this[entry.Key];
                    var newItem = (IPackageItem) last.Clone();
                    if (last.Count < entry.Value)
                    {
                        newItem.Count = entry.Value - last.Count;
                        inItems.Add(newItem);
                    }
                    else if (last.Count > entry.Value)
                    {
                        newItem.Count = last.Count - entry.Value;
                        outItems.Add(newItem);
                    }

                    if (entry.Value == 0)
                    {
                        outRemoved.Add(newItem);
                    }
                }
            }
            return Product(product);
        }

        public ErrorCode AddItem(IPackageItem item)
        {
            return AddItem(new[] {item});
        }

        private readonly Stack<OutSizeLogicHandler> mOutSizeLogicHandlers = new Stack<OutSizeLogicHandler>();

        public void PushOutLogicHandler(OutSizeLogicHandler handler)
        {
            mOutSizeLogicHandlers.Push(handler);
        }

        public void PopOutLogicHandler()
        {
            mOutSizeLogicHandlers.Pop();
        }

        #endregion

        #region 内部转换

        private static IPackageItem[] SplitOverMaxStackItem(IPackageItem item)
        {
            if (item.Count <= item.MaxStackCount)
            {
                return null;
            }

            var len = item.Count / item.MaxStackCount;
            if (item.Count % item.MaxStackCount != 0)
            {
                len++;
            }

            var ret = new IPackageItem[len];
            var count = item.Count;
            for (var i = 0; i < ret.Length; i++)
            {
                var next = (IPackageItem) item.Clone();
                next.Count = count > item.MaxStackCount ? item.MaxStackCount : count;
                ret[i] = next;
                count = count - next.Count;
            }

            return ret;
        }

        private static ICollection<IPackageItem> MergerSameItems(ICollection<IPackageItem> items)
        {
            var map = new Dictionary<int, List<IPackageItem>>();
            foreach (var item in items)
            {
                if (map.TryGetValue(item.TemplateID, out var list))
                {
                    var merger = false;
                    foreach (var last in list)
                    {
                        if (last.CompareAttribute(item))
                        {
                            last.Count += item.Count;
                            merger = true;
                            break;
                        }
                    }

                    if (!merger)
                    {
                        list.Add((IPackageItem) item.Clone());
                    }
                }
                else
                {
                    map.Add(item.TemplateID, new List<IPackageItem> {(IPackageItem) item.Clone()});
                }
            }

            var ret = new List<IPackageItem>();
            foreach (var entry in map)
            {
                ret.AddRange(entry.Value);
            }

            return ret;
        }

        #endregion

        #region 生产-消费内部实现细节

        /// <summary>
        /// 测试生产消费， todo 分拆逻辑
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="items"></param>
        /// <param name="outPredicate"></param>
        /// <returns></returns>
        private ActualProduct CreateActualProduct(ICollection<CostCondition> conditions, ICollection<IPackageItem> items)
        {
            var updateCountMap = new Dictionary<int, uint>();
            var ret = new ActualProduct();

            //消耗
            if (conditions != null)
            {
                foreach (var condition in conditions)
                {
                    if (condition.Match == null && condition.SlotIndex < 0 && condition.TemplateID == 0)
                    {
                        continue;
                    }

                    PackageSlot[] slots;
                    if (condition.SlotIndex >= 0)
                    {
                        slots = new[] {GetSlot(condition.SlotIndex)};
                    }
                    else if (condition.Match != null)
                    {
                        slots = FindSlotAs(condition.Match);
                    }
                    else if (condition.TemplateID != 0)
                    {
                        slots = FindSlotAs<IPackageItem>(item => item.TemplateID == condition.TemplateID);
                    }
                    else
                    {
                        slots = null;
                    }

                    if (slots == null)
                    {
                        ret.Result = ErrorCode.NotEnoughItem;
                        break;
                    }

                    var count = condition.Count;
                    foreach (var s in slots)
                    {
                        if (!updateCountMap.TryGetValue(s.Index, out var old))
                        {
                            old = s.Item.Count;
                        }

                        if (count > old)
                        {
                            count -= old;
                            updateCountMap[s.Index] = 0;
                        }
                        else
                        {
                            updateCountMap[s.Index] = old - (uint) count;
                            count = 0;
                            break;
                        }
                    }

                    if (count > 0)
                    {
                        ret.Result = ErrorCode.NotEnoughItem;
                        break;
                    }
                }

                if (ret.Result != ErrorCode.None)
                {
                    return ret;
                }
            }

            //产出
            if (items != null)
            {
                items = MergerSameItems(items);

                //检测合并
                foreach (var item in items)
                {
                    var added = item;
                    if (added == null || added.Count <= 0) continue;
                    var list = mTemplateMap.Get(added.TemplateID);
                    if (list == null) continue;
                    foreach (var i in list)
                    {
                        var slotItem = this[i];
                        if (slotItem.Count >= slotItem.MaxStackCount || !slotItem.CompareAttribute(added))
                        {
                            continue;
                        }

                        if (!updateCountMap.TryGetValue(i, out var last))
                        {
                            last = slotItem.Count;
                        }

                        var maxAdded = slotItem.MaxStackCount - last;
                        if (added.Count > maxAdded)
                        {
                            last = last + maxAdded;
                            added.Count = added.Count - maxAdded;
                        }
                        else
                        {
                            last = last + added.Count;
                            added.Count = 0;
                        }

                        updateCountMap[i] = last;
                        if (added.Count == 0)
                        {
                            break;
                        }
                    }
                }

                //添加多个道具或者Count大于MaxStackCount
                var realAddList = new List<IPackageItem>();
                foreach (var item in items)
                {
                    var s = SplitOverMaxStackItem(item);
                    if (s != null)
                    {
                        realAddList.AddRange(s);
                    }
                    else if (item.Count > 0)
                    {
                        realAddList.Add(item);
                    }
                }

                bool notSupportOutOfSize = false;
                //测试固定位置添加
                var indexs = new int[realAddList.Count];
                var emptyIndexs = EmptySlotIndexs;
                for (var i = 0; i < realAddList.Count; i++)
                {
                    IPackageItem t = realAddList[i];
                    var index = OnSelectSlot(t);
                    if (index > 0)
                    {
                        if (this[index] != null || Array.IndexOf(indexs, index) < 0)
                        {
                            ret.Result = ErrorCode.ExistItem;
                            break;
                        }

                        emptyIndexs.Remove(index);
                        notSupportOutOfSize = true;
                    }
                    else
                    {
                        if (emptyIndexs.Count > 0)
                        {
                            index = emptyIndexs[0];
                            emptyIndexs.RemoveAt(0);
                        }
                        else if (AutoResize)
                        {
                            index = Size + i;
                        }
                    }

                    indexs[i] = index;
                }

                if (ret.Result != ErrorCode.None)
                {
                    return ret;
                }

                var emptyCount = EmptySlotCount;
                //测试空间是否足够
                if (emptyCount >= realAddList.Count)
                {
                    ret.Added = new List<PackageSlot>(realAddList.Count);
                    for (var i = 0; i < realAddList.Count; i++)
                    {
                        ret.Added.Add(new PackageSlot {Index = indexs[i], Item = realAddList[i]});
                    }
                }
                else
                {
                    ret.Result = ErrorCode.OutOfBagSize;
                    if (notSupportOutOfSize) return ret;

                    var outOfSize = realAddList.GetRange(emptyCount, realAddList.Count - emptyCount);
                    var outPredicate = mOutSizeLogicHandlers.Count > 0 ? mOutSizeLogicHandlers.Peek() : null;
                    if (outPredicate == null || !outPredicate.Invoke(true, outOfSize))
                    {
                        return ret;
                    }

                    ret.OutOfSize = outOfSize;
                    ret.Added = new List<PackageSlot>(emptyCount);
                    for (var i = 0; i < emptyCount; i++)
                    {
                        ret.Added.Add(new PackageSlot {Index = indexs[i], Item = realAddList[i]});
                    }
                }
            }

            ret.UpdateCount = updateCountMap;
            return ret;
        }

        public bool SupportOutSize(ActualProduct product)
        {
            return product.Result == ErrorCode.OutOfBagSize && product.OutOfSize != null;
        }

        public ErrorCode AddItem(int index, IPackageItem item)
        {
            if (CheckItemOut(item, item.Count))
            {
                return ErrorCode.OutOfStack;
            }

            if (CheckOutOfSize(index))
            {
                return ErrorCode.OutOfBagSize;
            }

            if (CheckItemExists(index))
            {
                return ErrorCode.ExistItem;
            }

            TrySaveRecoverSnap(mDatas[index]);
            mDatas[index] = new PackageSlot() {Index = index, Item = item};
            //item.OnAdded(this, index);
            OnItemAdded(index);
            return ErrorCode.None;
        }

        public ErrorCode RemoveItem(int index)
        {
            if (CheckOutOfSize(index))
            {
                return ErrorCode.OutOfBagSize;
            }

            if (!CheckItemExists(index))
            {
                return ErrorCode.NotExistItem;
            }

            TrySaveRecoverSnap(mDatas[index]);
            var lastItem = mDatas[index].Item;
            mDatas[index] = new PackageSlot() {Index = index};
            //lastItem.OnRemoved(this, index);
            OnItemRemoved(lastItem, index);
            return ErrorCode.None;
        }

        public ErrorCode UpdateItemCount(int index, uint count)
        {
            if (CheckOutOfSize(index))
            {
                return ErrorCode.OutOfBagSize;
            }

            if (!CheckItemExists(index))
            {
                return ErrorCode.NotExistItem;
            }

            TrySaveRecoverSnap(mDatas[index]);
            var lastItem = this[index];
            var from = lastItem.Count;
            if (CheckItemOut(lastItem, count))
            {
                return ErrorCode.OutOfStack;
            }

            if (count == 0)
            {
                RemoveItem(index);
            }
            else
            {
                lastItem.Count = count;
                if (from != lastItem.Count)
                {
                    OnItemCountChanged(index, from);
                }
            }

            return ErrorCode.None;
        }

        public ErrorCode IncrementCount(int index, uint count)
        {
            var item = GetItemAt<IPackageItem>(index);
            if (item == null)
            {
                return ErrorCode.NotExistItem;
            }

            long newCount = item.Count + count;
            if (newCount > uint.MaxValue)
            {
                return ErrorCode.OutOfStack;
            }

            return UpdateItemCount(index, (uint) newCount);
        }

        public ErrorCode DecrementCount(int index, uint count)
        {
            var item = GetItemAt<IPackageItem>(index);
            if (item == null)
            {
                return ErrorCode.NotExistItem;
            }

            if (item.Count < count)
            {
                return ErrorCode.ItemCountNotEnough;
            }

            var newCount = item.Count - count;
            return UpdateItemCount(index, newCount);
        }


        private ErrorCode Product(ActualProduct info)
        {
            if (info.Result == ErrorCode.OutOfBagSize)
            {
                var outPredicate = mOutSizeLogicHandlers.Count > 0 ? mOutSizeLogicHandlers.Peek() : null;
                if (outPredicate?.Invoke(false, info.OutOfSize) ?? false)
                {
                    info.Result = ErrorCode.None;
                }
            }

            if (info.Result != ErrorCode.None)
            {
                return info.Result;
            }

            var err = ErrorCode.None;

            if (info.Added != null)
            {
                foreach (var slot in info.Added)
                {
                    err = AddItem(slot.Index, slot.Item);
                    if (err != ErrorCode.None)
                    {
                        break;
                    }
                }
            }

            if (info.UpdateCount != null)
            {
                foreach (var entry in info.UpdateCount)
                {
                    err = UpdateItemCount(entry.Key, entry.Value);
                    if (err != ErrorCode.None)
                    {
                        break;
                    }
                }
            }

            if (err != ErrorCode.None)
            {
                throw new Exception("不应该出现的的错误");
            }

            return ErrorCode.None;
        }

        #endregion
    }


    public static class CostConditionExtention
    {
        public static List<CostCondition> ToCostConditionList(this ICollection<TemplateItemSnap> snaps)
        {
            if (snaps == null)
            {
                return new List<CostCondition>();
            }

            return snaps.Select(info => info.ToCostCondition()).ToList();
        }

        public static List<CostCondition> ToCostConditionList(this ICollection<SlotDescription> snaps)
        {
            if (snaps == null)
            {
                return new List<CostCondition>();
            }

            return snaps.Select(info => info.ToCostCondition()).ToList();
        }
    }
}