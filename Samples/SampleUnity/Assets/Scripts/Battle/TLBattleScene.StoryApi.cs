//using System;
//using System.Collections.Generic;
//using System.Threading;
//using UnityEngine;
//using SLua;
//
//using UnityEngine.EventSystems;
// 

//namespace Assets.Scripts.Game.Story
//{
//    public  partial class TLBattleScene
//    {
//        public enum Style
//        {
//            BlackBorderAnimationUI,
//            BlackScreenAnimationUI,
//            ScreenTextAnimationUI,
//            ScreenTextMoveAnimationUI,
//            BlackBorderTalk
//        }
//        //protected StoryPlayer StoryPlayer;
//        private bool _mutex;
//        private UELabel mSkipButton;
//        //默认字体大小
//        private int fontSize = 24;
//        //跳过剧情按钮
//        private bool showSkipButton;

//        private void ShowSkipButton()
//        {
//            if (showSkipButton && mSkipButton == null)
//            {
//                mSkipButton = new UELabel("SkipButton");
//                mSkipButton.Name = "SkipButton";
//                mSkipButton.Enable = true;
//                mSkipButton.EnableChildren = true;
//                mSkipButton.IsInteractive = true;
//                mSkipButton.TextGraphics.IsUnderline = true;
//                mSkipButton.Text = HZHZLanguageManager.Instance.GetString("skip");//luaFunction.Instance.getLuaString("textConfig/AllText", "STORY_SKIP");
//                mSkipButton.event_PointerClick = OnStory_Skip;
//                mSkipButton.X = Screen.width - mSkipButton.Width;
//                mSkipButton.Y = 15;
//                HZUISystem.Instance.UIAlertLayerAddChild(mSkipButton);
//            }
//        }


//        private void RemoveSkipButton()
//        {
//            if (mSkipButton != null)
//            {
//                mSkipButton.RemoveFromParent();
//                mSkipButton.Dispose();
//                mSkipButton = null;
//            }
//        }

//        private void OnStory_Skip(DisplayNode sender, PointerEventData e)
//        {
//        }


//        /// <summary>
//        /// 重置剧情摄像机
//        /// </summary>
//        public void CameraReset()
//        {
//        }

//        /// <summary>
//        /// 摄像机抖动
//        /// </summary>
//        /// <param name="rate">振动频率</param>
//        /// <param name="time">持续时间(毫秒)</param>
//        public void CameraShake(float rate, float duration)
//        {
            
//        }

//        /// <summary>
//        /// 摄像机移动
//        /// </summary>
//        /// <param name="targetPos">目标坐标</param>
//        /// <param name="duration">持续时间(毫秒)</param>
//        public void CameraMove(Vector2 targetPos, float duration)
//        {
            
//        }
//        public void CameraMove(Vector2 targetPos, float duration, uTools.EaseType easyType)
//        {
            
//        }

//        /// <summary>
//        /// 移动摄像机到指定单位
//        /// </summary>
//        /// <param name="unitId">目标单位id</param>
//        /// <param name="duration">持续时间(毫秒)</param>
//        public void CameraMoveToUnit(uint unitId, float duration)
//        {
           
//        }
//        public void CameraMoveToUnit(uint unitId, float duration, uTools.EaseType easyType)
//        {
            
//        }


//        public void CameraFaceToUnit(uint unitId, float distance, Vector3 offset, Vector3 enlurAngle, float moveTime)
//        {
           
//        }
//        public void CameraFaceToUnit(uint unitId, float distance, Vector3 offset, Vector3 enlurAngle, float moveTime, uTools.EaseType easyType)
//        {
            
//        }

//        public void CameraLockUnit(uint unitId)
//        {
            

//        public void CameraPathToUnit(uint unitId, string unitPart, Vector3 offset, float speed, Vector2 cameraView, bool findPath)
//        {
           
//        }

