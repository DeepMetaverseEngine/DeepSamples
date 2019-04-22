using DeepCore.Template.MoonSharp;
using TLBattle.Client;
using TLBattle.Server;

namespace ThreeLives.Battle.Test
{
    public class HZDSB : TLServerDataFactory
    {
        static HZDSB()
        {
            new DeepCore.Lua.LuaTemplateLoader(new DeepCore.Template.MoonSharp.MoonSharpLuaAdapter());
        }
        public HZDSB()
        {
            new TLClientZoneFactory();
            new TLServerZoneFactory();
        }

    } 
}
