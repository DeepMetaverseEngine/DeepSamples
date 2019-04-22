using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameEvent;
using DeepCore.GameEvent.Events;
using DeepCore.GameEvent.Lua;
using DeepCore.Reflection;
using DeepCore.Template.SLua;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Utils;
using DeepMMO.Client.Battle;
using SLua;
using TLBattle.Common.Plugins;
using UnityEngine;

namespace EventScript.Client.Events
{
    [Event("获取玩家的UUID", "Sync")]
    public class GetActorUUIDEvent : CustomEvent
    {
        [EventOutput(null, 0)] public string UUID;

        protected override void OnStart()
        {
            base.OnStart();
            UUID = DataMgr.Instance.UserData.RoleID;
            Stop(!string.IsNullOrEmpty(UUID));
        }
    }

    [Event("显示错误消息", "Sync")]
    public class ShowErrorMessageEvent : CustomEvent
    {
        [EventArgument("消息内容", 0)] public string Message;

        protected override void OnStart()
        {
            base.OnStart();
            Message = HZLanguageManager.Instance.GetString(Message);
            GameAlertManager.Instance.ShowNotify("<f color='ffff0000'>" + Message + "</f>");
            Stop(true);
        }
    }

    [Event("显示消息", "Sync")]
    public class ShowMessageEvent : CustomEvent
    {
        [EventArgument("消息内容", 0)] public string Message;

        protected override void OnStart()
        {
            base.OnStart();
            Message = HZLanguageManager.Instance.GetString(Message);
            GameAlertManager.Instance.ShowNotify(Message);
            Stop(true);
        }
    }

    [Event("设置主角是否显示", "Sync")]
    public class SetMainActorVisibleEvent : CustomEvent
    {
        [EventArgument("是否显示", 0)] public bool Visible;

        protected override void OnStart()
        {
            base.OnStart();
            TLBattleScene.Instance.Actor.ObjectRoot.SetActive(Visible);
            BattleInfoBarManager.ActorInfoBar.HideAll(!Visible);
            Stop(true);
        }
    }

    [Event("设置玩家是否显示", "Sync")]
    public class SetPlayerVisibleEvent : CustomEvent
    {
        [EventArgument("roleID", 0)] public string roleId;
        [EventArgument("是否显示", 1)] public bool Visible;

        protected override void OnStart()
        {
            base.OnStart();
            var player = GameSceneMgr.Instance.BattleScene.GetAIPlayer(roleId);
            if (player != null)
            {
                player.ObjectRoot.SetActive(Visible);
                player.bindBehaviour.InfoBar.HideAll(!Visible);
            }
            Stop(true);
        }
    }

    [Event("设置主角是否可控制", "Sync")]
    public class SetActorEnableCtrlEvent : CustomEvent
    {
        [EventArgument("是否可控", 0)] public bool EnableCtrl;

        protected override void OnStart()
        {
            base.OnStart();
            if (EnableCtrl)
                GameUtil.ExitBlockTouch();
            else
                GameUtil.EnterBlockTouch(null, 0);
            Stop(true);
        }
    }