//        public void CameraRotateAround(float angle, float speed)
//        {
           
//        }
//        /// <summary>
//        /// 播放场景固定的特效
//        /// </summary>
//        /// <param name="objectName"></param>
//        /// <param name="duration"></param>
//        /// <param name="hideMainCamera"></param>
//		public void PlaySceneEffect(string key, float duration)
//        {
            
//        }

//        public void SavePlayerSeceneEffect(string key)
//        {
           
//        }

//        public void PlaySceneCamera(string key, float duration)
//        {
            
//        }

//        public void ResetSceneCamera()
//        {
           
//        }

//        ///// <summary>
//        ///// 角色移动到指定目标点
//        ///// </summary>
//        ///// <param name="target">目标坐标</param>
//        //public void ActorMoveToPos(Vector3 target)
//        //{
//        //    var e = StoryAction.PopStoryAction<ActorMoveToPos>();
//        //    e.target = target;
//        //    Events.Add(e);

//        //}

//        /// <summary>
//        /// 指定单位移动到指定目标点
//        /// </summary>
//        /// <param name="unitId">目标单位id</param>
//        /// <param name="target">目标坐标</param>
//        public void UnitMoveToPos(uint unitId, Vector3 target)
//        {
            
//        }


//        /// <summary>
//        /// 指定单位移动到指定目标点
//        /// </summary>
//        /// <param name="unitId"></param>
//        /// <param name="target"></param>
//        /// <param name="act">动作名称</param>
//        /// <param name="speed"></param>
//        public StoryAction UnitMoveToPos(uint unitId, Vector3 target, String act, float speed)
//        {
//            var e = StoryAction.PopStoryAction<UnitMoveToPos>();
//            e.unitId = unitId;
//            e.act = act;
//            e.speed = speed;
//            e.target = target;
//            Events.Add(e);
//            return e;
//        }

//        /// <summary>
//        /// 指定目标做动作
//        /// </summary>
//        /// <param name="unitId">指定目标</param>
//        /// <param name="AnimationType">动画类型 从0开始依次 "f_idle", "f_run", "f_hurt", "f_fly",  "f_situp","f_death", "f_stun", "f_keepfly", "f_flydown","f_show","f_win","f_out","f_weakidle"</param>
//        /// <param name="delay">延迟播放</param>
//        public StoryAction PlayAnimation(uint unitId, string AnimationName, float delay)
//        {

//            var e = StoryAction.PopStoryAction<PlayAnimation>();
//            e.unitId = unitId;
//            e.AnimtionName = AnimationName;
//            e.delay = delay;
//            e.mode = "1";
//            e.mShowOut = false;
//            Events.Add(e);
//            return e;
//        }

//        public StoryAction PlayAnimation(uint unitId, string AnimationName, float delay, string mode, bool showOut)
//        {

//            var e = StoryAction.PopStoryAction<PlayAnimation>();
//            e.unitId = unitId;
//            e.AnimtionName = AnimationName;
//            e.delay = delay;
//            e.mode = mode;
//            e.mShowOut = showOut;
//            Events.Add(e);
//            return e;
//        }

//        /// <summary>
//        /// 指定目标做动作
//        /// </summary>
//        /// <param name="unitId">单位id</param>
//        /// <param name="AnimationName">动画类型 从0开始依次 "f_idle", "f_run", "f_hurt", "f_fly",  "f_situp","f_death", "f_stun", "f_keepfly", "f_flydown","f_show","f_win","f_out","f_weakidle"</param>
//        /// <param name="delay">延迟播放</param>
//        /// <param name="mode">播放模式  1=Once 2=Loop</param>
//        public StoryAction PlayAnimation(uint unitId, string AnimationName, float delay, string mode)
//        {
//            var e = StoryAction.PopStoryAction<PlayAnimation>();
//            e.unitId = unitId;
//            e.AnimtionName = AnimationName;
//            e.delay = delay;
//            e.mode = mode;
//            e.mShowOut = false;
//            Events.Add(e);
//            return e;
//        }

