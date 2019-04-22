
using DeepCore;
using DeepCore.Unity3D;
using System;
using TLBattle.Common.Plugins;
using TLBattle.Message;
using UnityEngine;

partial class TLAIPlayer
{

    public Action<TLAIPlayer> OnTeamMemberListChange;

    public HashMap<int, TLAvatarInfo> GetAvatarMap()
    {
        if (!GameGlobal.Instance.netMode) //单机华仔组成裆部
        {
            var unitprop = ZUnit.Info.Properties as TLUnitProperties;
            if (unitprop.ServerData.AvatarList != null)
            {
                var AvatarMap = new HashMap<int, TLAvatarInfo>();
                foreach (var info in unitprop.ServerData.AvatarList)
                {
                    var avatarinfo = new TLAvatarInfo();
                    avatarinfo.PartTag = info.PartTag;
                    avatarinfo.FileName = info.FileName;
                    AvatarMap.Add((int)info.PartTag, avatarinfo);
                }
                return AvatarMap;
            }
            return null;
        }
        if (ShowModel)
        {
            if (this.SyncInfo.VisibleInfo == null)
            {
                return null;
            }
            return (this.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).AvatarMap;
        }
        else
        {
            var old = (this.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).AvatarMap;
            var empty = new HashMap<int, TLAvatarInfo>();
            foreach (var p in old)
            {
                var aInfo = new TLAvatarInfo();
                aInfo.PartTag = (TLAvatarInfo.TLAvatar)p.Key;
                if (aInfo.PartTag == TLAvatarInfo.TLAvatar.Avatar_Body)
                {
                    aInfo.FileName = "player_empty";
                    empty.Add(p.Key, aInfo);
                }
                else if (aInfo.PartTag == TLAvatarInfo.TLAvatar.Ride_Avatar01)
                {
                    if (string.IsNullOrEmpty(p.Value.FileName))
                    {
                        aInfo.FileName = p.Value.FileName;
                    }
                    else
                    {
                        aInfo.FileName = "mount_empty";
                    }
                    empty.Add(p.Key, aInfo);
                }
            }
            return empty;
        }
    }

    protected virtual void UpdateAvatarData(HashMap<int, TLAvatarInfo> aMap)
    {
        (this.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).AvatarMap.PutAll(aMap);
    }


    protected virtual void OnLoadVehicleFinish(RenderVehicle vehicle)
    {
        bindBehaviour.InitInfoBar(true);
        InitShadowCaster(vehicle.ObjectRoot, GetShadowCaster());
    }


    private void LoadModelByEditInfo()
    {
        DisplayCell.LoadModel(GetModelFileName(), System.IO.Path.GetFileNameWithoutExtension(GetModelFileName()), (loader) =>
        {
            if (IsDisposed)
            {
                if (loader)
                {
                    loader.Unload();
                }
                return;
            }
            OnLoadModelFinish(loader);
        });
    }

    //TODO 时装加载实现
    protected override void OnLoadModel()
    {
        var aMap = GetAvatarMap();
        string bodyFile = GameUtil.GetPartFile(aMap, (int)TLAvatarInfo.TLAvatar.Avatar_Body);
        if (string.IsNullOrEmpty(bodyFile))
        {
            Debugger.LogError("LoadAvatar aMap's bodyFile is empty So LoadModelByEdit ");
            LoadModelByEditInfo();
            return;
        }

        (DisplayCell as RenderUnit).ChangeBody(bodyFile, (aoe) =>
        {
            if (aoe == null)
            {
                Debugger.LogWarning(bodyFile + "__TLAIUnit OnLoadModel ChangeBody this.IsDisposed:" + this.IsDisposed);
                return;
            }
            if (this.IsDisposed)
            {
                return;
            }

            OnLoadModelFinish(aoe);
            this.ChangeAction(ZUnit.CurrentState, true);

            foreach (var item in aMap.Values)
            {
                string FileName = item.FileName;
                if (item.PartTag != TLAvatarInfo.TLAvatar.Ride_Avatar01
                   && item.PartTag != TLAvatarInfo.TLAvatar.Avatar_Body
                   && !string.IsNullOrEmpty(FileName))
                {
                    if (item.PartTag == TLAvatarInfo.TLAvatar.Foot_Buff && this.Vehicle.IsRiding)
                    {
                        this.Vehicle.ChangeAvatar(FileName, (int)item.PartTag, animPlayer, (succ) =>
                        {
                            if (this.IsDisposed)
                            {
                                return;
                            }
                            if (succ)
                            {
                                InitShadowCaster(DisplayCell.ObjectRoot, GetShadowCaster());
                                this.ChangeAction(ZUnit.CurrentState, true);
                            }
                        });
                    }
                    else
                    {
                        (DisplayCell as RenderUnit).ChangeAvatar(FileName, (int)item.PartTag, animPlayer, (succ) =>
                        {
                            if (this.IsDisposed)
                            {
                                return;
                            }
                            if (succ)
                            {
                                InitShadowCaster(DisplayCell.ObjectRoot, GetShadowCaster());
                                this.ChangeAction(ZUnit.CurrentState, true);
                            }

                        });
                    } 
                }
            }
        });

        TLAvatarInfo mountInfo;
        if (aMap.TryGetValue((int)TLAvatarInfo.TLAvatar.Ride_Avatar01, out mountInfo) && !string.IsNullOrEmpty(mountInfo.FileName))
        {
            LoadVehicle(mountInfo.FileName);
        }
    }

