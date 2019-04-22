using UnityEngine;
using System.Collections.Generic;
using System;
using DeepCore.GUI.Data;
using System.Diagnostics;
using Assets.Scripts.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.GameSlave;
using UnityEngine.EventSystems;
using DeepCore;
using Assets.Scripts;
using SLua;

public class SmallMapNpc : IMapNpcQuestInterface
{

    private readonly static int questStateLength = 3;

    public int templetaId;
    public int questState;

    private HZCanvas mRoot;

    //int [,] tileid = new int[2, 3]{ { 55, 74, 72 },
    //                                {59, 74,73 } };



    private UECanvas cvs_neutral;
    private UECanvas cvs_hostile;
    private UECanvas cvs_same;


    private HZImageBox[] storyQuestImg = new HZImageBox[questStateLength];

    private HZImageBox[] dailyQuestImg = new HZImageBox[questStateLength];

    private HZImageBox mainImg;

    public SmallMapNpc(int templetaId,HZCanvas npcNode)
    {

        this.mRoot = npcNode;
        this.templetaId = templetaId;
        this.questState = 0;
 

        for (int i = 0; i < questStateLength; i++)
        {
            HZImageBox taskImg = mRoot.FindChildByEditName("storyTask" + i.ToString()) as HZImageBox;
            taskImg.Visible = false;
            storyQuestImg[i] = taskImg;
        }

        for (int i = 0; i < questStateLength; i++)
        {
            HZImageBox taskImg = mRoot.FindChildByEditName("dailyTask" + i.ToString()) as HZImageBox;
            taskImg.Visible = false;
            dailyQuestImg[i] = taskImg;
        }
 
        this.cvs_neutral = this.mRoot.FindChildByEditName("cvs_neutral") as UECanvas;
        this.cvs_hostile = this.mRoot.FindChildByEditName("cvs_hostile") as UECanvas;
        this.cvs_same = this.mRoot.FindChildByEditName("cvs_same") as UECanvas;
        this.cvs_neutral.Visible = false;
        this.cvs_hostile.Visible = false;
        this.cvs_same.Visible = false;
        this.SetForce(0);


        DataMgr.Instance.QuestMangerData.AddMapNpcListener(this);
    }

    public bool Visible
    {
        get
        {
            return mRoot.Visible;
        }
        set
        {
            mRoot.Visible = value;
        }
    }

    private byte mForce;
    private UECanvas mShowCvs = null;
    public void SetForce(byte force)
    {
        this.mForce = force;
        this.cvs_neutral.Visible = false;
        this.cvs_hostile.Visible = false;
        this.cvs_same.Visible = false;
        if (force == 0)
        {
            this.cvs_neutral.Visible = true;
            this.mShowCvs = this.cvs_neutral;
        }
        else if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null && force == TLBattleScene.Instance.Actor.Force)
        {
            this.cvs_same.Visible = true;
            this.mShowCvs = this.cvs_same;
        }
        else
        {
            this.cvs_hostile.Visible = true;
            this.mShowCvs = this.cvs_hostile;
        }
    }

    public void HideMain()
    {
        this.cvs_neutral.Visible = false;
        this.cvs_hostile.Visible = false;
        this.cvs_same.Visible = false;
    }

    public void Clear()
    {
        DataMgr.Instance.QuestMangerData.RemoveMapListener(this);
        mRoot.RemoveFromParent(true);
    }

    public void SetPosition(float x, float y)
    {
        this.mRoot.Position2D = new Vector2(x, y);
    }

 
    public int GetTemplateId()
    {
        return this.templetaId;
    }

    public void UpdateQuest(int questId, int questState,int questType)
    {
        for (int i = 0; i < questStateLength; i++)
        {
           
            if (questId == 0)
            {
                storyQuestImg[i].Visible = false;
                dailyQuestImg[i].Visible = false;
                SetForce(this.mForce);
            }
            else
            {
                if(questType == QuestType.TypeStory )
                {
                    
                    bool visible = (i + 1 == (questState));

                    storyQuestImg[i].Visible = visible;

                    if (visible)
                    {
                        this.HideMain();
                    }

                    dailyQuestImg[i].Visible = false;
                  
                }
                else if(questType == QuestType.TypeDaily)
                {
                    bool visible = (i + 1 == (questState));

                    storyQuestImg[i].Visible = false;
                   
                    dailyQuestImg[i].Visible = (i+1 == (questState));

                    if (visible)
                    {
                        this.HideMain();
                    }
                     
                }
                
            }  
        }
    }


    public void SetImage(string ImgPath)
    {
        if (string.IsNullOrEmpty(ImgPath))
        {
            return;
        }
        UILayout img = HZUISystem.CreateLayout(ImgPath, UILayoutStyle.IMAGE_STYLE_BACK_4, 0);
        if (this.mShowCvs != null)
        {
            this.mShowCvs.Layout = img;
        }
        else
        {
            this.mRoot.Layout = img;
        }       
    }
}


