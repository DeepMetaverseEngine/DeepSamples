using DeepCore.GameData;
using DeepCore.Reflection;

namespace SampleBattle.Plugins
{
    [DescAttribute("公共配置")]
    public class SampleConfig : ICommonConfig
    {
        public override string ToString()
        {
            return "Sample 配置扩展属性";
        }
        public static SampleConfig Instance
        {
            get;
            private set;
        }
        public SampleConfig() { Instance = this; }

    }
}
