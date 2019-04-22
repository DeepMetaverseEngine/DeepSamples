using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.Helper;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.ZoneEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using ThreeLives.Battle.Data.Data;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Units;
using TLBattle.Server.Plugins.Virtual;
using TLCommonServer.Plugin.Scene;

namespace TLBattle.Server.Scene
{
    public class TLEditorScene : EditorScene
    {
        private TLServerSceneData ServerData;

        private HashMap<int, List<ZoneRegion>> mRebirthRegionMap = new HashMap<int, List<ZoneRegion>>();
        public event CheckVisibleAOIHandler OnCheckVisibleAOI;

        public delegate bool CheckVisibleAOIHandler(InstanceZoneObject src, InstanceZoneObject dst);

        public TLEditorScene(
            EditorTemplates templates,
            InstanceZoneListener listener,
            SceneData data)
            : base(templates, listener, data, System.DateTime.Now.Millisecond)
        {
            ServerData = (data.Properties as TLSceneProperties).ServerSceneData;
            //默认角色等级.
            PlayerLv = 1;
        }

        //-----------------------------------------------------------------------------
        #region PlayerReconnected

        public void playerReconnected(InstancePlayer p)
        {
            if (event_OnPlayerReconnected != null)
                event_OnPlayerReconnected.Invoke(p);
        }
        public delegate void OnPlayerReconnectedHandler(InstancePlayer p);
        private OnPlayerReconnectedHandler event_OnPlayerReconnected;
        public event OnPlayerReconnectedHandler OnPlayerReconnected
        {
            add { event_OnPlayerReconnected += value; }
            remove { event_OnPlayerReconnected -= value; }
        }

        #endregion
        //-----------------------------------------------------------------------------
        protected override void ClearEvents()
        {
            event_OnPlayerReconnected = null;
            base.ClearEvents();
        }

        protected override void Disposing()
        {
            base.Disposing();
        }

        public override bool IsVisibleAOI(InstanceZoneObject src, InstanceZoneObject dst)
        {
            if (src.AoiStatus == dst.AoiStatus)
            {
                var u = dst as InstanceUnit;
                if (u != null)
                {
                    if (dst is TLInstanceFollowUnit)
                    {
                        return (dst as TLInstanceFollowUnit).IsVisibled(src);
                    }

                    //TODO 隐身单位逻辑？
                    if (!u.IsVisible)
                        return false;
                }
                else
                {
                    if (src is TLInstancePlayer && dst is TLInstanceItem)
                    {
                        return CheckItemVisibale((TLInstancePlayer)src, (TLInstanceItem)dst);
                    }

                }
                return true;
            }
            else if (src.AoiStatus != null)
            {
                var aoi_src = src.AoiStatus as PlayerAOI;
                //自己禁止看到别的玩家//
                if (!aoi_src.CanSeeOther)
                {
                    return false;
                }
            }
            else if (dst.AoiStatus != null)
            {
                var aoi_dst = dst.AoiStatus as PlayerAOI;
                //别的禁止看到自己//
                if (!aoi_dst.CanSeeMe)
                {
                    return false;
                }
            }

            var ret = OnCheckVisibleAOI == null || OnCheckVisibleAOI.GetInvocationList().Cast<CheckVisibleAOIHandler>().All(handler => handler.Invoke(src, dst));

            return ret;
        }

