using System;
using DeepCore;
using DeepCore.GameData;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.Server.Node;
using DeepCore.GameHost.Server.Node.Interface;
using DeepCore.GameHost.ZoneEditor;
using DeepCore.Protocol;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Message;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Plugins.Virtual;
using static DeepCore.GameHost.Server.Node.ZoneNode;
using Action = DeepCore.GameData.Zone.Action;

namespace TLBattle.Host
{
    public class TLZoneNode : ZoneNode
    {
        public TLZoneNode(IServer server, EditorTemplates data_root, BattleCodec codec, ZoneNodeConfig cfg)
            : base(server, data_root, codec, cfg)
        {
            base.Config.GAME_UPDATE_INTERVAL_MS = 1000 / 15;
        }

        protected override void OnEndUpdate()
        {
            base.OnEndUpdate();
            var pc = base.PlayerCount;
            if (pc < 1)
            {
                base.UpdateIntervalMS = 1000 / 1;
            }
            else if (pc == 1)
            {
                base.UpdateIntervalMS = 1000 / 10;
            }
            else
            {
                base.UpdateIntervalMS = 1000 / 15;
            }
        }

        protected override PlayerClient CreatePlayerClient(IPlayer client, InstancePlayer actor)
        {
            var sceneprop = (this.Zone.Data.Properties as TLSceneProperties);
            if (sceneprop != null)
            {
                return new TLPlayerClient(client, actor, this, sceneprop.Sync_InRange, sceneprop.Sync_OutRange);
            }
            else
            {
                return base.CreatePlayerClient(client, actor);
            }

        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override void OnStopped()
        {
            base.OnStopped();
        }


        protected override void OnPlayerRpcInvoke(PlayerClient client, object message)
        {
            var actor = client.Actor;
            var v = actor.Virtual as TLVirtual_Player;

            if (v != null)
            {
                if (message is TLUnitProp)
                {
                    v.OnUnitPropChange(message as TLUnitProp);
                }
                else if (message is TLUnitBaseInfo)
                {
                    v.OnBaseInfoChange(message as TLUnitBaseInfo);
                }
                else if (message is InventorySizeChangeEventR2B)
                {
                    v.OnInventorySizeChange(message as InventorySizeChangeEventR2B);
                }
                else if (message is QuestAcceptedR2B)
                {
                    v.AcceptQuest(message as QuestAcceptedR2B);
                }
                else if (message is QuestDroppedR2B)
                {
                    v.DropQuest(message as QuestDroppedR2B);
                }
                else if (message is QuestCommittedR2B)
                {
                    v.CommitQuest(message as QuestCommittedR2B);
                }
                else if (message is MountSummonedR2B)
                {
                    var msg = message as MountSummonedR2B;
                    v.StartSummonMount(msg.IsSummonMount, msg.IsRideByUser);
                }
                else if (message is AvatarChangedR2B)
                {
                    v.ChangeAvatar(message as AvatarChangedR2B);
                }
                else if (message is MountChangedR2B)
                {
                    v.ChangeMount(message as MountChangedR2B);
                }
                else if (message is SkillChangeEventR2B)
                {
                    v.OnSkillChange(message as SkillChangeEventR2B);
                }
                else if (message is PKModeChangeEventR2B)
                {
                    v.OnPKModeChanged(message as PKModeChangeEventR2B);
                }
                else if (message is RebirthEventR2B)
                {
                    v.PlayerRebirth(message as RebirthEventR2B);
                }
                else if (message is TeamInfoChangeEventR2B)
                {
                    v.OnReceiveTeamInfoChange(message as TeamInfoChangeEventR2B);

                    var msg = message as TeamInfoChangeEventR2B;
                    TLPlayerClient c = this.GetPlayerClient(v.GetPlayerUUID()) as TLPlayerClient;
                    if (c != null)
                        c.CheckTeamMemberAOI(msg.TeamList);

                }
                else if (message is PetChangeEventR2B)
                {
                    v.OnPetChange((message as PetChangeEventR2B).data);
                }
                else if (message is PetBaseInfoChangeEventR2B)
                {
                    v.UpdatePetBaseInfo((message as PetBaseInfoChangeEventR2B).data);
                }
                else if (message is PetLvChangeEventR2B)
                {
                    var msg = (message as PetLvChangeEventR2B);
                    //先该属性，升级加血需要.
                    v.UpdatePetPropInfo(msg.battleProp);
                    v.UpdatePetBaseInfo(msg.baseInfo);
                }
                else if (message is PetPropChangeEventR2B)
                {
                    v.UpdatePetPropInfo((message as PetPropChangeEventR2B).data);
                }
                else if (message is GodSkillChangeEventR2B)
                {
                    v.UpdateGodSkillInfo((message as GodSkillChangeEventR2B).info);
                }
                else if (message is GodChangeEventR2B)
                {
                    v.ChangeGod((message as GodChangeEventR2B).god);
                }
                else if (message is RevengeListChangeEventR2B)
                {
                    v.OnRevengeListChange(message as RevengeListChangeEventR2B);
                }
                else if (message is StartTPEventR2B)
                {
                    v.OnStartTP(message as StartTPEventR2B);
                }
                else if (message is TriggerMedicineEffectR2B)
                {
                    v.OnTriggerMedicineEffect(message as TriggerMedicineEffectR2B);
                }
                else if (message is PKValueChangeEventR2B)
                {
                    v.OnReceivePKValueChangeEventR2B(message as PKValueChangeEventR2B);
                }
                else if (message is EscapeUnmoveableMapR2B)
                {
                    v.OnEscapeUnmoveable();
                }
                else if (message is TitleChangedR2B)
                {
                    var TitleID = ((TitleChangedR2B)message).TitleID;
                    var TitleNameExt = ((TitleChangedR2B)message).TitleNameExt;
                    v.OnTitleChanged(TitleID, TitleNameExt);
                }
                else if (message is PlayerPropChangeR2B)
                {
                    v.OnLogicPropChange(message as PlayerPropChangeR2B);
                }
                else if (message is PlayerAddBuffEvtR2B)
                {
                    v.OnReceivePlayerAddBuffEvtR2B(message as PlayerAddBuffEvtR2B);
                }
                else if(message is PlayerMeridiansChangeEvtR2B)
                {
                    v.OnReceivePlayerMeridiansChangeEvtR2B(message as PlayerMeridiansChangeEvtR2B);
                }
                else if(message is PlayerGuildChaseListChangeR2B)
                {
                    v.OnRecievePlayerGuildChaseListChangeR2B(message as PlayerGuildChaseListChangeR2B);
                }
            }
        }

        protected override bool FilterSendingMessage(PlayerClient client, IMessage msg)
        {

            if (msg is IB2RMessage)
            {
                if (msg is PlayerEvent)
                {
                    // 过滤不是本人的玩家事件 //
                    PlayerEvent pe = msg as PlayerEvent;
                    if (pe.object_id != client.Client.BindingPlayer.Actor.ID)
                    {
                        return false;
                    }
                }

                client.Client.GameServerRpcInvoke(msg);
                return false;
            }
            if (msg is TLBattle.Message.BattleAtkNumberEventB2C)
            {
                var evt = msg as TLBattle.Message.BattleAtkNumberEventB2C;

                if (client.Actor.ID == evt.ObjectID || client.Actor.ID == evt.VisibleUnit)
                    return true;
                else
                    return false;
            }
            else if (msg is TLBattle.Message.BattleSplitHitEventB2C)
            {
                var evt = msg as TLBattle.Message.BattleSplitHitEventB2C;

                if (client.Actor.ID == evt.ObjectID || client.Actor.ID == evt.sendID)
                {
                    return true;
                }
                else
                {
                    return (client.Actor.Virtual as TLVirtual).IsTeamMember(evt.sendID);
                }
            }
            else if (msg is UnitHitEvent)
            {

                //过滤不和自己有关的伤害(队友也与自己有关)
                UnitHitEvent he = msg as UnitHitEvent;
                if (he.AttackerID != client.Actor.ID && he.object_id != client.Actor.ID)
                {
                    if ((client.Actor.Virtual as TLVirtual).IsTeamMember(he.AttackerID))
                    {
                        return true;
                    }

                    var summon = he.Attacker as ISummonedUnit;
                    if (summon != null)
                    {
                        if (summon.SummonerUnit != client.Actor)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //是否是自己队友
                        return false;
                    }
                }
            }

            return base.FilterSendingMessage(client, msg);
        }

        protected override void OnPlayerLeft(PlayerClient client)
        {
            base.OnPlayerLeft(client);
        }

        protected override void OnPlayerDisconnect(PlayerClient client)
        {
            base.OnPlayerDisconnect(client);
        }

        //----------------------------------------------------------------------------------------------------------------------

        //----------------------------------------------------------------------------------------------------------------------
    }


