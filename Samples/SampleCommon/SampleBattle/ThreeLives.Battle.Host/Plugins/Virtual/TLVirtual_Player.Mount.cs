using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameHost.Instance;
using TLBattle.Common.Data;
using TLBattle.Message;
using TLBattle.Common.Plugins;
using TLBattle.Server.Message;
using TLBattle.Server.Scene;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    partial class TLVirtual_Player
    {
        /// <summary>
        /// 当前状态无法骑乘.
        /// </summary>
        public const string MOUNT_TIPS_BATTLE = "mount_inbattle";
        /// <summary>
        /// 当前场景无法骑乘.
        /// </summary>
        public const string MOUNT_TIPS_SCENE = "mount_inscence";

        private bool mIsRiding = false;
        private delegate void RidingStatusChangeEvt();
        private RidingStatusChangeEvt event_RidingStatusChangeEvt;
        private event RidingStatusChangeEvt OnRidingStatusChangeEvtHandler
        {
            add { event_RidingStatusChangeEvt += value; }
            remove { event_RidingStatusChangeEvt -= value; }
        }

        //坐骑状态变更.
        public bool IsRiding
        {
            private set
            {
                if (mIsRiding != value)
                {
                    mIsRiding = value;
                    event_RidingStatusChangeEvt?.Invoke();
                }
            }
            get { return mIsRiding; }
        }

        public int MountSpeed
        {
            private set;
            get;
        }

        public TLAvatarInfo MountAvatar
        {
            private set;
            get;
        }


        private void CheckMountStatus(TLUnitProperties prop)
        {
            var ServerData = prop.ServerData;
            this.IsRiding = ServerData.RideStatus;
            this.MountSpeed = ServerData.MountSpeed;

            var AvatarMap = ServerData.AvatarMap;
            if (AvatarMap != null)
            {
                foreach (var item in AvatarMap.Values)
                {
                    if (item.PartTag == TLAvatarInfo.TLAvatar.Ride_Avatar01)
                    {
                        if (!string.IsNullOrEmpty(item.FileName))
                        {
                            this.MountAvatar = item;
                            if (this.IsRiding == false)
                            {
                                TLAvatarInfo avatarInfo = new TLAvatarInfo();
                                avatarInfo.PartTag = this.MountAvatar.PartTag;
                                avatarInfo.FileName = string.Empty;
                                this.mProp.ServerData.AvatarMap.Put((int)avatarInfo.PartTag, avatarInfo);
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void CheckMountSpeed()
        {
            if (this.IsRiding)
            {
                SyncSpeed(this.MountSpeed);
            }
        }

        private int speedPropId = 0;
        private void SyncSpeed(int speed)
        {
            if (speed == 0)
            {
                return;
            }

            if (speed > 0)
            {
                TLPropObject prop = new TLPropObject();
                prop.Type = TLPropObject.ValueType.Value;
                prop.Prop = TLPropObject.PropType.runspeed;
                prop.Value = speed;
                speedPropId = PropModule.AddPropObject(prop);
            }
            else
            {
                PropModule.RemovePropObject(speedPropId);
            }

        }

        #region 上下坐骑.
        //--------------------------------------------------------------------------------------------------
        //流程:
        //1.C->R
        //2.R->C
        //3.R->B
        //4.B->R
        //5.R->C
        //--------------------------------------------------------------------------------------------------

        /// <summary>
        /// 开始召唤坐骑.
        /// </summary>
        /// <param name="timeMS"></param>
        /// <param name="isSummonMount"></param>
        /// <param name="status"></param>
        public void StartSummonMount(bool isSummonMount, bool IsRideByUser)
        {
            //传送状态不处理.
            if (this.mUnit.CurrentState is StatePickObject)
            {
                var s = this.mUnit.CurrentState as StatePickObject;
                if (s.Force == true)
                    return;
            }

            TakeOffMount();

            if (isSummonMount == true)
                SyncMountStatus(isSummonMount, IsRideByUser);
        }


        public void ChangeMount(MountChangedR2B msg)
        {
            PlayAvatarEventB2C toClientMsg = new PlayAvatarEventB2C(this.mUnit.ID, msg.AvatarMap);
            //修改avatarMap并同步到客户端 
            this.mProp.ServerData.AvatarMap.PutAll(msg.AvatarMap);

            this.MountAvatar = msg.AvatarMap[(int)TLAvatarInfo.TLAvatar.Ride_Avatar01];

            var lastSpeed = this.MountSpeed;
            this.MountSpeed = msg.mountSpeed;
            if (this.IsRiding == true)
            {
                SyncPlayerVisibleData();
                this.mUnit.queueEvent(toClientMsg);

                // 改变速度
                SyncSpeed(-lastSpeed);
                SyncSpeed(this.MountSpeed);
            }
        }

        // 坐骑avatar变更 最好卸载TLVirtual.Avatar里面
        public void ChangeAvatar(AvatarChangedR2B msg)
        {
            PlayAvatarEventB2C toClientMsg = new PlayAvatarEventB2C(this.mUnit.ID, msg.AvatarMap);
            //修改avatarMap并同步到客户端 
            this.mProp.ServerData.AvatarMap.PutAll(msg.AvatarMap);

            SyncPlayerVisibleData();
            this.mUnit.queueEvent(toClientMsg);
        }
        private void ChangeMountAvatar(TLAvatarInfo avatarInfo)
        {
            HashMap<int, TLAvatarInfo> AvatarMap = new HashMap<int, TLAvatarInfo>();
            AvatarMap.Add((int)avatarInfo.PartTag, avatarInfo);
            PlayAvatarEventB2C toClientMsg = new PlayAvatarEventB2C(this.mUnit.ID, AvatarMap);
            this.mProp.ServerData.AvatarMap.PutAll(AvatarMap);
            SyncPlayerVisibleData();
            this.mUnit.queueEvent(toClientMsg);
        }
        private void RefMountAvatar()
        {
            if (this.MountAvatar != null)
            {
                if (this.IsRiding)
                {
                    ChangeMountAvatar(this.MountAvatar);
                }
                else
                {
                    TLAvatarInfo avatarInfo = new TLAvatarInfo();
                    avatarInfo.PartTag = this.MountAvatar.PartTag;
                    avatarInfo.FileName = string.Empty;
                    ChangeMountAvatar(avatarInfo);
                }
            }
        }
        /// <summary>
        /// 通知游戏服单位坐骑状态变更.
        /// </summary>
        /// <param name="isSummonMount"></param>
        private void SendSummonMountNotify(bool isSummonMount, string reasonKey, bool IsRideByUser)
        {
            SummonMountEventB2R evt = new SummonMountEventB2R();

            if (this.mUnit is InstancePlayer)
            {
                evt.playerId = (this.mUnit as InstancePlayer).PlayerUUID;
                evt.IsSummonMount = isSummonMount;
                evt.ReasonKey = reasonKey;
                evt.IsRideByUser = IsRideByUser;
                this.mUnit.queueEvent(evt);

                if (this.mUnit.CurrentState is StatePickObject)
                {
                    this.mUnit.startIdle(true);
                }
            }

        }

        /// <summary>
        /// 同步坐骑状态.
        /// </summary>
        /// <param name="isSummonMount"></param>
        private void SyncMountStatus(bool isSummonMount, bool IsRideByUser = false)
        {
            string reason = null;

            if (isSummonMount == true)
            {
                //是否战斗状态，是否忙碌状态，是否在特殊状态.
                if (this.mUnit.IsDead || GetCombatState() != CombatStateChangeEventB2C.BattleStatus.None || IsBusy() || InDebuffStatus())
                {
                    isSummonMount = false;
                    reason = MOUNT_TIPS_BATTLE;
                }
                else
                {
                    SyncSpeed(this.MountSpeed);
                }
            }
            else
            {
                SyncSpeed(-this.MountSpeed);
            }
            IsRiding = isSummonMount;
            //通知游戏服.
            SendSummonMountNotify(isSummonMount, reason, IsRideByUser);

            this.RefMountAvatar();
        }

        /// <summary>
        /// 下坐骑.
        /// </summary>
        public void TakeOffMount()
        {
            if (IsRiding == true)
            {
                SyncMountStatus(false);
            }
        }

        #endregion


    }
}
