using DeepCore.GameEvent;
using DeepCore.GameEvent.Events;
using DeepCore.Reflection;
using DeepCore.Unity3D.Utils;
using System.Collections.Generic;
using DeepCore.GameData.Zone;
using TLBattle.Common.Plugins;
using UnityEngine;

namespace EventScript.Client.Events.CG
{
    public abstract class BaseEntityEvent : CustomEvent
    {
        //todo 优化
        protected DramaContext Context
        {
            get { return Mgr.GetObject<DramaContext>("Context"); }
        }

        protected int MapHeight
        {
            get { return TLBattleScene.Instance.TotalHeight; }
        }
    }

    [Event("进入一个游戏世界", "CG.Async")]
    public class EnterGameWorldEvent : BaseEntityEvent
    {
        private DramaContext mContext;

        protected override void OnStart()
        {
            base.OnStart();
            mContext = new DramaContext();
            mContext.AddSystem(new LoadAvatarSystem());
            mContext.AddSystem(new LoadModelSystem());
            mContext.AddSystem(new AnimationSystem());
            mContext.AddSystem(new LocationSystem());
            Mgr.PutObject("Context", mContext);
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            mContext.Update();
        }

        protected override void OnStop()
        {
            base.OnStop();
            mContext.Dispose();
            if (mContext == Context)
            {
                Mgr.PutObject("Context", null);
            }
        }
    }