    public class TLPlayerClient : PlayerClient
    {
        private TimeInterval<int> check_out_timer = new TimeInterval<int>(1000);

        public TLPlayerClient(IPlayer client, InstancePlayer actor, ZoneNode node, float look_in_range, float look_out_range)
            : base(client, actor, node, look_in_range, look_out_range)
        {

        }

        public event Action<InstanceZoneObject> OnObjectEnterView;
        public event Action<InstanceZoneObject> OnObjectLeaveView;
        protected override void OnEnterView(InstanceZoneObject obj)
        {
            base.OnEnterView(obj);
            OnObjectEnterView?.Invoke(obj);
        }

        protected override void OnLeaveView(InstanceZoneObject obj)
        {
            base.OnLeaveView(obj);
            OnObjectLeaveView?.Invoke(obj);
        }

        protected override void OnStart()
        {
            base.OnStart();

            CheckTeamMember();
        }

        protected override bool IsLookInRange(InstanceZoneObject obj)
        {
            if (obj is TLInstancePlayer)
            {
                var target = (obj as TLInstancePlayer).VirtualPlayer;
                var pv = (Actor as TLInstancePlayer).VirtualPlayer;
                if (pv.IsTeamMember(target))
                {
                    return true;
                }
            }
            return base.IsLookInRange(obj);
        }


