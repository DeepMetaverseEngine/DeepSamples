using DeepCore;
using DeepCore.GameData.RTS.Manhattan;
using DeepCore.GameHost.Helper;
using DeepCore.GameHost.Instance;
using System;
using System.Collections.Generic;
using System.Text;
using TLBattle.Common.Plugins;

namespace TLBattle.Server
{

    public class TLZoneSpaceDivision : ZoneSpaceDivision
    {
        private readonly int range;
        private InstanceZone zone;
        private TimeInterval<int> updateTimer;
        public TLZoneSpaceDivision(InstanceZone zone) : base(zone)
        {
            var sceneprop = (zone.SceneData.Properties as TLSceneProperties);
            this.range = (sceneprop.Sync_InRange / zone.SceneData.SpaceDiv);
            this.zone = zone;
            this.updateTimer = new TimeInterval<int>(1000);
            this.OnObjectAdded += ZoneSpaceDivision_OnObjectAdded;
            this.OnObjectRemoved += ZoneSpaceDivision_OnObjectRemoved;
        }
        protected override SpaceCellNode CreateSpaceCellNode(int cx, int cy)
        {
            return new TLZoneSpaceCellNode(cx, cy, this);
        }
        private void ZoneSpaceDivision_OnObjectAdded(SpaceCellNode node, object obj)
        {
            if (obj is InstancePlayer)
            {
                (node as TLZoneSpaceCellNode).PlayerChange(+1);
            }
        }
        private void ZoneSpaceDivision_OnObjectRemoved(SpaceCellNode node, object obj)
        {
            if (obj is InstancePlayer)
            {
                (node as TLZoneSpaceCellNode).PlayerChange(-1);
            }
        }
        public override void SpaceUpdate(int intervalMS)
        {
            if (updateTimer.Update(intervalMS))
            {
                base.ForEachSpaceCellNodes((e) =>
                {
                    (e as TLZoneSpaceCellNode).SpaceUpdate(this);
                });
            }
        }
        public class TLZoneSpaceCellNode : ZoneSpaceCellNode
        {
            public TLZoneSpaceCellNode(int six, int siy, TLZoneSpaceDivision div) : base(six, siy)
            {
                var aspace = (div.zone.PathFinder as AstarManhattan).GetSpaceMapNode(six, siy);
                if (aspace != null)
                {
                    this.IsBlocked = aspace.Blocked;
                }
                else
                {
                    this.IsBlocked = false;
                }
            }
            public void PlayerChange(int i)
            {
                PlayerCount += i;
            }
            public void SpaceUpdate(TLZoneSpaceDivision div)
            {
                if (!IsBlocked)
                {
                    NearPlayerCount = 0;
                    div.ForEachSpaceCellNodes(this.BX, this.BY, div.range, e =>
                    {
                        NearPlayerCount += (e as TLZoneSpaceCellNode).PlayerCount;
                    });
                }
            }
            public bool IsBlocked { get; private set; }
            public int PlayerCount { get; private set; }
            public int NearPlayerCount { get; private set; }
        }
    }

    public static class SpaceDivisionHelper
    {
        public static bool IsNearPlayer(this InstanceZoneObject obj)
        {
            var space = (obj.CurrentSpaceCellNode as TLZoneSpaceDivision.TLZoneSpaceCellNode);
            if (space != null)
            {
                return space.PlayerCount > 0 || space.NearPlayerCount > 0;
            }
            return false;
        }
    }
}