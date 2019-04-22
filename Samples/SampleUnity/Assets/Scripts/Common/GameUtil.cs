using UnityEngine;
using System.Collections.Generic;
using DeepCore.GUI.Display;
using DeepCore.Unity3D.Impl;
using DeepCore.GUI.Cell;
using DeepCore.GUI.Gemo;
using DeepCore.GUI.Display.Text;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.Utils;
using TLBattle.Common.Plugins;
using DeepCore;
using DeepCore.Unity3D;
using SLua;
using System;
using System.IO;
using Assets.Scripts;
using TLClient.Modules.Bag;
using TLClient.Protocol.Modules;
using TLProtocol.Data;
using DeepCore.GameData.Zone;
using DeepMMO.Data;
using DeepCore.GameSlave;
using DeepCore.GUI.Data;
using ThreeLives.Battle.Data.Data;
using TLClient;
using UnityEngine.EventSystems;

public class GameUtil
{

    #region 加载图片&CPJ.

    public const string IMG_SUFFIXNAME = ".png";

    /// <summary>
    /// 根据图片相对路径创建图片，并返回图片对象  
    /// </summary>
    /// <param name='imgPath'>
    /// 图片路径 相对与ui_edit/res下
    /// </param>
    /// <param name='suffixName'>
    /// 图片后缀名 默认为.png
    /// </param>
    public static Image CreateImage(string imgPath, string suffixName = GameUtil.IMG_SUFFIXNAME)
    {
        if (string.IsNullOrEmpty(imgPath) || imgPath.Equals(""))
        {
            Debugger.Log("GameUtil.CreateImage Error :arg is null");
            return null;
        }

        string imgMapName = string.Format("{0}{1}", imgPath, suffixName);

        return CreateImage(imgMapName);
    }

    public static Image CreateImage(string fullname)
    {
        if (string.IsNullOrEmpty(fullname))
        {
            Debugger.Log("GameUtil.CreateImage Error :arg is null");
            return null;
        }

        Image img = HZUISystem.Instance.Editor.GetImage(fullname);

        if (img == null)
        {
            string loadPath = string.Format("{0}{1}", HZUISystem.Instance.Editor.Root, fullname);
            Debugger.Log("GameUtil】CreateImage Error: " + loadPath);
        }

        return img;
    }

    /// <summary>
    /// 读取单张图片设置到UGUI的Image中.
    /// </summary>
    /// <param name="srcImg"></param>
    /// <param name="name"></param>
    /// <param name="path"></param>
    /// <param name="suffix"></param>
    public static void ConvertToUnityUISprite(UnityEngine.UI.Image srcImg, string path, string suffix = GameUtil.IMG_SUFFIXNAME)
    {
        UnityImage img = GameUtil.CreateImage(path, suffix) as UnityImage;
        if (img != null)
        {
            Texture2D t2d = img.Texture2D;
            if (t2d != null)
            {
                img.SupportReleaseTexture = false;
                //t2d = UnityEngine.Object.Instantiate(t2d);
                Sprite sprite = Sprite.Create(t2d, new Rect(0, (1 - img.MaxV) * t2d.height, img.Width, img.Height), new Vector2(0, 0));
                srcImg.sprite = sprite;
                if (srcImg.material == null)
                    srcImg.material = img.TextureMaterial; //CommonUI_Unity3D.Impl.UnityShaders.CreateMaterialUGUI(img);
            }
        }
    }

    /// <summary>
    /// 读取图集中的某张图片设置到UGUI的Image中(path, index)
    /// </summary>
    /// <param name="srcImg"></param>
    /// <param name="cpj_file_name"></param>
    /// <param name="atlas_name"></param>
    /// <param name="index"></param>
    public static void ConvertToUnityUISpriteFromAtlas(UnityEngine.UI.Image srcImg, string cpj_file_name, string atlas_name, int index)
    {
        CPJAtlas cpjAtlas = HZUISystem.CreateAtlas(cpj_file_name, atlas_name);
        if (cpjAtlas != null)
        {
            InitSprite(srcImg, cpjAtlas, index);
        }
    }

    /// <summary>
    /// 读取图集中的某张图片设置到UGUI的Image中(path, key)
    /// </summary>
    /// <param name="srcImg"></param>
    /// <param name="cpj_file_name"></param>
    /// <param name="atlas_name"></param>
    /// <param name="key"></param>
    public static void ConvertToUnityUISpriteFromAtlas(UnityEngine.UI.Image srcImg, string cpj_file_name, string atlas_name, string key)
    {
        CPJAtlas cpjAtlas = HZUISystem.CreateAtlas(cpj_file_name, atlas_name);
        if (cpjAtlas != null)
        {
            int index = cpjAtlas.GetIndexByKey(key);
            InitSprite(srcImg, cpjAtlas, index);
        }
    }

    /// <summary>
    /// 读取图集中的某张图片设置到UGUI的Image中(path|index)
    /// </summary>
    /// <param name="srcImg"></param>
    /// <param name="cpj_file_name"></param>
    /// <param name="atlas_name"></param>
    /// <param name="key"></param>
    public static void ConvertToUnityUISpriteFromAtlas(UnityEngine.UI.Image srcImg, string fullname)
    {
        if (!fullname.StartsWith("#") && !fullname.StartsWith("$"))
        {
            Debugger.Log("atlas name must start with '#' or '$'");
            return;
        }

        string name = fullname.Substring(1);
        string[] args = name.Split('|');
        string cpj_file_name = args[0];
        string atlas_name = args[1];
        CPJAtlas cpjAtlas = HZUISystem.CreateAtlas(cpj_file_name, atlas_name);
        if (cpjAtlas != null)
        {
            int index = 0;
            if (fullname.StartsWith("#"))
                index = int.Parse(args[2]);
            else if (fullname.StartsWith("$"))
                index = cpjAtlas.GetIndexByKey(args[2]);
            InitSprite(srcImg, cpjAtlas, index);
        }
    }

