

using DeepCore;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Utils;
using SLua;
using System;
using System.Collections.Generic;
using System.IO;
using TLBattle.Message;
using UnityEngine;

public partial class TLAIActor
{
    public enum RadarType
    {
        Normal,
        Dungeon,
    }
    [DoNotToLua]
    public class RadarData
    {
        public string radarkey;
        public int mapid;
        public string roadName;
        public float x;
        public float y;
        public float distance;
        public bool IsShow = false;
        public RadarType radartype = RadarType.Normal;
    }
    private bool mTreasureIsShow = false;
    private bool mLastTreasureShow = false;
    private RadarData mCurRadar = null;
    private float ShowRaderDistance = 0;

    //private RadarData mSceneRadarDatas = null;
    private void InitRadarManger()
    {
        var db = GameUtil.GetDBData("GameConfig", "scene_radar_range");
        ShowRaderDistance = db != null ? int.Parse((string)db["paramvalue"]) : 0;

        EventManager.Subscribe("EVENT_UI_FindTreasure", FindTreasure);
    }


    private void ClearRadarManager()
    {
        EventManager.Unsubscribe("EVENT_UI_FindTreasure", FindTreasure);
    }

    [DoNotToLua]
    private void InitFindTreasure(GameObject obj)
    {
        mLastTreasureShow = false;
        if (mTreasureObject == null)
        {

            string shadowPath = "/res/effect/ef_jiantou.assetbundles";
            var id = FuckAssetObject.GetOrLoad(shadowPath, Path.GetFileNameWithoutExtension(shadowPath), (loader) =>
            {
                if (loader)
                {
                    if (this.IsDisposed)
                    {
                        loader.Unload();
                        return;
                    }
                    mTreasureObject = loader;
                    loader.transform.parent = obj.transform;
                    loader.transform.localPosition = Vector3.zero;
                    loader.transform.localEulerAngles = Vector3.zero;
                    loader.transform.localScale = Vector3.one;
                    mTreasureObject.gameObject.SetActive(false);
                }
            });
        }
    }
    private void PlayerEvent_RadarEventB2C(RadarEventB2C obj)
    {
        string radarkey = "" + DataMgr.Instance.UserData.MapTemplateId;
        var radar = CreateRadarData(obj.X, obj.Y, DataMgr.Instance.UserData.MapTemplateId,
            obj.Distance, radarkey, "",  RadarType.Dungeon);
        DataMgr.Instance.UserData.AddRadarData(radarkey, radar);
        InitRadar();

    }

    private void InitRadar()
    {
        mCurRadar = DataMgr.Instance.UserData.GetCurMapRadarData(DataMgr.Instance.UserData.MapTemplateId);
        if (mCurRadar == null)
        {
            mTreasureIsShow = false;
            return;
        }
        Vector2 pos = Vector2.zero;
        if (!string.IsNullOrEmpty(mCurRadar.roadName))
        {
            var zoneflag = TLBattleScene.Instance.GetZoneFlag(mCurRadar.roadName);

            if (zoneflag != null)
            {
                pos = new Vector2(zoneflag.X, zoneflag.Y);

            }
        }
        else
        {
            pos = new Vector2(mCurRadar.x, mCurRadar.y);
        }
        mCurRadar.x = pos.x;
        mCurRadar.y = pos.y;
        mTreasureIsShow = true;
    }

    private RadarData CreateRadarData(float x, float y, int mapid, float distance,string radarkey, string roadName = "",RadarType radarType = RadarType.Normal)
    {
        var radarData = new RadarData
        {
            x = x,
            y = y,
            mapid = mapid,
            distance = distance,
            roadName = roadName,
            radarkey = radarkey,
            radartype = radarType
        };
        return radarData;
    }
    private void FindTreasure(EventManager.ResponseData res)
    {
        Dictionary<object, object> dic = (Dictionary<object, object>)res.data[1];
        object value;
        string radarkey = "";
        if (dic.TryGetValue("isShow", out value))
        {
            bool IsShow = (bool)value;
            if (dic.TryGetValue("radarkey", out value))
            {
                radarkey = (string)(value);
               
            }
            if (!string.IsNullOrEmpty(radarkey))
            {
                if (!IsShow && DataMgr.Instance.UserData.RemoveRadarData(radarkey))
                {
                    InitRadar();
                    return;
                }
            }
           
            Vector2 pos = Vector2.zero;
            if (dic.TryGetValue("pos", out value))
            {
                Dictionary<string, object> table = GameUtil.LuaTableToDictionary((LuaTable)value);
                pos.x = Convert.ToInt32(table["x"]);
                pos.y = Convert.ToInt32(table["y"]);

            }
            object value1;
            if (dic.TryGetValue("x", out value) && dic.TryGetValue("y", out value1))
            {
                pos.x = Convert.ToInt32(value);
                pos.y = Convert.ToInt32(value1);
            }

            int mapid = 0;
            if (dic.TryGetValue("mapid", out value))
            {
                mapid = Convert.ToInt32(value);
            }
            string flag = "";
            if (dic.TryGetValue("flag", out value))
            {
                flag = (string)value;
            }
            float distance = ShowRaderDistance;
            if (dic.TryGetValue("distance", out value))
            {
                distance = (float)value;
            }
            if (mapid == 0)
            {
                mapid = DataMgr.Instance.UserData.MapTemplateId;
            }
            var radar = CreateRadarData(pos.x, pos.y, mapid, distance, radarkey, flag);
            DataMgr.Instance.UserData.AddRadarData(radarkey, radar);
            InitRadar();
        }
    }


    private void UpdateFindTreasure(float deltaTime)
    {
        if (mTreasureObject != null)
        {
            if (mLastTreasureShow != mTreasureIsShow)
            {
                mTreasureObject.gameObject.SetActive(mTreasureIsShow);
                mLastTreasureShow = mTreasureIsShow;
            }
            if (mTreasureIsShow && mCurRadar != null)
            {
                Vector3 Treasure3DPos = DeepCore.Unity3D.Utils.Extensions.ZonePos2UnityPos(ZObj.Parent.TerrainSrc.TotalHeight
                     , mCurRadar.x, mCurRadar.y, 0);

                Vector3 scale = Vector3.one * (this.ZObj as DeepCore.GameSlave.ZoneUnit).Info.BodySize * 2;
                mTreasureObject.transform.localScale = scale;
                Treasure3DPos.y = this.ObjectRoot.Position().y;
                var direct = (Treasure3DPos - this.ObjectRoot.Position()).normalized;
                mTreasureObject.transform.forward = direct;
                Vector2 playpos = new Vector2(this.X, this.Y);
                Vector2 treasurePos = new Vector2(mCurRadar.x, mCurRadar.y);
                float distance = Vector2.Distance(treasurePos, playpos);
                if (distance <= ShowRaderDistance)
                {
                    if (mTreasureObject.gameObject.activeSelf)
                    {
                        mTreasureObject.gameObject.SetActive(false);
                    }
                    if (mCurRadar.radartype == RadarType.Dungeon)
                    {
                        DataMgr.Instance.UserData.RemoveRadarData(mCurRadar.radarkey);
                        InitRadar();
                    }
                }
                else
                {
                    if (!mTreasureObject.gameObject.activeSelf)
                    {
                        mTreasureObject.gameObject.SetActive(true);
                    }
                }
            }

        }

    }

}
