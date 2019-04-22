using DeepCore.FuckPomeloClient;
using DeepCore.IO;
using DeepCore.Reflection;
using DeepMMO.Client;
using DeepMMO.Client.Battle;
using DeepMMO.Data;
using DeepMMO.Protocol.Client;
using System;
using ThreeLives.Client.Common.Modules;
using TLClient.Battle;
using TLClient.Modules;
using TLProtocol.Data;

namespace TLClient
{
    public class TLNetClient : RPGClient
    {
        public static IExternalizableFactory TLNetCodec
        {
            get; private set;
        }
        static TLNetClient()
        {
            var cfg = PomeloClientFactory.Instance.Config;
            cfg.NoDelay = false;
            PomeloClientFactory.Instance.Config = cfg;
            TLNetCodec = ReflectionUtil.CreateInterface<IExternalizableFactory>("TLClient.Serializer");
        }
        public TLNetClient(ClientInfo client) : base(TLNetCodec, client)
        {
        }
        public TLQuestModule questModule { get; private set; }
        public TLBagModule bagModule { get; private set; }

        protected override RPGBattleClient CreateBattle(ClientEnterZoneNotify sd)
        {
            return new TLBattleClient(this, sd);
        }

        protected override void OnGameClientConnected(object arg1, ISerializable arg2)
        {
            base.OnGameClientConnected(arg1, arg2);
            if(this.bagModule != null && this.questModule != null)
            {
                return;
            }
            this.bagModule = new TLBagModule(this);
            this.questModule = new TLQuestModule(this);
            AddModule(bagModule);
            AddModule(questModule);
        }


    }
}