    [Event("新建一个单位", "CG.Sync")]
    public class CreateUnitEvent : BaseEntityEvent
    {
        //[EventArgument("X", 2)] public float X;
        //[EventArgument("Y", 3)] public float Y;
        //[EventArgument("Direction", 4)] public float Direction;
        [EventOutput("ID", 0)] public int EntityID;

        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.CreatEntity();
            EntityID = entity.ID;
            //entity.LogicLocation.Pos = new Vector2(X, Y);
            //entity.LogicLocation.Direction = Direction;
            Stop(true);
        }
    }

    [Event("加载单位模型和Avatar", "CG.Async")]
    public class LoadUnitEvent : BaseEntityEvent
    {
        [EventArgument("X", 0)] public int EntityID;
        [EventArgument("模型路径", 1)] public string ModelName;
        [EventArgument("模型Avartar", 2)] public Dictionary<int, string> Avatars;

        private UnitEntity mEntity;
        private bool mNextStop;

        protected override void OnStart()
        {
            base.OnStart();
            mEntity = Context.GetEntity(EntityID);
            if (mEntity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            mEntity.Model = new AssetComp(ModelName);
            if (Avatars != null)
            {
                mEntity.Avatar = new AvatarComp();
                foreach (var entry in Avatars)
                {
                    mEntity.Avatar.SetAvatar((TLAvatarInfo.TLAvatar) entry.Key, entry.Value);
                }
            }
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            if (mNextStop)
            {
                Stop(true);
            }
            else if (mEntity.Model.IsLoadOK && (mEntity.Avatar == null || mEntity.Avatar.IsLoadOK))
            {
                mEntity.Animation = new AnimationComp {Name = "n_idle"};
                mNextStop = true;
            }
        }
    }

    [Event("加载单位模型和Avatar", "CG.Sync")]
    public class UnitSetDirectionEvent : BaseEntityEvent
    {
        [EventArgument("EntityID", 0)] public int EntityID;
        [EventArgument("Direction", 1)] public float Direction;

        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.GetEntity(EntityID);
            if (entity == null)
            {
                Stop(false, "not found entity");
            }
            else
            {
                entity.LogicLocation.Direction = Direction;
                Stop(true);
            }
        }
    }

    [Event("新建并加载一个单位", "CG.Async")]
    public class CreateAndLoadUnitEvent : BaseEntityEvent
    {
        [EventArgument("模型路径", 0)] public string ModelName;
        [EventArgument("模型Avartar", 1)] public Dictionary<int, string> Avatars;
        [EventArgument("X", 2)] public float X;
        [EventArgument("Y", 3)] public float Y;
        [EventArgument("Direction", 4)] public float Direction;
        [EventOutput("ID", 0)] public int EntityID;

        private UnitEntity mEntity;
        private bool mNextStop;

        protected override void OnStart()
        {
            base.OnStart();
            var context = Context;
            mEntity = context.CreatEntity();
            mEntity.Model = new AssetComp(ModelName);
            mEntity.LogicLocation.Pos = new Vector2(X, Y);
            mEntity.LogicLocation.Direction = Direction;
            if (Avatars != null)
            {
                mEntity.Avatar = new AvatarComp();
                foreach (var entry in Avatars)
                {
                    mEntity.Avatar.SetAvatar((TLAvatarInfo.TLAvatar) entry.Key, entry.Value);
                }
            }

            EntityID = mEntity.ID;
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            if (mNextStop)
            {
                Stop(true);
            }
            else if (mEntity.Model.IsLoadOK && (mEntity.Avatar == null || mEntity.Avatar.IsLoadOK))
            {
                mEntity.Animation = new AnimationComp {Name = "n_idle"};
                mNextStop = true;
            }
        }
    }

    [Event("单位设置坐标", "CG.Sync")]
    public class UnitSetPostionEvent : BaseEntityEvent
    {
        [EventArgument("X", 0)] public int EntityID;
        [EventArgument("X", 1)] public float X;
        [EventArgument("Y", 2)] public float Y;

        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.GetEntity(EntityID);
            if (entity == null)
            {
                Stop(false, "not found entity");
            }
            else
            {
                entity.LogicLocation.Pos = new Vector2(X, Y);
                Stop(true);
            }
        }
    }


    [Event("单位前往某位置", "CG.Async")]
    public class UnitMoveToEvent : BaseEntityEvent
    {
        [EventArgument("EntityID", 0)] public int EntityID;
        [EventArgument(null, 1)] public float Duration;
        [EventArgument(null, 2)] public float X;
        [EventArgument(null, 3)] public float Y;
        [EventArgument(null, 4)] public string AnimName;

        private UnitEntity mEntity;
        private float mSpeed;
        private Vector2 mTarget;

        protected override void OnStart()
        {
            base.OnStart();
            mEntity = Context.GetEntity(EntityID);
            if (mEntity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            if (string.IsNullOrEmpty(AnimName))
            {
                AnimName = "n_run";
            }

            mTarget = new Vector2(X, Y);
            var distance = Vector2.Distance(mEntity.LogicLocation.Pos, mTarget);

            mSpeed = distance / Duration;
            mEntity.Animation = new AnimationComp {Name = AnimName};

            mEntity.LogicLocation.Direction = Mathf.Atan2(Y - mEntity.LogicLocation.Pos.y, X - mEntity.LogicLocation.Pos.x);
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            if (Duration * 1000 < RunningTimeMS)
            {
                mEntity.LogicLocation.Pos = mTarget;
                mEntity.Animation = new AnimationComp {Name = "n_idle"};
                Stop(true);
            }
            else
            {
                var d = Time.deltaTime * mSpeed;
                mEntity.LogicLocation.Pos = Vector2.MoveTowards(mEntity.LogicLocation.Pos, mTarget, d);
            }
        }
    }

    [Event("单位播放特效", "CG.Sync")]
    public class UnitPlayEffectEvent : BaseEntityEvent
    {
        [EventArgument("EntityID", 0)] public int EntityID;
        [EventArgument("LaunchEffect", 1)] public LaunchEffect Effect;
        [EventOutput("EffectID", 0)] public int EffectID;


        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.GetEntity(EntityID);
            if (entity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            EffectID = RenderSystem.Instance.PlayEffect(Effect, entity.Model.Asset);
            Stop(true);
        }
    }

    [Event("单位停止播放特效", "CG.Sync")]
    public class UnitStopEffectEvent : BaseEntityEvent
    {
        [EventArgument("EntityID", 0)] public int EntityID;
        [EventOutput("EffectID", 0)] public int EffectID;

        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.GetEntity(EntityID);
            if (entity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            RenderSystem.Instance.Unload(EffectID);
            Stop(true);
        }
    }

    [Event("单位播放动作", "CG.Sync")]
    public class UnitPlayAnimationEvent : BaseEntityEvent
    {
        [EventArgument("EntityID", 0)] public int EntityID;
        [EventArgument("动画名称", 1)] public string AnimName;

        protected override void OnStart()
        {
            base.OnStart();
            var entity = Context.GetEntity(EntityID);
            if (entity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            entity.Animation = new AnimationComp {Name = AnimName};
        }
    }

    [Event("单位添加冒泡文字", "CG.Sync")]
    public class AddBubbleTalkEvent : BaseEntityEvent
    {
        [EventArgument(null, 0)] public int EntityID;
        [EventArgument(null, 1)] public string Content;
        [EventArgument(null, 2)] public float KeepTime;

        private BubbleChatFrameUI mBubble;
        private UnitEntity mEntity;

        protected override void OnStart()
        {
            base.OnStart();
            mEntity = Context.GetEntity(EntityID);
            if (mEntity == null)
            {
                Stop(false, "not found entity");
                return;
            }

            mBubble = BubbleChatFrameManager.Instance.CreateBubbleChatFrame("Entity" + EntityID, Content, KeepTime);
            var head = mEntity.Model.Asset.FindNode("Head_Name");
            if (head)
            {
                mBubble.OnPositionSync(head.transform);
            }
            else
            {
                mBubble.OnPositionSync(mEntity.UnityObject.Obj.transform);
            }

            Stop(true);
        }
    }
}