    private void LoadVehicle(string mountFile)
    {
        var PlayerDisplayCell = DisplayCell as RenderUnit;
        if (!string.IsNullOrEmpty(mountFile))
        {

            this.Vehicle.LoadVehicle(mountFile, (VehicleAoe) =>
            {
                if (VehicleAoe)
                {
                    if (this.IsDisposed)
                    {
                        return;
                    }
                    var actionName = Vehicle.GetActionName();
                    this.Vehicle.Mount();
                    OnLoadVehicleFinish(Vehicle);
                    if (!string.IsNullOrEmpty(actionName))
                    {
                        RegistRideActionStatus(actionName);
                        this.ChangeAction(ZUnit.CurrentState, true);
                    }
                }
                else if (!IsDisposed)
                {
                    Debugger.LogWarning("LoadVehicle aoe is empty and this.IsDisposed:" + this.IsDisposed);
                }
            });
        }
    }

    public override Avatar AddAvatar(string assetBundleName, string assetName)
    {
        if (ShowModel)
        {
            return base.AddAvatar(assetBundleName, assetName);
        }
        else
        {
            int startIndex = assetBundleName.LastIndexOf('/') + 1;
            string oldName = assetBundleName.Substring(startIndex, assetBundleName.Length - startIndex);
            string newName = assetBundleName.Replace(oldName, "player_empty.assetbundles");
            return base.AddAvatar(newName, "player_empty");
        }
    }

    private Coroutine mCoroutine;