//        /// <summary>
//        /// 角色做动作
//        /// </summary>
//        /// <param name="AnimationType">动画类型 从0开始依次 "f_idle", "f_run", "f_hurt", "f_fly",  "f_situp","f_death", "f_stun", "f_keepfly", "f_flydown","f_show","f_win","f_out","f_weakidle"</param>
//        public void ActorPlayAnimation(string animName, string mode)
//        {
//            var e = StoryAction.PopStoryAction<ActorPlayAnimation>();
//            e.animName = animName;
//            e.mode = mode;
//            Events.Add(e);
//        }

//        public void ChangeActorDirection(float direction)
//        {
//            var e = StoryAction.PopStoryAction<ChangeActorDirection>();
//            e.direction = direction;
//            Events.Add(e);
//        }

//        public void ChangeUnitDirection(uint unitId, float direction)
//        {
//            var e = StoryAction.PopStoryAction<ChangeUnitDirection>();
//            e.objectId = unitId;
//            e.direction = direction;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 播放特效
//        /// </summary>
//        /// <param name="pos">目标坐标点</param>
//        /// <param name="name">资源路径(默认路径/res/effect/)</param>
//        /// /// <param name="name">角度</param>
//        public void PlayEffect(int key, Vector2 pos, string name, Vector3 eulerAngle, float time)
//        {
//            var e = StoryAction.PopStoryAction<PlayEffect>();
//            e.key = key;
//            e.pos = pos;
//            e.name = name;
//            e.eulerAngle = eulerAngle;
//            e.time = time;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 通过键移除某个特效
//        /// </summary>
//        /// <param name="key"></param>
//        public void RemoveEffectById(int key)
//        {
//            var e = StoryAction.PopStoryAction<RemoveEffectByKey>();
//            e.key = key;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 移除所有特效
//        /// </summary>
//        public void RemoveAllEffect()
//        {
//            var e = StoryAction.PopStoryAction<RemoveAllEffect>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 摄像机缩进
//        /// </summary>
//        /// <param name="depth">深度</param>
//        /// <param name="time">持续时间(毫秒)</param>
//        public void CameraZoomIn(int depth, int time)
//        {
//            var e = StoryAction.PopStoryAction<CameraZoomAction>();
//            e._size = -depth;
//            e._time = time;

//            Events.Add(e);
//        }

//        /// <summary>
//        /// 摄像机拉远
//        /// </summary>
//        /// <param name="depth">深度</param>
//        /// <param name="time">持续时间(毫秒)</param>
//        public void CameraZoomOut(int depth, int time)
//        {
//            var e = StoryAction.PopStoryAction<CameraZoomAction>();
//            e._size = depth;
//            e._time = time;

//            Events.Add(e);
//        }

//        /// <summary>
//        /// 剧情开始
//        /// </summary>
//        public void StoryStart()
//        {
//            var e = StoryAction.PopStoryAction<TalkStart>();
//            Events.Add(e);
//            StoryPlayer.GetInstance().Start();
//            if (showSkipButton)
//            {
//                ShowSkipButton();
//            }
//        }

