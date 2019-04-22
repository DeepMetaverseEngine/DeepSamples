using UnityEngine;
using System.Collections;
using Assets.Scripts.Data;

public class DataMgr {

    private static DataMgr mInstance;
    public static DataMgr Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = new DataMgr();
            return mInstance;
        }
    }

    private AccountData mAccountData;
    public AccountData AccountData
    {
        get
        {
            if (mAccountData == null)
                mAccountData = new AccountData();
            return mAccountData;
        }
    }

    private SettingData mSettingData;
    public SettingData SettingData
    {
        get
        {
            if (mSettingData == null)
                mSettingData = new SettingData();
            return mSettingData;
        }
    }

    private LoginData mLoginData;
    public LoginData LoginData
    {
        get
        {
            if (mLoginData == null)
                mLoginData = new LoginData();
            return mLoginData;
        }
    }

    private UserData mUserData;
    public UserData UserData
    {
        get
        {
            if (mUserData == null)
                mUserData = new UserData();
            return mUserData;
        }
    }

    private TLDropItemManager mDropItemManager;
    public TLDropItemManager DropItemManager
    {
        get
        {
            if (mDropItemManager == null)
                mDropItemManager = new TLDropItemManager();
            return mDropItemManager;
        }
    }

    private TeamData mTeamData;
    public TeamData TeamData
    {
        get
        {
            if (mTeamData == null)
                mTeamData = new TeamData();
            return mTeamData;
        }
    }

    private FlagPushData mFlagPushData;
    public FlagPushData FlagPushData
    {
        get
        {
            if (mFlagPushData == null)
                mFlagPushData = new FlagPushData();
            return mFlagPushData;
        }
    }

    private QuestData mQuestData;
    public QuestData QuestData
    {
        get
        {
            if (mQuestData == null)
                mQuestData = new QuestData();
            return mQuestData;
        }
    }

    private NpcQuestManager mQuestMangerData;
    public NpcQuestManager QuestMangerData
    {
        get
        {
            if (mQuestMangerData == null)
                mQuestMangerData = new NpcQuestManager();
            return mQuestMangerData;
        }
    }

    private MsgData mMsgData;
    public MsgData MsgData
    {
        get
        {
            if (mMsgData == null)
                mMsgData = new MsgData();
            return mMsgData;
        }
    }

    private GuildData mGuildData;
    public GuildData GuildData
    {
        get
        {
            if (mGuildData == null)
                mGuildData = new GuildData();
            return mGuildData;
        }
    }

    public void InitNetWork()
    {
        UserData.InitNetWork();
        DropItemManager.InitNetWork();
        TeamData.InitNetWork();
        QuestData.InitNetWork();
        QuestMangerData.InitNetWork();
        MsgData.InitNetWork();
        SettingData.InitNetWork();
        GuildData.InitNetWork();
    }

    public void Update(float deltaTime)
    {
        if(mLoginData != null)
        {
            mLoginData.Update(deltaTime);
        }
        if (mMsgData != null)
        {
            mMsgData.Update(deltaTime);
        }
        if (mTeamData != null)
        {
            mTeamData.Update(deltaTime);
        }
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (mUserData != null)
        {
            mUserData.Clear(reLogin, reConnect);
        }
        if (mSettingData!=null)
        {
            mSettingData.Clear(reLogin, reConnect);
        }
        if (mTeamData != null)
        {
            mTeamData.Clear(reLogin, reConnect);
        }
        if (mFlagPushData != null)
        {
            mFlagPushData.Clear(reLogin, reConnect);
        }
        if (mQuestData != null)
        {
            mQuestData.Clear(reLogin, reConnect);
        }
        if (mQuestMangerData != null)
        {
            mQuestMangerData.Clear(reLogin, reConnect);
        }
        if (mMsgData != null)
        {
            mMsgData.Clear(reLogin, reConnect);
        }
        if (mGuildData != null)
        {
            mGuildData.Clear(reLogin, reConnect);
        }
        if (reLogin)
        {
            mUserData = null;
            mTeamData = null;
            mFlagPushData = null;
            mAccountData = null;
            mQuestData = null;
            mQuestMangerData = null;
            mMsgData = null;
            mInstance = null;
            mSettingData = null;
            mGuildData = null;
        }
    }

}
