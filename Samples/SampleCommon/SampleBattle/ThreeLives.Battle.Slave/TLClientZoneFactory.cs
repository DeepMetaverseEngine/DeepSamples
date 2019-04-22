using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameSlave;
using DeepCore.Log;
using TLBattle.Client.Client;

namespace TLBattle.Client
{
    public class TLClientZoneFactory : ClientZoneFactory
    {
        private Logger log = LoggerFactory.GetLogger("TLZoneFactory");

        public static TLClientZoneFactory Instance { get; private set; }
        public TLClientZoneFactory()
        {
            log.Debug("TLClientZoneFactory initialized");
        }

        public override ZoneLayer CreateClientZoneLayer(EditorTemplates templates, ILayerClient listener)
        {
            return new TLZoneLayer(templates, listener);
        }
        public override ZoneUnit CreateClientUnit(ZoneLayer parent, UnitInfo info, SyncUnitInfo syn, AddUnitEvent add = null)
        {
            return new ZoneUnit(info, syn, parent, add);
        }
        public override ZoneActor CreateClientActor(ZoneLayer parent, UnitInfo info, LockActorEvent add)
        {
            info = info.Clone() as UnitInfo;
            info.Properties = add.GameServerProp;
            return new ZoneActor(info, add, parent);
        }
        public override IVirtualClientUnit CreateClientUnitVirtual(ZoneUnit owner)
        {
            switch (owner.Info.UType)
            {
                case UnitInfo.UnitType.TYPE_PLAYER:
                    return new TLClientVirtual_Player();
                case UnitInfo.UnitType.TYPE_PLAYERMIRROR:
                    return new TLClientVirtual_PlayerMirror();
                case UnitInfo.UnitType.TYPE_MONSTER:
                    return new TLClientVirtual_Monster();
                case UnitInfo.UnitType.TYPE_NPC:
                    return new TLClientVirtual_NPC();
                case UnitInfo.UnitType.TYPE_PET:
                    return new TLClientVirtual_Pet();
                default:
                    return new TLClientVirtual();
            }
        }
    }
}