public class SmallMapUnit
{
    private HZImageBox cvs_teammate;
    private UECanvas cvs_neutral;
    private UECanvas cvs_hostile;
    private UECanvas cvs_same;


    private UECanvas mRoot;


    public SmallMapUnit(UECanvas unitNode, byte force, bool isTemate = false)
    {
        this.mRoot = unitNode;
 
        this.cvs_teammate = this.mRoot.FindChildByEditName("cvs_teammate") as HZImageBox;
        this.cvs_neutral = this.mRoot.FindChildByEditName("cvs_neutral") as UECanvas;
        this.cvs_hostile = this.mRoot.FindChildByEditName("cvs_hostile") as UECanvas;
        this.cvs_same = this.mRoot.FindChildByEditName("cvs_same") as UECanvas;

        this.SetFore(force, isTemate);
    }


    
    private byte mForce;
    public void SetFore(byte force, bool isTemate = false)
    {
        this.mForce = force;
        this.mRoot.Layout = null;
        this.cvs_teammate.Visible = false;
        this.cvs_neutral.Visible = false;
        this.cvs_hostile.Visible = false;
        this.cvs_same.Visible = false;
        if (isTemate)
        {
            this.cvs_teammate.Visible = true;
            return;
        }
        else
        {
            if (force == 0)
            {
                this.cvs_neutral.Visible = true;
            }
            else if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null && force == TLBattleScene.Instance.Actor.Force)
            {
                this.cvs_same.Visible = true;
            }
            else
            {
                this.cvs_hostile.Visible = true;
            } 
        } 
    }

    public bool Visible
    {
        get
        {
            return mRoot.Visible;
        }
        set
        {
            mRoot.Visible = value;
        }
    }


    public void Clear()
    {
        mRoot.RemoveFromParent(true);
    }

    [DoNotToLua]
    public UECanvas CacheNode()
    {
        //mRoot.RemoveFromParent(false);
        mRoot.Visible = false;
        return mRoot;
    }
    
    public void SetPosition(float unitX,float unitY)
    {
        mRoot.Position2D = new Vector2(unitX, unitY);
    }

    public void SetImage(string ImgPath)
    {
        if(string.IsNullOrEmpty(ImgPath))
        {
            return;
        }

        UILayout img = HZUISystem.CreateLayout(ImgPath, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 0); 
        this.mRoot.Layout = img;

        this.cvs_teammate.Visible = false;
        this.cvs_neutral.Visible = false;
        this.cvs_hostile.Visible = false;
        this.cvs_same.Visible = false;
    }
 
}

 public class SmallMapHud : DisplayNode
{
    private HZRoot mRoot = null;
    private const string FILE_PATH = "xml/hud/ui_hud_map.gui.xml";
   
    public HZRoot Root { get { return mRoot; } }

    public SmallMapHud()
    {
        Init();
    }

    private void Init()
    {
        HZUISystem.SetNodeFullScreenSize(this);
        mRoot = (HZRoot)HZUISystem.CreateFromFile(FILE_PATH);
        HudManager.Instance.InitAnchorWithNode(mRoot, HudManager.HUD_TOP | HudManager.HUD_RIGHT);
        if (mRoot != null) { this.AddChild(mRoot); }

        InitCompmont();
    }

    //每次进场景前会调用
    public void OnEnterScene()
    {

    }

    private HZLabel mLB_coordinate;
    private HZLabel mLB_mapname;
    private HZCanvas smallMap;
    private HZCanvas realMap;

