
using DeepCore;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D;
using TLBattle.Common.Plugins;
using UnityEngine;

public class RenderUnit : DisplayCell
{
    public RenderUnit(GameObject root, string name = "RenderUnit")
    : base(root, name)
    {

    }

    //模型加载统一接口
    protected override void _SetModel(FuckAssetObject aoe)
    {
        base._SetModel(aoe);

        InitMatManager();
    }

    public void changeName(string name)
    {
        //ObjectRoot.name = name;
    } 
 
    protected HashMap<int, int> mLastLoadPart = new HashMap<int, int>();

    public bool ChangeBody(string bodyFile, Action<FuckAssetObject> callBack = null)
    {
        if (string.IsNullOrEmpty(bodyFile))
        {
            Debugger.LogError("body的资源为空: " + bodyFile);
            return false;
        }

        int PartTag = (int)TLAvatarInfo.TLAvatar.Avatar_Body;
        string assetName = GameUtil.getUnitAssetName(bodyFile);
        var loaderID = LoadModel(assetName, System.IO.Path.GetFileNameWithoutExtension(assetName), (aoe) =>
        { 
            if (aoe)
            { 
                //InitMatManager();
                //if (this.ObjectRoot.activeSelf == false)
                //{
                //    //Debugger.LogError("ChangeBody succ but this.ObjectRoot.activeSelf == false :" + bodyFile);
                //}
                if (callBack != null)
                {
                    callBack(aoe);
                }
            }
            else
            {
                if(!this.IsDisposed)
                {
                    Debugger.LogError("ChangeBody Load assetName aoe is empty and without IsDisposed this.IsDisposed: " + this.IsDisposed);
                }

                if (callBack != null)
                {
                    callBack(null);
                }
            }
        
        }, aoee =>
        {
            var lastID = mLastLoadPart.Get(PartTag);
            if (lastID != aoee.ID)
            {
                aoee.Unload();
                return false;
            }
            return true;
        });

        mLastLoadPart.Put(PartTag, loaderID);

        return true;
    }


