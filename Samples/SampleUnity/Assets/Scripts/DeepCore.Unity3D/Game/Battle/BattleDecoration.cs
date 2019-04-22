using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.GameSlave;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public class BattleDecoration : BattleFlag
    {
        private GameObject mDecrationRoot;


        public ZoneEditorDecoration ZDecoration
        {
            get { return base.ZFlag as ZoneEditorDecoration; }
        }

        public BattleDecoration(BattleScene battleScene, ZoneEditorDecoration zf)
            : base(battleScene, zf)
        {
            this.ObjectRoot.ZonePos2NavPos(BattleScene.Terrain.TotalHeight
                , zf.X, zf.Y, 0f);
            this.ObjectRoot.ZoneRot2UnityRot(zf.Data.StripDirection);
            
            mDecrationRoot = new GameObject("DecrationRoot");
            using (var pts = ListObjectPool<DeepCore.Geometry.Vector2>.AllocAutoRelease())
            {
                ZDecoration.Data.GetResourcePoints(pts);
                foreach(var pos in pts)
                {
                    GameObject cell = new GameObject("cell");
                    cell.ParentRoot(mDecrationRoot);
                    cell.Position(new Vector3(pos.X, 0, pos.Y));
                    cell.transform.localScale *= ZDecoration.Data.Scale;
                }
            }

            mDecrationRoot.ParentRoot(ObjectRoot);
        }

        protected override void OnDispose()
        {
            FuckAssetObject[] aoes = mDecrationRoot.GetComponentsInChildren<FuckAssetObject>();
            foreach (var elem in aoes)
            {
                elem.Unload();
            }
            base.OnDispose();
        }

        internal void OnChanged()
        {
            if (this.ZDecoration.Enable)
            {
                FuckAssetObject[] aoes = mDecrationRoot.GetComponentsInChildren<FuckAssetObject>();
                foreach (var elem in aoes)
                {
                    elem.Unload();
                }

                for (int i = 0; i < mDecrationRoot.transform.childCount; i++)
                {
                    LoadCellModel(mDecrationRoot.transform.GetChild(i).gameObject
                        , ZDecoration.Data.ResourceID);
                }
            }
            else
            {
                FuckAssetObject[] aoes = mDecrationRoot.GetComponentsInChildren<FuckAssetObject>();
                foreach (var elem in aoes)
                {
                    elem.Unload();
                }

                for (int i = 0; i < mDecrationRoot.transform.childCount; i++)
                {
                    LoadCellModel(mDecrationRoot.transform.GetChild(i).gameObject
                        , ZDecoration.Data.ResourceID_Disabled);
                }
            }
        }
        private void LoadCellModel(GameObject root, string name)
        {
            //加载模型
            if (!string.IsNullOrEmpty(name))
            {
                FuckAssetObject.GetOrLoad(name, System.IO.Path.GetFileNameWithoutExtension(name), (comp) =>
                {
                    if (comp)
                    {
                        if (this.IsDisposed)
                        {
                            comp.Unload();
                        }
                        else
                        {
                            comp.gameObject.ParentRoot(root);
                        }
                    }
                });
            }
        }
    }
}