    [Event("设置玩家下坐骑", "Sync")]
    public class SetActorUnMountEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            TLBattleScene.Instance.Actor.UnMount();
            Stop(true);
        }
    }

    [Event("播放镜花水月特效", "Sync")]
    public class PlayRippleEffectEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            FullScreenEffect.Instance.ShowRippleEffect();
            Stop(true);
        }
    }


    [Event("开关模糊特效", "Sync")]
    public class SetEnableBlurEffectEvent : CustomEvent
    {
        [EventArgument(null)] public bool Enable;
        [EventArgument(null)] public float Strength;

        protected override void OnStart()
        {
            base.OnStart();
            var eff = (TLBattleFactory.Instance.Camera as SceneCamera).mainCamera.GetComponent<BlurEffect>();
            eff.enabled = Enable;
            eff.strength = Strength;
            Stop(true);
        }
    }

    [Event("播放全屏Fadeout", "Sync")]
    public class PlayFadeOutEffectEvent : CustomEvent
    {
        [EventArgument(null, 0)] public float Time = -1;

        protected override void OnStart()
        {
            base.OnStart();
            GameGlobal.Instance.overlayEffect.FadeOut(Time);
            Stop(true);
        }
    }

    [Event("播放全屏Fadein", "Sync")]
    public class PlayFadeInEffectEvent : CustomEvent
    {
        [EventArgument(null, 0)] public float Time = -1;

        protected override void OnStart()
        {
            base.OnStart();
            GameGlobal.Instance.overlayEffect.FadeIn(Time);
            Stop(true);
        }
    }

    [Event("对场景内单位添加气泡框", "Sync")]
    public class AddBubbleTalkEvent : CustomEvent
    {
        [EventArgument(null, 0)] public uint ObjectID;
        [EventArgument(null, 1)] public string Content;
        [EventArgument(null, 2)] public float KeepTime;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell != null)
            {
                BubbleChatFrameManager.Instance.ShowBubbleChatFrame(cell, BubbleChatFrame.PRIORITY_NORMAL + "unit:" + ObjectID, Content, KeepTime);
            }

            Stop(cell != null);
        }
    }

    [Event("播放单位特效", "Sync")]
    public class PlayUnitEffectEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;
        [EventArgument("LaunchEffect", 1)] public LaunchEffect Effect;


        [EventOutput("特效ID", 0)] public int EffectID;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell != null)
            {
                EffectID = cell.PlayEffect(Effect, cell.EffectRoot.Position(), cell.EffectRoot.Rotation());
            }

            Stop(cell != null || EffectID == 0);
        }
    }

    [Event("播放玩家特效", "Sync")]
    public class PlayPlayerEffectEvent : CustomEvent
    {
        [EventArgument("PlayerUUID", 0)] public string PlayerUUID;
        [EventArgument("LaunchEffect", 1)] public LaunchEffect Effect;
        [EventOutput("特效ID", 0)] public int EffectID;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetAIPlayer(PlayerUUID);
            if (cell != null)
            {
                EffectID = cell.PlayEffect(Effect, cell.EffectRoot.Position(), cell.EffectRoot.Rotation());
            }

            Stop(cell != null || EffectID == 0);
        }
    }

    [Event("停止一个特效", "Sync")]
    public class StopEffectEvent : CustomEvent
    {
        [EventArgument("特效ID", 0)] public int EffectID;

        protected override void OnStart()
        {
            base.OnStart();
            if (EffectID != 0)
            {
                var eff = FuckAssetObject.Get(EffectID);
                if (eff != null && !eff.IsUnload)
                {
                    var autoEff = eff.GetComponent<EffectAutoDestroy>();
                    if (autoEff)
                    {
                        autoEff.DoDestroy();
                    }
                }
            }

            Stop(true);
        }
    }


    [Event("播放单位动作", "Sync")]
    public class PlayUnitAnimationEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;
        [EventArgument("AnimationName", 1)] public string AnimationName;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell != null)
            {
                cell.animPlayer.Play(AnimationName);
            }

            Stop(cell != null);
        }
    }

    [Event("获取主角ObjectID", "Sync")]
    public class GetActorIDEvent : CustomEvent
    {
        [EventOutput("ObjectID", 0)] public uint ObjectID;

        protected override void OnStart()
        {
            base.OnStart();
            ObjectID = TLBattleScene.Instance.Actor.ObjectID;
            Stop(true);
        }
    }

    [Event("获取主角ActorName", "Sync")]
    public class GetActorNameEvent : CustomEvent
    {
        [EventOutput("ActorName", 0)] public string ActorName;

        protected override void OnStart()
        {
            base.OnStart();
            ActorName = DataMgr.Instance.UserData.Name;
            Stop(true);
        }
    }

    [Event("获取主角阵营", "Sync")]
    public class GetActorForceEvent : CustomEvent
    {
        [EventOutput("Force", 0)] public byte Force;

        protected override void OnStart()
        {
            base.OnStart();
            Force = TLBattleScene.Instance.Actor.Force;
            Stop(true);
        }
    }

    [Event("主角加载完成", "Sync")]
    public class IsActorReadyEvent : CustomEvent
    {
        [EventOutput("是否加载完成", 0)] public bool IsLoadFinish;

        protected override void OnStart()
        {
            base.OnStart();
            IsLoadFinish = BattleLoaderMgr.Instance.IsLoadFinish();
            Stop(true);
        }
    }

    [Event("主角加载完成", "Listen")]
    public class ActorReadyEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            EventManager.Subscribe("Event.Scene.ChangeFinish", OnChangeFinish);
        }

        private void OnChangeFinish(EventManager.ResponseData res)
        {
            Trigger(UnionValue.Null);
        }

        protected override void OnStop()
        {
            base.OnStop();
            EventManager.Unsubscribe("Event.Scene.ChangeFinish", OnChangeFinish);
        }
    }

    [Event("退出该场景", "Listen")]
    public class ActorLeaveMapEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            TLNetManage.Instance.OnZoneChanged += OnZoneChanged;
        }

        private void OnZoneChanged(RPGBattleClient obj)
        {
            Trigger(obj.Enter.s2c_MapTemplateID);
        }

        protected override void OnStop()
        {
            TLNetManage.Instance.OnZoneChanged -= OnZoneChanged;
            base.OnStop();
        }
    }

    [Event("主角是否已死亡", "Sync")]
    public class IsActorDeadEvent : CustomEvent
    {
        [EventOutput("是否死亡", 0)] public bool IsDead;

        protected override void OnStart()
        {
            base.OnStart();
            if (TLBattleScene.Instance.Actor != null)
            {
                IsDead = TLBattleScene.Instance.Actor.IsDead;
                Stop(true);
            }
            else
            {
                Stop(false, "Actor not found");
            }
        }
    }

    [Event("获取Actor当前坐标", "Sync")]
    public class GetActorPostionEvent : CustomEvent
    {
        [EventOutput("X", 0)] public float X;
        [EventOutput("Y", 1)] public float Y;

        protected override void OnStart()
        {
            base.OnStart();
            if (TLBattleScene.Instance.Actor != null)
            {
                X = TLBattleScene.Instance.Actor.X;
                Y = TLBattleScene.Instance.Actor.Y;
                Stop(true);
            }
            else
            {
                Stop(false, "Actor not found");
            }
        }
    }

    [Event("某单位成功进入视野", "Listen")]
    public class UnitInViewEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;

        private bool mEntered;

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell != null)
            {
                if (!mEntered)
                {
                    Trigger(UnionValue.Null);
                    mEntered = true;
                }
            }
            else
            {
                mEntered = false;
            }
        }
    }

    [Event("单位是否已出生", "Listen")]
    public class ActorBirthEvent : CustomEvent
    {
        private bool mIsDead;

        protected override void OnStart()
        {
            base.OnStart();
            mIsDead = TLBattleScene.Instance.Actor.IsDead;
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            if (TLBattleScene.Instance == null || TLBattleScene.Instance.Actor == null)
            {
                Stop(false, "Instance null");
                return;
            }

            if (mIsDead)
            {
                if (!TLBattleScene.Instance.Actor.IsDead)
                {
                    Trigger(UnionValue.Null);
                    mIsDead = false;
                }
            }
            else if (TLBattleScene.Instance.Actor.IsDead)
            {
                mIsDead = true;
            }
        }
    }

    [Event("停止主角自动寻路", "Sync")]
    public class StopActorAutoRunEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            TLBattleScene.Instance.Actor.ClearAutoRun(true);
            Stop(true);
        }
    }

    [Event("主角是否在自动寻路状态", "Sync")]
    public class IsActorAutoRunEvent : CustomEvent
    {
        [EventOutput("IsAutoRun", 0)] public bool IsAutoRun;

        protected override void OnStart()
        {
            base.OnStart();
            IsAutoRun = TLBattleScene.Instance.Actor.IsAutoRun;
            Stop(true);
        }
    }

    [Event("获取主角公会ID", "Sync")]
    public class GetActorGuildUUIDEvent : CustomEvent
    {
        [EventOutput("公会ID", 0)] public string GuildUUID;

        protected override void OnStart()
        {
            base.OnStart();
            GuildUUID = DataMgr.Instance.UserData.GuildId;
            Stop(true);
        }
    }

    public struct UnitInfo
    {
        public string Name;
        public int Hp;
        public int MaxHp;
        public byte Force;

        /// <summary>
        /// Dead,Offline,Normal
        /// </summary>
        public string State;

        public float X;
        public float Y;
        public float Direction;

        public static UnitInfo FromAIUnit(TLAIUnit unit)
        {
            if (unit != null)
            {
                return new UnitInfo
                {
                    Name = unit.Name(),
                    Hp = unit.HP,
                    MaxHp = unit.MaxHP,
                    Force = unit.Force,
                    State = unit.IsDead ? "Dead" : "Normal",
                    X = unit.X,
                    Y = unit.Y,
                    Direction = unit.Direction
                };
            }
            else
            {
                return new UnitInfo {State = "Offline"};
            }
        }
    }

    [Event("获取指定阵营的玩家信息", "Sync")]
    public class GetPlayerInfoByForceEvent : CustomEvent
    {
        [EventArgument("Force", 0)] public byte Force;
        [EventOutput("玩家信息", 0)] public UnitInfo[] Players;

        protected override void OnStart()
        {
            base.OnStart();
            var all = TLBattleScene.Instance.FindBattleObjectsAs<TLAIPlayer>(m => m.Force == Force);
            Players = new UnitInfo[all.Length];
            for (var i = 0; i < all.Length; i++)
            {
                Players[i] = UnitInfo.FromAIUnit(all[i]);
            }

            Stop(true);
        }
    }

    [Event("查找寻路坐标点", "Sync")]
    public class FindPathEvent : CustomEvent
    {
        [EventArgument(null, 0)] public float X;
        [EventArgument(null, 1)] public float Y;
        [EventArgument(null, 2)] public float TargetX;
        [EventArgument(null, 3)] public float TargetY;
        [EventOutput("坐标列表", 0)] public List<Vector2> Ways;

        protected override void OnStart()
        {
            base.OnStart();
            var way = TLBattleScene.Instance.FindPath(X, Y, TargetX, TargetY);
            Ways = new List<Vector2>();
            while (way != null)
            {
                Ways.Add(new Vector2(way.PosX, way.PosY));
                way = way.Next;
            }

            Stop(true);
        }
    }

    [Event("获取指定的玩家信息", "Sync")]
    public class GetPlayerInfoEvent : CustomEvent
    {
        [EventArgument("玩家UUID", 0)] public string PlayerUUID;
        [EventOutput("玩家信息", 0)] public UnitInfo Player;

        protected override void OnStart()
        {
            base.OnStart();
            var p = TLBattleScene.Instance.FindBattleObjectAs<TLAIPlayer>(m => m.PlayerUUID == PlayerUUID);
            Player = UnitInfo.FromAIUnit(p);
            Stop(true);
        }
    }

    [Event("获取玩家所在场景UUID", "Sync")]
    public class GetZoneUUIDEvent : CustomEvent
    {
        [EventOutput("UUID", 0)] public string UUID;

        protected override void OnStart()
        {
            base.OnStart();
            UUID = DataMgr.Instance.UserData.ZoneUUID;
            Stop(true);
        }
    }

    [Event("获取玩家所在场景UUID", "Sync")]
    public class GetObjectPositionEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;
        [EventOutput("X", 0)] public float X;
        [EventOutput("Y", 1)] public float Y;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell == null)
            {
                Stop(false, "object not found");
                return;
            }

            X = cell.X;
            Y = cell.Y;
            Stop(true);
        }
    }

    [Event("获取玩家所在场景UUID", "Sync")]
    public class GetObjectUnityPositionEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;
        [EventOutput("Pos", 0)] public Vector3 Pos;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (cell == null)
            {
                Stop(false, "object not found");
                return;
            }

            Pos = cell.Position;
            Stop(true);
        }
    }

    [Event("2D坐标转换成3D坐标", "Sync")]
    public class GetUnityPosistionEvent : CustomEvent
    {
        [EventArgument("X", 0)] public float X;
        [EventArgument("Y", 1)] public float Y;
        [EventOutput("Pos", 0)] public Vector3 Pos;

        protected override void OnStart()
        {
            base.OnStart();
            Pos = Extensions.ZonePos2NavPos(TLBattleScene.Instance.TotalHeight, X, Y);
            Stop(true);
        }
    }

    [Event("通过ObjectID获取模板ID", "Sync")]
    public class GetUnitTemplateIDEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;
        [EventOutput("TemplateID", 0)] public int TemplateID;

        protected override void OnStart()
        {
            base.OnStart();
            var cell = TLBattleScene.Instance.GetBattleObject(ObjectID) as TLAIUnit;
            if (cell == null)
            {
                Stop(false, "object not found");
                return;
            }

            TemplateID = cell.TemplateID;
            Stop(true);
        }
    }

    [Event("获取Flag坐标", "Sync")]
    public class GetFlagPositionEvent : CustomEvent
    {
        [EventArgument("Flag", 0)] public string Flag;
        [EventOutput("X", 0)] public float X;
        [EventOutput("Y", 1)] public float Y;

        protected override void OnStart()
        {
            base.OnStart();

            var flag = TLBattleScene.Instance.GetZoneFlag(Flag);
            if (flag == null)
            {
                Stop(false, "flag not found");
                return;
            }

            X = flag.X;
            Y = flag.Y;
            Stop(true);
        }
    }

    [Event("获取当前地图模板ID", "Sync")]
    public class GetMapTemplateIDEvent : CustomEvent
    {
        [EventOutput("模板ID", 0)] public int MapTemplateID;

        protected override void OnStart()
        {
            base.OnStart();
            MapTemplateID = DataMgr.Instance.UserData.MapTemplateId;
            Stop(true);
        }
    }

    [Event("获取主角的Avartar map", "Sync")]
    public class GetActorAvartarMapEvent : CustomEvent
    {
        [EventOutput("AvatarMap", 0)] public Dictionary<int, string> AvatarMap;

        protected override void OnStart()
        {
            base.OnStart();
            AvatarMap = new Dictionary<int, string>();
            foreach (var info in DataMgr.Instance.UserData.GetAvatarList())
            {
                AvatarMap.Add(info.Key, info.Value.FileName);
            }

            Stop(true);
        }
    }

    [Event("是否正处于跟随状态", "Sync")]
    public class IsFollowStateEvent : CustomEvent
    {
        [EventArgument("IsFollow", 0)] public bool IsFollow;

        protected override void OnStart()
        {
            base.OnStart();
            IsFollow = TLBattleScene.Instance.Actor.CurGState is TLAIActor.FollowUnitState;
            Stop(true);
        }
    }

    [Event("跟随指定的单位", "Sync")]
    public class FollowUnitEvent : CustomEvent
    {
        [EventArgument("单位实例ID", 0)] public uint ObjectID;
        [EventOutput("是否执行成功", 0)] public bool FollowOK;

        protected override void OnStart()
        {
            base.OnStart();
            var u = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (u != null)
            {
                TLBattleScene.Instance.Actor.ChangeActorState(new TLAIActor.FollowUnitState(TLBattleScene.Instance.Actor, ObjectID));
                FollowOK = true;
            }

            Stop(true);
        }
    }

    [Event("跟随选中的单位", "Sync")]
    public class FollowSelectUnitEvent : FollowUnitEvent
    {
        protected override void OnStart()
        {
            if (TLBattleScene.Instance.Actor.Target != null)
            {
                ObjectID = TLBattleScene.Instance.Actor.Target.TargetId;
            }

            base.OnStart();
        }
    }

    [Event("主角是否在自动战斗状态", "Sync")]
    public class IsActorAutoGuardEvent : CustomEvent
    {
        [EventOutput("IsAutoGuard", 0)] public bool IsAutoGuard;

        protected override void OnStart()
        {
            base.OnStart();
            IsAutoGuard = TLBattleScene.Instance.Actor.CurGState is TLAIActor.AutoAttackState;
            Stop(true);
        }
    }

    [Event("获取并执行Url Lua文件", "Async")]
    public class LoadWWWLuaEvent : LuaBaseEvent
    {
        [EventArgument("Url", 0)] public string Url;
        private WWW mWww;
        [EventOutput("LuaLoad result", 0)]
        public object Obj;
        protected override void OnStart()
        {
            base.OnStart();
            mWww = new WWW(Url);
        }

        protected override void OnUpdate(int ms)
        {
            base.OnUpdate(ms);
            if (mWww.isDone)
            {
                if (mWww.bytes.Length > 0 && LuaSvr.mainState.doBuffer(mWww.bytes, null, out Obj))
                {
                    Stop(true);
                }
                else
                {
                    Stop(false, "load " + Url + "failed");
                }
            }
        }
    }

    [Event("二进制数据转字符串","Sync")]
    public class Bytes2StringEvent : LuaBaseEvent
    {
        [EventArgument("二进制数据",0)]
        public byte[] Bytes;

        [EventOutput("Utf8字符串",0)]
        public string Str;
        protected override void OnStart()
        {
            base.OnStart();
            Str = System.Text.Encoding.UTF8.GetString(Bytes);
            Stop(true);
        }
    }

    [Event("加载二进制数据", "Sync")]
    public class LoadLuaBytesEvent : LuaBaseEvent
    {
        [EventArgument("二进制数据", 0)] public byte[] Bytes;

        [EventOutput("LuaLoad result", 0)] public object Obj;

        protected override void OnStart()
        {
            base.OnStart();
            var ret = LuaSvr.mainState.doBuffer(Bytes, "LoadLuaBytes", out Obj);
            Stop(ret);
            Mgr.LuaSystem.DisposeNext(Obj as IDisposable);
        }
    }
    
}