    public void ChangeAvatar(string assetName, int PartTag, AnimPlayer animPlayer,Action<bool> callback = null)
    {
        string dummy = GameUtil.getDummy(PartTag);
        if (string.IsNullOrEmpty(assetName))
        {
            //assetName为空 则卸载之前的武器，然后return;
            this.DetachPart(dummy);
            return;
        }

        string partName = PartTag.ToString();
        var display = this.GetPart(dummy) as RenderUnit;
        if (display == null)
        { 
            display = this.AttachPart(dummy, dummy, animPlayer) as RenderUnit;
            if (display == null)
            {
                Debugger.LogError(dummy + " dummy ChangeAvatar AttachPart display is empty: ");
                return;
            }
        }

        string fileName = GameUtil.getUnitAssetName(assetName);
        display.changeName(fileName);
        var loaderID = display.LoadModel(fileName, System.IO.Path.GetFileNameWithoutExtension(fileName), (aoe) =>
        { 
            if (aoe)
            {  
                var soundobj = aoe.gameObject.GetComponent<DummyPlaySound>();
                if (soundobj == null)
                {
                    soundobj = aoe.gameObject.AddComponent<DummyPlaySound>();
                }
                //if (this.ObjectRoot.activeSelf == false )
                //{
                //    //Debugger.LogError("ChangeBody succ but this.ObjectRoot.activeSelf == false :" + fileName);
                //}

                callback(true);

            } 
        }, aoee =>
        {
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
 

    public void AddMatState(StateMaterial state, int time)
    {
        switch (state)
        {
            case StateMaterial.DISSOLOVE:
                MatDissolve(time);
                break;
        }
    }
 
    public void MatHit(int time)
    {
        foreach (var _render in Renderers)
        {
            MaterialManager mm = _render.Ren.gameObject.GetComponent<MaterialManager>();
            if (mm != null)
            {
                int alphaTime = time;
                mm.Onhit(StateMaterial.NORMAL, (matunit, render, t) =>
                {
                    //foreach (var mat in render.materials)
                    //{
                    //    float v = (alphaTime - Mathf.Min(alphaTime, t)) / alphaTime;
                    //    mat.SetColor("_fresnel_color", new Color(v, v, v, v));
                    //}
                    if (IsDisposed)
                    {
                        return;
                    }
                        if (t < alphaTime)
                        {
                            foreach (var mat in render.materials)
                            {
                                if (mat.HasProperty("_fresnel_color"))
                                {
                                    float v = (alphaTime - Mathf.Min(alphaTime, t)) / alphaTime;
                                    mat.SetColor("_fresnel_color", new Color(v, v, v, v));
                                }
                                if (mat.HasProperty("_fresnel_area"))
                                {
                                    if (matunit.OrgValue != null && matunit.OrgValue.Length > 0)
                                    {
                                        float areav = matunit.OrgValue[0] - (matunit.OrgValue[0] - 2) * ((alphaTime - Mathf.Min(alphaTime, t)) / alphaTime);
                                   
                                        mat.SetFloat("_fresnel_area", areav);
                                    }
                                }
                               
                            }
                        }
                        else
                        {
                            mm.resetHit(StateMaterial.NORMAL);
                        }
                });
            }
        }

        foreach (var DisPlayChild in mAttachParts.Values)
        {
            if (DisPlayChild != null)
            {
                (DisPlayChild as RenderUnit).MatHit(time);
            }
        }
    }

    private void MatDissolve(int time)
    {
        foreach (var _render in Renderers)
        {
            MaterialManager mm = _render.Ren.gameObject.GetComponent<MaterialManager>();
            if (mm != null)
            {
                int DeaddissoloveTime = time;
                mm.AddMatState(StateMaterial.DISSOLOVE, (matunit,render, t) =>
                {
                    if (t < DeaddissoloveTime)
                    {
                        if (t < DeaddissoloveTime)
                        {
                            foreach (var mat in render.materials)
                            {
                                float v = (DeaddissoloveTime - Mathf.Min(DeaddissoloveTime, t)) / DeaddissoloveTime;
                                mat.SetFloat("_alpha", v);
                            }
                        }
                    }
                    else
                    {
                        //mm.ResetMatState();
                    }

                    //if (t < DeaddissoloveTime)
                    //{
                    //    foreach (var mat in render.materials)
                    //    {
                    //        mat.SetFloat("_clip", (DeaddissoloveTime -  Mathf.Min(DeaddissoloveTime, t)) / DeaddissoloveTime);
                    //    }
                    //}
                });
            }
        }
    }

    public void InitMatManager()
    {
        foreach (var _render in Renderers)
        {
            MaterialManager mm = _render.Ren.gameObject.GetComponent<MaterialManager>();
            if (mm == null)
            {
                mm = _render.Ren.gameObject.AddComponent<MaterialManager>();
                mm.SaveOrgMat();
            }
            else
            {
                mm.ResetMatState();
            }
        }
    }



    public void LoadFishModel(AnimPlayer animPlayer, Action<bool> callback = null)
    {
        int PartTag = (int)TLAvatarInfo.TLAvatar.R_Hand_Buff;
        string dummy = "Bip001 R Hand";
        string assetName = "ef_buff_fish";
        string fileName = "/res/effect/" + assetName + ".assetbundles";
        string partName = PartTag.ToString();
        var display = this.GetPart(dummy) as RenderUnit;
        if (display == null)
        {
            display = this.AttachPart(dummy, dummy, animPlayer) as RenderUnit;
            if (display == null)
            {
                if(callback != null)
                {
                    callback(false);
                }
                return;
            }
        }
    
        display.changeName(fileName);
        var loaderID = display.LoadModel(fileName, System.IO.Path.GetFileNameWithoutExtension(fileName), (aoe) =>
        {
            if (aoe)
            {  
                if (callback != null)
                {
                    callback(true);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback(false);
                }
            }
             
        },aoee =>
        {
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


    public void RemoveFishModel()
    {
        int PartTag = (int)TLAvatarInfo.TLAvatar.R_Hand_Buff;
        string partName = PartTag.ToString();
        string dummy = "Bip001 R Hand";
        var display = this.GetPart(dummy) as RenderUnit;
        if (display != null)
        {
            //GetAnimPlayer().RemoveAnimator(display.Animator);
            //display.Dispose();
            this.DetachPart(dummy);
        }
    }
}
