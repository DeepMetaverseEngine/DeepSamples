using System;
using DeepCore.GUI.Data;
using UnityEngine;
using DeepCore;
using DeepCore.Unity3D;
using TLClient.Protocol.Modules;
using DeepCore.Unity3D.UGUI;

public class ItemShow : QuadItemShow
{
    public static readonly Vector2 DefaultSize = new Vector2(74, 74);
    public static readonly Vector2 IconSize = new Vector2(64, 64);
    public static readonly Vector2 DefaultSelectSize = new Vector2(74, 74);

    public const int ConfigRed = 2;
    public const int ConfigNum = 10;
    public const int ConfigSelect = 11;
    public const int ConfigEquiped = 12;
    public const int ConfigArrowUp = 13;
    public const int ConfigBind = 14;
    public const int ConfigCanNotEquip = 15;
    public const int ConfigStar1 = 17;
    public const int ConfigStar2 = 18;
    public const int ConfigStar3 = 19;
    public const int ConfigErrorTemplateID = 99;

    static readonly HashMap<int, int> mAtlasMap = new HashMap<int, int>()
    {
        {ConfigRed, 36},
        {ConfigSelect, 35},
        {ConfigEquiped, 23},
        {ConfigArrowUp, 15},
        {ConfigBind, 38},
        {ConfigCanNotEquip, 39},
        {ConfigStar1, 41},
        {ConfigStar2, 41},
        {ConfigStar3, 41},
    };

    public static int GetAtlasTitleID(int index)
    {
        return mAtlasMap.Get(index);
    }

    public static Vector2 SelectSizeToBodySize(Vector2 src)
    {
        float sx = DefaultSize.x / DefaultSelectSize.x;
        float sy = DefaultSize.y / DefaultSelectSize.y;
        return new Vector2(src.x * sx, src.y * sy);
    }

    public static Vector2 BodySizeToSelectSize(Vector2 src)
    {
        float sx = DefaultSelectSize.x / DefaultSize.x;
        float sy = DefaultSelectSize.y / DefaultSize.y;
        return new Vector2(src.x * sx, src.y * sy);
    }


    public const string Path = "dynamic/TL_tips/output/TL_tips.xml";

    protected ItemShow() : base("dynamic/TL_tips/output/TL_tips.xml", "TL_tips", "static/item/", DefaultSize, IconSize)
    {
        AddNodeConfig(ConfigRed);
        AddNodeConfig(ConfigNum);
        AddNodeConfig(ConfigSelect);
        AddNodeConfig(ConfigEquiped);
        AddNodeConfig(ConfigErrorTemplateID);
        AddNodeConfig(ConfigArrowUp);
        AddNodeConfig(ConfigBind);
        AddNodeConfig(ConfigCanNotEquip);
        AddNodeConfig(ConfigStar1);
        AddNodeConfig(ConfigStar2);
        AddNodeConfig(ConfigStar3);
    }

    private static readonly string[] QualityArray = new string[]
    {
        null,
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|7",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|6",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|8",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|5",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|12",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|12",
    };

    private static readonly string[] QualityCircleArray = new string[]
    {
        null,
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|31",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|30",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|32",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|27",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|29",
        "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|29",
    };

    private bool mIsCircleQualtiy;

    public bool IsCircleQualtiy
    {
        get { return mIsCircleQualtiy; }
        set
        {
            mIsCircleQualtiy = value;
            Quality = Quality;
        }
    }


    public uint Num
    {
        get { return GetNodeConfigVal<uint>(ConfigNum); }
        set
        {
            mForceNum = false;
            SetNodeConfigVal(ConfigNum, value);
        }
    }

    public bool IsSelected
    {
        get { return GetNodeConfigVal<bool>(ConfigSelect); }
        set { SetNodeConfigVal(ConfigSelect, value); }
    }

    public bool IsEquiped
    {
        get { return GetNodeConfigVal<bool>(ConfigEquiped); }
        set { SetNodeConfigVal(ConfigEquiped, value); }
    }

    public bool IsArrowUp
    {
        get { return GetNodeConfigVal<bool>(ConfigArrowUp); }
        set { SetNodeConfigVal(ConfigArrowUp, value); }
    }

    public bool IsBinded
    {
        get { return GetNodeConfigVal<bool>(ConfigBind); }
        set { SetNodeConfigVal(ConfigBind, value); }
    }

    public bool CanNotEquip
    {
        get { return GetNodeConfigVal<bool>(ConfigCanNotEquip); }
        set { SetNodeConfigVal(ConfigCanNotEquip, value); }
    }

