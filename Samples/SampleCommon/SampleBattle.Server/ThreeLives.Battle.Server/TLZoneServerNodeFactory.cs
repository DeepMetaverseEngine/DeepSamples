using DeepCore.GameData;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameHost.Server.Node;
using DeepCore.GameHost.Server.Node.Interface;

namespace TLBattle.Host
{
    /// <summary>
    /// 容器工场.
    /// </summary>
    public class TLZoneServerNodeFactory : ZoneServerFactory
    {
        public TLZoneServerNodeFactory()
        {
        }

        public override DeepCore.GameHost.Server.Node.ZoneNode CreateZoneNode(IServer server, EditorTemplates data_root, ZoneNodeConfig cfg)
        {
            var codec = base.GetOrCreateCodec(data_root);
            return new TLZoneNode(server, data_root, codec, cfg);
            //return base.CreateZoneNode(server, data_root, cfg);
        }

    }


}
