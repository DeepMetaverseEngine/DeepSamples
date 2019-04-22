using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using System;
using TLBattle.Message;

namespace TLBattle.Client.Client
{
    public class TLClientVirtual_Monster : TLClientVirtual
    {
        public event Action<string> OwnerShipChangeHandle;
        private MonsterVisibleDataB2C mData = null;

        protected override void DoInit(ZoneUnit owner)
        {
            base.DoInit(owner);
            mData = owner.SyncInfo.VisibleInfo as MonsterVisibleDataB2C;
        }

        public override string GetName()
        {
            string ret = null;

            if (mData != null)
            {
                ret = mData.Name;
            }
            else
            {
                ret = mOwner.Info.Name;
            }

            return ret;
        }

        public override int GetLv()
        {
            int ret = 0;

            if (mData != null)
            {
                ret = mData.Lv;
            }
            else
            {
                ret = mOwner.Level;
            }

            return ret;
        }

        /// <summary>
        /// 获得怪物类别 BOSS/普通怪/精英.
        /// </summary>
        /// <returns></returns>
        public MonsterVisibleDataB2C.MonsterType GetMonsterType()
        {
            MonsterVisibleDataB2C.MonsterType ret = MonsterVisibleDataB2C.MonsterType.Normal;
            if (mData != null)
            {
                ret = mData.Type;
            }

            return ret;
        }

        /// <summary>
        /// 获得怪物倾向.
        /// </summary>
        /// <returns></returns>
        public MonsterVisibleDataB2C.AtkTendency GetMonsterTendency()
        {
            MonsterVisibleDataB2C.AtkTendency ret = MonsterVisibleDataB2C.AtkTendency.Active;

            if (mData != null)
            {
                ret = mData.Tendency;
            }

            return ret;
        }

        public string GetOwnerShipUUID()
        {
            if (mData != null)
                return mData.OwnerShipUUID;
            return null;
        }

        public bool IsOwnerShipMonster()
        {
            if (mData != null)
                return mData.ShowOwnerShip;
            return false;
        }

        public int TitleID()
        {
            if (mData != null)
                return mData.TitleID;
            return 0;
        }

        protected override void MOwner_OnDoEvent(ZoneObject obj, ObjectEvent e)
        {
            if (e is MonsterOwnerShipChangeEventB2C)
                OnReceiveMonsterOwnerShipChangeEventB2C(e as MonsterOwnerShipChangeEventB2C);

            base.MOwner_OnDoEvent(obj, e);
        }

        private void OnReceiveMonsterOwnerShipChangeEventB2C(MonsterOwnerShipChangeEventB2C evt)
        {
            if (mData != null)
            {
                mData.OwnerShipUUID = evt.s2c_uuid;
                OwnerShipChangeHandle?.Invoke(evt.s2c_uuid);
            }
        }
    }
}
