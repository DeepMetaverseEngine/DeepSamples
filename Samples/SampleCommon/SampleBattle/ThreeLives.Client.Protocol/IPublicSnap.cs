using System;

namespace TLClient.Protocol
{
    public interface IPublicSnap
    {
        DateTime ExpiredUtcTime { get; }
    }
}