        protected override void UpdateLookOutRange()
        {
            if (check_out_timer.Update(Zone.UpdateIntervalMS))
            {
                base.UpdateLookOutRange();
            }
        }
        protected override bool IsLookOutRange(InstanceZoneObject obj)
        {
            if (obj is TLInstancePlayer)
            {
                var target = (obj as TLInstancePlayer).VirtualPlayer;
                var pv = (Actor as TLInstancePlayer).VirtualPlayer;
                if (pv.IsTeamMember(target))
                {
                    return false;
                }
            }
            return base.IsLookOutRange(obj);
        }


        public void AddObjectInView(InstanceZoneObject obj)
        {
            this.ForceAddObjectInView(obj);
        }

        private void CheckTeamMember()
        {
            //扫描所有已在场景里的单位.
            this.Node.ForEachPlayers((c) =>
            {
                var target = (c.Actor as TLInstancePlayer).VirtualPlayer;
                var pv = (this.Actor as TLInstancePlayer).VirtualPlayer;

                //是自己队伍的单位进入视野.
                if (pv.IsTeamMember(target))
                {
                    this.ForceAddObjectInView(c.Actor);
                    //通知被扫单位符合组队条件.
                    (c as TLPlayerClient).AddObjectInView(this.Actor);
                }
            });
        }

        internal void CheckTeamMemberAOI(List<TeamMemberSnap> teamList)
        {
            if (teamList != null)
            {
                string id = null;
                bool cancel = false;
                InstanceZoneObject obj = null;

                for (int i = 0; i < teamList.Count; i++)
                {
                    id = teamList[i].UUID;
                    //检查队友是否进出视野.
                    if (id != this.Actor.PlayerUUID)
                    {
                        obj = Zone.getPlayerByUUID(id);

                        if (obj != null)
                        {
                            //是否进视野.
                            TryLookInRange(obj, ref cancel);
                        }
                    }
                }
            }
        }
    }

}