//        /// <summary>
//        /// 结束剧情对话
//        /// </summary>
//        public void StoryEnd()
//        {
//            var e = StoryAction.PopStoryAction<TalkEnd>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 剧情对话
//        /// </summary>
//        /// <param name="name">名字</param>
//        /// <param name="content">内容</param>
//        /// <param name="type">位置</param>
//        /// <param name="templeteId">模型id</param>
//        public void Talk(string name, string content, int type, string templete)
//        {
//            var e = StoryAction.PopStoryAction<Talk>();
//            var tmp = templete.Split(',');
//            if (tmp.Length == 5)
//            {
//                e.templeteId = tmp[0];
//                e.position = new Vector3(float.Parse(tmp[1]), float.Parse(tmp[2]), float.Parse(tmp[3]));
//                e.scaleSize = float.Parse(tmp[4]);
//            }
//            else
//            {
//                e.templeteId = null;
//                e.position = Vector3.zero;
//                e.scaleSize = 1;
//            }
//            e.name = name;
//            e.content = content;
//            e.type = type;
//            e.fontSize = this.fontSize;
//            e.delayTime = 0;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 剧情对话
//        /// </summary>
//        /// <param name="name">名字</param>
//        /// <param name="content">内容</param>
//        /// <param name="position">位置</param>
//        /// <param name="templeteId">模型id</param>
//        /// <param name="delayTIme">延迟关闭</param>
//        public void Talk(string name, string content, int type, string templete, int delayTime)
//        {

//            var e = StoryAction.PopStoryAction<Talk>();
//            var tmp = templete.Split(',');
//            if (tmp.Length == 5)
//            {
//                e.templeteId = tmp[0];
//                e.position = new Vector3(float.Parse(tmp[1]), float.Parse(tmp[2]), float.Parse(tmp[3]));
//                e.scaleSize = float.Parse(tmp[4]);
//            }
//            else
//            {
//                e.templeteId = null;
//                e.position = Vector3.zero;
//                e.scaleSize = 1;
//            }

//            e.name = name;
//            e.content = content;
//            e.type = type;
//            e.fontSize = this.fontSize;
//            e.delayTime = delayTime;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 关闭当前已打开UI
//        /// </summary>
//        public void CloseCurrentUI()
//        {
//            var e = StoryAction.PopStoryAction<UIAction>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 显示电影黑幕
//        /// </summary>
//        public void ShowBlackFrame()
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackBorderAnimationAction>();
//            Events.Add(e);
//        }

//        public void ShowBlackFrameImmediate()
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackFrameImmediate>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 隐藏电影黑幕
//        /// </summary>
//        public void HideBlackFrame()
//        {
//            var e = StoryAction.PopStoryAction<HideBlackBorderAnimationAction>();
//            Events.Add(e);
//        }

//        public void ShowBlackScreen()
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackScreenAnimationAction>();
//            e.isImmediate = false;
//            Events.Add(e);
//        }

//        public void ShowBlackScreenImmediate()
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackScreenAnimationAction>();
//            e.isImmediate = true;
//            Events.Add(e);
//        }

//        public void HideBlackScreen()
//        {
//            var e = StoryAction.PopStoryAction<HideBlackScreenAnimationAction>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 屏幕居中渐隐渐出特效
//        /// </summary>
//        /// <param name="table">字符串数组格式见http://192.168.5.6/bugfree/Bug.php?BugID=36093</param>
//        /// <param name="fontSize">字体大小 0为默认</param>
//        /// <param name="lineSpace">行间距 0为默认</param>
//        public void ShowScreenText(LuaTable table, int fontSize, int lineSpace)
//        {
//            var e = StoryAction.PopStoryAction<ShowScreenTextAnimationAction>();
//            e.textList = new List<string>();
//            e.fontSize = fontSize;
//            e.lineSpace = lineSpace;
//            foreach (var item in table)
//            {
//                e.textList.Add(item.value as string);
//            }
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 屏幕横向切入文字 格式见http://192.168.5.6/bugfree/Bug.php?BugID=36598
//        /// </summary>
//        /// <param name="textA">文字A</param>
//        /// <param name="textB">文字B</param>
//        public void ShowMoveText(string textA, string textB)
//        {
//            var e = StoryAction.PopStoryAction<ShowScreenMoveTextAnimationAction>();
//            e.textA = textA;
//            e.textB = textB;
//            Events.Add(e);
//        }

//        public StoryAction ShowBlackBorderTalk(string text)
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackBorderTalkAction>();
//            e.text = text;
//            e.showAlert = false;
//            e.delayCloseTime = 0;
//            Events.Add(e);
//            return e;
//        }