    private HZCanvas smallActor;

    //private Vector2 actorCenterPos;

  

    public void SmallMapTouch(DisplayNode sender, PointerEventData e)
    {
        if (mUiType == 1)
        {
            var TemplateID = DataMgr.Instance.UserData.MapTemplateId;
            object[] param = { "SceneMap", TemplateID, this.mapName };
            MenuMgr.Instance.OpenUIByTag("SmallMapFrame", 0, param);
        }
        else if(mUiType == 2)
        {
            object[] param = { TLBattleScene.Instance.Actor.Force };
            MenuMgr.Instance.OpenUIByTag("PlayMap", 0, param);
        }
       

        //DataMgr.Instance.UserData.TestGetFlag();
    }

    private void InitCompmont()
    {

        this.Enable = false;
        this.EnableChildren = true;

        mLB_coordinate = GetRootUI("lb_coordinate") as HZLabel;     
        mLB_mapname = GetRootUI("lb_mapname") as HZLabel;
        

        smallActor = GetRootUI("csv_actor") as HZCanvas;
        //actorCenterPos = smallActor.Position2D;

        //右上角小地图框
        smallMap = GetRootUI("csv_smallMap") as HZCanvas;

        //用来显示的实际小地图，只显示了这张图的一部分
        realMap = GetRootUI("csv_realMap") as HZCanvas;


        var touchMap = GetRootUI("cvs_maskScroll") as UECanvas; // (GetRootUI("maskScroll") as HZScrollPan).Scrollable;
        //touchMap.Layout = new UILayout();
        touchMap.Enable = true;
        touchMap.EnableChildren = false;
        touchMap.event_PointerUp = SmallMapTouch;

        var sroll = (GetRootUI("maskScroll") as HZScrollPan).Scrollable;
        sroll.Enable = false;
        sroll.IsInteractive = false;
    }

    private int mUiType = 0;
    private int mMapType = 0;

    private Dictionary<string, object> mMapSetting;

    private void InitMapSetting(ZoneLayer layer)
    {

        this.ShowTeamMate = true;

        this.ShowSamePlayer = true;
        this.ShowHostilePlayer = true;

        this.ShowNeutralMonster = true;
        this.ShowSameMonster = true;
        this.ShowHostileMonster = true;

        this.ShowNeutralNpc = true;
        this.ShowSameNpc = true;
        this.ShowHostileNpc = true;
 
        var data = GameUtil.GetDBData2("MapSetting", "{ type =" + this.mMapType + "}");
        if( data != null)
        {
            this.mMapSetting = data[0];
        }

        if (this.mMapSetting != null)
        {
 
            mUiType = Convert.ToInt32(mMapSetting["ui_type"]);
     
           // if (mUiType == 2)
            {
                this.ShowTeamMate = Convert.ToInt32(mMapSetting["show_team"]) == 1;

                this.ShowSamePlayer = Convert.ToInt32(mMapSetting["same_player"]) == 1;
                this.ShowHostilePlayer = Convert.ToInt32(mMapSetting["hostile_player"]) == 1;

                this.ShowNeutralMonster = Convert.ToInt32(mMapSetting["neutral_monster"]) == 1;
                this.ShowSameMonster = Convert.ToInt32(mMapSetting["same_monster"]) == 1;
                this.ShowHostileMonster = Convert.ToInt32(mMapSetting["hostile_monster"]) == 1;


                this.ShowNeutralNpc = Convert.ToInt32(mMapSetting["neutral_npc"]) == 1;
                this.ShowSameNpc = Convert.ToInt32(mMapSetting["same_npc"]) == 1;
                this.ShowHostileNpc = Convert.ToInt32(mMapSetting["hostile_npc"]) == 1;
            }
        }
    }

    public void InitSmallMap(ZoneLayer layer)
    {

        this.InitMapData(layer);

        this.InitMapSetting(layer);

      

        //this.initNpc(layer);

    }

    private Vector2 mSmallMapSize;
    private Vector2 mSmallKuangSize;

    private string mPicName;
    private string mMapImgPath;

    private float mMapW = 0;
    private float mMapH = 0;


