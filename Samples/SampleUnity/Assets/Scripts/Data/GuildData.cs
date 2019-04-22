using System.Collections.Generic;
using DeepCore;
using SLua;
using TLProtocol.Protocol.Client;

public class GuildData : ISubject<GuildData>
{

    public enum NotiFyStatus : int
    {
        Level = 1 << 1,
        Build = 1 << 2,
        Monster = 1 << 3,
        Donate = 1 << 4,
        Position = 1 << 5,
        Talent = 1 << 6,

        //所有标志位
        ALL = int.MaxValue,
    }

    //跟着xls表走
    public enum GuildBuild : int
    {
        Office = 1,
        Stable,
        Monster,
        College,
        Store,
    }

    public int Level { get; private set; }
    public HashMap<int, int> BuildList { get; private set; }
    public int MonsterRank { get; private set; }
    public int DonateCount { get; private set; }
    public int Position { get; private set; }
    public HashMap<int, int> TalentList { get; private set; }

    HashSet<IObserver<GuildData>> mObservers = new HashSet<IObserver<GuildData>>();
    Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();

    public GuildData()
    {
        BuildList = new HashMap<int, int>();
        TalentList = new HashMap<int, int>();
    }

    public void InitNetWork()
    {
        TLNetManage.Instance.Listen<ClientGuildInfoChangeNotify>(OnGuildInfoChangeNotify);
    }

    private void OnGuildInfoChangeNotify(ClientGuildInfoChangeNotify msg)
    {
        Level = msg.s2c_level;
        BuildList = msg.s2c_buildList;
        MonsterRank = msg.s2c_monsterRank;
        DonateCount = msg.s2c_donateCount;
        Position = msg.s2c_position;
        TalentList = msg.s2c_talentList;
        Notify(msg.s2c_type);
    }

    public int GetGuildBuildLv(int buildId)
    {
        int value;
        if(BuildList.TryGetValue(buildId, out value))
        {
            return value;
        }
        return 0;
    }

    public int GetGuildTalentLv(int talentId)
    {
        int value;
        if (TalentList.TryGetValue(talentId, out value))
        {
            return value;
        }
        return 0;
    }

    public void ResetDonateCount()
    {
        DonateCount = 0;
    }

    public void AttachObserver(IObserver<GuildData> ob)
    {
        mObservers.Add(ob);
    }

    public void DetachObserver(IObserver<GuildData> ob)
    {
        mObservers.Remove(ob);
    }

    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void Notify(int status)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { (NotiFyStatus)status, this, ob.Value });
        }
    }

    public bool ContainsKey(NotiFyStatus status, NotiFyStatus key)
    {
        int sl = (int)status;
        int kl = (int)key;
        if ((sl & kl) != 0)
        {
            return true;
        }
        return false;
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mObservers.Clear();
            mLuaObservers.Clear();
        }
    }

}