    private static void InitSprite(UnityEngine.UI.Image srcImg, CPJAtlas cpjAtlas, int index)
    {
        UnityImage img = cpjAtlas.GetTile(index) as UnityImage;
        if (img != null)
        {
            Texture2D t2d = img.Texture2D;
            Rectangle2D r2d = cpjAtlas.GetClipRect(index);
            if (t2d != null && r2d != null)
            {
                img.SupportReleaseTexture = false;
                Sprite sprite = Sprite.Create(t2d, new Rect(r2d.x, t2d.height - (r2d.y + r2d.height), r2d.width, r2d.height), new Vector2(0, 0));
                srcImg.sprite = sprite;
                if(srcImg.material == null)
                    srcImg.material = img.TextureMaterial; //CommonUI_Unity3D.Impl.UnityShaders.CreateMaterialUGUI(img);
            }
        }
    }

    #endregion

    #region 品质.

    public const int Quality_Default = 0;
    public const int Quality_Green = 1;
    public const int Quality_Blue = 2;
    public const int Quality_Purple = 3;
    public const int Quality_Orange = 4;

    //白绿蓝紫橙红
    static Dictionary<int, uint> mQualityColors = null;

    public static void InitItemQuality()
    {
        if (mQualityColors == null)
        {
            mQualityColors = new Dictionary<int, uint>();
            //先临时写死，到时候改成读策划配置文件
            uint[] qColor = { 0xffffffff, 0x5eb905ff, 0x479dedff, 0xd562e3ff, 0xf27937ff };
            for (int i = 0 ; i < 5 ; ++i)
            {
                mQualityColors.Add(i, qColor[i]);
            }
        }
    }
    /// <summary>
    /// 获得品质颜色rgba，此方法禁止在lua中使用（返回值很大时，在lua中会丢失）
    /// </summary>
    /// <param name="quality"></param>
    /// <returns></returns>
    public static uint GetQualityColorRGBA(int quality)
    {
        InitItemQuality();
        uint ret = 0;
        mQualityColors.TryGetValue(quality, out ret);
        return ret;
    }

    public static uint GetQualityColorARGB(int quality)
    {
        uint rgba = GetQualityColorRGBA(quality);
        return RGBA_To_ARGB(rgba);
    }

    #endregion

    #region 时间转换.

    public const string FREETIME = "{0}:{1}:{2}";
    public const string FREETIME_MINITE = "{0}:{1}";
    /// <summary>
    /// 秒转时间【00:00:00】
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string GetTimeToString(int time)
    {
        return string.Format(FREETIME, (time / 3600).ToString("#00"), (time / 60 % 60).ToString("#00"), (time % 60).ToString("#00"));
    }

    public static string GetMiniteTimeToString(int time)
    {
        return string.Format(FREETIME_MINITE, (time / 60).ToString("#00"), (time % 60).ToString("#00"));
    }

    #endregion

    public static UnityEngine.Color HexToColor(string hexColor)
    {
        UnityEngine.Color color = UnityEngine.Color.white;
        ColorUtility.TryParseHtmlString(hexColor, out color);
        return color;
    }

