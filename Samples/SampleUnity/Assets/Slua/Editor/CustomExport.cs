// The MIT License (MIT)

// Copyright 2015 Siney/Pangweiwei siney@yeah.net
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.


using Assets.Scripts;
using Assets.Scripts.Data;
using DeepCore;
using DeepCore.FuckPomeloClient;
using DeepCore.GUI.Cell.Game;
using DeepCore.GUI.Data;
using DeepCore.GUI.Display.Text;
using DeepCore.IO;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIAction;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.Utils;
using DeepMMO.Data;
using DeepMMO.Protocol;
using System;
using System.Collections.Generic;
using Cache;
using DeepCore.Unity3D.Impl;
using ThreeLives.Client.Common.Modules.Quest;
using ThreeLives.Client.Protocol.Data;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using TLClient.Modules;
using TLClient.Modules.Bag;
using TLClient.Protocol;
using TLClient.Protocol.Modules;
using TLClient.Protocol.Modules.Package;
using TLProtocol.Data;
using TLProtocol.Protocol.Client;

namespace SLua
{
    public class CustomExport
    {
        public static void OnGetAssemblyToGenerateExtensionMethod(out List<string> list) {
            list = new List<string> {
                "Assembly-CSharp",
            };
        }

        public static void OnAddCustomClass(LuaCodeGen.ExportGenericDelegate add)
        {
			// below lines only used for demostrate how to add custom class to export, can be delete on your app

            //------------- System --------------
            add(typeof(TouchClickHandle), null);
            add(typeof(System.Action), null);
            add(typeof(System.Action<int>), null);
            add(typeof(System.Action<bool>), null);
            add(typeof(System.Action<string>), null);
            add(typeof(System.Action<float>), null);
            add(typeof(System.Func<int>), null);
            add(typeof(System.Action<object[]>), null);
            add(typeof(System.Action<int, string>), null);
            add(typeof(System.Action<string, string>), null);
            add(typeof(System.Action<int, DisplayNode>), null);
            add(typeof(System.Action<int, Dictionary<int, object>>), null);
            add(typeof(System.Action<Action<bool>>), null);

            add(typeof(Action<PomeloException, BinaryMessage>), null);
            add(typeof(Action<BinaryMessage>), null);
            add(typeof(System.DateTime), "System.DateTime");
            add(typeof(Predicate<ItemData>), null);
            add(typeof(Predicate<string>), null);
            add(typeof(List<int>), "ListInt");
            add(typeof(List<string>), "ListString");
            add(typeof(Dictionary<int, string>), "DictIntStr");
            add(typeof(string), "String");
            add(typeof(System.Action<FuckAssetLoader>), null);
            add(typeof(System.Action<FuckAssetObject>), null);
            add(typeof(Comparison<ItemData>), null);
            add(typeof(Action<AssetGameObject>), null);
            add(typeof(Action<ItemUpdateAction>), null);
            add(typeof(Action<GetZonePlayersUUIDResponse>), null);
            add(typeof(Action<TLRoleSnap>), null);
            add(typeof(Action<Action>), null);
            add(typeof(Action<TLRoleSnap[]>), null);
            add(typeof(Disposable), null);
            add(typeof(MenuBase.ComponentPredicate), null);
            add(typeof(SoundManager), "SoundManager");
            add(typeof(HashMap<string, string>), null);
            // add your custom class here
            // add( type, typename)
            // type is what you want to export
            // typename used for simplify generic type name or rename, like List<int> named to "ListInt", if not a generic type keep typename as null or rename as new type name

            //----------------GameEventManager---------
            add(typeof(DeepCore.GameEvent.Lua.LuaEventManager), null);
            add(typeof(DeepCore.GameEvent.EventManager), null);
            //------------- HZUI --------------
            add(typeof(UIUtils), "UGUI.UIUtils");
            add(typeof(ImageAnchor), "ImageAnchor");
            add(typeof(UIComponent), "UIComponent");
            add(typeof(InteractiveInputField), "InteractiveInputField");
            add(typeof(TextLayerInputField), "TextLayerInputField");
            add(typeof(DisplayNode), "DisplayNode");
            add(typeof(DisplayNodeInteractive), null);
            add(typeof(UETextComponent), "UETextComponent");
            add(typeof(HZRoot), "HZRoot");
            add(typeof(UERoot), "UERoot");
            add(typeof(HZTextButton), "HZTextButton");
            add(typeof(DisplayNode.ChildEventHandler), null);
            add(typeof(UETextButton), "UETextButton");
            add(typeof(HZToggleButton), "HZToggleButton");
            add(typeof(HZToggleButton.LockState), "HZToggleButton.LockState");
            add(typeof(HZImageBox), "HZImageBox");
            add(typeof(UEImageBox), "UEImageBox");
            add(typeof(HZLabel), "HZLabel");
            add(typeof(UELabel), "UELabel");
            add(typeof(HZCanvas), "HZCanvas");
            add(typeof(UECanvas), "UECanvas");
            add(typeof(UEGauge), "UEGauge");
            add(typeof(HZGauge), "HZGauge");
            add(typeof(HZFileNode), "HZFileNode");
            add(typeof(UEFileNode), "UEFileNode");
            add(typeof(HZScrollPan), "HZScrollPan");
            add(typeof(HZScrollPan.ScrollPanUpdateHandler), null);
            add(typeof(HZScrollPan.TrusteeshipChildInit), null);
            add(typeof(UEScrollPan), "UEScrollPan");
            add(typeof(ScrollablePanel), "ScrollablePanel");
            add(typeof(CachedGridScrollablePanel), "CachedGridScrollablePanel");
            add(typeof(AutoSizeScrollablePanel), "AutoSizeScrollablePanel");
            add(typeof(HZTextBox), "HZTextBox");
            add(typeof(UETextBoxBase), "UETextBoxBase");
            add(typeof(UETextBox), "UETextBox");
            add(typeof(HZTextInput), "HZTextInput");
            add(typeof(UETextInput), "UETextInput");
            add(typeof(HZTextBoxHtml), "HZTextBoxHtml");
            add(typeof(UETextBoxHtml), "UETextBoxHtml");
            add(typeof(HZUIEditor), "HZUIEditor");
            add(typeof(UERichTextPan), "UERichTextPan");
            add(typeof(HZRichTextPan), "HZRichTextPan");
            add(typeof(BaseUERichTextBox), "BaseUERichTextBox");
            add(typeof(RichTextBox), "RichTextBox");
            add(typeof(BaseRichTextLayer), "BaseRichTextLayer");
            add(typeof(UGUIRichTextLayer), "UGUIRichTextLayer");
            add(typeof(RichTextClickInfo), "RichTextClickInfo");
            add(typeof(UILayoutStyle), "UILayoutStyle");
            add(typeof(UILayout), "UILayout");
            add(typeof(UIFactory), "UIFactory");
            add(typeof(UIEditor), "UIEditor");
            add(typeof(ActionBase), "ActionBase");
            add(typeof(EaseType), "EaseType");
            add(typeof(MoveAction), "MoveAction");
            add(typeof(FadeAction), "FadeAction");
            add(typeof(DelayAction), "DelayAction");
            add(typeof(ScaleAction), "ScaleAction");
            add(typeof(ScaleAction.ScaleTypes), "ScaleAction.ScaleTypes");
            add(typeof(TextAnchor), "CommonUI.TextAnchor");
            add(typeof(FontStyle), "FontStyle");
            add(typeof(DeepCore.GUI.Display.FontStyle), "UGUIFontStyle");
            add(typeof(TextBorderCount), "TextBorderCount");
            add(typeof(TextAttribute), "TextAttribute");
            add(typeof(CSpriteController), "CSpriteController");
            add(typeof(ICellSpriteController), null);
            add(typeof(UIAnimeType), "UIAnimeType");
            add(typeof(MessageCodeManager), "MessageCodeManager");
            add(typeof(UnityHelper), "UnityHelper");
            //---------------- CommonAI -------------------
            add(typeof(DisplayCell), "DisplayCell");
            add(typeof(DummyNode), "DummyNode");
            add(typeof(QuestDataSnap), null);
            add(typeof(AssetGameObject), "AssetGameObject");
            add(typeof(FuckAssetLoader), "FuckAssetLoader");
            add(typeof(FuckAssetObject), "FuckAssetObject");
            add(typeof(AssetComponent), "AssetComponent");
            //add(typeof(TaskProgress), "TaskProgress");
            add(typeof(TLProgressData), "QuestProgressData");
            //add(typeof(List<TaskProgress>), null);
            add(typeof(List<QuestDataSnap>), null);
            add(typeof(List<TLProtocol.Data.TeamMember>), null);
            add(typeof(ArrayList<TLProtocol.Data.TeamMember>), null);
            add(typeof(PomeloException), null);


            add(typeof(DeepCore.GameData.Zone.UnitInfo), "UnitInfo");
            add(typeof(DeepCore.GameData.Zone.UnitInfo.UnitType), "UnitInfo.UnitType");
            //---------------- TL -----------------

            //**************** OneGameSDK ***********
            add(typeof(OneGameSDK), "OneGameSDK");
            add(typeof(SDKBaseData), "SDKBaseData");
            
            add(typeof(SDKAttName), "SDKAttName");

            add(typeof(ItemUpdateAction), "ItemUpdateAction");
            add(typeof(ItemUpdateAction.ActionType), "ItemUpdateAction.ActionType");
            add(typeof(MenuMgr), "MenuMgr");
            add(typeof(MenuBase), "MenuBase");
            add(typeof(List<MenuBase>), null);
            add(typeof(HudManager), "HudManager");
            add(typeof(HudManager.HudName), "HudManager.HudName");
            add(typeof(DataMgr), "DataMgr");
            add(typeof(FlagPushData), "FlagPushData");
            add(typeof(HZLanguageManager), "LanguageManager");
            add(typeof(UIShowType), "UIShowType");
            add(typeof(HZUISystem), "HZUISystem");
            add(typeof(GameAlertManager), "GameAlertManager");
            add(typeof(AlertDialog), "AlertDialog");
            add(typeof(AttributedString), "AttributedString");
            add(typeof(UserData), "UserData");
            add(typeof(UserData.NotiFyStatus), "UserData.NotiFyStatus");
            add(typeof(SettingData), "SettingData");
            add(typeof(SettingData.NotifySettingState), "SettingData.NotifySettingState");
            add(typeof(MsgData), "MsgData");
            add(typeof(MsgData.NotiFyStatus), "MsgData.NotiFyStatus");
            add(typeof(GuildData), "GuildData");
            add(typeof(GuildData.NotiFyStatus), "GuildData.NotiFyStatus");
            add(typeof(GuildData.GuildBuild), "GuildData.GuildBuild");
            add(typeof(TeamData), "TeamData");
            add(typeof(QuestData), "QuestData");
            add(typeof(QuestState), "QuestState");
            add(typeof(QuestType), "QuestType");
            add(typeof(CacheImage), "CacheImage");
            add(typeof(Quest), null);
            add(typeof(TLQuest), "TLQuest");
            add(typeof(List<Quest>), null);
            add(typeof(List<TLQuest>), null);
            add(typeof(GameUtil), "GameUtil");
            add(typeof(GameSceneMgr), "GameSceneMgr");
            add(typeof(TLBattleScene), "TLBattleScene");
            add(typeof(QuadItemShow), null);
            add(typeof(ItemShow), "ItemShow");
            add(typeof(QuadItemShow.ItemStatus), "ItemStatus");
            add(typeof(ItemContainer), null);
            add(typeof(ItemList), "ItemList");
            add(typeof(ItemPanel), "ItemPanel");
            add(typeof(ItemData), "ItemData");
            add(typeof(BasePackage), null);
            add(typeof(CommonBag), null);
            add(typeof(ClientBag), null);
            add(typeof(ClientNormalBag), null);
            add(typeof(ClientFateBag), null);
            add(typeof(ClientEquipBag), null);
            add(typeof(ClientVirtualBag), null);
            add(typeof(ClientWarehourse), null);
            add(typeof(ClientFateEquipBag), null);
            add(typeof(ClientSimpleExternBag), "ClientSimpleExternBag");
            add(typeof(ClientPackageListener), "PackageListener");
            add(typeof(ItemPropertiesData), "ItemPropertiesData");
            add(typeof(List<ItemPropertyData>), null);
            add(typeof(ArrayList<ItemPropertyData>), null);
            add(typeof(ItemPropertyData), "ItemPropertyData");
            add(typeof(HashMap<string, string>), null);
            add(typeof(HashMap<string, byte>), null);
            add(typeof(HashMap<int, int>), null);
            //add(typeof(PackageAction), "PackageAction");
            add(typeof(ItemSelectHandler), null);
            //add(typeof(RoleBag), "RoleBag");
            //add(typeof(RoleEquipBag), "RoleEquipBag");
            add(typeof(ItemListener), null);
            add(typeof(LuaOutputStream), "LuaOutputStream");
            add(typeof(LuaInputStream), "LuaInputStream");
            add(typeof(BinaryMessage), "BinaryMessage");
            add(typeof(TimeSpan), "TimeSpan");
            add(typeof(TLNetManage), "TLNetManage");
            add(typeof(TLNetManage.PackExtData), "PackExtData");
            add(typeof(TLPropObject.PropType), "TLBattle.RolePropType");
            add(typeof(TLPropObject.ValueType), "TLBattle.RoleValueType");
            add(typeof(NpcQuestManager), "NpcQuestManager");
            add(typeof(TLAINPC.NpcQuestType), "NpcQuestType");
            add(typeof(TLAIActor.MoveEndAction), "MoveEndAction");
            add(typeof(TLAIActor.MoveAndNpcTalk), "MoveAndNpcTalk");
            add(typeof(TLAIActor.MoveAndBattle), "MoveAndBattle");
            add(typeof(TLAIActor.EnterGuildAction), "EnterGuildAction");
            add(typeof(TLAIActor.EnterGuildAndNpcTalk), "EnterGuildAndNpcTalk");
            add(typeof(TLAIActor.TransPortMoveAction), "TransPortMoveAction");
            add(typeof(QuestAutoMaticType), "QuestAutoMaticType");
            add(typeof(TLAINPC), "TLAINPC");
            add(typeof(UILayerMgr), "UILayerMgr");
            add(typeof(AutoRemoveAnimation), "AutoRemoveAnimation");
            add(typeof(TLAIActor), "TLAIActor");
            add(typeof(TLAIPlayer), "TLAIPlayer");
            add(typeof(TLAIUnit), "TLAIUnit");
            add(typeof(ComAIUnit), "ComAIUnit");
            add(typeof(ComAICell), "ComAICell");
            add(typeof(UI3DModelAdapter), "UI3DModelAdapter");
            add(typeof(UI3DModelAdapter.UIModelInfo), "UIModelInfo");
            add(typeof(TLAvatarInfo.TLAvatar), "TLAvatarInfo.TLAvatar");
            add(typeof(FullScreenEffect), "FullScreenEffect");
            add(typeof(PKInfo.PKMode), "PKInfo.PKMode");
            add(typeof(Vector3Move), "Vector3Move");
            add(typeof(TLQuestCondition), "QuestCondition");
            add(typeof(TLClientCreateRoleExtData.ProType), "RoleProType");
            add(typeof(TLClientCreateRoleExtData.GenderType), "GenderType");
            add(typeof(TLAIActor.AutoMoveType), "AutoMoveType");
            //add(typeof(PKInfo.PKLevel), "PKInfo.PKLevel");      
            add(typeof(PracticeScene), "PracticeScene");
            add(typeof(GameGlobal), "GameGlobal");
            add(typeof(FingerGesturesCtrl), "FingerGesturesCtrl");
            add(typeof(UGUIMgr), "UGUIMgr");
            add(typeof(CpjAnimeHelper), "CpjAnimeHelper");
            add(typeof(RenderSystem), "RenderSystem");
            add(typeof(TransformSet), "TransformSet");
			add(typeof(ClientGodSnap.GodStatus), "GodStatus"); 
            add(typeof(TLProtocol.Data.TeamMember), null); 
			add(typeof(TeamSetting), "TeamSetting"); 
			add(typeof(ClientPublicSnapReader<TLClientRoleSnap>), null); 
			add(typeof(PublicSnapReader<TLClientRoleSnap>), null); 
			add(typeof(List<AvatarInfoSnap>), null);
            add(typeof(ArrayList<AvatarInfoSnap>), null);
            add(typeof(AvatarInfoSnap), "AvatarInfoSnap"); 
			add(typeof(List<MsgData.MsgInfo>), null); 
			add(typeof(MsgData.MsgInfo), null);
			add(typeof(MessageHandleData), null);
			add(typeof(AlertHandlerType), "AlertHandlerType");
			add(typeof(ClientHandleMessageResponse), null);
			add(typeof(Action<ClientHandleMessageResponse>), null);
            add(typeof(AlertMessageType), "AlertMessageType");
            add(typeof(TLGameOptionsData), "TLGameOptionsData");
            add(typeof(SyncServerTime), "SyncServerTime");
            add(typeof(NpcMapData), "NpcMapData");
            add(typeof(MonsterMapData), "MonsterMapData");
            add(typeof(PlayerMapData), "PlayerMapData");
            add(typeof(List<PlayerMapData>), null);
            add(typeof(ArrayList<string>), null);
            add(typeof(PlatformMgr), "PlatformMgr");
            //add(typeof(TeamInfoMapData), "TeamInfoMapData");
            add(typeof(SmallMapNpc), "SmallMapNpc");
            add(typeof(SmallMapUnit), "SmallMapUnit");
            add(typeof(GoRoundMgr), "GoRoundMgr");

            add(typeof(PlayMapUnitData), "PlayMapUnitData");
            add(typeof(ZoneInfoSnap), "ZoneInfoSnap");
            add(typeof(DramaUIManage), "DramaUIManage");
            add(typeof(ColliderRotateZ), "ColliderRotateZ");
            add(typeof(PublicConst), "PublicConst");
            add(typeof(PublicConst.SceneType), "PublicConst.SceneType");
            add(typeof(TeamQuestHud),null);
            add(typeof(DayOfWeek), "DayOfWeek");
            add(typeof(BattleScene), "");
            add(typeof(TLUnityDebug), "TLUnityDebug");
            //-------System, HZUI, CommonAI, TL各自的模块对号入座，别都随便往下面加--------------
        }

