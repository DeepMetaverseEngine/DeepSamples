using DeepCore;
using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.TLSkillTemplate.Skills;
using static DeepCore.GameHost.Instance.InstanceUnit;

namespace TLBattle.Server.Plugins.Virtual
{
    /// <summary>
    /// BUFF相关.
    /// </summary>
    public partial class TLVirtual
    {
       
        public void AddTLBuff(BuffTemplate bt, TLVirtual sender, List<UnitBuff> list, int lifeTime, int passTime, byte overlayLevel)
        {
            if (bt == null || list == null) { return; }

            var template = bt;

            for (int buffIndex = 0; buffIndex < list.Count; buffIndex++)
            {
                list[buffIndex].BindTemplate(template);
            }

            AddBuff add = new AddBuff();
            add.template = bt;
            add.sender = sender.mUnit;
            add.tag = list;
            add.lifeTimeMS = lifeTime;
            add.passTimeMS = passTime;
            add.overLayLevel = overlayLevel;
            this.mUnit.addBuff(add);
        }

        public void AddTLBuff(BuffTemplate bt, TLVirtual sender, UnitBuff buff = null)
        {
            if (bt == null) { return; }

            var template = bt;

            if (buff != null) buff.BindTemplate(template);

            AddBuff add = new AddBuff();
            add.template = bt;
            add.sender = sender.mUnit;
            List<UnitBuff> list = new List<UnitBuff>();

            if (buff != null)
                list.Add(buff);

            add.tag = list;

            this.mUnit.addBuff(add);
        }

        public void AddTLBuff(int buffID, TLVirtual sender, List<UnitBuff> list)
        {
            var template = TLBattleSkill.GetBuffTemplate(buffID, false);

            if (template != null)
            {
                AddTLBuff(template, sender, list, 0, 0, 0);
            }
        }

        public void AddTLBuff(int buffID, TLVirtual sender, UnitBuff buff = null)
        {
            var template = TLBattleSkill.GetBuffTemplate(buffID, false);

            if (template != null)
            {
                AddTLBuff(template, sender, buff);
            }
        }

        public void AddTLBuff(TLVirtual sender, List<BuffSnap> buffSnaps)
        {
            if (buffSnaps != null && buffSnaps.Count > 0)
            {
                List<UnitBuff> ubList = null;
                BuffTemplate template = null;
                BuffSnap bs = null;
                UnitBuff ub = null;
                for (int i = 0; i < buffSnaps.Count; i++)
                {

                    bs = buffSnaps[i];
                    template = TLBattleSkill.GetBuffTemplate(bs.BuffID, false);

                    if (template == null)
                        continue;

                    ubList = new List<UnitBuff>();

                    for (int index = 0; index < bs.BuffDataLt.Count; index++)
                    {
                        ub = SkillHelper.Instance.GetUnitBuff(bs.BuffDataLt[index]);
                        ubList.Add(ub);
                    }

                    AddTLBuff(template, sender, ubList, bs.TotalTimeMS, bs.PassTimeMS, bs.OverlayLevel);
                }
            }
        }

        internal void OnBuffBegin(InstanceUnit.BuffState buff, TLVirtual sender)
        {
            UnitBuff_OnBuffBegin(buff, sender);
        }

        internal void OnBuffUpdate(InstanceUnit.BuffState buff, int time)
        {
        }

        internal void OnBuffEnd(InstanceUnit.BuffState buff)
        {
            UnitBuff_OnBuffEnd(buff);
            BuffProperties_OnBuffEnd(buff);
        }

        protected virtual void UnitBuff_OnBuffBegin(InstanceUnit.BuffState buff, TLVirtual sender)
        {
            if (buff.Tag == null) { return; }
            List<UnitBuff> list = buff.Tag as List<UnitBuff>;
            if (list == null || list.Count == 0) { return; }

            for (int listIndex = 0; listIndex < list.Count; listIndex++)
            {
                var ub = list[listIndex];
                ub.BuffBegin(this, sender, buff);
            }
        }

        private void UnitBuff_OnBuffEnd(InstanceUnit.BuffState buff)
        {
            if (buff.Tag == null) { return; }
            List<UnitBuff> list = buff.Tag as List<UnitBuff>;
            if (list == null || list.Count == 0) { return; }

            for (int listIndex = 0; listIndex < list.Count; listIndex++)
            {
                var ub = list[listIndex];
                ub.BuffEnd(this, buff);
            }

            //dispatch buff end evt.
        }

        private void BuffProperties_OnBuffEnd(InstanceUnit.BuffState buff)
        {
            //var prop = buff.Data.Properties as TLBuffProperties;
            //if (prop != null)
            //{
            //    SetSkillLoopList(null);
            //}
        }

        private void SetSkillLoopList(List<TLGameSkillSnap> list)
        {
            this.SkillModule.SkillLoopList = list;
        }

        public List<BuffSnap> GetBuffSnaps()
        {
            List<UnitBuff> tempLt = null;

            BuffState bs = null;

            List<BuffSnap> lt = new List<BuffSnap>();
            BuffSnap buffSnap = null;
            TLBuffData tbd = null;
            int lifeTime = 0;
            TLBuffProperties buffProp = null;
            using (var list = ListObjectPool<BuffState>.AllocAutoRelease())
            {
                this.mUnit.GetAllBuffStatus(list);
                if (list != null)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        bs = list[i];
                        buffProp = bs.Data.Properties as TLBuffProperties;

                        if (buffProp.SaveOnChangeScene == false)//切场景是否保存.
                        {
                            continue;
                        }

                        lifeTime = bs.LifeTimeMS - bs.PassTimeMS;
                        if (lifeTime < 1000)//剩余时间大于1秒再保存.
                        {
                            continue;
                        }
                        buffSnap = new BuffSnap();
                        buffSnap.BuffID = bs.ID;
                        buffSnap.PassTimeMS = bs.PassTimeMS;
                        buffSnap.TotalTimeMS = bs.LifeTimeMS;
                        buffSnap.OverlayLevel = bs.OverlayLevel;
                        buffSnap.BuffDataLt = new List<TLBuffData>();
                        lt.Add(buffSnap);
                        tempLt = bs.Tag as List<UnitBuff>;

                        if (tempLt != null)
                        {
                            for (int index = 0; index < tempLt.Count; index++)
                            {
                                tbd = tempLt[index].ToBuffData();
                                if (tbd != null)
                                {
                                    buffSnap.BuffDataLt.Add(tbd);
                                }
                            }
                        }
                    }
                }
            }

            return lt;
        }

        public bool ContainChangeAvatarBuff()
        {
            bool ret = false;
            ForEachAllBuffStatus((bs) =>
            {
                if (bs.Data.MakeAvatar)
                {
                    ret = true;
                    return true;
                }

                return false;
            });

            return ret;
        }

        private void ForEachAllBuffStatus(Predicate<BuffState> action)
        {
            using (var list = ListObjectPool<BuffState>.AllocAutoRelease())
            {
                this.mUnit.GetAllBuffStatus(list);

                for (int i = 0; i < list.Count; i++)
                {
                    if (action.Invoke(list[i]))
                    {
                        break;
                    }
                }
            }
        }

    }
}
