using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Attributes;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost.EventTrigger;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor.EventTrigger;
using DeepCore.Reflection;
using DeepCore.Vector;
using System.Collections.Generic;
using DeepCore;
using DeepCore.GameEvent;
using DeepCore.GameEvent.Lua;
using TLCommonServer.Plugin.Scene;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Units;

namespace CommonServer.Plugin.Editor
{
    [Desc("触发事件库事件", "TL扩展 - 事件库")]
    public class StartEventScript : AbstractAction
    {
        [Desc("事件名称")] public string Name;

        [Desc("是否广播给所有玩家，如果不是玩家事件，后果严重！！！")] public bool SendAllPlayer;
        [Desc("触发玩家-根据事件类型可选参数")] public UnitValue Player = new UnitValue.Trigging();

        [Desc("触发单位-根据事件类型可选参数")] public UnitValue Target = new UnitValue.Trigging();

        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            if (EventManagerFactory.Instance == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                return;
            }

            var mgr = EventManagerFactory.Instance.GetEventManager("Zone", api.ZoneAPI.UUID);
            if (mgr is LuaEventManager luaMgr)
            {
                var unionArg = UnionValue.NewMap;
                object objArg = null;

                unionArg["ZoneUUID"] = api.ZoneAPI.UUID;

                var unit = Target.GetValue(api, args);
                if (unit != null)
                {
                    objArg = unit.ID;
                }

                if (SendAllPlayer)
                {
                    foreach (var player in api.ZoneAPI.AllPlayers)
                    {
                        unionArg["PlayerUUID"] = player.PlayerUUID;
                        luaMgr.CallFunction("EventApi.Task.StartEventByKey", Name, luaMgr.UnionValueToLuaObject(unionArg), objArg);
                    }
                }
                else
                {
                    if (Player.GetValue(api, args) is InstancePlayer p)
                    {
                        unionArg["PlayerUUID"] = p.PlayerUUID;
                        luaMgr.CallFunction("EventApi.Task.StartEventByKey", Name, luaMgr.UnionValueToLuaObject(unionArg), objArg);
                    }
                    else
                    {
                        luaMgr.CallFunction("EventApi.Task.StartEventByKey", Name, luaMgr.UnionValueToLuaObject(unionArg), objArg);
                    }
                }

            }
        }

