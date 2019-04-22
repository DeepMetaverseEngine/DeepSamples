using System;
using System.Collections.Generic;
using SLua;
using UnityEngine;

public class SettingData : ISubject<SettingData> {

    //1代表打开 0代表关闭
    public enum NotifySettingState
    {
        NULL = 0,
        QUALITY = 1<<1,     //画质区分（1 2 3）
        ISFOGGY = 1<<2,
        ISPOWERSAVING = 1<<3,
        PERSONCOUNT = 1<<4, //人数范围（1-50）
        MUSIC = 1<<5,       //音乐范围（0-100）
        AUDIO = 1<<6,       //音效范围（0-100）
        ISMUSIC = 1<<7,
        ISAUDIO = 1<<8,
        ADDFRIEND = 1<<9,
        ADDTEAM = 1<<10,
        MSGFRIEND = 1<<11,
        MSGGUILD = 1<<12,
        MSGSTRANGER = 1<<13,
        PKFRIEND = 1<<14,
        PKGUILD = 1<<15,
        PKSTRANGER = 1<<16,
        BLOOM = 1<<17,
        ALL = int.MaxValue
    };

    public bool bIsSavePower;
    static Dictionary<string, NotifySettingState> mSettingKey = new Dictionary<string, NotifySettingState>();
    