    public bool LevelLimit
    {
        get { return GetNodeConfigVal<bool>(ConfigRed); }
        set { SetNodeConfigVal(ConfigRed, value); }
    }

    private int ErrorTemplateID
    {
        get { return GetNodeConfigVal<int>(ConfigErrorTemplateID); }
        set { SetNodeConfigVal(ConfigErrorTemplateID, value); }
    }

    public bool IsEmpty
    {
        get { return Num <= 0 && Quality <= 0 && string.IsNullOrEmpty(Icon); }
    }

    private bool mForceNum;

    /// <summary>
    /// 强制显示数字
    /// </summary>
    public int ForceNum
    {
        get { return GetNodeConfigVal<int>(ConfigNum); }
        set
        {
            mForceNum = true;
            SetNodeConfigVal(ConfigNum, value);
        }
    }

    private int mStarNum;
    public const int MaxStarNum = 3;

    public int Star
    {
        get { return mStarNum; }
        set
        {
            mStarNum = value;
            for (var i = ConfigStar1; i <= ConfigStar3; i++)
            {
                SetNodeConfigVal(i, i < mStarNum + ConfigStar1);
            }
        }
    }

    public int Index { get; set; }

    protected override void OnInitNodeConfig(NodeConfig conf)
    {
        if (conf.Name == ConfigNum)
        {
            var quad = new BatchQuadsGraphics.BatchImageQuad("")
            {
                anchor = ImageAnchor.R_B,
                offset = new Vector2(-5, -5),
                visible = false
            };
            conf.QuadIndex = mQuadSprite.AddQuad(quad);
        }
        else if (conf.Name == ConfigErrorTemplateID)
        {
            var quad = new BatchQuadsGraphics.BatchImageQuad("")
            {
                anchor = ImageAnchor.C_C,
                visible = false
            };
            conf.QuadIndex = mQuadSprite.AddQuad(quad);
        }
        else
        {
            BatchQuadsGraphics.BatchImageQuad quad = new BatchQuadsGraphics.BatchImageQuad(GetAtlasTitleID(conf.Name));
            quad.visible = false;

            if (conf.Name == ConfigEquiped)
            {
                quad.anchor = ImageAnchor.R_T;
                //quad.offset = new Vector2(-5, 0);
            }
            else if (conf.Name == ConfigArrowUp)
            {
                quad.anchor = ImageAnchor.R_T;
                quad.offset = new Vector2(-5, 5);
            }
            else if (conf.Name == ConfigBind)
            {
                quad.anchor = ImageAnchor.L_T;
                quad.offset = new Vector2(5, 5);
            }
            else if (conf.Name == ConfigCanNotEquip)
            {
                quad.anchor = ImageAnchor.R_T;
                quad.offset = new Vector2(-5, 5);
            }
            else if (conf.Name >= ConfigStar1 && conf.Name <= ConfigStar3)
            {
                quad.anchor = ImageAnchor.R_B;
            }

            conf.QuadIndex = mQuadSprite.AddQuad(quad);
        }
    }

    protected override void OnConfigSetValue(NodeConfig conf)
    {
        if (conf.Name == ConfigNum)
        {
            //number
            var num = (uint) conf.Val;
            VisibleConfigNode(conf, num > 1 || mForceNum);
            if (num > 1 || mForceNum)
            {
                var quad = mQuadSprite.GetQuad(conf.QuadIndex);
                quad.text = num.ToString();
                mQuadSprite.SetQuad(conf.QuadIndex, quad);
            }
        }
        else if (conf.Name == ConfigErrorTemplateID)
        {
            //number
            var num = (int) conf.Val;
            VisibleConfigNode(conf, num != 0);
            if (num != 0)
            {
                var quad = mQuadSprite.GetQuad(conf.QuadIndex);
                quad.text = num.ToString();
                mQuadSprite.SetQuad(conf.QuadIndex, quad);
            }
        }
        else if (conf.Name >= ConfigStar1 && conf.Name <= ConfigStar3)
        {
            VisibleConfigNode(conf, (bool) conf.Val);
            if ((bool) conf.Val)
            {
                var index = conf.Name - ConfigStar1;
                var quad = mQuadSprite.GetQuad(conf.QuadIndex);
                var atlasID = mAtlasMap.Get(conf.Name);
                var w = mQuadSprite.Atlas.getWidth(atlasID);
                var h = mQuadSprite.Atlas.getHeight(atlasID);
                var x = index * (w-5);
                var y = index * (h-5);
                quad.offset = new Vector2(-x - 4, -4);
                mQuadSprite.SetQuad(conf.QuadIndex, quad);
            }
        }
        else
        {
            VisibleConfigNode(conf, (bool) conf.Val);
        }
    }