    private float minX = 85;  // 170/2
    private float maxX = 330; // 500 - 170;

    private float minY = 50;  // 100/2
    private float maxY = 400; // 500 - 100;

    private bool needUpdateMapX = true;
    private bool needUpdateMapY = true;

    private string mapName;
    private void InitMapData(ZoneLayer layer)
    {
        this.mMapW = layer.TerrainSrc.TotalWidth;
        this.mMapH = layer.TerrainSrc.TotalHeight;
 
        int TemplateID = layer.Data.TemplateID;
        
        var mapData = GameUtil.GetDBData("MapData", TemplateID);
        if (mapData != null)
        {
            mMapType = Convert.ToInt32(mapData["type"]);
            var small_map  = Convert.ToString(mapData["small_map"]);
            this.mMapImgPath = "dynamic/minimap/" + small_map + ".png";
            var mapName = Convert.ToString(mapData["name"]);
            this.mapName = HZLanguageManager.Instance.GetString(mapName);
        }
        else
        {
            this.mPicName = layer.Data.TemplateID.ToString();
            this.mMapImgPath = "dynamic/minimap/" + mPicName + ".png";
            this.mapName = layer.Data.Name;
        }
         
        DataMgr.Instance.UserData.MapName = this.mapName;
        if(!string.IsNullOrEmpty(DataMgr.Instance.UserData.CurSceneGuildName))
            this.mLB_mapname.Text = DataMgr.Instance.UserData.CurSceneGuildName;
        else
            this.mLB_mapname.Text = this.mapName;
 

        UILayout img = HZUISystem.CreateLayout(mMapImgPath, UILayoutStyle.IMAGE_STYLE_BACK_4, 0);//HZUISystem.CreateLayoutFromFile(mMapImgPath, UILayoutStyle.IMAGE_STYLE_BACK_4, 0);

        if (img != null && img.MainTexture != null)
        {

            this.realMap.Layout = img;
           
            Vector2 board = new Vector2(img.ImageSrc.Width, img.ImageSrc.Height);
            this.realMap.Size2D = board;
            //mOperateLayer.Position2D = new Vector2(-200, -200);
         
            mSmallMapSize = board;


            UEScrollPan pan = GetRootUI("maskScroll") as UEScrollPan;
            Vector2 panSize = pan.Size2D;
            if(board.x < panSize.x)
            {
                float x = (panSize.x - board.x) * 0.5f;
                pan.Position2D = new Vector2(x, pan.Position2D.y);
                needUpdateMapX = false;
            }
            else
            {
                pan.Position2D = new Vector2(0, pan.Position2D.y);
                needUpdateMapX = true;
            }

            if(board.y < panSize.y)
            {
                float y = (panSize.y - board.y) * 0.5f;
                pan.Position2D = new Vector2(pan.Position2D.x, y);
                needUpdateMapY = false;
            }
            else
            {
                pan.Position2D = new Vector2(pan.Position2D.x, 0);
                needUpdateMapY = true;
            }
        }

        mSmallKuangSize = smallMap.Size2D;

        this.minX = smallMap.Width / 2;
        this.maxX = realMap.Width - smallMap.Width;

        this.minY = smallMap.Height / 2;
        this.maxY = realMap.Height - smallMap.Height;

        this.realMap.Position2D = new Vector2(0, 0);

    }

    
    private void initNpc(ZoneLayer layer)
    {
        //Stopwatch stopwatch = new Stopwatch();
        //stopwatch.Start();
       
       
        //foreach (var unit in layer.Data.Units)
        //{
        //    UnitInfo info = TLBattleScene.Instance.DataRoot.Templates.GetUnit(unit.UnitTemplateID);
        //    if(info.UType  == UnitInfo.UnitType.TYPE_NPC)
        //    {
        //        HZCanvas smallNpc = GetRootUI("cvs_unit0") as HZCanvas;
        //        HZCanvas npcNode = smallNpc.Clone() as HZCanvas;
        //        npcNode.Visible = true;
        //        realMap.AddChild(npcNode);

        //        //SmallMapNpc npc = new SmallMapNpc(unit.UnitTemplateID, npcNode);
        //        //npc.setPosition(unit.X, unit.Y);
        //        //npc.Visible = false;
        //        //npcMap.Put(unit.UnitTemplateID, npc);
        //        //TODO

        //    }
        //}
        //stopwatch.Stop();
        //Console.WriteLine(stopwatch);
    }

 
    /// <summary>
    /// AI单位(服务器同步，这里只是保存的引用)
    /// </summary>
    private HashMap<uint, ComAICell> mUnitMap = new HashMap<uint, ComAICell>();

 
    private HashMap<uint, SmallMapUnit> mUnitCvsMap = new HashMap<uint, SmallMapUnit>();

