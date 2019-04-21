using DeepCore.Unity3D.Utils;
using DeepCore.GameSlave;

namespace DeepCore.Unity3D.Battle
{
    public class ComAIItem : ComAICell
    {
        protected ZoneItem ZItem { get { return ZObj as ZoneItem; } }


        public ComAIItem(BattleScene battleScene, ZoneItem obj)
            : base(battleScene, obj)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            OnLoadModel();
        }

        protected virtual void OnLoadModel()
        {
            FuckAssetLoader.GetOrLoad(ZItem.Info.FileName, System.IO.Path.GetFileNameWithoutExtension(ZItem.Info.FileName), (loader) =>
            {
                if (this.IsDisposed)
                {
                    if (loader.AssetComp)
                    {
                        loader.AssetComp.Unload();
                    }
                    return;
                }
                OnLoadModelFinish(loader.AssetComp);
            });
        }
       
        protected virtual void OnLoadModelFinish(FuckAssetObject aoe)
        {
            if (aoe)
            {
                this.DisplayCell.SetModel(aoe);
                CorrectDummyNode();
            }
        }
    }
}