        public static void OnAddCustomAssembly(ref List<string> list)
        {
            // add your custom assembly here
            // you can build a dll for 3rd library like ngui titled assembly name "NGUI", put it in Assets folder
            // add its name into list, slua will generate all exported interface automatically for you

            //list.Add("NGUI");
        }

        public static HashSet<string> OnAddCustomNamespace()
        {
            return new HashSet<string>
            {
                //"NLuaTest.Mock"
            };
        }

        // if uselist return a white list, don't check noUseList(black list) again
        public static void OnGetUseList(out List<string> list)
        {
            list = new List<string>
            {
                //"UnityEngine.GameObject",
            };
        }

        public static List<string> FunctionFilterList = new List<string>()
        {
            "UIWidget.showHandles",
            "UIWidget.showHandlesWithMoveTool",
        };
        // black list if white list not given
        public static void OnGetNoUseList(out List<string> list)
        {
            list = new List<string>
            {      
                "HideInInspector",
                "ExecuteInEditMode",
                "AddComponentMenu",
                "ContextMenu",
                "RequireComponent",
                "DisallowMultipleComponent",
                "SerializeField",
                "AssemblyIsEditorAssembly",
                "Attribute", 
                "Types",
                "UnitySurrogateSelector",
                "TrackedReference",
                "TypeInferenceRules",
                "FFTWindow",
                "RPC",
                "Network",
                "MasterServer",
                "BitStream",
                "HostData",
                "ConnectionTesterStatus",
                "GUI",
                "EventType",
                "EventModifiers",
                "FontStyle",
                "TextAlignment",
                "TextEditor",
                "TextEditorDblClickSnapping",
                "TextGenerator",
                "TextClipping",
                "Gizmos",
                "ADBannerView",
                "ADInterstitialAd",            
                "Android",
                "Tizen",
                "jvalue",
                "iPhone",
                "iOS",
                "Windows",
                "CalendarIdentifier",
                "CalendarUnit",
                "CalendarUnit",
                "ClusterInput",
                "FullScreenMovieControlMode",
                "FullScreenMovieScalingMode",
                "Handheld",
                "LocalNotification",
                "NotificationServices",
                "RemoteNotificationType",      
                "RemoteNotification",
                "SamsungTV",
                "TextureCompressionQuality",
                "TouchScreenKeyboardType",
                "TouchScreenKeyboard",
                "MovieTexture",
                "UnityEngineInternal",
                "Terrain",                            
                "Tree",
                "SplatPrototype",
                "DetailPrototype",
                "DetailRenderMode",
                "MeshSubsetCombineUtility",
                "AOT",
                "Social",
                "Enumerator",       
                "SendMouseEvents",               
                "Cursor",
                "Flash",
                "ActionScript",
                "OnRequestRebuild",
                "Ping",
                "ShaderVariantCollection",
                "SimpleJson.Reflection",
                "CoroutineTween",
                "GraphicRebuildTracker",
                "Advertisements",
                "UnityEditor",
			    "WSA",
			    "EventProvider",
			    "Apple",
			    "ClusterInput",
				"Motion",
                "UnityEngine.UI.ReflectionMethodsCache",
                "CurveUtility",
				"NativeLeakDetection",
				"Light",
				"NativeLeakDetectionMode",
				"WWWAudioExtensions",
                "UnityEngine.Experimental",
                "VertexHelper",
                "UnityEngine.AnimationCurve",
                "UnityEngine.TextureFormat",
                "Unity.Jobs.LowLevel",
                "Collections.LowLevel.Unsafe",
                "LineRenderer",
                "Physics2D",
                "TrailRenderer",
                "Collections.LowLevel",
                "Rigidbody2D",
                "Location",
                "WebCamTexture",
                "Microphone",
                "Handheld.Vibrate",
            };
        }
    }
}