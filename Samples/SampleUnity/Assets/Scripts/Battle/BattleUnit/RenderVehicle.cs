
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using TLBattle.Common.Plugins;
using UnityEngine;

public class RenderVehicle : RenderUnit
{
    //是否已经是乘骑状态（资源已经加载完毕）
    public bool IsRiding
    {
        get; set;
    }

    public VehicleSetting Setting
    {
        get; private set;
    }

    public string MountAction {get; private set; }
    public string LeaveAction{ get; private set; }

    private DisplayCell unitDisplay;

    public RenderVehicle(GameObject root, DisplayCell DisplayCell,string name = "RenderVehicle") : base(root, name)
    {
        this.unitDisplay = DisplayCell;
    }

    private GameObject mRideRoot;

    public GameObject getRideRoot()
    {
        return mRideRoot;
    }

     

    public string GetActionName()
    {
        if(Setting == null)
        {
            return string.Empty;
        }
        return Setting.prefix;
    }
     
    private void TryAttachFootBuff2Mount()
    {
        string footDummy = GameUtil.getDummy((int)TLAvatarInfo.TLAvatar.Foot_Buff);
        DisplayCell footBuff = this.unitDisplay.TryDetachPart(footDummy);

        if (footBuff != null)
        {
            this.AttachPart(footDummy, footDummy, footBuff);
        }
    }
     
    private void TryAttachFootBuff2Unit()
    {
        string footDummy = GameUtil.getDummy((int)TLAvatarInfo.TLAvatar.Foot_Buff);
        DisplayCell footBuff = this.TryDetachPart(footDummy);
        if (footBuff != null)
        {
            this.unitDisplay.AttachPart(footDummy, footDummy, footBuff);
        }
    }

  

    public void Mount()
    {
        this.IsRiding = true;

        string dummy = GameUtil.getDummy((int)TLAvatarInfo.TLAvatar.Ride_Avatar01);
        this.AttachPart(dummy, dummy, unitDisplay);

        TryAttachFootBuff2Mount();
    }

    public void UnMount()
    {
        if (this.IsRiding == false || this.IsDisposed)
            return ;

        this.IsRiding = false;
 
        this.DetachFootBuffAndUnit();

    }
 

    public void SetEmpty()
    {
        Setting = null;
        this.MountAction = "";
        this.LeaveAction = "";
 
        Unload();
    }

    private void TryUnitDetach()
    {
        string unitDummy = GameUtil.getDummy((int)TLAvatarInfo.TLAvatar.Ride_Avatar01);
        this.TryDetachPart(unitDummy);
    }

    public void DetachFootBuffAndUnit()
    {
        this.TryAttachFootBuff2Unit();
        this.TryUnitDetach();
    }

    protected override void OnDispose()
    {
        //先把人和坐骑分离开来，进行Dispose  人走自己的Dispose
        this.TryUnitDetach();
        base.OnDispose();
    }

    public void LoadVehicle(string assetName, Action<FuckAssetObject> callback = null)
    {
        string fileName = GameUtil.getUnitAssetName(assetName);
        int PartTag = (int)TLAvatarInfo.TLAvatar.Ride_Avatar01;
        var loaderID = this.LoadModel(fileName, System.IO.Path.GetFileNameWithoutExtension(fileName), (aoe) =>
        {
            if (aoe)
            {
 
                mRideRoot = aoe.FindNode("Ride_Avatar01");
                Setting = aoe.GetComponent<VehicleSetting>();
                 
                if (this.Setting != null)
                {
                    string prefix = Setting.prefix;
                    this.MountAction = string.Format(prefix, "mount");
                    this.LeaveAction = string.Format(prefix, "leave");
                }

        
                 
                if (callback != null)
                {
                    callback(aoe);
                }

            }
            else
            {
                if (callback != null)
                {
                    callback(null);
                }
            }

        },aoee => {
            var lastID = mLastLoadPart.Get(PartTag);
            if (lastID != aoee.ID)
            {
                aoee.Unload();
                return false;
            }
            return true;
        });
        mLastLoadPart.Put(PartTag, loaderID);
    }
}