    protected override string GetQualityLayout(int quality)
    {
        if (IsCircleQualtiy)
        {
            return QualityCircleArray[quality];
        }

        return QualityArray[quality];
    }


    protected override string GetStateLayout(ItemStatus state)
    {
        if (state == ItemStatus.Lock)
        {
            return "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|34";
        }
        else if(IsCircleQualtiy)
        {
            return "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|28";
        }
        return "#dynamic/TL_tips/output/TL_tips.xml|TL_tips|4";
    }

    private static readonly LRUCache<string, ItemShow> Cache = new LRUCache<string, ItemShow>(10, RemoveCallBack);

    public void ResetItemData(ItemData itdata)
    {
        var map = GameUtil.GetDBData("Item", itdata.TemplateID);
        if (map != null)
        {
            string atlas_id = map["atlas_id"].ToString();
            var quality = Convert.ToInt32(map["quality"]);
            var starlv = Convert.ToInt32(map["star_level"]);
            Icon = atlas_id;
            Quality = quality;
            Num = itdata.Count;
            Star = starlv;
            if (!string.IsNullOrEmpty(itdata.ID))
            {
                var map_equip = GameUtil.GetDBData("Equip", itdata.TemplateID);
                if (map_equip != null)
                {
                    var pro = Convert.ToInt32(map_equip["profession"]);
                    if (pro != 0 && pro != DataMgr.Instance.UserData.Pro)
                    {
                        CanNotEquip = true;
                    }
                }
            }

            IsBinded = !itdata.CanTrade;
        }
        else
        {
            Icon = "nitx.png";
            Quality = 0;
            Num = itdata.Count;
            ErrorTemplateID = itdata.TemplateID;
            Star = 0;
        }
    }

    public static ItemShow Create(ItemData itdata)
    {
        var map = GameUtil.GetDBData("Item", itdata.TemplateID);
        if (map != null)
        {
            string atlas_id = map["atlas_id"].ToString();
            var quality = Convert.ToInt32(map["quality"]);
            var starlv = Convert.ToInt32(map["star_level"]);
            var ret = Create(atlas_id, quality, itdata.Count);
            if (!string.IsNullOrEmpty(itdata.ID))
            {
                var map_equip = GameUtil.GetDBData("Equip", itdata.TemplateID);
                if (map_equip != null)
                {
                    var pro = Convert.ToInt32(map_equip["profession"]);
                    if (pro != 0 && pro != DataMgr.Instance.UserData.Pro)
                    {
                        ret.CanNotEquip = true;
                    }
                }
            }

            ret.Star = starlv;
            ret.IsBinded = !itdata.CanTrade;
            return ret;
        }

        //return ??
        var errorItshow = Create("nitx.png", 0, itdata.Count);
        errorItshow.ErrorTemplateID = itdata.TemplateID;
        return errorItshow;
        //return null;
    }

    public static ItemShow Create(string icon, int quality, uint num)
    {
        ItemShow ret;
        if (Cache.ContainsKey(icon))
        {
            ret = Cache.Get(icon);
        }
        else
        {
            ret = new ItemShow {Icon = icon};
            ret.CreateItemNode(ret.GetNodeConfig(ConfigRed));
            ret.LevelLimit = false;
        }

        ret.Star = 0;
        ret.IsArrowUp = false;
        ret.IsCircleQualtiy = false;
        ret.Transform.SetParent(null);
        ret.CanNotEquip = false;
        ret.IsBinded = false;
        ret.Quality = quality;
        ret.Num = num;
        ret.Position2D = Vector2.zero;
        ret.Size2D = DefaultSize;
        ret.IsSelected = false;
        ret.ShowBackground = false;

        return ret;
    }

    public static ItemShow Create()
    {
        return Create("", 0, 0);
    }


    private static void RemoveCallBack(ItemShow val)
    {
        val.Dispose();
    }

    protected override void OnDisposeEvents()
    {
        base.OnDisposeEvents();
        TouchClick = null;
    }

    public void ToCache()
    {
        RemoveFromParent(false);
        UnityObject.transform.SetParent(UnityHelper.DisableParent);
        //清空数据放入缓存
        ClearExternChildren();
        Cache.Add(Icon == null ? "" : Icon, this);
        OnDisposeEvents();
    }
}