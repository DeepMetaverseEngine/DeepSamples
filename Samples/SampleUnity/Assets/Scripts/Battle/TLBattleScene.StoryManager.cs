using Assets.Scripts.Cinema;
using DeepCore;
using DeepCore.GameData.Zone;
using SLua;
using System;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D;
using TLBattle.Message;
using UnityEngine;
using uTools;
using DeepCore.Unity3D.Utils;
using DeepCore.Unity3D.Battle;

public partial class TLBattleScene
{
   
    private HashMap<uint, TLCGUnit> mAllCGObj = new HashMap<uint, TLCGUnit>();//战斗中所有程序生成单位的集合. 
    //指定位置播放特效
    public HashMap<int, StoryEffect> mEffectList = new HashMap<int, StoryEffect>();
    public uint mActorObjectId = 0;
    private static uint UnitObjectID { get; set; }
    //默认三围向量值
    private readonly Vector3 _defaultVec3 = new Vector3(10000, 10000, 10000);
    public delegate void CameraMoveOK();

    public bool isStory = false;

    public void init()
    {
        UnitObjectID = 0;
    }
    
    //开始剧情模式
    public void StartStory()
    {
        isStory = true;
    }
    

    /// <summary>
    /// 剧情模式结束
    /// </summary>
    public void StoryOver()
    {
        mCellList.Clear();
        ResetSceneCamera();
        RemoveAllStoryObject();
        RemoveAllEffect();
        
        isStory = false;
    }

    public bool RemoveLastUnit()
    {
        if (mAllCGObj == null|| mAllCGObj.Count == 0)
        {
            return false;
        }
        using (var keys = ListObjectPool<uint>.AllocAutoRelease(mAllCGObj.Keys))
        {
            return RemoveStoryObject((uint)keys[mAllCGObj.Count - 1]);
        }
    }
    //添加一个ZoneObject对象进入场景,objectId10000以上,templateId = 0就是添加玩家自己的模型
    public uint AddStoryUnit(int templateId, Vector2 pos, float direction, System.Action CallBack = null)
    {

        if (templateId == 0)
        {
            templateId = this.Actor.TemplateID;
        }
        UnitInfo uInfo = this.DataRoot.Templates.GetUnit(templateId);
        uint objid = UnitObjectID++;
        if (uInfo == null)
        {
            Debugger.LogError("templateid " + templateId + " is not exist");
            if (CallBack != null)
            {
                CallBack();
                CallBack = null;
            }
            return objid;
        }
        TLCGUnit cgu = new TLCGUnit(this, uInfo, pos, direction, UnitObjectID, (unit) =>
        {
            if (CallBack != null)
            {
                CallBack();
                CallBack = null;
            }
        });
        cgu.LoadModel();
        if (cgu != null)
        {
            mAllCGObj[objid] = cgu;
        }
        return objid;
    }

    //添加现有对象气泡框
    public void AddBubbleTalk(int TemplateID, string context, string TalkActionType, float keepTimeMS)
    {
        if (TemplateID == 0)
        {
            //this.Actor.AddBubbleChat(context, TalkActionType, keepTimeMS);
           var bubble = BubbleChatFrameManager.Instance.ShowBubbleChatFrame(Actor, BubbleChatFrame.PRIORITY_NORMAL + "unit:" + Actor.ObjectID, context, keepTimeMS);
          
        }
        else
        {
            foreach (var unit in this.BattleObjects.Values)
            {
                if (unit is TLAIUnit)
                {
                    TLAIUnit _unit = (unit as TLAIUnit);
                    if (_unit.TemplateID == TemplateID)
                    {
                        //_unit.AddBubbleChat(context, TalkActionType, keepTimeMS);
                         BubbleChatFrameManager.Instance.ShowBubbleChatFrame(_unit,BubbleChatFrame.PRIORITY_NORMAL + "unit:" + _unit.ObjectID, context, keepTimeMS);
                        
                    }
                }
            }
        }
        
        
    }
    //添加气泡框
    public void AddCGBubbleTalk(uint objid, string context, string TalkActionType,float keepTimeMS)
    {
        TLCGUnit unit = GetStoryUnitObject(objid);
        if (unit != null)
        {
            //unit.AddBubbleTalkInfo(context,TalkActionType,keepTimeMS);
            BubbleChatFrameManager.Instance.ShowBubbleChatFrame(unit,BubbleChatFrame.PRIORITY_NORMAL + "cgunit:" + unit.ObjectID, context, keepTimeMS);
           
        }
        else
        {
           Debug.LogError("objid = " + objid + " is not exist");
        }
    }

    //添加cg剧情气泡框
    public BubbleChatFrameTimeUI AddCGBubbleTalkByObject(GameObject target, string context)
    {
        BubbleChatFrameTimeUI chatframe = BubbleChatFrameManager.Instance.ShowBubbleChatFrameByObject(target,context);
        return chatframe;
    }

