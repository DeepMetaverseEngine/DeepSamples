using DeepCore.GameData.Zone;
using DeepCore.GameHost.Instance;
using DeepCore.GameHost.Instance.Abilities;
using System.Collections.Generic;
using System.Linq;
using ThreeLives.Battle.Data.Data;
using TLBattle.Server.Plugins.Units;
using TLBattle.Common.Data;
namespace TLBattle.Server.Scene
{
    public class TLTransportTrigger : Ability
    {
        public string NextPositionName;
        public UnitInfo.UnitType AcceptUnitType = UnitInfo.UnitType.TYPE_PLAYER;
        public bool AcceptUnitTypeForAll = false;
        public byte AcceptForce = 0;
        public bool AcceptForceForAll = false;
        public LaunchEffect TransportEffect;

        public int AreaCheckPlayerCount = 0;
        public bool AreaCheckForceForAll;
        public byte AreaCheckForce;


        private InstanceFlag mNext;
        private ZoneRegion mOwner;

        public delegate bool SelectHandler(ZoneRegion region, InstanceUnit obj);
        private SelectHandler mOnSelect;

        public event SelectHandler OnSelect { add { mOnSelect += value; } remove { mOnSelect -= value; } }


        public TLTransportTrigger(TLUnitTransportData data, InstanceZone zone, string name) : base(zone, data)
        {
            NextPositionName = data.Transport.NextPosition;
            AcceptUnitType = data.Transport.AcceptUnitType;
            AcceptUnitTypeForAll = data.Transport.AcceptUnitTypeForAll;
            AcceptForce = data.Transport.AcceptForce;
            AcceptForceForAll = data.Transport.AcceptForceForAll;
            TransportEffect = data.Transport.TransportEffect;
            AreaCheckPlayerCount = data.AreaCheckPlayerCount;
            AreaCheckForceForAll = data.AreaCheckForceForAll;
            AreaCheckForce = data.AreaCheckForce;
        }

        public void bindToRegion(ZoneRegion region)
        {
            InstanceZone zone = region.Parent;
            this.mNext = zone.getFlag(this.NextPositionName);
            this.mOwner = region;
            if (mNext != null)
            {
                region.OnUnitEnter += onUnitEnter;
            }
        }

        private bool Select(InstanceUnit unit)
        {
            string msg = null;
            if (!AcceptUnitTypeForAll && unit.Info.UType != this.AcceptUnitType)
            {
                msg = TLCommonConfig.TIPS_TRANSPORT_ERROR_FORCE;
            }
            if (!AcceptForceForAll && unit.Force != this.AcceptForce)
            {
                msg = TLCommonConfig.TIPS_TRANSPORT_ERROR_FORCE;
            }
            if (AreaCheckPlayerCount > 0 && msg == null)
            {
                var area = Zone.GetArea(mNext.X, mNext.Y);
                var list = new List<InstancePlayer>();
                var sx = area.X - area.Width / 2;
                var sy = area.Y - area.Height / 2;
                var dx = sx + area.Width;
                var dy = sy + area.Height;

                var currentCount = 0;
                if (AreaCheckForceForAll)
                {
                    currentCount = mOwner.getObjectsCountInRegion<InstanceUnit>();
                }
                else
                {
                    currentCount = mOwner.getObjectsCountInRegion<InstanceUnit>(u => u.Force == AreaCheckForce);
                }

                msg = currentCount < AreaCheckPlayerCount ? null : TLCommonConfig.TIPS_TRANSPORT_ERROR_FULL;
            }
            if (msg != null)
            {
                var p = (unit as TLInstancePlayer);
                (p?.VirtualPlayer)?.SendMsgToClient(msg);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void onUnitEnter(ZoneRegion region, InstanceUnit obj)
        {
            if (Select(obj) && (mOnSelect == null || mOnSelect.Invoke(region, obj)))
            {
                if (TransportEffect != null)
                {
                    mOwner.Parent.queueEvent(new AddEffectEvent(obj.ID,Zone.IsHalfSync, region.X, region.Y, region.Direction, TransportEffect));
                }
                obj.transport(mNext.X, mNext.Y);
                obj.resetAI();
                ZoneRegion rg = mNext as ZoneRegion;
                if (rg != null)
                {
                    rg.addInRegionViewed(obj);
                }
            }
        }

        protected override void Disposing()
        {
            mOnSelect = null;
            if (mOwner != null && mNext != null)
            {
                mOwner.OnUnitEnter -= onUnitEnter;
            }
            base.Disposing();
        }
    }
}