    public static UnityEngine.Color ARGB2Color(uint argb)
    {
        UnityEngine.Color color = UnityEngine.Color.white;
        float a = argb >> 24 & 0xff;
        float r = argb >> 16 & 0xff;
        float g = argb >> 8 & 0xff;
        float b = argb & 0xff;

        return new UnityEngine.Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public static UnityEngine.Color RGBA2Color(uint rgba)
    {
        UnityEngine.Color color = UnityEngine.Color.white;
        float r = rgba >> 24 & 0xff;
        float g = rgba >> 16 & 0xff;
        float b = rgba >> 8 & 0xff;
        float a = rgba & 0xff;

        return new UnityEngine.Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    public static UnityEngine.Color RGB2Color(uint rgb)
    {
        var rgba = (rgb << 8) | 0x000000ff;
        return RGBA2Color(rgba);
    }

    public static uint RGBA_To_ARGB(uint rgba)
    {
        uint color = (rgba >> 8 & 0xffffff) | (rgba << 24 & 0xff000000);
        return color;
    }

    public static string RGB_To_ARGBString(uint rgb)
    {
        var rgba = (rgb << 8) | 0x000000ff;
        
        return RGBA_To_ARGB(rgba).ToString("x8");
    }

    public static uint ARGB_To_RGBA(uint argb)
    {
        uint color = (argb << 8 & 0xffffff00) | (argb >> 24 & 0xff);
        return color;
    }

    public static object DEBUG_TEST_OBJECT(object obj)
    {
        //var ms = new System.IO.MemoryStream((obj as System.IO.MemoryStream).GetBuffer());
        Debugger.Log("DEBUG_TEST_OBJECT" + obj);
        return null;
    }

    public static Texture2D CaptureCamera(Camera camera, Rect rect, int cullingMask = 0x7fffffff)
    {
        // 创建一个RenderTexture对象 
        RenderTexture rt = new RenderTexture((int)rect.width, (int)rect.height, 16);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机 
        //Camera camera2 = GameSceneMgr.Instance.NGUICamera;

        int cullingMask_old = camera.cullingMask;
        camera.cullingMask = camera.cullingMask & cullingMask;
        camera.targetTexture = rt;
        camera.Render();

        //camera2.cullingMask = camera2.cullingMask & 0x7fffffff;
        //camera2.targetTexture = rt;
        //camera2.Render();

        // 激活这个rt, 并从中中读取像素。  
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        // 重置相关参数，以使用camera继续在屏幕上显示 
        camera.targetTexture = null;
        camera.cullingMask = cullingMask_old;

        //camera2.targetTexture = null;

        RenderTexture.active = null; // JC: added to avoid errors  
        DeepCore.Unity3D.UnityHelper.Destroy(rt);
        // 最后将这些纹理数据，成一个png图片文件  
        //byte[] bytes = screenShot.EncodeToPNG();
        //string filename = Application.dataPath + "/Screenshot.png";
        //System.IO.File.WriteAllBytes(filename, bytes);
        //Debug.Log(string.Format("截屏了一张照片: {0}", filename));

        return screenShot;
    }

    public static TextAttribute createTextAttribute(uint fColor = 0, float fSize = 0)
    {
        var ret = new TextAttribute(fColor, fSize);
        SetTextAttributeFontColorRGB(ret, fColor);
        return ret;
    }

    public static void SetTextAttributeFontColorRGB(TextAttribute attr, uint frbg)
    {
        attr.fontColor = (frbg << 8) | 0x000000ff;
    }

    public static uint GetTextAttributeFontColorRGB(TextAttribute attr)
    {
        var ret = attr.fontColor >> 8;
        return ret;
    }
    public static Vector2 GetLastCharPos(UELabel label)
    {
        var uiText = label.TextGraphics as TextGraphics;
        float h = uiText.preferredHeight;
        var chars = uiText.cachedTextGeneratorForLayout.characters;
        if (chars.Count == 0)
            return Vector2.zero;

        var charInfo = chars[chars.Count - 1];
        Vector2 pos = charInfo.cursorPos;
        pos.x = (pos.x + charInfo.charWidth) / uiText.pixelsPerUnit;
        pos.y = h;
        return pos;
    }

    public static ItemShow GetOrCreateItemShow(DisplayNode node)
    {

        for (int i = 0 ; i < node.NumChildren ; i++)
        {
            var child = node.GetChildAt(i) as ItemShow;
            if (child != null)
                return child;
        }
        ItemShow itemShow = ItemShow.Create();
        node.AddChild(itemShow);
        itemShow.Size2D = node.Size2D;
        return itemShow;
    }
    

    public static Transform FindGameObjectByName(GameObject parent, string name)
    {
        Transform[] tfs = parent.GetComponentsInChildren<Transform>();
        for (int i = tfs.Length - 1 ; i >= 0 ; --i)
        {
            Transform t = tfs[i];
            if (t.name.Trim() == name)
            {
                return t;
            }
        }
        return null;
    }

    public static bool IsGameObjectExists(UnityEngine.GameObject go)
    {
        return go != null && !go.Equals(null);
    }

    public static bool IsObjectExists(UnityEngine.Object go)
    {
        return go != null && !go.Equals(null);
    }

    private static Dictionary<string, Dictionary<string, object>> DBDataCache = new Dictionary<string, Dictionary<string, object>>();
    private static Dictionary<string, List<Dictionary<string, object>>> DBDataListCache = new Dictionary<string, List<Dictionary<string, object>>>();
    /// <summary>
    /// 查找带有唯一索引的db数据, lua中请使用lua接口
    /// </summary>
    /// <param name="table">tableName</param>
    /// <param name="key">key</param>
    /// <returns></returns>
    public static Dictionary<string, object> GetDBData(string table, object key)
    {
        if (string.IsNullOrEmpty(table) || key == null)
        {
            return null;
        }
        Dictionary<string, object> db = null;
        var cacheKey = table + "_" + key.ToString();
        if (!DBDataCache.TryGetValue(cacheKey, out db))
        {
            object[] args = { table, key };
            object result = LuaSystem.Instance.DoFunc("GlobalHooks.DB.Find", args);
            if (result != null)
            {
                db = LuaTableToDictionary(result as SLua.LuaTable);
                DBDataCache.Add(cacheKey, db);
            }
        }
        return db;
    }

    public static int GetIntGameConfig(string key)
    {
        object result = LuaSystem.Instance.DoFunc("GlobalHooks.DB.GetGlobalConfig", key);
        if (result == null)
        {
            return default(int);
        }
        return System.Convert.ToInt32(result);
    }

    public static string GetStringGameConfig(string key)
    {
        object result = LuaSystem.Instance.DoFunc("GlobalHooks.DB.GetGlobalConfig", key);
        if (result == null)
        {
            return null;
        }
        return result.ToString();
    }

    /// <summary>
    /// 获取修行之道显示名称
    /// </summary>
    /// <param name="practiceLv">修行之道等级</param>
    /// <param name="fightPower">玩家当前战力</param>
    /// <returns>示例：筑基初期</returns>
    public static string GetPracticeName(int practiceLv, int fightPower)
    {
        string name = "";
        var db = GameUtil.GetDBData2("practice", "{ artifact_stage =" + practiceLv + "}");
        if (db != null && db.Count > 0)
        {
            string practiceName = db[0]["artifact_name"].ToString();
            //int stageLv = GetPracticeStageLv(practiceLv, fightPower);
            //var dbStage = GameUtil.GetDBData2("practice_stage", string.Format("{{ artifact_stage = {0}, stage_lv = {1} }}", practiceLv, stageLv));
            //string stageName = dbStage[0]["lv_name"].ToString();
            //name = HZLanguageManager.Instance.GetString(practiceName) + HZLanguageManager.Instance.GetString(stageName);
            name = HZLanguageManager.Instance.GetString(practiceName);
        }
        return name;
    }

    /// <summary>
    /// 获取修行之道阶段等级
    /// </summary>
    /// <param name="practiceLv">修行之道等级</param>
    /// <param name="fightPower">玩家当前战力</param>
    /// <returns>1-4</returns>
    public static int GetPracticeStageLv(int practiceLv, int fightPower)
    {
        int stageLv = 1;
        var dbStage = GameUtil.GetDBData2("practice_stage", "{ artifact_stage =" + practiceLv + "}");
        int stageLvMax = dbStage.Count;
        for (int i = 0; i < stageLvMax; ++i)
        {
            var stageData = dbStage[i];
            if (fightPower < int.Parse(stageData["power"].ToString()))
                break;
            stageLv = int.Parse(stageData["stage_lv"].ToString());
        }
        return stageLv;
    }

    /// <summary>
    /// 设置名字和底到label
    /// </summary>
    /// <param name="label"></param>
    /// <param name="practiceLv"></param>
    /// <param name="fightPower"></param>
    /// <returns></returns>
    public static void SetPracticeName(HZLabel label, int practiceLv, int fightPower)
    {
        var db = GameUtil.GetDBData2("practice", "{ artifact_stage =" + practiceLv + "}");
        if (db != null && db.Count > 0)
        {
            string practiceName = db[0]["artifact_name"].ToString();
            //label.Text = HZLanguageManager.Instance.GetString(practiceName);
            label.Layout = HZUISystem.CreateLayout(db[0]["chat_icon"].ToString(), UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
        }
    }

    /// <summary>
    /// 根据条件查询，返回多个结果, lua中请使用lua接口
    /// </summary>
    /// <param name="table">tableName</param>
    /// <param name="key">{ key = value }</param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> GetDBData2(string table, string key)
    {
        if (string.IsNullOrEmpty(table) || string.IsNullOrEmpty(key))
        {
            return null;
        }
        var cacheKey = table + "_" + key.ToString();
        List<Dictionary<string, object>> tbs = null;
        if (!DBDataListCache.TryGetValue(cacheKey, out tbs))
        {
            string lua = string.Format("return GlobalHooks.DB.Find('{0}', {1})", table, key);
            SLua.LuaTable luaTb = (SLua.LuaTable)LuaSystem.Instance.LoadString(lua);
            if (luaTb != null)
            {
                tbs = new List<Dictionary<string, object>>();
                foreach (var it in luaTb)
                {
                    tbs.Add(LuaTableToDictionary(it.value as SLua.LuaTable));
                }
                DBDataListCache.Add(cacheKey, tbs);
            }
        }
        return tbs;
    }

    /// <summary>
    /// 查找完整的db数据, lua中请使用lua接口
    /// </summary>
    /// <param name="table">tableName</param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> GetDBDataFull(string table)
    {
        if (string.IsNullOrEmpty(table))
        {
            return null;
        }
        var cacheKey = table + "_Full";
        List<Dictionary<string, object>> tbs = null;
        if (!DBDataListCache.TryGetValue(cacheKey, out tbs))
        {
            object[] args = { table };
            object result = LuaSystem.Instance.DoFunc("GlobalHooks.DB.GetFullTable", args);
            if (result != null)
            {
                tbs = new List<Dictionary<string, object>>();
                SLua.LuaTable luaTb = result as SLua.LuaTable;
                foreach (var it in luaTb)
                {
                    tbs.Add(LuaTableToDictionary(it.value as SLua.LuaTable));
                }
                DBDataListCache.Add(cacheKey, tbs);
            }
        }
        return tbs;
    }

    public static Dictionary<string, object> LuaTableToDictionary(SLua.LuaTable luaTb)
    {
        if (luaTb != null)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>(luaTb.length());
            foreach (var it in luaTb)
            {
                dict[it.key as string] = it.value;
            }
            return dict;
        }
        else
        {
            return null;
        }
    }

 
    // 飞向背包的特效，老王说不要了
    //public static void ShowPickItemEffect(FuckAssetObject aoe, FlyToBag.FinishCallback callback = null)
    //{

    //    Vector3 scenePos = aoe.gameObject.Position();
    //    DisplayNode node = new DisplayNode();
    //    node.Name = "pickEffect";
    //    aoe.gameObject.Parent(node.UnityObject);
    //    aoe.gameObject.transform.localPosition = Vector3.zero;
    //    //UILayerMgr.SetLayer(aoe.gameObject, (int)PublicConst.LayerSetting.UI);

    //    HZUISystem.Instance.UIPickLayerAddChild(node);
    //    //HZUISystem.Instance.UERoot.AddChild(node);
    //    UILayerMgr.SetLayerOrder(node.UnityObject, 1500, false, (int)PublicConst.LayerSetting.UI);
    //    var sp = GameSceneMgr.Instance.SceneCamera.WorldToScreenPoint(scenePos);
    //    var uiPos = GameSceneMgr.Instance.UICamera.ScreenToWorldPoint(sp);
    //    node.Transform.position = uiPos;
    //    var obj = node.UnityObject;
    //    var c = obj.AddComponent<FlyToBag>();
    //    c.Foe = aoe;
    //    c.Fly(callback);
    //}



    public static bool IsPreparePickItem()
    {
        var actor = TLBattleScene.Instance.Actor;
        var gstate = actor != null ? actor.CurGState : null;
        return gstate is TLAIActor.PreparePickState || gstate is TLAIActor.StartPickState;
    }

    public static void PickItem()
    {
        var actor = TLBattleScene.Instance.Actor;
        var gstate = actor != null ? actor.CurGState : null;
        var pickGState = gstate as TLAIActor.PreparePickState;
        if (pickGState != null)
            pickGState.PickItem();
    }

    public static HashMap<int,TLAvatarInfo> GetAvatarByTemplateId(int tempid)
    {
        var temp = TLBattleScene.Instance.DataRoot.Templates.GetUnit(tempid).Properties as TLUnitProperties;
        return AvatarSnapList2AvatarMap(temp.ServerData.AvatarList);
    }

    //public static bool IsInRoadName(string name)
    //{
    //    return IsInRoadName(name, 2f);
    //}

    //public static bool IsInRoadName(string name, float distance)
    //{
    //    var actor = TLBattleScene.Instance.Actor;
    //    if (actor == null) return false;

    //    var flag = actor.ZActor.Parent.GetFlag(name);
    //    if (flag == null) return false;

    //    return MathVector.getDistance(actor.ZActor.X, actor.ZActor.Y, flag.X, flag.Y) <= distance;
    //}

    [DoNotToLua]
    public static string GetPartFile(HashMap<int, TLAvatarInfo> aMap, int partTag)
    { 
        if (aMap == null || aMap.Count == 0)
            return "";
        TLAvatarInfo avatarInfo = GameUtil.GetTLAvatarInfo(aMap, partTag);
        if (avatarInfo != null)
        {
            return avatarInfo.FileName;
        }
        return "";
    }
 

    //Avatar
    public static TLAvatarInfo GetTLAvatarInfo(HashMap<int, TLAvatarInfo> avatarMap, int PartTag)
    {
        TLAvatarInfo avatrInfo = null;
        if (avatarMap != null&& avatarMap.TryGetValue(PartTag, out avatrInfo))
        {
            return avatrInfo;
        }
        return null;
    }

    private static Dictionary<TLAvatarInfo.TLAvatar, string> enumDict = new Dictionary<TLAvatarInfo.TLAvatar, string>();
    private static void InitEnumDict()
    {
        if (enumDict.Count == 0)
        {
            System.Array values = System.Enum.GetValues(typeof(TLAvatarInfo.TLAvatar));
            for (int i = 0; i < values.Length; i++)
            {
                TLAvatarInfo.TLAvatar t = (TLAvatarInfo.TLAvatar)values.GetValue(i);
                enumDict[t] = t.ToString().ToLower();
            }
        }
    }

    public static string getDummy(int PartTag)
    {
        InitEnumDict();

        string dumyName;
        if (enumDict.TryGetValue((TLAvatarInfo.TLAvatar)PartTag, out dumyName))
        {
            return dumyName;
        }
       
        //if (PartTag == (int)TLAvatarInfo.TLAvatar.Avatar_Head)
        //{
        //    return "Bip001 Head";
        //}
        //else if (PartTag == (int)TLAvatarInfo.TLAvatar.L_Hand_Weapon)
        //{
        //    return "Bip001 Prop2";
        //}
        //else if (PartTag == (int)TLAvatarInfo.TLAvatar.R_Hand_Weapon)
        //{
        //    return "fuck_weapon";
        //}
        //else if (PartTag == (int)TLAvatarInfo.TLAvatar.Rear_Equipment)
        //{
        //    return "Bip001_Treasure";
        //}
        //else if (PartTag == (int)TLAvatarInfo.TLAvatar.Avatar_Wing)
        //{
        //    return "Bip001_Wing";
        //}
        //else if(PartTag == (int)TLAvatarInfo.TLAvatar.R_Hand_Buff)
        //{
        //    return "R_Hand_Buff";
        //}
        return "Root" + PartTag;
    }

    public static string getUnitAssetName(string assetName)
    {
        if (assetName.StartsWith("/"))
        {
            return assetName;
        }
        return "/res/unit/" + assetName + ".assetbundles";
    }

    public static int TryEnumToInt(object enumValue)
    {
        return System.Convert.ToInt32(enumValue);
    }

    public static bool EqualsObj(object a, object b)
    {
        return a == b;
    }

    //设置层级
    public static void ReplaceLayer(GameObject go, int oldLayer, int newLayer)
    {
        if (IsGameObjectExists(go))
        {
            go.layer = go.layer == oldLayer ? newLayer : go.layer;
            Transform[] translist = go.GetComponentsInChildren<Transform>(true);
            foreach (var o in translist)
            {
                if (o.gameObject.layer == oldLayer)
                    o.gameObject.layer = newLayer;
            }
        }
    }

    /// <summary>
    /// 是否是带刘海屏的iphone机器
    /// </summary>
    /// <returns></returns>
    public static bool IsIPhoneX()
    {
#if UNITY_EDITOR
        return Screen.width == 2436 && Screen.height == 1125;
#else
        if (PublicConst.OSType == 5)
        {
            return PlatformMgr.PluginGetScreenNotch() > 0;
        }

        return false;
#endif
    }

    /// <summary>
    /// 是否是带刘海屏的机器（目前只支持android和ios平台）
    /// </summary>
    /// <returns></returns>
    public static int GetNotchX()
    {
#if UNITY_EDITOR
        if(Screen.width == 2436 && Screen.height == 1125)
        {
            return 3 * 44;
        }
#else
        int ret = PlatformMgr.PluginGetScreenNotch();
        if (ret > 0)
        {
            if (PublicConst.OSType == 5)    //ios 返回的是分辨率缩放倍率
            {
                return ret * 44;
            }
            else if (PublicConst.OSType == 6)   //android 直接返回刘海宽度
            {
                return ret;
            }
        }
#endif
        return 0;
    }

    /// <summary>
    /// 返回ios屏幕模式 2x, 3x等
    /// </summary>
    /// <returns></returns>
    public static int IOSScnMode()
    {
#if UNITY_EDITOR
        if (Screen.width == 2436 && Screen.height == 1125)
        {
            return 3;
        }
#else
        if (PublicConst.OSType == 5)
        {
            return PlatformMgr.PluginGetScreenNotch();
        }
#endif
        return 0;
    }

    public static void ShowGetItemTip(int TemplateID, long Count)
    {
        var map = GameUtil.GetDBData("Item", TemplateID);
        if (map == null)
        {
            return;
        }
        var Name = Convert.ToString(map["name"]);
        //var FileName = Convert.ToString(map["item_resource"]);
        var title = HZLanguageManager.Instance.GetString("common_get");
        var itemName = HZLanguageManager.Instance.GetString(Name);
        var Quality = Convert.ToInt32(map["quality"]);
        uint argb = GameUtil.GetQualityColorARGB(Quality);
        var message = string.Format(title, argb.ToString("x16"), itemName, Count);
        GameAlertManager.Instance.ShowFloatingTips(message);
    }
    
    public static void ShowOverFlowExpTips(long Count)
    {
        var title = HZLanguageManager.Instance.GetFormatString("common_storeexp", Count);
        GameAlertManager.Instance.ShowFloatingTips(title);
    }

    public static ItemData CreateItemData(int templateID, uint count, uint maxstack)
    {
        var data = new EntityItemData()
        {
            SnapData = new ItemSnapData()
            {
                Count = count,
                MaxStackCount = maxstack == 0 ? uint.MaxValue : maxstack,
                TemplateID = templateID,
            },
        };
        return new ItemData(data);
    }

    public static ClientSimpleExternBag CreateCustomBag()
    {
        return new ClientSimpleExternBag(0, null);
    }


    public static List<MonsterMapData> GetMonsterData(SLua.LuaTable monster)
    {
        List<MonsterMapData> monsterDataList = new List<MonsterMapData>();
        foreach (SLua.LuaTable.TablePair p in monster)
        {
            int monsterId = Convert.ToInt32(p.key);
            string flagName = p.value as string;
            UnitInfo unit = TLBattleScene.Instance.Actor.ZActor.Templates.GetUnit(monsterId);
            if (unit != null)
            {
                MonsterMapData monsterData = new MonsterMapData();
                monsterData.templateId = monsterId;
                monsterData.name = unit.Name;
                monsterData.level = 10; //测试数据
                var flag = TLBattleScene.Instance.Actor.ZActor.Parent.GetFlag(flagName);
                if (flag != null)
                {
                    monsterData.X = flag.X;
                    monsterData.Y = flag.Y;
                    monsterDataList.Add(monsterData);
                }
            }
        }
        return monsterDataList;
    }

    // 
    public static List<NpcMapData> GetTransferData(SLua.LuaTable transfer)
    {
        List<NpcMapData> TransferList = new List<NpcMapData>();
        foreach (SLua.LuaTable.TablePair p in transfer)
        {
            string transferName = p.key as string;
            string flagName = p.value as string;
            var flag = TLBattleScene.Instance.Actor.ZActor.Parent.GetFlag(flagName);
            if (flag != null)
            {
                NpcMapData mapData = new NpcMapData();
                mapData.templateId = 0;
                mapData.name = transferName;
                mapData.X = flag.X;
                mapData.Y = flag.Y;
                TransferList.Add(mapData);
            }
        }

        return TransferList;
    }

    public static List<NpcMapData> GetNpcData(SLua.LuaTable npc)
    {
        HashMap<int, string> npcLuaData = new HashMap<int, string>();
        foreach (SLua.LuaTable.TablePair p in npc)
        {
            int npcId = Convert.ToInt32(p.key);
            string icon = p.value as string;
            npcLuaData.Add(npcId, icon);
        }

        //HashMap<int, NpcMapData> dataMap = new HashMap<int, NpcMapData>();
        List<NpcMapData> ncpDataList = new List<NpcMapData>();
        var allUnits = TLBattleScene.Instance.Actor.ZActor.Parent.Data.Units;
        foreach (var unit in allUnits)
        {

            if (npcLuaData.ContainsKey(unit.UnitTemplateID))
            {
                NpcMapData mapData = new NpcMapData();
                mapData.templateId = unit.UnitTemplateID;
                mapData.name = unit.Name;
                mapData.Icon = npcLuaData[unit.UnitTemplateID];
                mapData.X = unit.X;
                mapData.Y = unit.Y;
                //dataMap.Add(unit.UnitTemplateID, mapData);
                ncpDataList.Add(mapData);
            }
        }
        //return dataMap;
        return ncpDataList;
    }


    // x.y是坐标 z存了angle 给小地图用
    public static UnityEngine.Vector3 GetActorPos()
    {
        var actor = DataMgr.Instance.UserData.GetActor();
        if (actor == null)
        {
            return UnityEngine.Vector3.one;
        }
        float direction = actor.Direction;
        float angle = 0;
        if (direction < 0)
        {
            angle = (float)Math.Abs((direction * 180 / Math.PI)) - 90;
        }
        else
        {
            angle = (float)((Math.PI - direction) * 180 / Math.PI) + 90;
        }
        return new UnityEngine.Vector3(actor.X, actor.Y, angle);
    }



    // 0.中立 ,1.自己 ,2.对立  
    public static List<PlayMapUnitData> GetPlayMapUnitList(UnitInfo.UnitType unitType,int forceType)
    {
 
        List<PlayMapUnitData> datalist = new List<PlayMapUnitData>();

        TLAIUnit[] allUnits = TLBattleScene.Instance.FindBattleObjectsAs<TLAIUnit>(u =>
        {
            if (u.Info.UType != unitType)
            {
                return false;
            }
            if (forceType == 0 || forceType == TLBattleScene.Instance.Actor.Force)
            {
                return u.Force == forceType;
            }

            return u.Force != 0 && u.Force != TLBattleScene.Instance.Actor.Force;
        });
 
        for (int i = 0; i < allUnits.Length; i++)
        {
            TLAIUnit AIUnit = allUnits[i];

            ZoneUnit zu = AIUnit.ZObj as ZoneUnit;
            if(zu != null)
            {

                if(AIUnit.ObjectID == TLBattleScene.Instance.Actor.ObjectID)
                {
                    int a = 0;
                }

                PlayMapUnitData playUnitData = new PlayMapUnitData();
                playUnitData.templateId = zu.TemplateID;
                playUnitData.ID = AIUnit.ObjectID;
                playUnitData.Name = zu.Name;
               
                playUnitData.X = zu.X;
                playUnitData.Y = zu.Y;

                if (AIUnit.Info.Attributes != null)
                {
                    Properties properties = new Properties();
                    var AttrMap = Properties.ParseLines(AIUnit.Info.Attributes);
                    string icon = AttrMap["icon"];
                    playUnitData.ICON = icon;
                }

                playUnitData.UnitType = AIUnit.Info.UType;

                playUnitData.ForceType = forceType;

                datalist.Add(playUnitData);
            }           
        }

        return datalist;

    }

    //只改了方法里面的内容 暂时没改方法名称，传type改成了传ID
    public static List<PlayMapUnitData> GetPlayMapUnitByMapType(int id)
    {
        var mapInfo = GameUtil.GetDBData2("MapSetting", "{ id =" + id + "}")[0];

        List<PlayMapUnitData> datalist = new List<PlayMapUnitData>();


        HashMap<uint, PlayMapUnitData> teamData = null;
        var result = Convert.ToInt32(mapInfo["show_team"]);
        if(result == 1)
        {
            teamData = DataMgr.Instance.UserData.GetTeamInfoMap();
        }

        //同阵营不包括队友和自己
        result = Convert.ToInt32(mapInfo["same_player"]);
        if (result == 1)
        {
            var samePlayers = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_PLAYER, TLBattleScene.Instance.Actor.Force);

            foreach (var playerData in samePlayers)
            {
        
                if (TLBattleScene.Instance.Actor.ObjectID == playerData.ID || (teamData != null && teamData[playerData.ID] != null))
                {
                    continue;
                }

                datalist.Add(playerData);
            }
        }


        result = Convert.ToInt32(mapInfo["hostile_player"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_PLAYER, -1);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["neutral_monster"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_MONSTER, 0);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["same_monster"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_MONSTER, TLBattleScene.Instance.Actor.Force);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["hostile_monster"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_MONSTER, -1);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["neutral_npc"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_NPC, 0);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["same_npc"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_NPC, TLBattleScene.Instance.Actor.Force);
            datalist.AddRange(data);
        }

        result = Convert.ToInt32(mapInfo["hostile_npc"]);
        if (result == 1)
        {
            var data = GetPlayMapUnitList(UnitInfo.UnitType.TYPE_NPC, -1);
            datalist.AddRange(data);
        }

        return datalist;
    }

    //模板时装数据
    public static HashMap<int, TLAvatarInfo> GetAvatarTemplateIdInfo(int templateid)
    {
        HashMap<int, TLAvatarInfo> map = new HashMap<int, TLAvatarInfo>();
        var info = TLClientBattleManager.Templates.GetUnit(templateid);
        if (info != null)
        {
            map = ToAvatarMap((info.Properties as TLUnitProperties).ServerData.AvatarList);
        }
        return map;
    }

    public static HashMap<int, TLAvatarInfo> ConvertAvatarListToMap(List<AvatarInfoSnap> avatarData)
    {
        HashMap<int, TLAvatarInfo> avatarMap = new HashMap<int, TLAvatarInfo>();
        if(avatarData != null)
        {
            for (int i = 0; i < avatarData.Count; ++i)
            {
                AvatarInfoSnap snap = avatarData[i];
                TLAvatarInfo info = new TLAvatarInfo();
                info.PartTag = (TLAvatarInfo.TLAvatar)snap.PartTag;
                info.FileName = snap.FileName;
                info.DefaultName = snap.DefaultName;
                avatarMap.Add(snap.PartTag, info);
            }
        }
        return avatarMap;
    }
    
    public static HashMap<int, TLAvatarInfo> ToAvatarMap(List<TLAvatarInfo> list)
    {
        if (list == null || list.Count == 0) return null;
        HashMap<int, TLAvatarInfo> ret = new HashMap<int, TLAvatarInfo>();
        TLAvatarInfo info = null;
        for (int i = 0; i < list.Count; i++)
        {
            info = list[i];
            ret.Put((int)info.PartTag, info);
        }

        return ret;
    }
    public static string FormatDateTime(DateTime t, string format)
    {
       return t.ToString(format);
    }

    public static int GetSceneIDToMapID(int sceneid)
    {
        var data = GameUtil.GetDBData("MapData", sceneid);
        object obj = null;
        if (data == null)
        {
            Debugger.LogError("sceneid ["+ sceneid + "] is bullshit in mapdata");
            return sceneid;

        }
        if (data.TryGetValue("zone_template_id", out obj))
        {
            return Convert.ToInt32(obj);
        }

        return sceneid;
    }

    public static string GetHeadIcon(int pro, int gen)
    {
        if (pro >= 0 && pro <=4 && gen >=0 && gen <= 1)
            return string.Format("static/target/{0}_{1}.png", pro, gen);
        else
            return "static/target/default.png";
    }

    public static string GetProIcon(int pro)
    {
        return "$static/TL_staticnew/output/TL_staticnew.xml|TL_static|prol_" + pro;
    }

    public static string GetGrayProIcon(int pro)
    {
        return "$static/TL_staticnew/output/TL_staticnew.xml|TL_static|proa_" + pro;
    }

    public static void EnterBlockTouch(RectTransform except, float alpha)
    {
        DramaUIManage.Instance.highlightMask.SetArrowTransform(true,except, alpha);
    }

    public static void ExitBlockTouch()
    {
        DramaUIManage.Instance.highlightMask.SetArrowTransform(false);
    }

    /// <summary>
    /// 判断当前地图是否可以使用快速传送
    /// </summary>
    /// <param name="isShowTips">true显示错误提示</param>
    /// <returns></returns>
    public static bool CanQuickTransfer(bool isShowTips = false)
    {
        int sceneid = DataMgr.Instance.UserData.MapTemplateId;
        var data = GameUtil.GetDBData("MapData", sceneid);
        object obj = null;
        if (data == null)
        {
            Debugger.LogError("sceneid [" + sceneid + "] is bullshit in mapdata");
            return false;

        }
        if (data.TryGetValue("type", out obj))
        {
            int maptype = Convert.ToInt32(obj);
            string limit = GetStringGameConfig("scene_quicktransfer_type");
            string[] arr = limit.Split(',');
            foreach (var _v in arr)
            {
                if (int.Parse(_v) == maptype)
                {
                    return true;
                }
            }                           
        }
        GameAlertManager.Instance.ShowNotify(HZLanguageManager.Instance.GetString("scene_not_use_transfer"));
        return false;
    }

    public static HashMap<int, TLAvatarInfo> GetNewAvatar(SLua.LuaTable avatar, HashMap<int, TLAvatarInfo> aMap)
    {
        if(aMap == null)
            aMap = new HashMap<int, TLAvatarInfo>();
        foreach (SLua.LuaTable.TablePair p in avatar)
        {
            string partTagName = p.key as string;
            string fileName = p.value as string;
            if (!string.IsNullOrEmpty(partTagName) && !string.IsNullOrEmpty(fileName))
            {
                try
                {
                    object v = Enum.Parse(typeof(TLAvatarInfo.TLAvatar), partTagName);
                    if (v != null)
                    {
                        TLAvatarInfo.TLAvatar PartTag = (TLAvatarInfo.TLAvatar)v;
                        TLAvatarInfo avatarInfo = new TLAvatarInfo();
                        avatarInfo.PartTag = PartTag;
                        avatarInfo.FileName = fileName;
                        aMap.Put((int)PartTag, avatarInfo);
                    }
                }
                catch (Exception e)
                {
                    Debugger.LogError("TLCharacterBaseData " + partTagName, e);
                }
            }
        }
        return aMap;
    }

    private static Dictionary<string, bool> sUIRootNames = new Dictionary<string, bool>
    {
        {"HudRoot",true },
        {"HudMenuRoot",true },
        {"MenuRoot",true },
        {"AlertLayer",true },
        {"DramaUILayer",true },
    };

    public static bool ContainsUITag(string tag)
    {
        var lua = string.Format("return GlobalHooks.UI.UITAG[\"{0}\"] ~= nil", tag);
        return (bool)LuaSystem.Instance.LoadString(lua);
    }

    public static HashMap<int, TLAvatarInfo> AvatarSnapList2AvatarMap(List<AvatarInfoSnap> info)
    {
        var ret = new HashMap<int, TLAvatarInfo>();
        foreach (var snap in info)
        {
            ret.Add(snap.PartTag, new TLAvatarInfo {FileName = snap.FileName, DefaultName = snap.DefaultName, PartTag = (TLAvatarInfo.TLAvatar)snap.PartTag});
        }

        return ret;
    }
    
    public static HashMap<int, TLAvatarInfo> AvatarSnapList2AvatarMap(List<TLAvatarInfo> info)
    {
        var ret = new HashMap<int, TLAvatarInfo>();
        foreach (var snap in info)
        {
            ret.Add((int)snap.PartTag, new TLAvatarInfo {FileName = snap.FileName, DefaultName = snap.DefaultName, PartTag = (TLAvatarInfo.TLAvatar)snap.PartTag});
        }

        return ret;
    }

    
    public static bool CheckGameObjectRaycast(GameObject u)
    {
        // 检查自身状态
        var isActive = UnityHelper.IsObjectExist(u) && u.activeInHierarchy;
        if (!isActive)
        {
            return false;
        }

        var sb = u.GetComponent<TL.UGUI.Skill.SkillButton>();
        if (sb)
        {
            u = u.transform.Find("BG").gameObject;
        }
        var world = u.transform.position;
        var screenPos = UGUIMgr.UGUICamera.WorldToScreenPoint(world);
        var pointData = new PointerEventData(EventSystem.current);

        var rectTransform = u.GetComponent<RectTransform>();

        var offset = new Vector3(rectTransform.rect.x + 0.5f * rectTransform.rect.width, rectTransform.rect.y + 0.5f * rectTransform.rect.height);
        pointData.position = screenPos + offset;
        var result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointData, result);

        if (result.Count == 0)
        {
            return false;
        }

        int rayIndex = -1;
        int minUIRayIndex = -1;
        var pdict = new Dictionary<int, GameObject>();
        for (int i = 0; i < result.Count; i++)
        {
            var obj = result[i].gameObject;
            Transform p = null;
            if (rayIndex < 0)
            {
                p = u.transform;
                while (p != null)
                {
                    if (p.gameObject == obj)
                    {
                        rayIndex = i;
                        break;
                    }

                    p = p.parent;
                }
            }


            // 检查自身父节点  
            p = obj.transform;
            while (p != null)
            {
                if (p.parent && (ContainsUITag(p.name) || sUIRootNames.ContainsKey(p.parent.name)))
                {
                    if (minUIRayIndex < 0)
                    {
                        minUIRayIndex = i;
                    }

                    pdict.Add(i, p.gameObject);
                    break;
                }

                p = p.parent;
            }
        }

        bool ret = true;
        if (rayIndex >= 0)
        {
            GameObject curParent;
            if (pdict.TryGetValue(rayIndex, out curParent))
            {
                // 检查遮挡, 上=》下
                for (int i = 0; i < rayIndex; i++)
                {
                    GameObject obj;
                    if (pdict.TryGetValue(i, out obj))
                    {
                        if (curParent && obj != curParent)
                        {
                            ret = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                //非ui下 rayIndex需要小于ui的
                ret = minUIRayIndex < 0 || rayIndex < minUIRayIndex;
            }
        }
        else
        {
            ret = false;
        }

        return ret;
    }

    public static bool IsShowRedName(int mapTemplateId)
    {
        bool ret = false;

        var data = GameUtil.GetDBData("MapData", mapTemplateId);
        object obj = null;

        if (data != null)
        {
            data.TryGetValue("show_redname", out obj);

            if (obj != null)
            {
                int v = Convert.ToInt32(obj);
                if (v == 1) ret = true;
                else if (v == 0) ret = false;
            }
        }

        return ret;
    }

}