        private bool CheckItemVisibale(TLInstancePlayer player, TLInstanceItem item)
        {
            if (player != null && item != null)
            {
                if (item.PrivatePlayerUUID != null)
                {
                    return player.PlayerUUID == item.PrivatePlayerUUID;
                }

                TLItemProperties zp = item.Info.Properties as TLItemProperties;
                if (zp.Type == TLItemProperties.ItemType.Task)
                {
                    var playerVirtual = player.Virtual as TLVirtual_Player;
                    var num = playerVirtual.GetInteractiveNum(item.Info.TemplateID);
                    if (num > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            return false;
        }

        #region Attack.

        public override bool IsAttackable(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            //安全区域.
            if (InSafeArea(src, target, expectTarget, reason, weapon))
            {
                return false;
            }

            //中立单位.
            if (IsNeutrality(src, target, expectTarget, reason, weapon))
            {
                return false;
            }

            //是否无敌.
            if (IsInvincible(src, target, expectTarget, reason, weapon))
            {
                return false;
            }

            return IsAttackable2(src, target, expectTarget, reason, weapon);
        }

        /// <summary>
        /// 是否在安全区域.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="expectTarget"></param>
        /// <param name="reason"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private bool InSafeArea(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            //安全区域检测//
            var sp = (src.Virtual as TLVirtual);
            var dp = (target.Virtual as TLVirtual);

            if (sp.InSafeArea || dp.InSafeArea)
            {
                if (expectTarget == SkillTemplate.CastTarget.Enemy) { return true; }
            }

            return false;
        }

        /// <summary>
        /// 是否为中立单位.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="expectTarget"></param>
        /// <param name="reason"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private bool IsNeutrality(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            var dp = (target.Virtual as TLVirtual);
            return dp.IsNeutrality();
        }

        /// <summary>
        /// 是否无敌.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <param name="expectTarget"></param>
        /// <param name="reason"></param>
        /// <param name="weapon"></param>
        /// <returns></returns>
        private bool IsInvincible(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            if (target.IsInvincible && expectTarget == SkillTemplate.CastTarget.Enemy)
            {
                return true;
            }

            return false;
        }

        private bool IsAttackable2(InstanceUnit src, InstanceUnit target, SkillTemplate.CastTarget expectTarget, AttackReason reason, ITemplateData weapon)
        {
            //AOI.
            if (!IsVisibleAOI(src, target))
            {
                return false;
            }

            //死亡单位且不可鞭尸.
            if (target.IsActive == false && target.CanWhiplashDeadBody == false)
            {
                return false;
            }

            switch (expectTarget)
            {
                case SkillTemplate.CastTarget.Enemy:
                    return (src.Virtual as TLVirtual).IsEnemy(target.Virtual as TLVirtual);
                case SkillTemplate.CastTarget.EveryOne:
                    return true;
                case SkillTemplate.CastTarget.PetForMaster:
                    break;
                case SkillTemplate.CastTarget.Alias:
                case SkillTemplate.CastTarget.AlliesExcludeSelf:
                    return (src.Virtual as TLVirtual).IsAllies(target.Virtual as TLVirtual, false);
                case SkillTemplate.CastTarget.AlliesIncludeSelf:
                    return (src.Virtual as TLVirtual).IsAllies(target.Virtual as TLVirtual, true);
                case SkillTemplate.CastTarget.EveryOneExcludeSelf:
                    return src != target;
                case SkillTemplate.CastTarget.Self:
                    return src == target;
                case SkillTemplate.CastTarget.NA:
                    return false;
                default:
                    return false;
            }


            return true;
        }

        #endregion

        #region 出生点.

        protected override void InitEditRegion(RegionData rdata)
        {
            base.InitEditRegion(rdata);
            //单位复活点.
            ZoneRegion flag = getFlag(rdata.Name) as ZoneRegion;
            if (flag != null && rdata.Abilities != null)
            {
                List<ZoneRegion> temp = null;

                foreach (AbilityData td in rdata.Abilities)
                {
                    if (td is TLRebirthAbilityData)
                    {
                        TLRebirthAbilityData zra = td as TLRebirthAbilityData;

                        if (mRebirthRegionMap.TryGetValue(zra.START_Force, out temp))
                        {
                            temp.Add(flag);
                        }
                        else
                        {
                            temp = new List<ZoneRegion>();
                            temp.Add(flag);
                            mRebirthRegionMap.Add(zra.START_Force, temp);
                        }
                    }
                    else if (td is TLUnitTransportData tp)
                    {
                        var tg = new TLTransportTrigger(tp, this, tp.Name);
                        tg.bindToRegion(flag);
                    }
                }
            }
        }

        /// <summary>
        /// 遍历所有复活点
        /// </summary>
        /// <param name="force"></param>
        /// <param name="action"></param>
        public void ForEachRebirthRegion(int force, Action<ZoneRegion> action)
        {
            List<ZoneRegion> temp = null;

            if (mRebirthRegionMap.TryGetValue(force, out temp))
            {
                for (int i = temp.Count - 1; i >= 0; --i)
                {
                    var rg = temp[i];
                    action(rg);
                }
            }
        }

        /// <summary>
        /// 获得最近的复活点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="regionForce"></param>
        /// <returns></returns>
        public ZoneRegion GetNearestRebirthRegion(float x, float y, byte regionForce)
        {
            using (var list = ListObjectPool<ZoneRegion>.AllocAutoRelease())
            {
                ZoneRegion ret = null;
                float min_value = float.MaxValue;
                float len;

                this.ForEachRebirthRegion(regionForce, (rg) =>
                {
                    if (rg.Enable)
                    {
                        len = DeepCore.Vector.MathVector.getDistanceSquare(x, y, rg.X, rg.Y);
                        if (len < min_value)
                        {
                            min_value = len;
                            ret = rg;
                        }
                    }
                });

                return ret;
            }
        }

        /// <summary>
        /// 获得场景下对应阵营的出生点.
        /// </summary>
        /// <param name="startForce"></param>
        /// <returns></returns>
        public List<ZoneRegion> GetRebirthRegions(byte startForce)
        {
            List<ZoneRegion> ret = null;

            if (mRebirthRegionMap != null)
            {
                mRebirthRegionMap.TryGetValue(startForce, out ret);
            }

            return ret;
        }

        /// <summary>
        /// 创建者等级.
        /// </summary>
        public int PlayerLv { set; get; }
        /// <summary>
        /// 副本人数.
        /// </summary>
        public int PlayerCount { set; get; }
        /// <summary>
        /// 组队本动态难度.
        /// </summary>
        public bool TeamDynamic { set; get; }
        /// <summary>
        /// 场景所属公会ID.
        /// </summary>
        public string GuildUUID { set; get; }
        /// <summary>
        /// 队长等级.
        /// </summary>
        public int TeamLeaderLv { set; get; }
        /// <summary>
        /// 场景线.
        /// </summary>
        public int LineIndex { set; get; }
        #endregion
    }


}