    public SettingData()
    {
        if (PlayerPrefs.GetInt("imgquality") == 0)
        {
#if UNITY_IPHONE 
            if (SystemInfo.systemMemorySize >= 2000)
                PlayerPrefs.SetInt("imgquality", 3);
            else if(SystemInfo.systemMemorySize >= 1000 && SystemInfo.systemMemorySize < 2000)
                PlayerPrefs.SetInt("imgquality", 2);
            else
                PlayerPrefs.SetInt("imgquality", 1);
#endif
#if UNITY_ANDROID
            if (SystemInfo.systemMemorySize >= 2000)
                PlayerPrefs.SetInt("imgquality", 2);
            else
                PlayerPrefs.SetInt("imgquality", 1);
#endif
#if UNITY_EDITOR || UNITY_STANDALONE
            PlayerPrefs.SetInt("imgquality", 3);
#endif
            PlayerPrefs.SetInt("isfoggy",1);
            PlayerPrefs.SetInt("ispowersaving",1);
            PlayerPrefs.SetInt("personcount", 25);
            PlayerPrefs.SetInt("music", 100);
            PlayerPrefs.SetInt("audio", 100);
            PlayerPrefs.SetInt("ismusic", 1);
            PlayerPrefs.SetInt("isaudio", 1);
            PlayerPrefs.SetInt("addfriend", 1);
            PlayerPrefs.SetInt("addteam", 1);
            PlayerPrefs.SetInt("msgfriend", 1);
            PlayerPrefs.SetInt("msgguild", 1);
            PlayerPrefs.SetInt("msgstranger", 1);
            PlayerPrefs.SetInt("pkfriend", 1);
            PlayerPrefs.SetInt("pkguild", 1);
            PlayerPrefs.SetInt("pkstranger", 1);
#if UNITY_IPHONE || UNITY_IOS || UNITY_EDITOR ||UNITY_STANDALONE
            PlayerPrefs.SetInt("bloom",1);
 #elif UNITY_ANDROID
            PlayerPrefs.SetInt("bloom",0);
#endif
        }
        if (PlayerPrefs.GetInt("imgquality") != 0)
        {
            SetAttribute(NotifySettingState.QUALITY, PlayerPrefs.GetInt("imgquality"));
            SetAttribute(NotifySettingState.ISFOGGY, PlayerPrefs.GetInt("isfoggy"));
            SetAttribute(NotifySettingState.ISPOWERSAVING, PlayerPrefs.GetInt("ispowersaving"));
            SetAttribute(NotifySettingState.PERSONCOUNT, PlayerPrefs.GetInt("personcount"));
            SetAttribute(NotifySettingState.MUSIC, PlayerPrefs.GetInt("music"));
            SetAttribute(NotifySettingState.AUDIO, PlayerPrefs.GetInt("audio"));
            SetAttribute(NotifySettingState.ISMUSIC, PlayerPrefs.GetInt("ismusic"));
            SetAttribute(NotifySettingState.ISAUDIO, PlayerPrefs.GetInt("isaudio"));
            SetAttribute(NotifySettingState.ADDFRIEND, PlayerPrefs.GetInt("addfriend"));
            SetAttribute(NotifySettingState.ADDTEAM, PlayerPrefs.GetInt("addteam"));
            SetAttribute(NotifySettingState.MSGFRIEND, PlayerPrefs.GetInt("msgfriend"));
            SetAttribute(NotifySettingState.MSGGUILD, PlayerPrefs.GetInt("msgguild"));
            SetAttribute(NotifySettingState.MSGSTRANGER, PlayerPrefs.GetInt("msgstranger"));
            SetAttribute(NotifySettingState.PKFRIEND, PlayerPrefs.GetInt("pkfriend"));
            SetAttribute(NotifySettingState.PKGUILD, PlayerPrefs.GetInt("pkguild"));
            SetAttribute(NotifySettingState.PKSTRANGER, PlayerPrefs.GetInt("pkstranger"));
            SetAttribute(NotifySettingState.BLOOM, PlayerPrefs.GetInt("bloom"));
        }
        if (mSettingKey.Count == 0)
        {
            //网络消息到枚举的映射
            mSettingKey.Add("quality", NotifySettingState.QUALITY);
            mSettingKey.Add("isfoggy", NotifySettingState.ISFOGGY);
            mSettingKey.Add("ispowersaving", NotifySettingState.ISPOWERSAVING);
            mSettingKey.Add("personcount", NotifySettingState.PERSONCOUNT);
            mSettingKey.Add("music", NotifySettingState.MUSIC);
            mSettingKey.Add("audio", NotifySettingState.AUDIO);
            mSettingKey.Add("ismusic", NotifySettingState.ISMUSIC);
            mSettingKey.Add("isaudio", NotifySettingState.ISAUDIO);
            mSettingKey.Add("addfriend", NotifySettingState.ADDFRIEND);
            mSettingKey.Add("addteam", NotifySettingState.ADDTEAM);
            mSettingKey.Add("msgfriend", NotifySettingState.MSGFRIEND);
            mSettingKey.Add("msgguild", NotifySettingState.MSGGUILD);
            mSettingKey.Add("msgstranger", NotifySettingState.MSGSTRANGER);
            mSettingKey.Add("pkfriend", NotifySettingState.PKFRIEND);
            mSettingKey.Add("pkguild", NotifySettingState.PKGUILD);
            mSettingKey.Add("pkstranger", NotifySettingState.PKSTRANGER);
            mSettingKey.Add("bloom",NotifySettingState.BLOOM);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("imgquality", GetAttribute(NotifySettingState.QUALITY));
        PlayerPrefs.SetInt("isfoggy", GetAttribute(NotifySettingState.ISFOGGY));
        PlayerPrefs.SetInt("ispowersaving", GetAttribute(NotifySettingState.ISPOWERSAVING));
        PlayerPrefs.SetInt("personcount", GetAttribute(NotifySettingState.PERSONCOUNT));
        PlayerPrefs.SetInt("music", GetAttribute(NotifySettingState.MUSIC));
        PlayerPrefs.SetInt("audio", GetAttribute(NotifySettingState.AUDIO));
        PlayerPrefs.SetInt("ismusic", GetAttribute(NotifySettingState.ISMUSIC));
        PlayerPrefs.SetInt("isaudio", GetAttribute(NotifySettingState.ISAUDIO));
        PlayerPrefs.SetInt("addfriend", GetAttribute(NotifySettingState.ADDFRIEND));
        PlayerPrefs.SetInt("addteam", GetAttribute(NotifySettingState.ADDTEAM));
        PlayerPrefs.SetInt("msgfriend", GetAttribute(NotifySettingState.MSGFRIEND));
        PlayerPrefs.SetInt("msgguild", GetAttribute(NotifySettingState.MSGGUILD));
        PlayerPrefs.SetInt("msgstranger", GetAttribute(NotifySettingState.MSGSTRANGER));
        PlayerPrefs.SetInt("pkfriend", GetAttribute(NotifySettingState.PKFRIEND));
        PlayerPrefs.SetInt("pkguild", GetAttribute(NotifySettingState.PKGUILD));
        PlayerPrefs.SetInt("pkstranger", GetAttribute(NotifySettingState.PKSTRANGER));
        PlayerPrefs.SetInt("bloom",GetAttribute(NotifySettingState.BLOOM));
    }

    HashSet<IObserver<SettingData>> mObservers = new HashSet<IObserver<SettingData>>();
    Dictionary<string, LuaTable> mLuaObservers = new Dictionary<string, LuaTable>();
    Dictionary<long, int> mAttributes = new Dictionary<long, int>();

    [DoNotToLua]
    public void AttachObserver(IObserver<SettingData> ob)
    {
        if (!mObservers.Contains(ob))
        {
            mObservers.Add(ob);
        }
        
    }

    [DoNotToLua]
    public void DetachObserver(IObserver<SettingData> ob)
    {
        if (mObservers.Contains(ob))
        {
            mObservers.Remove(ob);
        }
    }


    //mObservers有问题，每次登陆值没了，在没写Clear方法之前就这样，先这样写，每次等游戏监听，小退取消监听，回头再查mObservers值的问题
    public void InitNetWork()
    {
        TLBattleFactory.Instance.RegisterListener();
        ChangerData("quality",GetAttribute(NotifySettingState.QUALITY));
        ChangerData("personcount", GetAttribute(NotifySettingState.PERSONCOUNT));
        ChangerData("isfoggy", GetAttribute(NotifySettingState.ISFOGGY));
    }

    public void Clear(bool reLogin, bool reConnect)
    {
        if (reLogin)
        {
            mAttributes.Clear();
            mObservers.Clear();
            mLuaObservers.Clear();
            TLBattleFactory.Instance.ClearListener();
        }
    }

    public void AttachLuaObserver(string key, LuaTable t)
    {
        mLuaObservers[key] = t;
    }

    public void DetachLuaObserver(string key)
    {
        mLuaObservers.Remove(key);
    }

    public void ChangerData(string key,int val)
    {
        var state = 0;
        state |= SetAttribute(mSettingKey[key], val);
        Notify(state);
    }

    public int GetAttribute(NotifySettingState s)
    {
        //如果省电模式开，则取假值
         if (bIsSavePower)
        {
            if (s == NotifySettingState.QUALITY)
            {
                return QUALITY_LOW;
            }
            if (s == NotifySettingState.PERSONCOUNT)
            {
                return 3;
            }
            if (s == NotifySettingState.BLOOM || s == NotifySettingState.ISFOGGY)
            {
                return 0;
            }
        }
        if (mAttributes.ContainsKey((long)s))
            return mAttributes[(long)s];
        else
            return 0;
    }
    
    public int TryGetIntAttribute(NotifySettingState s, int defaultValue = 0)
    {
        var value = defaultValue;
        object obj = GetAttribute(s);
        if (obj != null)
        {
            value = System.Convert.ToInt32(obj);
        }
        return value;
    }

    Dictionary<long, int> savePower = new Dictionary<long, int>();
    public int SetAttribute(NotifySettingState s, int val)
    {   
        //如果省电模式开，则新建一个字典，设置假值，避免将真实数据覆盖
        if (bIsSavePower && s!=NotifySettingState.ISPOWERSAVING)
        {
            var savePowernotifykey = (int)s;
            savePower[savePowernotifykey] = val;
            Notify(savePowernotifykey);
            return (int)(s);
        }
        var notifykey = (int)s;
        mAttributes[notifykey] = val;
        Notify(notifykey);
        return (int)(s);
    }

    public bool ContainsKey(NotifySettingState status, NotifySettingState key)
    {
        var sl = (long)status;
        var kl = (long)key;
        if ((sl & kl) != 0)
        {
            return true;
        }
        return false;
    }
    
    public void Notify(int status = (int)NotifySettingState.ALL)
    {
        foreach (var ob in mObservers)
        {
            ob.Notify(status, this);
        }

        foreach (var ob in mLuaObservers)
        {
            ob.Value.invoke("Notify", new object[] { (NotifySettingState)status, this, ob.Value });
        }
    }
    public const int QUALITY_LOW = 1;
    public const int QUALITY_MED = 2;
    public const int QUALITY_HIGH = 3;
    
    public int GetQuailty()
    {
        return Convert.ToInt32(GetAttribute(NotifySettingState.QUALITY));
    }

    public bool IsLowerPower()
    {
        var ret = Convert.ToInt32(GetAttribute(NotifySettingState.ISPOWERSAVING));
        return ret == 1;
    }
    
    public int PowerSavingMode(bool isOn)
    {
        if (isOn)//省电模式开
        {
            bIsSavePower = true;
            AudioListener.volume = 0;
            SetAttribute(NotifySettingState.QUALITY, QUALITY_LOW);
            SetAttribute(NotifySettingState.PERSONCOUNT, 3);
            SetAttribute(NotifySettingState.ISFOGGY, 0);
            SetAttribute(NotifySettingState.BLOOM, 0);
            //PlatformMgr.SetBrightness(0);
            Application.targetFrameRate = 10;
        }
        else//省电模式关
        {
            bIsSavePower = false;
            AudioListener.volume = 1;
            SetAttribute(NotifySettingState.QUALITY, GetAttribute(NotifySettingState.QUALITY));
            SetAttribute(NotifySettingState.PERSONCOUNT, GetAttribute(NotifySettingState.PERSONCOUNT));
            SetAttribute(NotifySettingState.ISFOGGY, GetAttribute(NotifySettingState.ISFOGGY));
            SetAttribute(NotifySettingState.BLOOM, GetAttribute(NotifySettingState.BLOOM));
            //PlatformMgr.SetBrightness(-1);
            Application.targetFrameRate = 30;
        }
        return GetAttribute(NotifySettingState.QUALITY);
    }
}
