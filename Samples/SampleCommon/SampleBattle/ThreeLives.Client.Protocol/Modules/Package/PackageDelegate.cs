using System.Collections.Generic;

namespace TLClient.Protocol.Modules.Package
{
    public interface IPackageListener
    {
        void OnUpdatePackageAction(BasePackage package, ICollection<ItemUpdateAction> acts);
    }
    public delegate bool OutSizeLogicHandler(bool isTest, List<IPackageItem> items);

}
