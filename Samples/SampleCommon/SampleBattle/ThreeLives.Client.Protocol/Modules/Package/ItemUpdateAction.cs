using System;
using System.Collections.Generic;

namespace TLClient.Protocol.Modules.Package
{
    public static class ItemUpdateActionExtensions
    {
        public static ICollection<ItemUpdateAction> Merger(this ICollection<ItemUpdateAction> actions)
        {
            var ret = new List<ItemUpdateAction>();
            var map = new Dictionary<int, ItemUpdateAction>();
            foreach (var act in actions)
            {
                switch (act.Type)
                {
                    case ItemUpdateAction.ActionType.Init:
                        ret.Add(act);
                        break;
                    case ItemUpdateAction.ActionType.Add:
                        map[act.Index] = act;
                        break;
                    case ItemUpdateAction.ActionType.Remove:
                        map[act.Index] = act;
                        break;
                    case ItemUpdateAction.ActionType.UpdateCount:
                        if (!map.ContainsKey(act.Index))
                        {
                            map[act.Index] = act;
                        }
                        break;
                    case ItemUpdateAction.ActionType.UpdateAttribute:
                        if (act.Package.GetItemAt<IPackageItem>(act.Index) != null)
                        {
                            ret.Add(act);
                        }
                        break;
                    case ItemUpdateAction.ActionType.ChangeSize:
                        ret.Add(act);
                        break;
                }
            }

            ret.AddRange(map.Values);
            return ret;
        }
    }

    public class ItemUpdateAction
    {
        public enum ActionType
        {
            //param 为null index 无效
            Init,

            //param 为null index 位置
            Add,

            //param 为item index 位置
            Remove,

            //param 为from index 位置
            UpdateCount,

            //param 为null index 位置
            UpdateAttribute,

            //param 为null index 无效
            ChangeSize,
        }

        public int Index;
        public ActionType Type;
        public object Param;
        internal readonly BasePackage Package;
        public string Reason;

        public ItemUpdateAction(BasePackage package, ActionType t, int index = -1, object param = null)
        {
            Index = index;
            Type = t;
            Package = package;
            Param = param;
        }

        public ItemUpdateAction(ActionType t, int index = -1, object param = null)
        {
            Index = index;
            Type = t;
            Param = param;
        }

        public ItemUpdateAction()
        {
        }

        public TemplateItemSnap TemplateSnap
        {
            get
            {
                if (Package == null)
                {
                    throw new NotSupportedException();
                }

                switch (Type)
                {
                    case ActionType.Add:
                        var it = Package.GetItemAt<IPackageItem>(Index);
                        return new TemplateItemSnap
                        {
                            Count = it.Count,
                            TemplateID = it.TemplateID
                        };
                    case ActionType.UpdateAttribute:
                        it = Package.GetItemAt<IPackageItem>(Index);
                        return new TemplateItemSnap
                        {
                            Count = 0,
                            TemplateID = it.TemplateID
                        };
                    case ActionType.UpdateCount:
                        it = Package.GetItemAt<IPackageItem>(Index);
                        return new TemplateItemSnap {Count = it.Count - Convert.ToInt64(Param), TemplateID = it.TemplateID};
                    case ActionType.Remove:
                        it = (IPackageItem) Param;
                        return new TemplateItemSnap {Count = it.Count, TemplateID = it.TemplateID};
                }

                return null;
            }
        }
    }
}