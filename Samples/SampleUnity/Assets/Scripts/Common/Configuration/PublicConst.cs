using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PublicConst
{

    //显示版本号
    public static string ClientVersion = "1.1.1";
    //SVN版本号
    public static string SVNVersion = "20071";
    //逻辑版本号
    public static int LogicVersion = 20071;
    //客户端区域信息
    public static string ClientRegion = "CHN";

    //融合时间
    public static float crossFadeLength { get { return 0.15f; } }

    public const string DUMMY_REAR_BUFF = "rear_buff";
    public const string DUMMY_HEAD_BUFF = "head_buff";
    public const string DUMMY_CHEST_BUFF = "chest_buff";
    public const string DUMMY_FOOT_BUFF = "foot_buff";
    public const string DUMMY_RIGHT_SPELL = "RightHand_Spell";//法师特效挂载点
    public const string DUMMY_LEFT_SPELL = "LeftHand_Spell";
    public const string DUMMY_RIGHT_HAND = "RightHand_Weapon";//法师特效挂载点
    public const string DUMMY_LEFT_HAND = "LeftHand_Weapon";
    public const string DUMMY_FIRSTGLOVE = "Female_FirstQT";//初始拳套
    public const string DUMMY_HEAD = "Bip001 HeadNub";//头部节点
    public const string DUMMY_COFFIN = "Rear_Weapon";//棺材节点

    public enum SceneType
    {
        Story = 0,                      //剧情场景.
        Public = 1,                     //野外.
        SingleDungeon = 2,              //单人副本.
        TeamDungeon = 3,                //组队副本.
        ZhenYaoTa = 4,                  //镇妖塔.
        XianLinDao = 5,                 //仙灵岛.
        XianMenZhuChen = 6,             //仙盟.
        ShiMen = 7,                     //师门.
        MiJing = 8,                     //秘境.
        TianJiangQiBao = 9,             //天降奇宝.
        ZhanChang10v10 = 10,            //10V10战场.
        zhanChang4v4 = 11,              //4V4竞技场.
        CrossServerMap = 12             //连服.
    }

    public enum LayerSetting
    {
        UI = 5,
        STAGE_NAV = 8,
        CAGE = 9,             //场景中3D遮挡物层
        CG = 10,              //剧情动画层
        LightLayer   = 11,      //场景特殊光照层
        SelectableUnit = 12,    //场景中可触摸的单位.
        CharacterUnlit = 13, //接受平行光方向信息
        ColliderObject = 14, //用于隐藏遮挡玩家的场景物体.
        NpcLayer = 15,      //场景中的npc层
        SelfLayer = 16,      //场景中的npc层
        FuckUI = 17,      //67专用
    }

    public enum FingerLayer
    {
        Loading = 0,
        DramaUILayer = 9,
        HZUI = 10,
        SkillTouch = 20,
        Rocker = 30,
        SkillRocker = 31,
        SKillBar = 40,
        SKillBarEnd = 49,
        BattleScene = 100,
        CameraLayer = 200,
    }

    public enum ProType : byte
    {
        None,       //无.
        YiZu,         //翼族.
        TianGong,     //天宫.
        KunLun,     //昆仑.
        QinqQiu,     //青丘.
    }

    private static PublicConst mInstance;
    public static PublicConst Instance
    {
        get
        {
            if(mInstance == null){
                mInstance = new PublicConst();
            }
            return mInstance;
        }
    }

    //账号长度.
    private static int mAccountLength;
    public static int AccountLength
    {
        get
        {
            if (mAccountLength == 0)
            {
                mAccountLength = 8;// ConfigMgr.Instance.TxtCfg.GetIntByKey(TextConfig.Type.PUBLICCFG, "accountLength");
            }
            return mAccountLength;
        }
        private set
        {
            mAccountLength = value;
        }
    }

    //密码长度.
    private static int mPasswordLength;
    public static int PasswordLength
    {
        get
        {
            if (mPasswordLength == 0)
            {
                mPasswordLength = 12;// ConfigMgr.Instance.TxtCfg.GetIntByKey(TextConfig.Type.PUBLICCFG, "passwordLength");
            }
            return mPasswordLength;
        }
        private set
        {
            mPasswordLength = value;
        }
    }
    //角色名长度.
    private static int mRoleNameLength;
    public static int RoleNameLength
    {
        get
        {
            if (mRoleNameLength == 0)
            {
                mRoleNameLength = 10;// ConfigMgr.Instance.TxtCfg.GetIntByKey(TextConfig.Type.PUBLICCFG, "roleNameLength");
            }
            return mRoleNameLength;
        }
        private set
        {
            mRoleNameLength = value;
        }
    }

    //  手机系统 5是IOS,6是android.
    public static int OSType
    {
        get
        {
#if UNITY_IOS
		    return 5;
#elif UNITY_ANDROID
		    return 6;
#else
            return 0;
#endif
            //7是wp,8是hd
        }
    }

}
