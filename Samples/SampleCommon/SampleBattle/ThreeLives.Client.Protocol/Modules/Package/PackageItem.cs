using System;
using System.Collections.Generic;
using System.Linq;

namespace TLClient.Protocol.Modules.Package
{

    //sign  template attribute
    public interface IPackageItem : ICloneable, IComparable<IPackageItem>
    {
        uint MaxStackCount { get; }
        int TemplateID { get; }
        bool CompareAttribute(IPackageItem other);
        string Flag { get; set; }
        uint Count { get; set; }
    }
}
