
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using TLBattle.Common.Plugins;
using DeepCore.Unity3D.Battle;
using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.Unity3D;

public class TLCGUnit : BattleObject, IUnit
{
    private List<TLAvatarInfo> mAvatarList;
    private List<AttachLoadInfo> mEquipLoads = new List<AttachLoadInfo>();
    private UnitInfo m_info;
    private uint ID;
    private int mEquipLoadIndex = 0;
    private DisplayCell mShadow;
    private Action<TLCGUnit> mLoadCallBack;
    private int mCurrentLayer;
    private Vector2 mPos = Vector2.zero;
    private Action<TLCGUnit> callBack;
    private float m_direction;
    public float Direction { get { return m_direction; } }
    private BattleInfoBar m_InfoBar = null;
    private DummyNode mBubbleNode = null;
    public delegate void OnPositionChangeHandler(Transform target);
    private OnPositionChangeHandler mOnPositionChange;
    public event OnPositionChangeHandler OnPositionChange { add { mOnPositionChange += value; } remove { mOnPositionChange -= value; } }
    private string mfileName;
    private bool mIsSingleModule = false;//纯加载
    public UnitInfo Info
    {
        get { return m_info; }
    }

    public uint ObjectID
    {
        get { return ID; }
    }

    public float X
    {
        get { return mPos.x; }
    }

    public float Y
    {
        get { return mPos.y; }
    }
    public Action<Vector3> OnPositonChange { get; set; }
    private bool IsCG = true;
    private Transform mHeadTransform;
    private HashMap<int, TLAvatarInfo> mAvatarmap;

    //cg编辑器调用
    public TLCGUnit(BattleScene battleScene, UnitInfo _info, Action<TLCGUnit> loadCallBack = null, string name = "cgUnit") : base(battleScene, name)
    {
        m_info = _info;
        mLoadCallBack = loadCallBack;
    }
    //avatar系统的cg
    public TLCGUnit(BattleScene battleScene, UnitInfo _info, HashMap<int, TLAvatarInfo> avatarmap, Action<TLCGUnit> loadCallBack = null, string name = "cgUnit") : this(battleScene, _info, loadCallBack, name)
    {
        mAvatarmap = avatarmap;
    }

    //直接加载 比如特效 特殊场景饰品
    public TLCGUnit(BattleScene battleScene, string fileName, Action<TLCGUnit> callBack = null, string name = "cgUnit") : base(battleScene, name)
    {
        mIsSingleModule = true;
        mfileName = fileName;
        mLoadCallBack = callBack;
    }
    //lua创建单位
    public TLCGUnit(BattleScene battleScene, UnitInfo _info, Vector2 pos, float direction, uint unitObjectID, Action<TLCGUnit> callBack = null, string name = "cgUnit")
     : this(battleScene, _info, callBack, name)
    {
        this.m_info = _info;
        this.m_direction = direction;
        this.mPos = pos;
        this.ID = unitObjectID;
        this.callBack = callBack;
        IsCG = false;
    }

    //
    public RenderVehicle Vehicle
    {
        get; private set;
    }

    public bool IsPlayAnimation
    {
        get;
        set;
    }
 
    //玩家播放动作
    public void PlayAnimForPlayer(string actionname)
    {
        if (animPlayer != null)
        {
            animPlayer.Play(actionname);
        }

    }