    private HashMap<uint, SmallMapNpc> npcMap = new HashMap<uint, SmallMapNpc>();

 
    public bool ShowTeamMate
    {
        get;
        private set;
    }

    public bool ShowSamePlayer
    {
        get;
        private set;
    }

    public bool ShowHostilePlayer
    {
        get;
        private set;
    }

    public bool ShowNeutralMonster
    {
        get;
        private set;
    }

    public bool ShowSameMonster
    {
        get;
        private set;
    }

    public bool ShowHostileMonster
    {
        get;
        private set;
    }

    public bool ShowNeutralNpc
    {
        get;
        private set;
    }

    public bool ShowSameNpc
    {
        get;
        private set;
    }

    public bool ShowHostileNpc
    {
        get;
        private set;
    }

 


    private SmallMapNpc GetNpcCvs(uint ID,int templateId, byte force = 0)
    {
        SmallMapNpc npcUnit;
        if (!npcMap.TryGetValue(ID, out npcUnit))
        {
            HZCanvas temp = GetRootUI("cvs_npc") as HZCanvas;
            HZCanvas npcNode = temp.Clone() as HZCanvas;
            npcUnit = new SmallMapNpc(templateId, npcNode);
            npcMap.Put(ID, npcUnit);
            realMap.AddChild(npcNode);
        }
 
        npcUnit.Visible = true;
        return npcUnit;
    }

 

    Queue<HZCanvas> mUnitCvsCache = new Queue<HZCanvas>();

    private SmallMapUnit GetUnitCvs(uint ObjectID,byte force,bool IsTeammate = false)
    {
        SmallMapUnit unit;
        if (!mUnitCvsMap.TryGetValue(ObjectID, out unit))
        {
            HZCanvas unitNode = null;
            if (mUnitCvsCache.Count > 0)
            {
                unitNode = mUnitCvsCache.Dequeue();
            } 

            if(unitNode == null)
            {
                var temp  = GetRootUI("cvs_unit") as HZCanvas;
                unitNode = temp.Clone() as HZCanvas;
            }
            unit = new SmallMapUnit(unitNode, force, IsTeammate);
            mUnitCvsMap.Put(ObjectID, unit);
            realMap.AddChild(unitNode);
        }
        else
        {
            unit.SetFore(force, IsTeammate);
        }

        unit.Visible = true;
        return unit;
    }