//        public StoryAction ShowBlackBorderTalk(string text, bool showAlert)
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackBorderTalkAction>();
//            e.text = text;
//            e.showAlert = showAlert;
//            Events.Add(e);
//            return e;
//        }

//        public StoryAction ShowBlackBorderTalk(string text, bool showAlert, int delayTime)
//        {
//            var e = StoryAction.PopStoryAction<ShowBlackBorderTalkAction>();
//            e.text = text;
//            e.showAlert = showAlert;
//            e.delayCloseTime = delayTime;
//            Events.Add(e);
//            return e;
//        }

//        public void CloseBlackBorderTalk()
//        {
//            var e = StoryAction.PopStoryAction<CloseBlackBorderTalkAction>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 获得玩家坐标
//        /// </summary>
//        /// <returns></returns>
//        public Vector2 GetActorPosition()
//        {
//            if (TianyuBattle.Instance != null && TianyuBattle.Instance.Actor != null)
//            {
//                return TianyuBattle.Instance.Actor.ZonePos;
//            }
//            else
//            {
//                return Vector2.zero;
//            }
//        }

//        public int GetPetTempleteId()
//        {
//            return DataMgr.Instance.UserData.StoryPetModel;
//        }


//        /// <summary>
//        /// 添加单位到场景
//        /// </summary>
//        /// <param name="objectId">单位id</param>
//        /// <param name="templateId">模板id</param>
//        /// <param name="pos">目标坐标点</param>
//        /// <param name="direction">方向</param>
//        public StoryAction AddUnit(uint objectId, int templateId, Vector2 pos, float direction)
//        {
//            var e = StoryAction.PopStoryAction<AddUnit>();
//            e._templateId = templateId;
//            e._pos = pos;
//            e._direction = direction;
//            e._objectId = objectId;
//            e.mShowOut = true;
//            e.mVisible = true;
//            Events.Add(e);
//            return e;
//        }

//        public StoryAction AddUnit(uint objectId, int templateId, Vector2 pos, float direction, bool showOut)
//        {
//            var e = StoryAction.PopStoryAction<AddUnit>();
//            e._templateId = templateId;
//            e._pos = pos;
//            e._direction = direction;
//            e._objectId = objectId;
//            e.mShowOut = showOut;
//            e.mVisible = true;
//            Events.Add(e);
//            return e;
//        }

//        public StoryAction AddUnit(uint objectId, int templateId, Vector2 pos, float direction, bool showOut, bool visible)
//        {
//            var e = StoryAction.PopStoryAction<AddUnit>();
//            e._templateId = templateId;
//            e._pos = pos;
//            e._direction = direction;
//            e._objectId = objectId;
//            e.mShowOut = showOut;
//            e.mVisible = visible;
//            Events.Add(e);
//            return e;
//        }


//        /// <summary>
//        /// 移除指定单位
//        /// </summary>
//        /// <param name="objectId">单位id</param>
//        public void RemoveUnit(uint objectId)
//        {
//            var e = StoryAction.PopStoryAction<RemoveUnit>();
//            e._objectId = objectId;
//            Events.Add(e);
//        }

//        public void RemoveAllUnit()
//        {
//            var e = StoryAction.PopStoryAction<RemoveAllUnit>();
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 显示/隐藏所有单位
//        /// </summary>
//        /// <param name="flag"></param>
//        public void ShowAllUnit(bool flag)
//        {
//            var e = StoryAction.PopStoryAction<ShowAllUnit>();
//            e._flag = flag;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 显示聊天气泡
//        /// </summary>
//        /// <param name="objectid">id</param>
//        /// <param name="content">内容</param>
//        /// <param name="talkActionType">聊天气泡样式</param>
//        /// <param name="delayTimeMs">延迟时间</param>
//        /// <param name="keepTimeMs">持续时间</param>
//        public void ShowBubbleTalk(uint objectid, string content, string talkActionType, int delayTimeMs, int keepTimeMs)
//        {
//            var e = StoryAction.PopStoryAction<ShowBubbleTalk>();
//            e._objectId = objectid;
//            e._content = content;
//            e._talkActionType = talkActionType;
//            e._delayTimeMs = delayTimeMs;
//            e._keepTimeMs = keepTimeMs;
//            Events.Add(e);
//        }