        public override string ToString()
        {
            return string.Format($"{Player}触发事件库事件{Name}-{Target}(SendAllPlayer:{SendAllPlayer})");
        }
    }

    [Desc("单位是否拥有buff", "单位")]
    public class UnitExistsBuff : BooleanValue
    {
        [DescAttribute("单位")]
        public UnitValue Unit = new UnitValue.Trigging();

        [DescAttribute("Buff模板ID")]
        [TemplateIDAttribute(typeof(BuffTemplate))]
        public int UnitTemplateID = 0;
        public override string ToString()
        {
            return string.Format("单位({0})是否拥有buff状态", Unit);
        }
        
        public override bool GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Unit.GetValue(api, args);
            var s =  unit?.GetBuffByID(UnitTemplateID);
            return s != null;
        }
    }

    [Desc("触发中的玩家添加AOI道具", "TL扩展 - 位面")]
    public class AddItemActionAOI : AbstractAction
    {
        [DescAttribute("玩家")]
        public UnitValue Player = new UnitValue.Trigging();
        [DescAttribute("道具模板ID")]
        [TemplateIDAttribute(typeof(ItemTemplate))]
        public int ItemTemplateID = 0;
        [DescAttribute("道具阵营")]
        public IntegerValue Force = new IntegerValue.VALUE(0);
        [DescAttribute("朝向")]
        public float Direction;
        [DescAttribute("位置")]
        public PositionValue Position = new PositionValue.VALUE();
        [DescAttribute("用户定义名字")]
        public string ItemName;

        public override string ToString()
        {
            return string.Format($"玩家({Player})添加AOI道具({ItemTemplateID})到({Position})");
        }

        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer player = Player.GetValue(api, args) as InstancePlayer;
            Vector2 pos = Position.GetValue(api, args);
            var temp = api.ZoneAPI.Templates.GetItem(ItemTemplateID);
            if (temp != null && pos != null && player != null && player.AoiStatus != null)
            {
                var item = api.ZoneAPI.AddItem(new AddItem()
                {
                    template = temp,
                    name = ItemName,
                    force = (byte)Force.GetValue(api, args),
                    pos = pos,
                    direction = Direction
                });
                item?.setAoiStatus(player.AoiStatus);
            }
        }
    }

    [Desc("触发中的玩家添加AOI单位", "TL扩展 - 位面")]
    public class AddUnitActionAOI : AbstractAction
    {
        [DescAttribute("玩家")]
        public UnitValue Player = new UnitValue.Trigging();

        [DescAttribute("单位模板ID")]
        [TemplateIDAttribute(typeof(UnitInfo))]
        public int UnitTemplateID = 0;

        [DescAttribute("单位等级")]
        [TemplateLevelAttribute]
        public int UnitLevel = 0;

        [DescAttribute("单位阵营")]
        public IntegerValue Force = new IntegerValue.VALUE(0);

        [DescAttribute("用户定义名字(编辑器名字)")]
        public string UnitName;

        [DescAttribute("位置")]
        public PositionValue Position = new PositionValue.VALUE();

        [DescAttribute("朝向")]
        public float Direction;

        [DescAttribute("开始寻路")]
        public FlagValue.EditorPoint StartPoint;

        public override string ToString()
        {
            return string.Format("玩家({0})添加单位({1})到({2})", Player, UnitTemplateID, Position);
        }

        override public void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer player = Player.GetValue(api, args) as InstancePlayer;
            Vector2 pos = Position.GetValue(api, args);
            var temp = api.ZoneAPI.Templates.GetUnit(UnitTemplateID);
            if (temp != null && pos != null && player != null && player.AoiStatus != null)
            {
                InstanceUnit unit = api.ZoneAPI.AddUnit(new AddUnit()
                {
                    info = temp,
                    editor_name = UnitName,
                    force = (byte)Force.GetValue(api, args),
                    level = UnitLevel,
                    pos = pos,
                    direction = Direction
                });
                if (unit != null)
                {
                    unit.setAoiStatus(player.AoiStatus);
                    if (unit is InstanceGuard && StartPoint != null)
                    {
                        InstanceGuard guard = unit as InstanceGuard;
                        InstanceFlag flag = StartPoint.GetValue(api, args);
                        if (flag != null)
                        {
                            guard.attackTo(flag as ZoneWayPoint);
                        }
                    }
                }
            }
        }
    }

    [DescAttribute("触发中的玩家添加AOI区域能力", "TL扩展 - 位面")]
    public class AddSpawnUnitActionAOI : AbstractAction
    {
        [DescAttribute("玩家")]
        public UnitValue Player = new UnitValue.Trigging();

        [DescAttribute("区域")]
        public FlagValue Region = new FlagValue.EditorRegion();

        [ListAttribute(typeof(RegionAbilityData))]
        public List<RegionAbilityData> Abilities = new List<RegionAbilityData>();

        public override string ToString()
        {
            return $"玩家({Player})添加AOI区域{Region}能力";
        }

        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer player = Player.GetValue(api, args) as InstancePlayer;
            ZoneRegion region = Region.GetValue(api, args) as ZoneRegion;
            if (region != null && player != null && player.AoiStatus is PlayerAOI)
            {
                (player.AoiStatus as PlayerAOI).AddRegionAbilities(region, Abilities);
            }
        }
    }

    [DescAttribute("玩家进入位面", "TL扩展 - 位面")]
    public class PlayerEnterAOI : AbstractAction
    {
        [DescAttribute("玩家")]
        public UnitValue Player = new UnitValue.Trigging();

        [DescAttribute("别人可看到自己")]
        public BooleanValue CanSeeMe = new BooleanValue.VALUE(false);

        [DescAttribute("自己可看到别人")]
        public BooleanValue CanSeeOther = new BooleanValue.VALUE(false);

        public override string ToString()
        {
            return $"({Player})进入位面";
        }

        override public void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer player = Player.GetValue(api, args) as InstancePlayer;
            if (player != null)
            {
                PlayerAOI aoi = new PlayerAOI(player, CanSeeMe.GetValue(api, args), CanSeeOther.GetValue(api, args));
                player.setAoiStatus(aoi);
            }
        }
    }

    [DescAttribute("玩家离开位面", "TL扩展 - 位面")]
    public class PlayerLeaveAOI : AbstractAction
    {
        [DescAttribute("玩家")]
        public UnitValue Player = new UnitValue.Trigging();

        public override string ToString()
        {
            return $"({Player})离开位面";
        }

        override public void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer player = Player.GetValue(api, args) as InstancePlayer;
            if (player != null && player.AoiStatus != null)
            {
                player.setAoiStatus(null);
            }
        }
    }


    [DescAttribute("单位是否在位面", "TL扩展 - 位面")]
    public class UnitInAOIStatus : BooleanValue
    {
        [DescAttribute("单位")]
        public UnitValue Player = new UnitValue.Trigging();

        public override string ToString()
        {
            return string.Format("单位({0})是否在位面", Player);
        }

        public override bool GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var player = Player.GetValue(api, args);
            if (player != null)
            {
                return player.AoiStatus != null;
            }
            return false;
        }
    }

    [DescAttribute("单位位面是否一致", "TL扩展 - 位面")]
    public class UnitEqualAOIStatus : BooleanValue
    {
        [DescAttribute("单位")]
        [ListAttribute(typeof(UnitValue))]
        public List<UnitValue> Units = new List<UnitValue>();

        public override string ToString()
        {
            return @"单位位面是否一致";
        }

        public override bool GetValue(IEditorValueAdapter api, EventArguments args)
        {
            ObjectAoiStatus aoi = null;
            for (int index = 0; index < Units.Count; index++)
            {
                var unitValue = Units[index];
                var u = unitValue.GetValue(api, args);
                if (index != 0 && u.AoiStatus != aoi)
                {
                    return false;
                }
                aoi = u.AoiStatus;
            }
            return true;
        }
    }

    [DescAttribute("单位位面宿主", "TL扩展 - 位面")]
    public class UnitAOIOwnerUnit : UnitValue
    {
        [DescAttribute("单位")]
        public UnitValue Unit = new UnitValue.Trigging();

        public override string ToString()
        {
            return string.Format("({0})位面宿主", Unit);
        }

        public override InstanceUnit GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Unit.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).Owner;
            }
            return null;
        }
    }

    [DescAttribute("位面内单位数", "TL扩展 - 位面")]
    public class GetAOIUnitCount : IntegerValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("指定Force")]
        public IntegerValue Force = new IntegerValue.UnitForce();

        [DescAttribute("指定TemplateID")]
        public IntegerValue TemplateID = new IntegerValue.UnitTemplateID();

        public override string ToString()
        {
            return string.Format("({0})位面内单位数", Owner);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).GetUnitCount(
                    Force.GetValue(api, args),
                    TemplateID.GetValue(api, args));
            }
            return 0;
        }
    }

    [DescAttribute("位面内单位数ByForce", "TL扩展 - 位面")]
    public class GetAOIUnitCountByForce : IntegerValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("指定Force")]
        public IntegerValue Force = new IntegerValue.UnitForce();

        public override string ToString()
        {
            return string.Format("({0})位面内(Force={1})单位数", Owner, Force);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).GetUnitCountByForce(
                    Force.GetValue(api, args));
            }
            return 0;
        }
    }

    [DescAttribute("位面内单位数ByTemplateID", "TL扩展 - 位面")]
    public class GetAOIUnitCountByTemplateID : IntegerValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("指定TemplateID")]
        public IntegerValue TemplateID = new IntegerValue.UnitTemplateID();

        public override string ToString()
        {
            return string.Format("({0})位面内(模板={1})单位数", Owner, TemplateID);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).GetUnitCountByTemplateID(
                    TemplateID.GetValue(api, args));
            }
            return 0;
        }
    }

    [DescAttribute("位面内单位数By区域刷新点", "TL扩展 - 位面")]
    public class GetAOIUnitCountByRegion : IntegerValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("区域")]
        public FlagValue Region = new FlagValue.EditorRegion();

        public override string ToString()
        {
            return string.Format("({0})位面内(Region={1})单位数", Owner, Region);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            var region = Region.GetValue(api, args);
            if (unit?.AoiStatus is PlayerAOI && region != null)
            {
                return (unit.AoiStatus as PlayerAOI).GetSpawnAliveCount(region as ZoneRegion);
            }
            return 0;
        }
    }

    [DescAttribute("找到单位所属位面的单位ByTemplateID", "TL扩展 - 位面")]
    public class GetAOIUnitByTemplateID : UnitValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("指定TemplateID")]
        public IntegerValue TemplateID = new IntegerValue.UnitTemplateID();

        public override string ToString()
        {
            return string.Format("({0})位面内(TemplateID={1})的单位", Owner, TemplateID);
        }

        public override InstanceUnit GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).FindUnitByTemplateID(TemplateID.GetValue(api, args));
            }
            return null;
        }
    }

    [DescAttribute("找到单位所属位面的单位ByName", "TL扩展 - 位面")]
    public class GetAOIUnitByName : UnitValue
    {
        [DescAttribute("宿主")]
        public UnitValue Owner = new UnitValue.Trigging();

        [DescAttribute("指定Name")]
        public StringValue Name = new StringValue.VALUE("");

        public override string ToString()
        {
            return string.Format("({0})位面内(Name={1})的单位", Owner, Name);
        }

        public override InstanceUnit GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var unit = Owner.GetValue(api, args);
            if (unit != null && unit.AoiStatus is PlayerAOI)
            {
                return (unit.AoiStatus as PlayerAOI).FindUnitByName(Name.GetValue(api, args));
            }
            return null;
        }
    }

    [Desc("获得单位职业.(0无1翼族2天宫3昆仑4青丘)", "TL扩展")]
    public class GetUnitProType : IntegerValue
    {
        [DescAttribute("单位")]
        public UnitValue Player = new UnitValue.Trigging();

        public override string ToString()
        {
            return string.Format("单位({0})的职业", Player);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var player = Player.GetValue(api, args);
            if (player != null && player is TLInstancePlayer playerUnit)
            {
                var baseInfo = playerUnit.VirtualPlayer.GetBaseInfo();

                if (baseInfo != null)
                    return (int)baseInfo.RolePro;
            }

            return 0;
        }
    }

    [Desc("获得单位性别.(男0女1)", "TL扩展")]
    public class GetUnitGender : IntegerValue
    {
        [DescAttribute("单位")]
        public UnitValue Player = new UnitValue.Trigging();

        public override string ToString()
        {
            return string.Format("单位({0})的性别", Player);
        }

        public override int GetValue(IEditorValueAdapter api, EventArguments args)
        {
            var player = Player.GetValue(api, args);
            if (player != null && player is TLInstancePlayer playerUnit)
            {
                var baseInfo = playerUnit.VirtualPlayer.GetBaseInfo();

                if (baseInfo != null)
                    return (int)baseInfo.Gender;
            }

            return 0;
        }
    }
}