    public void ShowUnitPos(ComAICell unit)
    {
        var ID = unit.ZObj.ObjectID;
        if (unit is TLAIPlayer)
        {
            var player = unit as TLAIPlayer;

            if (ShowTeamMate && DataMgr.Instance.TeamData.IsTeamMember(player.PlayerUUID))
            {
                var smallMapUnit = GetUnitCvs(ID, player.Force,true);
                smallMapUnit.SetPosition(unit.X, unit.Y);
            }
            else
            {
                if((ShowSamePlayer && player.Force == TLBattleScene.Instance.Actor.Force) ||
                    (ShowHostilePlayer && player.Force != TLBattleScene.Instance.Actor.Force && player.Force!= 0))
                {
                    var smallMapUnit = GetUnitCvs(ID, player.Force);
                    smallMapUnit.SetPosition(unit.X, unit.Y);
                }
            }
        }
        else if(unit is TLAINPC)
        {
            int templateId = (unit.ZObj as ZoneUnit).Info.ID;
            var npcUnit = unit as TLAINPC;

            if ((ShowNeutralNpc && npcUnit.Force == 0) 
                || (ShowSameNpc && npcUnit.Force == TLBattleScene.Instance.Actor.Force)
                 ||(ShowHostileNpc && npcUnit.Force != TLBattleScene.Instance.Actor.Force && npcUnit.Force != 0))
            {
                SmallMapNpc npcCvs = GetNpcCvs(ID, templateId, npcUnit.Force);
                npcCvs.SetPosition(unit.X, unit.Y);
                npcCvs.Visible = npcUnit.IsShow;
            }
           
        }
        else if(unit is TLAIMonster)
        {
            var monsterUnit = unit as TLAIMonster;
            if ((ShowNeutralMonster && monsterUnit.Force == 0)
           || (ShowSameMonster && monsterUnit.Force == TLBattleScene.Instance.Actor.Force)
            || (ShowHostileMonster && monsterUnit.Force != TLBattleScene.Instance.Actor.Force && monsterUnit.Force != 0))
            {
                var smallMapUnit = GetUnitCvs(ID, monsterUnit.Force);

                TLAIUnit AIUnit = unit as TLAIUnit;
                if (AIUnit.Info.Attributes != null)
                {
                    Properties properties = new Properties();
                    var AttrMap = Properties.ParseLines(AIUnit.Info.Attributes);
                    string icon = AttrMap["icon"];
                    if (!string.IsNullOrEmpty(icon))
                    {
                        smallMapUnit.SetImage(icon);
                    }
                }
                smallMapUnit.SetPosition(unit.X, unit.Y);
            }
        }
    }



    /// <summary>
    /// 移除单位
    /// </summary>add
    public void RemoveUnit(uint ObjectID)
    {
        //本场景内移出视野不销毁
        ComAICell unit = mUnitMap.RemoveByKey(ObjectID);
        if (unit != null)
        {
            if (unit is TLAIPlayer)
            {
                SmallMapUnit smallMapUnit = mUnitCvsMap.RemoveByKey(ObjectID);
                if (smallMapUnit != null)
                {
                    HZCanvas canvs = (HZCanvas)smallMapUnit.CacheNode();
                    mUnitCvsCache.Enqueue(canvs);
                }
            }
            else if (unit is TLAINPC)
            {
                SmallMapNpc npc;
                if (npcMap.TryGetValue(ObjectID, out npc))
                {
                    npc.Visible = false;

                    //npc.Clear();
                    //npcMap.RemoveByKey(ObjectID);
                }
            }
            else if (unit is TLAIMonster)
            {
                SmallMapUnit smallMapUnit = mUnitCvsMap.RemoveByKey(ObjectID);
                if (smallMapUnit != null)
                {
                    //smallMapUnit.Clear();
                    HZCanvas canvs = (HZCanvas)smallMapUnit.CacheNode();
                    mUnitCvsCache.Enqueue(canvs);
                  
                }
            }
        }
    }



    /// <summary>
    /// 添加单位
    /// </summary>
    public void AddUnit(ComAICell unit)
    {

        if (unit != null && (unit is TLAIPlayer || unit is TLAINPC || unit is TLAIMonster))
        {
            uint uid = unit.ZObj.ObjectID;
            if (!mUnitMap.ContainsKey(uid))
            { 
                mUnitMap.Add(uid, unit);
            }

        }
    }


    float time_elps = 0;
    protected override void OnUpdate()
    {
        time_elps += Time.deltaTime;
        if (time_elps > 0.1f)
        {
            UpdateTagPos();
            time_elps = 0;
        }
    }

    //蛋疼的坐标转方向
    //   -1.5                                 0 
    //PI        0    坐标转方向 ======>>  90      270
    //   1.5                                180  
    private float updateActorDir(float direction)
    {
        float angle = 0;
        if (direction < 0)
        {
            angle = (float)Math.Abs((direction * 180 / Math.PI)) - 90;
        }
        else
        {
            angle = (float)((Math.PI - direction) * 180 / Math.PI) + 90;
        }

        var oldV3 = this.smallActor.Transform.localEulerAngles;
        this.smallActor.Transform.pivot = new Vector2(0.5f, 0.5f);
        this.smallActor.Transform.localEulerAngles = new Vector3(oldV3.x, oldV3.y, angle);
        return angle;
    }