    public void LoadModel()
    {
        int id = 0;
        string bodyFile = GameUtil.GetPartFile(mAvatarmap, (int)TLAvatarInfo.TLAvatar.Avatar_Body);
        if (mIsSingleModule || string.IsNullOrEmpty(bodyFile))
        {
            if (string.IsNullOrEmpty(m_info.FileName))
            { 
                return;
            }

            this.mfileName = m_info.FileName;
            id = DisplayCell.LoadModel(mfileName, System.IO.Path.GetFileNameWithoutExtension(mfileName), (loader) =>
            { 
                if (IsDisposed)
                {
                    if (mLoadCallBack != null)
                    {
                        mLoadCallBack(this);
                    }
                    if (loader)
                    {
                        DisplayCell.Unload();
                    }
                    return;
                }
                if (loader)
                {
                    OnLoadModelFinish(loader);
                }
                else
                {
                    if (mLoadCallBack != null)
                    {
                        mLoadCallBack(this);
                    }
                }
            });
            
        }
        else
        {
            (DisplayCell as RenderUnit).ChangeBody(bodyFile, (aoe) =>
            {
                OnLoadModelFinish(aoe);
   
                foreach (var item in mAvatarmap.Values)
                {
                    string FileName = item.FileName;
                    if (item.PartTag != TLAvatarInfo.TLAvatar.Ride_Avatar01
                       && item.PartTag != TLAvatarInfo.TLAvatar.Avatar_Body
                       && !string.IsNullOrEmpty(FileName))
                    {
                        (DisplayCell as RenderUnit).ChangeAvatar(FileName, (int)item.PartTag, animPlayer, (succ) =>
                        {
                            if (succ && item.PartTag != TLAvatarInfo.TLAvatar.Foot_Buff)
                            {
                                GameUtil.ReplaceLayer(DisplayCell.ObjectRoot, (int)PublicConst.LayerSetting.CharacterUnlit, (int)PublicConst.LayerSetting.SelfLayer);
                            }
                        });
                    }
                }
            });
        }
        
    }

    protected  void OnLoadModelFinish(FuckAssetObject aoe)
    {
        if (aoe)
        {
            //this.animPlayer.AddAnimator(aoe.gameObject.GetComponent<Animator>());
            if (m_info != null && m_info.BodyScale != 0)
            {
                this.ObjectRoot.transform.localScale *= this.m_info.BodyScale;
            }

            if (!mIsSingleModule)
            {
                CorrectDummyNode();
            }
            mCurrentLayer = aoe.gameObject.layer;
            
            DummyNode BubbleNode = GetDummyNode("Head_Name");
            mHeadTransform = BubbleNode != null ? BubbleNode.transform : this.DisplayCell.ObjectRoot.transform;
            Debugger.Log("Name = " + aoe.name + "is in CG");
            
            UILayerMgr.SetLayer(ObjectRoot, IsCG? (int)PublicConst.LayerSetting.CG:(int)PublicConst.LayerSetting.SelfLayer);
            
        }
        if (mLoadCallBack != null)
        {
            mLoadCallBack(this);
        }
    }
    protected override void OnUpdate(float deltaTime)
    {
        base.OnUpdate(deltaTime);
        UpdatePos();
    }
    protected void AddInfoBar(GameObject obj)
    {
        if (m_InfoBar == null)
        {
            DummyNode node = this.GetDummyNode("Head_Name");
            this.m_InfoBar = BattleInfoBarManager.AddInfoBar(node.transform, Vector3.zero, false, true);
        }
        m_InfoBar.SetName(this.Info.Name);
        
    }
    protected void UpdatePos()
    {
        if (!IsCG)
        {
            var pos = Extensions.ZonePos2NavPos(this.BattleScene.Terrain.TotalHeight, this.X, this.Y);
            SetPosition(pos);
            SetDirection();
            if (mOnPositionChange != null && mHeadTransform != null)
            {
                mOnPositionChange(mHeadTransform);
                mOnPositionChange = null;
            }
        }
    }

    private void SetPosition(Vector3 pos)
    {
        if (!pos.IsNaN())
        {
            this.DisplayRoot.Position(pos);
        }
    }
    public void SetDirection()
    {
        Quaternion qt = Quaternion.Euler(0, this.m_direction * Mathf.Rad2Deg + 90, 0);
        this.DisplayRoot.Rotation(qt);
    }