    private void ChangeVehicle(string mountFile)
    {
        //非骑乘状态

        var PlayerDisplayCell = DisplayCell as RenderUnit;
        if (!string.IsNullOrEmpty(mountFile))
        {

            this.Vehicle.LoadVehicle(mountFile, (VehicleAoe) =>
            {
                if (VehicleAoe)
                {
                    if (this.IsDisposed)
                    {
                        return;
                    }

                    //换坐骑 直接换
                    var actionName = Vehicle.GetActionName();
                    if (this.Vehicle.IsRiding)
                    {
                        OnLoadVehicleFinish(Vehicle);

                        if (!string.IsNullOrEmpty(actionName))
                        {
                            RegistRideActionStatus(actionName);
                            this.ChangeAction(ZUnit.CurrentState, true);
                        }
                    }
                    else
                    {

                        if (this.mCoroutine != null)
                        {
                            GameGlobal.Instance.StopCoroutine(this.mCoroutine);
                            this.mCoroutine = null;
                        }
                        // 上坐骑 来一个动画

                        if (this.mCoroutine != null)
                            throw new Exception("[this.mCoroutine != null]");

                        animPlayer.Play(this.Vehicle.MountAction);
                        this.mCoroutine = GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(0.767f, () =>
                        {
                            if (this.IsDisposed)
                            {
                                return;
                            }

                            if (VehicleAoe.IsUnload)
                            {
                                RemoveRideActionStatus();
                                this.ChangeAction(ZUnit.CurrentState, true);
                                return;
                            }

                            if (this.PlayerVirtual.IsRiding() == false)
                            {
                                this.Vehicle.SetEmpty();
                                RemoveRideActionStatus();
                                this.ChangeAction(ZUnit.CurrentState, true);
                                return;
                            }

                            this.Vehicle.Mount();
                            OnLoadVehicleFinish(Vehicle);


                            if (!string.IsNullOrEmpty(actionName))
                            {
                                RegistRideActionStatus(actionName);
                                this.ChangeAction(ZUnit.CurrentState, true);

                            }

                            this.mCoroutine = null;

                        }));
                    }
                }
                else if (!IsDisposed)
                {
                    Debugger.LogWarning("LoadVehicle aoe is empty and this.IsDisposed:" + this.IsDisposed);
                }
            });
        }
        else
        {
            if (this.mCoroutine != null)
            {
                GameGlobal.Instance.StopCoroutine(this.mCoroutine);
                this.mCoroutine = null;
            }

            //单纯下坐骑
            animPlayer.Play(this.Vehicle.LeaveAction);
            RemoveRideActionStatus();
            this.ChangeAction(ZUnit.CurrentState, true);

            this.DisplayCell.ParentRoot(this.DisplayRoot);
            Vehicle.UnMount();
            OnLoadVehicleFinish(Vehicle);


            if (this.mCoroutine != null) throw new Exception("[this.mCoroutine != null]");
            this.mCoroutine = GameGlobal.Instance.StartCoroutine(GameGlobal.WaitForSeconds(0.767f, () =>
            {
                if (this.IsDisposed)
                {
                    return;
                }
                if (this.PlayerVirtual.IsRiding())
                {
                    return;
                }
                this.Vehicle.SetEmpty();
                this.mCoroutine = null;
            }));

        }
    }


    protected override void RegistAllObjectEvent()
    {
        base.RegistAllObjectEvent();
        RegistObjectEvent<PlayAvatarEventB2C>(ObjectEvent_PlayAvatarEvent);
        RegistObjectEvent<ForceChangeEventB2C>(ObjectEvent_ForceChangeEventB2C);
        RegistObjectEvent<TeamMemberListChangeEvtB2C>(PlayerEvent_TeamMemberListChangeEvtB2C);
    }

    protected virtual void OnChangeBodyFinish(FuckAssetObject aoe)
    {
        base.OnLoadModelFinish(aoe);
        var soundobj = aoe.gameObject.GetComponent<TLPlaySound>();
        if (soundobj == null)
        {
            soundobj = aoe.gameObject.AddComponent<TLPlaySound>();
        }
        soundobj.CanPlay = false;
    }

    private void ObjectEvent_ForceChangeEventB2C(ForceChangeEventB2C e)
    {
        if (bindBehaviour != null && bindBehaviour.HasInit)
        {
            bindBehaviour.CheckHpBarType();
        }
    }

    protected virtual void ObjectEvent_PlayAvatarEvent(PlayAvatarEventB2C ev)
    {
        //Debugger.LogError("ObjectEvent_PlayAvatarEvent...:" + ev);
        string FileName = string.Empty;
        UpdateAvatarData(ev.AvatarMap);

        var aMap = GetAvatarMap();
        if(aMap == null)
        {
            return;
        }

        foreach (var item in ev.AvatarMap)
        {
            TLAvatarInfo avatarInfo;
            if (!aMap.TryGetValue(item.Key, out  avatarInfo))
            {
                Debugger.LogError(item.Key + " PlayAvatarEventB2C aMap was not find key : " + item.Value.FileName);
                continue;
            }
            FileName = avatarInfo.FileName;

            if (item.Key == (int)TLAvatarInfo.TLAvatar.Ride_Avatar01)
            {
                ChangeVehicle(FileName);
            }
            else
            {
                if (item.Key == (int)TLAvatarInfo.TLAvatar.Avatar_Body)
                {
                    (DisplayCell as RenderUnit).ChangeBody(FileName, (aoe) =>
                    {

                        if (this.IsDisposed)
                        {
                            return;
                        }

                        if (aoe)
                        {
                            OnChangeBodyFinish(aoe);
                        }

                    });
                    return;
                }

                if (item.Key == (int)TLAvatarInfo.TLAvatar.Foot_Buff && this.Vehicle.IsRiding)
                {
                    this.Vehicle.ChangeAvatar(FileName, item.Key, animPlayer, (succ) =>
                    {
                        if (this.IsDisposed)
                        {
                            return;
                        }
                        if (succ)
                        {
                            InitShadowCaster(DisplayCell.ObjectRoot, GetShadowCaster());
                            this.ChangeAction(ZUnit.CurrentState, true);
                        }
                    });
                }
                else
                {
                    (DisplayCell as RenderUnit).ChangeAvatar(FileName, item.Key, animPlayer, (succ) =>
                    {
                        if (succ)
                        {
                            InitShadowCaster(DisplayCell.ObjectRoot, GetShadowCaster());
                            this.ChangeAction(ZUnit.CurrentState, true);
                        }

                    });
                }

            }
        }
    }

    protected virtual void PlayerEvent_TeamMemberListChangeEvtB2C(TeamMemberListChangeEvtB2C evt)
    {
        if (OnTeamMemberListChange != null)
        {
            OnTeamMemberListChange.Invoke(this);
        }

        //CheckOwnerShip();
    }

    public void TestLoadAvatar(bool reload, SLua.LuaTable avatarmap)
    {
        var newmap = new HashMap<int, TLAvatarInfo>();
        foreach (var entry in avatarmap)
        {
            var part = Convert.ToInt32(entry.key);
            newmap.Add(part, new TLAvatarInfo { FileName = entry.value.ToString(), PartTag = (TLAvatarInfo.TLAvatar)part });
        }
        if (reload)
        {
            Vehicle.UnMount();
            DisplayCell.DetachAllPart();
            (this.SyncInfo.VisibleInfo as PlayerVisibleDataB2C).AvatarMap.Clear();
            UpdateAvatarData(newmap);
            OnLoadModel();
        }
        else
        {
            ObjectEvent_PlayAvatarEvent(new PlayAvatarEventB2C { AvatarMap = newmap });
        }
    }

    public void ChangeFish(Action<bool> callback = null)
    {
        (DisplayCell as RenderUnit).LoadFishModel(animPlayer, callback);
    }

    public void RemoveFishhModel()
    {
        (DisplayCell as RenderUnit).RemoveFishModel();
    }


}
