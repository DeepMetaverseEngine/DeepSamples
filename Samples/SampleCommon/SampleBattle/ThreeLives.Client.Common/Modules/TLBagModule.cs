using System;
using DeepCore;
using DeepCore.FuckPomeloClient;
using DeepCore.IO;
using DeepCore.Log;
using DeepMMO.Client;
using TLClient.Modules.Bag;
using TLClient.Protocol.Modules;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;

namespace TLClient.Modules
{
    public class TLBagModule : RPGClientModule
    {
        private readonly HashMap<byte, ClientBag> mPackageMap = new HashMap<byte, ClientBag>(4);

        protected Logger Logger;

        private PushHandler slotPushHandler;
        public TLBagModule(RPGClient client) : base(client)
        {
            Logger = LoggerFactory.GetLogger(GetType().Name);
        }

        public ClientNormalBag Bag
        {
            get { return GetBag(BaseBagType.FirstNormal) as ClientNormalBag; }
        }

        public ClientEquipBag EquipedBag
        {
            get { return GetBag(BaseBagType.FirstEquiped) as ClientEquipBag; }
        }


        public ClientWarehourse WarehourseBag
        {
            get { return GetBag(BaseBagType.FirstWarehourse) as ClientWarehourse; }
        }

        public ClientVirtualBag VirtualBag
        {
            get { return GetBag(BaseBagType.FirstVirtual) as ClientVirtualBag; }
        }

        public ClientFateBag FateBag
        {
            get { return GetBag(BaseBagType.FirstFate) as ClientFateBag; }
        }

        public ClientFateEquipBag FateEquipBag
        {
            get { return GetBag(BaseBagType.FateEquiped) as ClientFateEquipBag; }
        }
        

        private Action mOnInit;

        public event Action OnInit
        {
            add { mOnInit += value; }
            remove { mOnInit -= value; }
        }

        public ClientBag GetBag(byte bagType)
        {
            return mPackageMap.Get(bagType);
        }

        public ClientBag GetBag(BaseBagType bagType)
        {
            return GetBag((byte) bagType);
        }


        public override void OnStart()
        {
            slotPushHandler = game_client.Listen<ClientPackageSlotNotify>(OnClientPackageSlotNotify);
            game_client.Request<ClientGetAllPackageResponse>(new ClientGetAllPackageRequest(), OnGetAllPackagetInitData);
        }

        public override void OnStop()
        {
            if (slotPushHandler != null)
            {
                slotPushHandler.Clear();
                slotPushHandler = null;
            }
        }

        protected override void Disposing()
        {
            OnStop();
            mOnInit = null;
        }

        private void OnGetAllPackagetInitData(PomeloException pomeloException, ClientGetAllPackageResponse rep)
        {
            var firstInit = mPackageMap.Count == 0;
            var bagDict = rep.s2c_bags;
            if (bagDict == null)
            {
                return;
            }
            foreach (var entry in bagDict)
            {
                var bag = GetBag(entry.Key) ?? CreateBag(entry.Key);
                bag?.InitData(entry.Value);
            } 

            if (firstInit)
            {
                mOnInit?.Invoke();
            }
        }

        protected virtual ClientNormalBag OnCreateFirstNormalBag()
        {
            return new ClientNormalBag((byte) BaseBagType.FirstNormal, client.GameClient);
        }

        protected virtual ClientEquipBag OnCreateFirstEquipedBag()
        {
            return new ClientEquipBag((byte) BaseBagType.FirstEquiped, client.GameClient);
        }

        protected virtual ClientWarehourse OnCreateFirstWarehourseBag()
        {
            return new ClientWarehourse((byte) BaseBagType.FirstWarehourse, client.GameClient);
        }

        protected virtual ClientVirtualBag OnCreateFirstVirtualBag()
        {
            return new ClientVirtualBag((byte) BaseBagType.FirstVirtual, client.GameClient);
        }

        protected virtual ClientFateBag OnCreateFateBag()
        {
            return new ClientFateBag((byte)BaseBagType.FirstFate, client.GameClient);
        }
          
        protected virtual ClientFateEquipBag OnCreateFateEquipedBag()
        {
            return new ClientFateEquipBag((byte)BaseBagType.FateEquiped, client.GameClient);
        }
        

        protected virtual ClientBag OnCreateExternBag(byte t)
        {
            return new ClientSimpleExternBag(t, client.GameClient);
        }

        protected virtual void OnClientPackageSlotNotify(ClientPackageSlotNotify notify)
        {
            var bag = GetBag(notify.s2c_type);
            if (bag != null)
            {
                if (notify.s2c_init)
                {
                    bag.InitData(notify.s2c_slots);
                }
                else
                {
                    bag.OnSlotNotify(notify.s2c_slots, notify.s2c_reason);
                }
                //Logger.Log($"ClientPackageSlotNotify {notify.s2c_init} {notify.s2c_type}  {notify.s2c_reason}");
            }
        }

        private ClientBag CreateBag(byte bagType)
        {
            ClientBag ret = null;
            var t = (BaseBagType) bagType;
            switch (t)
            {
                case BaseBagType.FirstNormal:
                    ret = OnCreateFirstNormalBag();
                    break;
                case BaseBagType.FirstEquiped:
                    ret = OnCreateFirstEquipedBag();
                    break;
                case BaseBagType.FirstWarehourse:
                    ret = OnCreateFirstWarehourseBag();
                    break;
                case BaseBagType.FirstVirtual:
                    ret = OnCreateFirstVirtualBag();
                    break;
                case BaseBagType.FirstFate:
                    ret = OnCreateFateBag();
                    break;
                case BaseBagType.FateEquiped:
                    ret = OnCreateFateEquipedBag();
                    break;
            }
            if (ret == null)
            {
                ret = OnCreateExternBag(bagType);
            }
            if (ret != null)
            {
                mPackageMap.Add(bagType, ret);
            }
            return ret;
        }
    }
}