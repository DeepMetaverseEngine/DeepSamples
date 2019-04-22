using DeepCore.GameData.Zone.Helper;
using DeepCore.GameHost.EventTrigger;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor.EventTrigger;
using DeepCore.Reflection;
using TLBattle.Message;
using TLBattle.Server.Plugins.Virtual;

namespace TLBattle.Server.Scene
{
    [DescAttribute("TL扩展事件-通知客户端触发场景事件", "TL扩展")]
    public class TLPlayerTriggerEventtAction : AbstractAction
    {
        [DescAttribute("单位")]
        public UnitValue Unit = new UnitValue.LastKilled();
        [DescAttribute("事件ID")]
        public string EventID = null;

        public override string ToString()
        {
            return string.Format("TL扩展事件-通知客户端家单位{0}触发事件{1}", Unit, EventID);
        }

        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer unit = Unit.GetValue(api, args) as InstancePlayer;

            if (unit != null)
            {
                PlayerTriggerEventB2C evt = new PlayerTriggerEventB2C();
                evt.EventID = EventID;
                unit.queueEvent(evt);
            }
        }
    }

    [DescAttribute("TL扩展事件-指向目标点(箭头）", "TL扩展")]
    public class TLPlayerRadarEventtAction : AbstractAction
    {
        [DescAttribute("单位")]
        public UnitValue Unit = new UnitValue.Trigging();
        [DescAttribute("位置")]
        public PositionValue Position = new PositionValue.VALUE();
        [DescAttribute("距离")]
        public float Distance = 3;
        public override string ToString()
        {
            return string.Format("TL扩展事件-通知{0}显示箭头,距离范围{1}", Unit,Distance);
        } 
        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstancePlayer unit = Unit.GetValue(api, args) as InstancePlayer;
            
            if (unit != null)
            {
                RadarEventB2C evt = new RadarEventB2C
                {
                    X = Position.GetValue(api, args).X,
                    Y = Position.GetValue(api, args).Y,
                    Distance = Distance
                };
                unit.queueEvent(evt);
            }
        }
    }

    [DescAttribute("剧情结束", "TL扩展")]
    public class StoryOverTrigger : AbstractTrigger
    {
        public override string ToString()
        {
            return string.Format("某个单位结束剧情播放");
        }
        public override void Listen(EventTriggerAdapter api, EventArguments args)
        {
            //var args2 = args.Clone();
            InstanceZone iz = api.ZoneAPI;
            InstanceZone.OnHandleObjectActionHandler handle = new InstanceZone.OnHandleObjectActionHandler((unit, action) =>
            {
                if (action is FinishStoryC2B)
                {
                    args.TriggingUnit = unit as InstanceUnit;
                    args.TriggingQuestID = (action as FinishStoryC2B).fileName;
                    api.TestAndDoAction(args);
                }
            });
            iz.OnHandleObjectAction += handle;
            api.OnDisposed += (a) =>
            {
                iz.OnHandleObjectAction -= handle;
            };
        }
    }

    [DescAttribute("剧情播放完成条件", "TL扩展")]
    public class StoryOverCondition : AbstractCondition
    {
        [DescAttribute("剧情名")]
        public StringValue value = new StringValue.VALUE("");
        public override string ToString()
        {
            return string.Format("触发的剧情等于{0}", value.ToString());
        }
        public override bool Test(EventTriggerAdapter api, EventArguments args)
        {
            string msg = value.GetValue(api, args);
            if (args.TriggingQuestID.IndexOf(msg) != -1)
            {
                return true;
            }
            return false;
        }
    }

    [DescAttribute("TL扩展 - 指定攻击单位.", "TL扩展")]
    public class TLUnitFollowAndAtkAction : AbstractAction
    {
        [DescAttribute("单位")]
        public UnitValue Unit = new UnitValue.Editor();
        [DescAttribute("目标")]
        public UnitValue Target = new UnitValue.Editor();
        [DescAttribute("我的眼里都是你")]
        public BooleanValue ForceAtk = new BooleanValue.VALUE();

        public override string ToString()
        {
            return string.Format("手动控制({0})开始攻击单位{1}", Unit, Target);
        }

        public override void DoAction(EventTriggerAdapter api, EventArguments args)
        {
            InstanceGuard unit = Unit.GetValue(api, args) as InstanceGuard;
            InstanceUnit target = Target.GetValue(api, args);
            bool force = ForceAtk.GetValue(api, args);

            if (unit != null && target != null)
            {
                unit.followAndAttack(target, AttackReason.Tracing);
                if (force)
                    (unit.Virtual as TLVirtual).LockAtkUnit(target);
            }
        }
    }
}