//        /// <summary>
//        /// 战斗HUD显示/隐藏
//        /// </summary>
//        /// <param name="visible"></param>
//        public void ShowBattleHUD(bool visible)
//        {
//            var e = StoryAction.PopStoryAction<ShowBattleHUD>();
//            e.flag = visible;
//            Events.Add(e);
//        }

//        public void ShowActor(bool visible)
//        {
//            var e = StoryAction.PopStoryAction<ShowActor>();
//            e.flag = visible;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 为指定单位添加buff 0位玩家自己
//        /// </summary>
//        /// <param name="ObjectID">单位id</param>
//        /// <param name="buffId"></param>
//        public void AddBuff(uint ObjectID, int buffID)
//        {
//            var e = StoryAction.PopStoryAction<AddBuff>();
//            e.ObjectID = ObjectID;
//            e.buffID = buffID;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 为指定单位移除buff 0位玩家自己
//        /// </summary>
//        /// <param name="ObjectID">单位id</param>
//        /// <param name="buffId"></param>
//        public void RemoveBuff(uint ObjectID, int buffID)
//        {
//            var e = StoryAction.PopStoryAction<RemoveBuff>();
//            e.ObjectID = ObjectID;
//            e.buffID = buffID;
//            Events.Add(e);
//        }

//        //切场景
//        public void ChangeScene(int Id, int sceneType)
//        {
//            var e = StoryAction.PopStoryAction<ChangeScene>();
//            e.SceneId = Id;
//            e.SceneType = sceneType;
//            Events.Add(e);
//        }


//        /// <summary>
//        /// 播放指定音效(assetbundle)
//        /// </summary>
//        /// <param name="name"></param>
//        public void PlaySound(string name)
//        {
//            var e = StoryAction.PopStoryAction<PlaySound>();
//            e.name = name;
//            e.type = 0;
//            Events.Add(e);
//        }

//        /// <summary>
//        /// 播放声音
//        /// </summary>
//        /// <param name="name"></param>
//        /// <param name="type">0为人声 1为音效</param>
//        public void PlaySound(string name, int type)
//        {
//            var e = StoryAction.PopStoryAction<PlaySound>();
//            e.name = name;
//            e.type = type;
//            Events.Add(e);
//        }


//        protected void WaitAction()
//        {
//            while (!_mutex)
//            {
//                try
//                {
//                    Thread.Sleep(10);
//                }
//                catch (Exception err)
//                {
//                    GameDebug.LogWarning(err.Message);
//                }
//            }
//        }

//        public void Wait(uint t)
//        {
//            var e = StoryAction.PopStoryAction<Wait>();
//            e.init(t, null, false);
//            Events.Add(e);
//        }

//        public void WaitAction(StoryAction action)
//        {
//            var e = StoryAction.PopStoryAction<Wait>();
//            e.init(0, action, false);
//            Events.Add(e);
//        }

//        public void WaitAll()
//        {
//            var e = StoryAction.PopStoryAction<Wait>();
//            e.init(0, null, true);
//            Events.Add(e);
//        }

//        public void SetMutexComplete()
//        {
//            _mutex = true;
//        }

//        public void SetFontSize(int size)
//        {
//            fontSize = size;
//        }

//        public void ShowSkipButton(bool visible)
//        {
//            showSkipButton = visible;
//        }

//        public void Realese()
//        {
//            RemoveSkipButton();
//        }
//    }
//}