    //protected virtual void LoadShadow()
    //{
    //    //加载模型
    //    BattleFactory.Instance.GameObjectAdapter.Load(
    //        "/res/effect/EF_Shadows.assetBundles",
    //        System.IO.Path.GetFileNameWithoutExtension("/res/effect/EF_Shadows.assetBundles"),
    //        (succ, aoe) =>
    //        {
    //            if (this.IsDisposed)
    //            {
    //                BattleFactory.Instance.GameObjectAdapter.Unload(ref aoe);
    //                return;
    //            }

    //            if (succ)
    //            {
    //                mShadow.SetModel(aoe);
    //                //float scale = this.m_info.BodySize * this.m_info.Properties.ShadowCoefficient;
    //                mShadow.localScale = Vector3.one * this.m_info.BodySize;
    //            }
    //        });
    //}

    //private void LoadEquip(int index)
    //{
    //    if (index == mEquipLoads.Count)
    //    {
    //        mEquipLoads.Clear();
    //        if (mLoadCallBack != null)
    //        {
    //            mLoadCallBack(this);
    //        }
    //        return;
    //    }

    //    string fileColor = this.mEquipLoads[index].fileColor;
    //    var displayCell = this.DisplayCell.AttachPart(this.mEquipLoads[index].dummy, this.mEquipLoads[index].dummy);
    //    //displayCell.SetMatColor(fileColor);
    //    //加载Equip
    //    string equipName = System.IO.Path.GetFileNameWithoutExtension(this.mEquipLoads[index].fileName);
    //    BattleFactory.Instance.GameObjectAdapter.Load(
    //        this.mEquipLoads[index].fileName,
    //        System.IO.Path.GetFileNameWithoutExtension(this.mEquipLoads[index].fileName),
    //        (succ, aoe) =>
    //        {
    //            if (displayCell.IsDisposed)
    //            {
    //                BattleFactory.Instance.GameObjectAdapter.Unload(ref aoe);
    //                return;
    //            }

    //            if (succ)
    //            {
    //                displayCell.SetModel(aoe);
    //            }
    //            LoadEquip(++mEquipLoadIndex);
    //        });
    //}

    protected override void OnDispose()
    {
        if (m_InfoBar != null)
        {
            m_InfoBar.Remove();
            m_InfoBar = null;
        }
        if (m_info != null && m_info.BodyScale != 0)
        {
            this.ObjectRoot.transform.localScale *= 1.0f/this.m_info.BodyScale;
        }
        UILayerMgr.SetLayer(this.DisplayCell.ObjectRoot, mCurrentLayer);
        //BubbleChatU.StopBubbleChat(mBubbleNode != null ? mBubbleNode.transform : this.DisplayCell.ObjectRoot.transform);
        mBubbleNode = null;
        OnPositonChange = null;
        mLoadCallBack = null;
        base.OnDispose();
    }

    bool IUnit.PlayAnim(string name, bool crossFade, WrapMode wrapMode, float speed)
    {
        //this.DisplayCell.PlayAnim(name, crossFade, wrapMode, speed, 0);
        return true;
    }

    public Vector3 EulerAngles()
    {
        return this.ObjectRoot.transform.eulerAngles;
    }

    public Vector3 TransformDirection(Vector3 v)
    {
        return this.ObjectRoot.transform.TransformDirection(v);
    }

    //public void ITweenMoveTo(Hashtable args)
    //{
    //    iTween.MoveTo(ObjectRoot, args);
    //}

    //public void iTweenRotateTo(Hashtable args)
    //{
    //    iTween.RotateTo(ObjectRoot, args);
    //}

    public void AddBubbleTalkInfo(string content, string TalkActionType, int keepTimeMS)
    {
        if (!IsDisposed)
        {
            if(mBubbleNode == null)
            {
               mBubbleNode = GetDummyNode("Head_Name");
            }
            //BubbleChatU bc = BubbleChatU.Add(content, mBubbleNode != null ? mBubbleNode.transform : this.DisplayCell.ObjectRoot.transform, Vector3.zero, keepTimeMS / 1000f);
        }
    }

    public int GetAnimLength(string name)
    {

        return 0;
           
    }
}



