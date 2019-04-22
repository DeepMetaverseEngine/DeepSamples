using System;
using TLClient.Protocol;

public class ClientPublicSnapReader<TV> :  PublicSnapReader<TV> where TV :class, IPublicSnap
{
    private static DateTime UtcNow()
    {
        return GameSceneMgr.Instance.syncServerTime.GetServerTimeUTC();
    }

    public ClientPublicSnapReader(LoadDataDelegate handler) : base(handler, UtcNow)
    {
    }

    public void GetMany(SLua.LuaTable keys, Action<TV[]> all)
    {
        var strKeys = new string[keys.length()];

        for (var i = 0; i < strKeys.Length; i++)
        {
            strKeys[i] = keys[i+1].ToString();
        }

        GetMany(strKeys, all);
        keys.Dispose();
    }
}