    private void UpdateTagPos()
    {
        if (mMapW > 0 && mMapH > 0)
        {
            foreach (TLAIUnit unit in mUnitMap.Values)
            {
                if (unit is TLAIActor)
                {

                    float direction = unit.Direction;
                    float angle = updateActorDir(direction);

                    var X = unit.X;
                    var Y = unit.Y;
                    UpdateMap(X, Y);
                    UpdateActor(X, Y);


                    //DataMgr.Instance.UserData.ActorPos = new Vector3(X, Y, angle);
                    //UpdateActorPos(X, Y);
                }
                //else if(unit is TLAIPlayer)
                else
                {
                    ShowUnitPos(unit);
                }
            }
        }
    }


    private void UpdateMap(float posX, float posY)
    {
        if(!needUpdateMapX && !needUpdateMapY)
        {
            return;
        }


        float mapX = 0;
        float mapY = 0;

        if (needUpdateMapX)
        {
            if (posX < minX)
            {
                mapX = 0;
            }
            else
            {
                mapX = posX - minX;

                if (mapX > maxX)
                {
                    mapX = maxX;
                }
            }
        }

        if (needUpdateMapY)
        {
            if (posY < minY)
            {
                mapY = 0;
            }
            else
            {
                mapY = posY - minY;

                if (mapY > maxY)
                {
                    mapY = maxY;
                }
            }
        }

       

        // 根据玩家的位置移动小地图（realMap）
        realMap.Position2D = new Vector2(-mapX, -mapY);
        //边缘情况下移动玩家

        
    }

    private void UpdateActor(float posX, float posY)
    {
        this.smallActor.Position2D = new Vector2(posX, posY);
        mLB_coordinate.Text = string.Format("{0},{1}", (int)posX, (int)posY);
    }

  
    //private void UpdateActorPos(float posX, float posY)
    //{
    //    float mapX = 0;
    //    float mapY = 0;

    //    float actorPosX = actorCenterPos.x;
    //    float actorPosY = actorCenterPos.y;

    //    if(posX < minX)
    //    {
    //        mapX = 0;
    //        actorPosX = posX;
    //    }
    //    else
    //    {
    //        mapX = posX - minX;

    //        if(mapX > maxX)
    //        {
    //            mapX = maxX;
    //            actorPosX = mSmallKuangSize.x - (mSmallMapSize.x - posX);
    //        }
    //    }

    //    if (posY < minY)
    //    {
    //        mapY = 0;
    //        actorPosY = posY;
    //    }
    //    else
    //    {
    //        mapY = posY - minY;

    //        if(mapY > maxY)
    //        {
    //            mapY = maxY;
    //            actorPosY = mSmallKuangSize.y - (mSmallMapSize.y - posY);
    //        }
    //    }

    //    // 根据玩家的位置移动小地图（realMap）
    //    realMap.Position2D = new Vector2(-mapX, -mapY);

    //    this.smallActor.Position2D = new Vector2(actorPosX, actorPosY);

    //    //边缘情况下移动玩家

    //    mLB_coordinate.Text = string.Format("{0},{1}", (int)posX, (int)posY);

    //}

    public void Clear(bool reLogin, bool reConnect)
    {
        this.mMapSetting = null;
        //npc
        foreach (var item in this.npcMap.Values)
        {
            item.Clear();
        }
        npcMap.Clear();
 

        //单位  怪物和玩家 
        foreach (var item in this.mUnitCvsMap.Values)
        {
            item.Clear();
        }
        mUnitCvsMap.Clear();

        while (mUnitCvsCache.Count > 0)
        {
            var tempNode = mUnitCvsCache.Dequeue();
            tempNode.RemoveFromParent(true); 
        }

        mUnitMap.Clear();
    }

    Dictionary<string, UIComponent> mCaches = new Dictionary<string, UIComponent>();
    private UIComponent GetRootUI(string childname)
    {
        UIComponent comp;
        if (mCaches.TryGetValue(childname, out comp))
        {
            return comp;
        }
        else
        {
            comp = GetUI(mRoot, childname);
            mCaches[childname] = comp;
            return comp;
        }
    }

    private static UIComponent GetUI(UIComponent parent, string childname)
    {
        if (parent == null)
        {
            return null ;
        }
        UIComponent child = parent.FindChildByEditName<UIComponent>(childname);
        return child;
    }
}
