using DeepCore.GameSlave;
using DeepCore.GameSlave.Client;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeepCore.Unity3D
{
    public class DefaultBattleScene : BattleScene
    {
        public DefaultBattleScene(AbstractBattle client) : base(client)
        {
        }
    }

    public class DefaultBattleFactory : BattleFactory
    {
        public enum LayerSetting
        {
            UI = 5,
            STAGE_NAV = 8,
            CAGE = 9,
            CG = 10,
        }

        protected DefaultTerrainAdapter mTerrainAdapater = null;


        protected DefaultBattleFactory() : base()
        {
            //mTerrainAdapater = new DefaultTerrainAdapter();
        }

        public override void Update(float deltaTime)
        {
        }

        public override BattleDecoration CreateBattleDecoration(BattleScene battleScene, ZoneEditorDecoration zf)
        {
            return new BattleDecoration(battleScene, zf);
        }

        public override BattleScene CreateBattleScene(AbstractBattle battle)
        {
            return new DefaultBattleScene(battle);
        }



        public override TerrainAdapter TerrainAdapter
        {
            get
            {
                return mTerrainAdapater;
            }
        }

        public override int StageNavLay
        {
            get
            {
                return (int)LayerSetting.STAGE_NAV;
            }
        }

        public override ICamera Camera
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DisplayCell CreateDisplayCell(GameObject root, string name)
        {
            return new DisplayCell(root, name);
        }

        public override ComAICell CreateComAICell(BattleScene battleScene, ZoneObject obj)
        {
            ComAICell cell = null;
            if (obj is ZoneActor)
            {
                cell = new ComAIActor(battleScene, obj as ZoneActor);
            }
            else if (obj is ZoneUnit)
            {
                cell = new ComAIUnit(battleScene, obj as ZoneUnit);
            }
            else if (obj is ZoneItem)
            {
                cell = new ComAIItem(battleScene, obj as ZoneItem);
            }
            else if (obj is ZoneSpell)
            {
                cell = new ComAISpell(battleScene, obj as ZoneSpell);
            }

            if (cell != null)
            {
                cell.OnCreate();
            }

            return cell;
        }

        public override void OnError(string msg)
        {

        }

        public override void MakeDamplingJoint(GameObject body, GameObject from, GameObject to)
        {
            //DampingJoint dj = body.transform.GetComponentInChildren<DampingJoint>();
            //if (dj != null)
            //{
            //    dj.Hook(from.transform, to.transform);
            //}
        }
    }


    public class DefaultTerrainAdapter : TerrainAdapter
    {
        private string mAssetBundleName;
        private string mAsssetName;
        private FuckAssetLoader mAssetLoader;
        private System.Action<bool, GameObject> mCallback;
        private GameObject mMapRoot;
        //todo maproot asset ab 泄漏
        protected GameObject MapRoot { get { return mMapRoot; } }

        private bool mStepLoad;
        private LightmapData[] mLmDataSet;

        public DefaultTerrainAdapter()
        {

        }

        public DefaultTerrainAdapter(bool stepLoad) : this()
        {
            mStepLoad = stepLoad;
        }

        public void Clear()
        {
            foreach (var file in mAssetLoader.GetDeps())
            {
                if (file.IndexOf("shadervariants", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    HZUnityAssetBundleManager.GetInstance().UnloadAssetBundleImmediate(file, true, false);
                }
            }
            HZUnityAssetBundleManager.GetInstance().UnloadAssetBundleImmediate(mAssetBundleName, true, true);
            //mAssetLoader.Bundle.Unload(true, true);
        }

        public override void Load(string assetBundleName, System.Action<bool, GameObject> callback)
        {
            if (!string.IsNullOrEmpty(assetBundleName) && callback != null)
            {
                mAssetBundleName = assetBundleName.ToLower();
                mCallback = callback;
                mAssetLoader = FuckAssetLoader.Load(mAssetBundleName, null, OnLoadFinish);
                mAsssetName = FuckAssetLoader.GetAssetNameFromBundleName(mAssetBundleName);
                //OnLoadFinish(mAssetLoader);
            }
        }

        protected virtual void OnLoadFinish(FuckAssetLoader loader)
        {
            if (!loader.IsSuccess)
            {
                mCallback(false, null);
                return;
            }

            mMapRoot = (GameObject)GameObject.Instantiate(loader.AssetObject);
            mMapRoot.name = "MapNode";
            mMapRoot.Position(Vector3.zero);

            LoadLightmap(loader.Bundle);
            LoadNav(loader.Bundle);

            Transform statics = mMapRoot.transform.Find("static");
            if (statics != null)
            {
                StaticBatchingUtility.Combine(statics.gameObject);
                //Transform ground = statics.Find("ground");
                //if (ground != null)
                //{
                //    StaticBatchingUtility.Combine(ground.gameObject);
                //}
            }

            Transform small_items = mMapRoot.transform.Find("smallitem");
            if (small_items != null)
            {
                var layer = LayerMask.NameToLayer("SMALLITEM");
                if(layer >= 0 && layer < 32)
                {
                    var renders = small_items.GetComponentsInChildren<Renderer>();
                    foreach (var render in renders)
                    {
                        render.gameObject.layer = layer;
                    }
                }
            }

            // fog settings
            if (!mStepLoad)
                InitFogParam();

            mCallback(true, mMapRoot);
        }

        public bool InitFogParam()
        {
            FogAmbientColorSetting fs = mMapRoot.GetComponent<FogAmbientColorSetting>();
            if (null != fs)
            {
                fs.Reset();
                return true;
            }
            return false;
        }

        private void LoadLightmap(HZUnityAssetBundle ab)
        {
            List<UnityEngine.Object> lms = new List<UnityEngine.Object>();
            int count = 0;
            UnityEngine.Object lightMapObj;
            do
            {
                string reslight = "Lightmap-" + count + "_comp_light";
                lightMapObj = ab.AssetBundle.LoadAsset(reslight);
                if (lightMapObj != null)
                {
                    lms.Add(lightMapObj);
                    ++count;
                }
                else
                {
                    break;
                }
            }
            while (true);

            if (lms.Count > 0)
            {
                LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
                UnityEngine.LightmapData[] lmDataSet = new UnityEngine.LightmapData[lms.Count];

                for (int i = 0; i < lms.Count; i++)
                {
                    Texture2D tex = lms[i] as Texture2D;
                    if (tex != null)
                    {
                        UnityEngine.LightmapData lmData = new UnityEngine.LightmapData();
#if UNITY_5_6_OR_NEWER
                        lmData.lightmapColor = (Texture2D)tex;
#else
                        lmData.lightmapLight = (Texture2D)tex;
#endif
                        lmDataSet[i] = lmData;
                    }
                }
                if (mStepLoad)
                    mLmDataSet = lmDataSet;
                else
                    LightmapSettings.lightmaps = lmDataSet;
            }
            InitLightMapParam(mMapRoot);
        }

        protected virtual bool InitLightMapParam(GameObject mMapRoot)
        {
            LightmapParam[] lmd = mMapRoot.GetComponentsInChildren<LightmapParam>(true);
            for (int i = 0; i < lmd.Length; i++)
            {
                Renderer r = lmd[i].gameObject.GetComponent<Renderer>();
                if (r != null)
                {
                    r.gameObject.isStatic = true;
                    r.lightmapIndex = lmd[i].lightmapIndex;
                    r.lightmapScaleOffset = lmd[i].lightmapScaleOffset;
                }
                else
                {
                    Terrain t = lmd[i].gameObject.GetComponent<Terrain>();
                    if (t != null)
                    {
                        t.gameObject.isStatic = true;
                        t.lightmapIndex = lmd[i].lightmapIndex;
                        t.lightmapScaleOffset = lmd[i].lightmapScaleOffset;
                    }
                }
            }

            return false;
        }

        public void InitLightMap()
        {
            LightmapSettings.lightmaps = mLmDataSet;
        }

        private void LoadNav(HZUnityAssetBundle ab)
        {
            string resnav = mAsssetName + "_nav";
            UnityEngine.Object navObj = ab.AssetBundle.LoadAsset(resnav);
            if (navObj != null)
            {
                GameObject nav = (GameObject)GameObject.Instantiate(navObj);
                nav.layer = BattleFactory.Instance.StageNavLay;
                nav.transform.parent = mMapRoot.transform;

                foreach (Transform e in nav.GetComponentInChildren<Transform>())
                {
                    MeshCollider bc = e.gameObject.GetComponent<MeshCollider>();
                    if (bc == null)
                    {
                        bc = e.gameObject.AddComponent<MeshCollider>();
                    }
                    bc.gameObject.layer = nav.layer;
                }
            }
        }

    }


}