    //添加气泡框
    public void AddStoryTip(string context,int keepTimeMS)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("talkinfo", context);
        dic.Add("keepTime", keepTimeMS);
        EventManager.Fire("EVENT_UI_STORYTIP", dic);
    }

    //播放cg
    public void PlayCG(string fileName,bool canSkip,System.Action callback,int mapid = 0)
    {
        CutSceneManager.Instance.Play(fileName,canSkip,callback,mapid);
    }
    ////播放cg
    public void CanSkipCG(bool value)
    {
        CutSceneManager.Instance.CanSkip(value);
    }

    private int mLastVoice;
    public void PlayVoice(string fileName)
    {
        string str = "/res/sound/" + fileName + ".assetbundles";
        mLastVoice = SoundManager.Instance.PlaySound(str);
    }

    public void StopVoice()
    {
        SoundManager.Instance.StopSound(mLastVoice);
    }

    public void PlaySound(string fileName)
    {
        string str= "/res/sound/" + fileName + ".assetbundles";
        SoundManager.Instance.PlaySound(str);
    }

    public void StopSound()
    {
        //TLSoundManager.Instance.stopPlaySound();
        //TLBattleFactory.Instance.SoundAdapter.StopBGM();
    }
    public void PlayBGM(string fileName)
    {
        string str = "/res/sound/" + fileName + ".assetbundles";
        SoundManager.Instance.PlayBGM(str);
    }
    public void ChangeBGM(string fileName)
    {
        string str = "/res/sound/" + fileName + ".assetbundles";
        SoundManager.Instance.ChangeBGM(str);
    }
    public void StopBGM()
    {
        SoundManager.Instance.StopBGM();
    }
    public void ResumeBGM()
    {
        SoundManager.Instance.ResumeBGM();
    }
    public void PauseBGM()
    {
        SoundManager.Instance.PauseBGM();
    }
    

    public void BlackScreen(bool isShow)
    {
        if (!isShow)
        {
            CutSceneManager.Instance.BlackScreenOver();
        }
        else
            DramaUIManage.Instance.highlightMask.SetArrowTransform(isShow,null,isShow == true?1:0);
    }
    //通过ObjectId移除某个对象
    public bool RemoveStoryObject(uint objectId)
    {
        TLCGUnit zo = null;
        if (mAllCGObj.TryGetValue(objectId, out zo))
        {
            //RemoveCGUnit(objectId, true);
            zo.Dispose();
            zo = null;
            mAllCGObj.Remove(objectId);
            return true;
        }
        return false;
    }

    public TLCGUnit GetStoryUnitObject(uint objectId)
    {
        TLCGUnit unit = null;
         if (mAllCGObj.TryGetValue(objectId, out unit))
        {
            return unit;
        }
        return unit;
    }

    //移除所有对象
    public void RemoveAllStoryObject()
    {
        using (var keys = ListObjectPool<uint>.AllocAutoRelease(mAllCGObj.Keys))
        {
            for (int i = 0; i < keys.Count; i++)
            {
                RemoveStoryObject(keys[i]);
            }
        }
    }
    //不修改相机的位置和角度
    public void MoveFromActor(uint objectId, CameraMoveOK mameraMoveOKCallBack, float moveTime = 0, EaseType easeType = EaseType.linear)
    {
        MoveFromActor(objectId, _defaultVec3, _defaultVec3, mameraMoveOKCallBack, moveTime, easeType);
    }

    /// <summary>
    /// 移动摄像机到指定位置
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="callback"></param>
    /// <param name="moveTime"></param>
    public void MoveCamera(Vector2 targetPos, CameraMoveOK callback, float moveTime = 0, EaseType easeType = EaseType.linear)
    {

        //Vector3 newVec = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , targetPos.x, targetPos.y, 0);
        //if (Camera.main != null)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(newVec);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        newVec.y = hit.point.y;
        //    }
        //}
        //Vector3 fromPos = TianyuBattle.Instance.Actor.Position;
        //Vector3 fromCameraPos = BattleCamera.pos[BattleCamera.mCurZoomIndex];
        //Vector3 fromCameraAngle = BattleCamera.agl[BattleCamera.mCurZoomIndex];
        //CameraManager.Instance.StoryCameraMove(fromPos, newVec, fromCameraAngle, fromCameraAngle, fromCameraPos, fromCameraPos, () => { callback.Invoke(); }, moveTime, easeType);
    }



    /// <summary>
    /// 剧情相机从主角身上移动到目标对象身上
    /// </summary>
    /// <param name="objectId">摄像机节点要转到的目标对 象的id</param>
    /// <param name="anlVec">要转到的摄像机角度</param>
    /// <param name="cameraVec">要转到的摄像机位置</param>
    /// <param name="moveTime">移动的时间</param>
    public void MoveFromActor(uint objectId, Vector3 anlVec, Vector3 cameraVec, CameraMoveOK mameraMoveOKCallBack, float moveTime = 0, EaseType easeType = EaseType.linear)
    {
        //var zo = GetUnitObject(objectId);
        //if (zo != null)
        //{
        //    float bodyHight = zo.Info.BodyHeight;
        //    Vector2 vec2 = new Vector2(zo.X, zo.Y);
        //    Vector3 targetPos = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , vec2.x, vec2.y, 0);
        //    float z = bodyHight * Mathf.Cos(BattleCamera.pitchNode.localEulerAngles.x * Mathf.PI / 180);
        //    float y = bodyHight * Mathf.Sin(BattleCamera.pitchNode.localEulerAngles.x * Mathf.PI / 180);
        //    targetPos = new Vector3(targetPos.x, targetPos.y + y, targetPos.z + z);
        //    GameDebug.Log("======" + vec2 + "===" + targetPos + "====" + y + ":" + z);
        //    Vector3 fromPos = TianyuBattle.Instance.Actor.Position;
        //    Vector3 fromCameraPos = Vector3.zero;
        //    Vector3 fromCameraAngle = Vector3.zero;

        //    if (BattleCamera != null)
        //    {
        //        fromCameraPos = BattleCamera.positionNode.localPosition;
        //        fromCameraAngle = BattleCamera.pitchNode.localEulerAngles;
        //    }
        //    else
        //    {
        //        GameDebug.LogError("====mBattleCamera is null====");
        //    }
        //    if (anlVec == _defaultVec3)
        //    {
        //        anlVec = fromCameraAngle;
        //    }
        //    if (cameraVec == _defaultVec3)
        //    {
        //        cameraVec = fromCameraPos;
        //    }
        //    if (anlVec.y - fromCameraAngle.y > 180)
        //    {
        //        anlVec.y -= 360;
        //    }

        //    CameraManager.Instance.StoryCameraMove(fromPos, targetPos, fromCameraAngle, anlVec, fromCameraPos, cameraVec, () => { mameraMoveOKCallBack.Invoke(); }, moveTime, easeType);
        //}
        //else
        //{
        //    GameDebug.LogError("Can't find object " + objectId);
        //}
    }

    /// <summary>
    /// 镜头移动到面对boss
    /// </summary>
    /// <param name="objectId">如果配0就是玩家自己</param>
    /// <param name="mameraMoveOKCallBack"></param>
    /// <param name="moveTime"></param>
    public void CameraFaceToUnit(uint objectId, float distance, Vector3 offsets, Vector3 enlurAngle, CameraMoveOK mameraMoveOKCallBack, float moveTime = 0, EaseType easeType = EaseType.linear)
    {
        //IUnit unit = GetUnitObject(objectId);
        //if (unit != null)
        //{
        //    Vector2 vec2 = new Vector2(unit.X, unit.Y);
        //    Vector3 targetPos = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , vec2.x, vec2.y, 0);
        //    Vector3 fromPos = TianyuBattle.Instance.Actor.Position;
        //    Vector3 fromCameraPos = Vector3.zero;
        //    Vector3 fromCameraAngle = Vector3.zero;
        //    if (BattleCamera != null)
        //    {
        //        fromPos = BattleCamera.transform.position;
        //        fromCameraPos = BattleCamera.positionNode.localPosition;
        //        fromCameraAngle = BattleCamera.pitchNode.localEulerAngles;
        //    }
        //    Vector3 angle = unit.EulerAngles();
        //    float r = distance;
        //    float angleY = angle.y + 180;
        //    if (angleY > 360)
        //    {
        //        angleY -= 360;
        //    }
        //    if (angleY - fromCameraAngle.y > 180)
        //    {
        //        angleY -= 360;
        //    }
        //    float y = -90 - angleY;
        //    float x = Mathf.Cos(y * Mathf.Deg2Rad) * r;
        //    float z = Mathf.Sin(y * Mathf.Deg2Rad) * r;
        //    //计算出转动角度和方向
        //    Vector3 anlVec = new Vector3(0, angleY, 0) + enlurAngle;
        //    Vector3 cameraVec = new Vector3(x, r, z) + offsets;
        //    CameraManager.Instance.StoryCameraMove(fromPos, targetPos, fromCameraAngle, anlVec, fromCameraPos, cameraVec, () => { mameraMoveOKCallBack.Invoke(); }, moveTime, easeType);
        //}
        //else
        //{
        //    GameDebug.LogError("Can't find object " + objectId);
        //}

    }

    /// <summary>
    /// 从一个单位移动到另外一个单位
    /// </summary>
    /// <param name="objectId1"></param>
    /// <param name="objectId2"></param>
    /// <param name="mameraMoveOKCallBack"></param>
    /// <param name="moveTime"></param>
    public void CameraFromUnitToUnit(uint objectId1, uint objectId2, CameraMoveOK mameraMoveOKCallBack, float moveTime = 0)
    {
        //var unit1 = GetUnitObject(objectId1);
        //var unit2 = GetUnitObject(objectId2);
        //if (unit1 != null && unit2 != null)
        //{
        //    Vector2 vec21 = new Vector2(unit1.X, unit1.Y);
        //    Vector3 targetPos1 = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , vec21.x, vec21.y, 0);


        //    Vector2 vec22 = new Vector2(unit2.X, unit2.Y);
        //    Vector3 targetPos2 = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , vec22.x, vec22.y, 0);

        //    Vector3 anlVec = BattleCamera.pitchNode.localEulerAngles;
        //    Vector3 cameraVec = BattleCamera.positionNode.localPosition;
        //    CameraManager.Instance.StoryCameraMove(targetPos1, targetPos2, anlVec, anlVec, cameraVec, cameraVec, () => { mameraMoveOKCallBack.Invoke(); }, moveTime);
        //}
        //else
        //{
        //    GameDebug.LogError("Can't find objects ");
        //}
    }


    public void CameraLockUnit(uint objectId)
    {
        //IUnit unit = GetUnitObject(objectId);
        //if (unit != null)
        //{
        //    TianyuBattle.Instance.ClearPositionChange();
        //    TianyuBattle.Instance.SetUnitPositionChange(unit);
        //    BattleCamera.mRunCamera = true;
        //}
    }

    // 摄像机从寻路到一个单位
    public void CameraPath2Unit(uint objectId, string objectPart, Vector3 offset, CameraMoveOK cameraMoveOKCallBack, float moveSpeed, Vector2 cameraView, bool bFindPath)
    {
        //var unit = GetUnitObject(objectId);
        //if (unit != null)
        //{
        //    TianyuAIActor actor = TianyuBattle.Instance.Actor;
        //    Vector3 fromPos = new Vector2(actor.X, actor.Y);
        //    Vector3 targetPos = new Vector2(unit.X, unit.Y);

        //    Vector3 lookAtPos = unit.GetDummyNode(objectPart).gameObject.Position();
        //    offset = unit.TransformDirection(offset);
        //    CameraManager.Instance.StroyCameraPath(fromPos, targetPos, lookAtPos, offset, moveSpeed, () => { cameraMoveOKCallBack.Invoke(); }, cameraView, bFindPath);
        //}
        //else
        //{
        //    GameDebug.LogError("Can't find object " + objectId);
        //}
    }

    public void CameraRotateAround(float angle, float speed, CameraMoveOK rotateOK)
    {
        //CameraManager.Instance.RotateAround(angle, speed, () => { rotateOK(); });
    }

    public void PlaySceneEffect(string key, float duration, CameraMoveOK callBack)
    {
        //UnityEngine.GameObject root = UnityEngine.GameObject.Find("MapNode");
        //string objectName = luaFunction.Instance.getLuaString("textConfig/SceneEffectConfig", "OBJECT_ROOT_" + key);
        //Transform tran = root.transform.FindChild(objectName);
        //if (null == tran)
        //{
        //    callBack.Invoke();
        //    return;
        //}
        //UnityEngine.GameObject go = tran.gameObject;
        //foreach (Camera o in go.GetComponentsInChildren<Camera>(true))
        //{
        //    o.cullingMask = ~(1 << 11);  // 渲染除去层x的所有层
        //}
        //GameGlobal.Instance.StartCoroutine(playSceneEffect(go, duration, callBack));
    }



    private IEnumerator playSceneEffect(UnityEngine.GameObject go, float duration, CameraMoveOK callBack)
    {
        //go.SetActive(true);
        yield return new WaitForSeconds(duration + 1);
        //iTween.CameraFadeAdd();
        //iTween.CameraFadeFrom(1, 0);
        //iTween.CameraFadeTo(0, 1);
        //GameGlobal.Instance.StartCoroutine(TianyuBattle.Instance.DelayDestoryFade());
        //go.SetActive(false);
        //callBack();
    }

    private GameObject mSceneCamera;
    private CameraMoveOK mSceneCameraCallBack;

    public Boolean PlaySceneCamera(string key, float duration, CameraMoveOK callBack)
    {
        //if(mSceneCameraCallBack != null)
        //{
        //    return false;
        //}
        //UnityEngine.GameObject root = UnityEngine.GameObject.Find("MapNode");
        //if(root == null)
        //{
        //    return false;
        //}
        //string objectName = luaFunction.Instance.getLuaString("textConfig/SceneEffectConfig", "CAMERA_" + key);
        //Transform tran = root.transform.FindChild(objectName);

        //mSceneCameraCallBack = callBack;
        //if (null == tran)
        //{
        //    mSceneCameraCallBack.Invoke();
        //    mSceneCameraCallBack = null;
        //    return true;
        //}
        //mSceneCamera = tran.gameObject;
        //foreach (Camera o in mSceneCamera.GetComponentsInChildren<Camera>(true))
        //{
        //    o.cullingMask = ~(1 << 11);  // 渲染除去层x的所有层
        //}
        //GameGlobal.Instance.StartCoroutine(PlaySceneCamera(duration));
        return true;
    }


    private IEnumerator PlaySceneCamera(float duration)
    {
        
        //mSceneCamera.SetActive(true);
        //BattleCamera.mRunCamera = false;
        //BattleCamera.camera.gameObject.SetActive(false);
        yield return new WaitForSeconds(duration);
        //ResetSceneCamera();
    }

    public void ResetSceneCamera()
    {
        //lock (this)
        //{
        //    if (mSceneCamera != null && mSceneCameraCallBack != null)
        //    {
        //        Camera c = mSceneCamera.GetComponent<Camera>();
        //        if (c == null)
        //        {
        //            c = mSceneCamera.GetComponentInChildren<Camera>();
        //        }

        //        if (c != null)
        //        {
        //            CameraManager.Instance.BattleCamera.camera.fieldOfView = c.fieldOfView;
        //            CameraManager.Instance.pitchNode.transform.eulerAngles = c.transform.eulerAngles;
        //            CameraManager.Instance.positionNode.transform.position = c.transform.position;
        //        }
        //        mSceneCamera.SetActive(false);
        //        BattleCamera.camera.gameObject.SetActive(true);
        //        mSceneCameraCallBack.Invoke();
        //        mSceneCamera = null;
        //        mSceneCameraCallBack = null;
        //    }
        //}
        //iTween.CameraFadeDestroy();
    }

    public void CameraShake(float rate, float time)
    {
        //string str = "action=11,time=" + time + ",size=" + rate + ",dur1=0,dur2=0,self=1,blood=0,baiping=0";
        //CameraManager.Instance.doAnimation(str);
    }

    private float mDelayTime = 0;
    private float mDoPlayAnimationTime = 0;
    private bool mDoPlayAnimation = false;
    private List<PlayAnimationCell> mCellList = new List<PlayAnimationCell>();

    /// <summary>
    /// 播放某个人物的动画
    /// </summary>
    /// <param name="objectId">添加的人物id</param>
    /// <param name="action">需要播放的动作</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="mode">播放模式</param>
    public bool PlayAnimation(uint objectId, string animName, System.Action playAnimationCallBack, bool isCrossFade,float delay = 0, float speed = 1,WrapMode mode = WrapMode.Once)
    {
        if (GetUnitAnimation(objectId)) return false;
        var unit = GetStoryUnitObject(objectId);
        if (unit != null)
        {
            mDoPlayAnimation = true;
            mDelayTime = delay;
            mDoPlayAnimationTime = 0;
           // AddPlayAnimation(unit, delay, animName,isCrossFade,mode, speed, playAnimationCallBack);
        }
        else
        {
            Debugger.LogError("Can't find object " + objectId);
        }
        return true;
    }

    private void AddPlayAnimation(TLAIUnit unit,float delay,string animName,bool isCrossFade,WrapMode mode,float speed, System.Action playAnimationCallBack)
    {
        PlayAnimationCell animationCell = new PlayAnimationCell();
        animationCell.mUnit = unit;
        animationCell.mDelayTime = delay * 1000;
        animationCell.mAnimName = animName;
        animationCell.mMode = mode;
        animationCell.speed = speed;
        animationCell.IsCrossFade = isCrossFade;
        animationCell.mCallDoneBack = playAnimationCallBack;
        mCellList.Add(animationCell);
    }

    public void UnitChangeDirection(uint templateId,float dir,bool isSmooth)
    {
        var iter = this.BattleObjects.GetEnumerator();
        while (iter.MoveNext())
        {
            var _unit = iter.Current.Value as TLAIUnit;
            if (_unit != null && _unit.TemplateID == templateId)
            {
                _unit.ChangeDirection(dir, isSmooth);
                break;
            }
        }

    }

    public void FaceToPlayer(uint templateId)
    {
        var iter = this.BattleObjects.GetEnumerator();
        while (iter.MoveNext())
        {
            var _unit = iter.Current.Value as TLAIUnit;
            if (_unit != null && _unit.TemplateID == templateId)
            {
                _unit.FaceTo(GetActor() as TLAIActor);
                break;
            }
        }
    }

    public void UnitStopTurnDirect(uint templateId,bool isStopTurnDir)
    {
        var iter = this.BattleObjects.GetEnumerator();
        while (iter.MoveNext())
        {
            var _unit = iter.Current.Value as TLAIUnit;
            if (_unit != null && _unit.TemplateID == templateId)
            {
                _unit.StopFaceToDirect = isStopTurnDir;
                break;
            }
        }
    }



    public float GetDirFromPlayer(int templateId)
    {
        var iter = this.BattleObjects.GetEnumerator();
        while (iter.MoveNext())
        {
            var _unit = iter.Current.Value as TLAIUnit;
            if (_unit != null && _unit.TemplateID == templateId)
            {
                var actor = GetActor() as TLAIActor;
                return  UnityEngine.Mathf.Atan2(actor.Y - _unit.Y, actor.X - _unit.X);

            }
        }
        return 0;
    }
    [DoNotToLua]
    public void StoryUpdate(int delta)
    {
        if (Actor == null)
            return;
        if (mEffectList != null && mEffectList.Count >0)
        {
            foreach (var effect in mEffectList)
            {
                if (!effect.Value.IsInit)
                {
                    effect.Value.onInit();
                }
            }
        }

        for (int i = mCellList.Count -1; i >=0 ; i--)
        {
            if (mCellList[i].DoAnimation(delta))
            {
                mCellList.RemoveAt(i);
            }
        }
        for (int i = 0; i < mMoveUnitList.Count; i++)
        {
            mMoveUnitList[i].update(delta);
        }
        for (int i = 0; i < mRotationUnitList.Count; i++)
        {
            mRotationUnitList[i].update(delta);
        }
        foreach (var u in mAllCGObj.Values)
        {
            u.Update(delta);
        }

        if (mEffectList.Count > 0)
        {
            List<int> removeeffectlist = new List<int>();
            foreach (KeyValuePair<int, StoryEffect> el in mEffectList)
            {
                if (el.Value.update(delta))
                {
                    removeeffectlist.Add(el.Key);
                }
            }
            foreach (var e in removeeffectlist)
            {
                mEffectList.Remove(e);
            }
        }

        if (doActorRotation)
        {
            mActorRotationTime += delta;
            if (mActorRotationTime >= mActorRotaionNeedTime)
            {
                doActorRotation = false;
                if (ActorRotationCallBack != null)
                {
                    ActorRotationCallBack.Invoke();
                    ActorRotationCallBack = null;
                }
            }
        }
    }

    public bool GetUnitAnimation(uint id)
    {
        foreach (var cell in mCellList)
        {
            if (cell.mUnit.ObjectID == id && cell.mAnim != null && cell.mAnim.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 管理动画播放的类
    /// </summary>
    public class PlayAnimationCell
    {
        public TLAIUnit mUnit = null;
        public float mDelayTime = 0;
        public float mDoTime = 0;
        public string mAnimName = "n_idle";
        public WrapMode mMode = WrapMode.Once;
        public System.Action mCallDoneBack = null;
        public Animation mAnim = null;
        public bool isDone = false;
        //动作剩余时间只能用于once
        public int mAnimLengthMS;
        public float speed = 1;
        private int state = 0;
        public bool IsCrossFade = true;
        public bool DoAnimation(int delta)
        {
            switch (state)
            {
                case 0: //开始
                    mDoTime += delta;
                    mDoTime = Math.Min(mDelayTime, mDoTime);
                    if (mDoTime >= mDelayTime )
                    {
                       
                        if (string.IsNullOrEmpty(mAnimName))
                        {
                            mUnit.PlaySpeicalAnimationByScript(null);
                            DoneAnimation();
                            return true;
                        }
                        state = 1;
                        bool isLoop = false;
                        if (mMode == WrapMode.Loop)
                        {
                            isLoop = true;
                        }
                        mAnimLengthMS = mUnit.GetAnimLength(mAnimName);
                        TLAIUnit.TLSpeicalActionStatus spaction = new TLAIUnit.TLSpeicalActionStatus(mUnit.CurrentState, "sp_" + mAnimName,
                            mAnimName, IsCrossFade, isLoop, speed);
                        mUnit.PlaySpeicalAnimationByScript(spaction);
                        
                        //mUnit.PlayAnim(mAnimName, IsCrossFade, mMode, speed);
                    }
                    break;
                case 1:
                    mAnimLengthMS -= delta;
                    mAnimLengthMS = Math.Max(0, mAnimLengthMS);
                    if (mMode == WrapMode.Loop)
                    {
                        DoneAnimation();
                        return true;
                    }
                    else if (mAnimLengthMS <= 0)
                    {
                        DoneAnimation();
                        return true;
                    }
                    break;
            }

            return false;
        }

        public void DoneAnimation()
        {
            if (mCallDoneBack != null)
            {
                mCallDoneBack.Invoke();
            }
           // BattleStoryManager.GetInstance().mCellList.Remove(this);
        }
    }

    //暂停客户端
    public void PauseClient(bool isPause)
    {
        //if (isPause)
        //{
        //    HudManager.Instance.SkillBar.ClearKeep();
        //    BattleRun.StoryPause = true;
        //}
        //else
        //{
        //    BattleRun.StoryPause = false;
        //    BattleRun.PauseClient = false;
        //    GameDebug.Log("mPause7 is false");
        //}
    }

    //玩家播放动作
    public void ActorPlayAnimation(string animName, WrapMode mode = WrapMode.Once)
    {
        //TianyuAIActor actor = TianyuBattle.Instance.Actor;
        //actor.PlayAnim(animName, false, mode);
    }

    public delegate void UnitMoveOK();


    //计算在U3D坐标系里面跑动朝向
    public float CalUnitTowards(Vector3 from, Vector3 to)
    {
        Vector2 vecPos = new Vector2(to.x - from.x, to.z - from.z);
        float angle = 0f;
        if (vecPos.x != 0)
        {
            //算出弧度值
            angle = Mathf.Atan(vecPos.y / vecPos.x);
            //算出角度值
            angle = angle * Mathf.Rad2Deg;

            if (vecPos.x < 0)
            {
                angle += 180f;
            }
        }
        else
        {
            if (vecPos.y > 0)
            {
                angle = 90f;
            }
            else
            {
                angle = -90f;
            }
        }
        angle = 90 - angle;
        return angle;
    }

    public List<MoveUnit> mMoveUnitList = new List<MoveUnit>();
    //为了返回单位移动到位置后有回调设置个类
    public class MoveUnit
    {
        public int mUnitNeedMoveTime = 0;
        public int mUnitMoveTime = 0;
        public IUnit moveUnit = null;
        public UnitMoveOK moveOkCallBack = null;
        public void update(int delta)
        {
            mUnitMoveTime += delta;
            if (moveUnit != null)
            {
                if (moveUnit.OnPositonChange != null)
                {
                    moveUnit.OnPositonChange(moveUnit.Position);
                }
            }
            if (mUnitMoveTime >= mUnitNeedMoveTime)
            {
                if (moveOkCallBack != null)
                {
                    moveOkCallBack.Invoke();
                    //BattleStoryManager.GetInstance().mMoveUnitList.Remove(this);
                }
            }
        }
    }
    //移动产生的单位到指定位置
    public void MoveUnitToPos(uint objectId, Vector2 pos, String act, float speed, UnitMoveOK callback)
    {
        //var unit = GetUnitObject(objectId);
        //if (unit != null)
        //{
        //    Vector3 toPos = Extensions.ZonePos2NavPos(TianyuBattle.Instance.Terrain.TotalHeight
        //        , pos.x, pos.y, 0);
        //    Vector3 unitPos = unit.Position;

        //    //计算跑动朝向
        //    float angle = CalUnitTowards(unitPos, toPos);
        //    Vector3 forward = toPos - unitPos;
        //    unit.Forward = new Vector3(forward.x, 0, forward.z);
        //    float distance = Vector3.Distance(unitPos, toPos);
        //    //计算移动的时间
        //    TianyuUnitProperties prop = unit.Info.Properties as TianyuUnitProperties;
        //    if (speed == 0)
        //    {
        //        speed = prop.ServerData.Prop.getPropValue(ExtraPropList.ExtraPropType.RunSpeed);
        //    }
        //    float moveTime = distance * 10 / speed;

        //    //主角位移
        //    toPos.y = unitPos.y;
        //    unit.ITweenMoveTo(iTween.Hash("position", toPos, "time", moveTime, "EaseType", "linear"));
        //    //主角播放跑动动作
        //    unit.PlayAnim(act, false, WrapMode.Loop, 1, true);
        //    MoveUnit moveUnit = new MoveUnit();
        //    moveUnit.mUnitNeedMoveTime = (int)(moveTime * 1000);
        //    moveUnit.moveUnit = unit;
        //    moveUnit.moveOkCallBack = callback;
        //    mMoveUnitList.Add(moveUnit);
        //}
        //else
        //{
        //    GameDebug.LogError("Can't find object " + objectId);
        //}
    }

    public void PlayEffect(int key,string nodeparams, Vector2 pos, float scale,float driect, 
        string name, 
        float playTime,
        System.Action playAnimationCallBack = null)
    {

        if (name != "")
        {
            if (!mEffectList.ContainsKey(key))
            {
                var effect = new StoryEffect(name, pos, scale, driect, playTime, nodeparams, playAnimationCallBack);
                mEffectList.Add(key, effect);
            }
        }
    }

    public void RemoveEffectByKey(int key)
    {
        if (mEffectList.ContainsKey(key))
        {
            mEffectList.Get(key).Destory();
            mEffectList.RemoveByKey(key);
        }
    }

    public void RemoveAllEffect()
    {
        using (var keys = ListObjectPool<int>.AllocAutoRelease(mEffectList.Keys))
        {
            for (int i = 0; i < keys.Count; i++)
            {
                RemoveEffectByKey(keys[i]);
            }
        }
    }

    public class StoryEffect
    {
        private UnityEngine.GameObject eff;
        private Vector2 mPos;
        private Vector3 mEulerAngles;
        private int mTime;
        private bool IsLoadingEffect = true;
        private bool mIsDestroy = false;
        private FuckAssetObject mObject = null;
        private System.Action mPlayAnimationCallBack;
        private string mNodeparams;
        private bool isDispose = false;
        private string mPath = "";
        private string mName = "";
        private float mSize;
        private float mDirect;
        private FuckAssetLoader mAssetloader;
        public StoryEffect(string name, Vector2 pos, float size, float direct, float time, string nodeparams, System.Action playAnimationCallBack)
        {
            mTime = mTime == -1 ? -1 : (int)(time * 1000);
            mPlayAnimationCallBack = playAnimationCallBack;
            mNodeparams = nodeparams;
            mPath = "/res/effect/" + name + ".assetbundles";
            mName = name;
            mSize = size;
            mPos = pos;
            mDirect = direct;
            IsInit = false;
            //throw new Exception("老蔡说要重新实现");
            //ResourceMgr.Instance.RequestUnitInstance(path, LoadEffect);
            //HZUnityLoad load = HZUnityLoad.CreateLoad(path, typeof(UnityEngine.GameObject), Path.GetFileNameWithoutExtension(path), LoadEffect);
            //load.Load();
        }
        public bool IsInit{get;set; }
        public void onInit()
        {
            int id = FuckAssetObject.GetOrLoad(mPath, mName, (loader) =>
            {
                if (loader)
                {
                    if (isDispose)
                    {
                        loader.Unload();
                        return;
                    }
                    Transform trans = loader.transform;
                    Transform target = null;
                    bool isSceneEffect = false;
                    if (!string.IsNullOrEmpty(mNodeparams))
                    {
                        int result = 0;
                        var _params = mNodeparams.Split('|');
                        TLAIUnit unit = null;
                        if (int.TryParse(_params[0], out result))
                        {
                            if (result != 0)
                            {
                                unit = TLBattleScene.Instance.GetUnitByTemplateId(result);
                            }
                            else
                            {
                                unit = TLBattleScene.Instance.Actor;
                            }

                            if (unit != null)
                            {
                                target = unit.ObjectRoot.transform;
                                if (_params.Length > 1)
                                {
                                    result = 0;
                                    string _nodename = _params[1];
                                    List<Transform> mTransforms = new List<Transform>();
                                    target = unit.GetDummyNode(_nodename).transform;
                                }
                            }
                            else
                            {
                                Debugger.LogError("not found unit with nodeparams = " + mNodeparams);
                                target = TLBattleScene.Instance.EffectRoot.transform;
                                isSceneEffect = true;
                            }

                        }
                        else
                        {
                            Debugger.LogError("Error nodeparams = " + mNodeparams);
                            target = TLBattleScene.Instance.EffectRoot.transform;
                            isSceneEffect = true;
                        }
                    }
                    else
                    {
                        target = TLBattleScene.Instance.EffectRoot.transform;
                        isSceneEffect = true;
                    }
                    trans.SetParent(target);
                    trans.localPosition = Vector3.zero;
                    trans.rotation = Extensions.ZoneRot2UnityRot(mDirect);
                    trans.localScale = Vector3.one * mSize;
                    mObject = loader;

                    if (isSceneEffect)
                    {
                        
                        Vector3 _pos = Extensions.ZonePos2NavPos(TLBattleScene.Instance.Actor.ZActor.Parent.TerrainSrc.TotalHeight
                          , mPos.x, mPos.y, 0);
                        trans.position = _pos;
                    }
                }
            });
            mAssetloader = FuckAssetLoader.GetLoader(id);
            IsInit = true;
        }

        //private void LoadEffect(string url, UnityEngine.GameObject obj, object userdata)
        //{
        //    IsLoadingEffect = false;
        //    if (mIsDestroy)
        //    {
        //        FuckAssetObject.Unload(obj);
        //        return;
        //    }
        //    eff = obj;
        //    Vector3 effectPos = DeepCore.Unity3D.Utils.Extensions.ZonePos2NavPos(TLBattleScene.Instance.Terrain.TotalHeight
        //        , mPos.x, mPos.y, 0);
        //    eff.transform.position = effectPos;
        //    eff.transform.eulerAngles = mEulerAngles;
        //}
        //public void LoadEffect(HZUnityLoadCallBackArg arg)
        //{
        //    if (arg.IsSuccess)
        //    {
        //        eff = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(arg.asset);
        //        Vector3 effectPos = BattleClientBase.Instance.GetBattleManager().GetU3DPosByUnitCell(mPos);
        //        effectPos = BattleClientBase.Instance.GetBattleManager().AdjustHeight(effectPos);
        //        eff.transform.position = effectPos;
        //        eff.transform.eulerAngles = mEulerAngles;
        //    }
        //}

        public bool update(int delta)
        {
            if (mTime > 0)
            {
                mTime -= delta;
                if (mTime <= 0)
                {
                    Destory();
                    return true;
                }
            }
            return false;
        }

        public void Destory()
        {
            isDispose = true;
            if (mPlayAnimationCallBack != null)
            {
                mPlayAnimationCallBack();
                mPlayAnimationCallBack = null;
            }

            if (mObject != null)
            {
                mObject.Unload();
                mObject = null;
            }
            else if(mAssetloader != null)
            {
                mAssetloader.Discard();
            }
        }
    }

    //public void Clear()
    //{
    //    RemoveAllStoryObject();
    //    //mAllCGObjCallBack.Clear();
    //    mCellList.Clear();
    //}

    //public void Release()
    //{
    //    Clear();
    //    //mInstance = null;
    //}


    //主角转向
    public delegate void UnitRotationOk();
    private UnitRotationOk ActorRotationCallBack;
    private bool doActorRotation = false;
    private int mActorRotaionNeedTime = 0;
    private int mActorRotationTime = 0;

    public void ActorRotation(float direction, float time, UnitRotationOk callback)
    {
        doActorRotation = true;
        mActorRotaionNeedTime = (int)(time * 1000);
        mActorRotationTime = 0;
        //TianyuAIActor actor = TianyuBattle.Instance.Actor;
        ActorRotationCallBack = callback;
        //UnityEngine.GameObject actObj = actor.DummyRoot;
        float angle = direction * Mathf.Rad2Deg + 90;
        Vector3 ro = new Vector3(0, angle);

        //iTween.RotateTo(actObj, iTween.Hash("rotation", ro, "time", time, "easetype", "linear"));
    }

    List<RotationUnit> mRotationUnitList = new List<RotationUnit>();
    //单位转向
    class RotationUnit
    {
        public int mUnitRotaionNeedTime = 0;
        public int mUnitrRotationTime = 0;
        public UnitRotationOk RotationOkCallBack = null;

        public void update(int delta)
        {

            mUnitrRotationTime += delta;
            if (mUnitrRotationTime >= mUnitRotaionNeedTime)
            {
                if (RotationOkCallBack != null)
                {
                    RotationOkCallBack.Invoke();
                    RotationOkCallBack = null;
                }
            }

        }
    }


    //public void UnitRotation(uint objectId, float direction, float time, UnitRotationOk callback)
    //{
    //    var unit = GetStoryUnitObject(objectId);
    //    if (unit != null)
    //    {
    //        float angle = direction * Mathf.Rad2Deg + 90;
    //        Vector3 ro = new Vector3(0, angle);
    //        unit.iTweenRotateTo(iTween.Hash("rotation", ro, "time", time, "easetype", "linear"));
    //        RotationUnit rotationUnit = new RotationUnit();
    //        rotationUnit.mUnitRotaionNeedTime = (int)(time * 1000);
    //        rotationUnit.RotationOkCallBack = callback;
    //        mRotationUnitList.Add(rotationUnit);
    //    }
    //    else
    //    {
    //        Debugger.LogError("Can't find object " + objectId);
    //    }
    //}

    //剧情播放完成
    public void StoryFinish(string filename)
    {
        if (this.Actor != null)
        {
            Debugger.Log("Story ["+filename+"] is finish");
            this.Actor.ZActor.SendAction(new FinishStoryC2B(this.Actor.ObjectID,filename));
        }
    }


    //剧情播放完成
    public void ChangeDirection(float dir,bool isCameraFollow,bool isSmooth)
    {
        if (this.Actor != null)
        {
            if (isCameraFollow)
            {
                this.Actor.ChangeCameraWithDirection(dir, isSmooth);
            }
            else
            {
                this.Actor.ChangeDirection(dir, isSmooth);
            }
            
        }
    }

    //修改unit的动作
    public void ChangeUnitAnimation(int TemplateId,string animname, bool isCrossFade,System.Action playAnimationCallBack, float delay, float speed, int wrapmode)
    {
       
        foreach (var unit in this.BattleObjects)
        {
            if (unit.Value is TLAIUnit)
            {
                var _unit = unit.Value as TLAIUnit;
                if (_unit.TemplateID == TemplateId)
                {
                    WrapMode mode = WrapMode.Once;

                    if (wrapmode != 1)
                    {
                        mode = WrapMode.Loop;
                    }

                    //_unit.RegistAction(DeepCore.GameData.Data.UnitActionStatus.Idle, animname, animname, isCrossFade)
                    AddPlayAnimation(_unit, delay, animname, isCrossFade, mode, speed, playAnimationCallBack);
                }
            }
        }
    }
}