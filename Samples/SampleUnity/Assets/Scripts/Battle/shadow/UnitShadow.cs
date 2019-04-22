using UnityEngine;
using System.IO;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Utils;

public class UnitShadow : MonoBehaviour
{
    private FuckAssetObject mShadowObject;
    private SelectShadow mShadowCode;
    private TLAIUnit mUnit;
    private bool isDisposed = false;

    public void Init(TLAIUnit unit)
    {
        if (unit != null && !unit.IsDisposed)
        {
            mUnit = unit;
            Vector3 scale = Vector3.one * (mUnit.ZObj as DeepCore.GameSlave.ZoneUnit).Info.BodySize * 2 / (mUnit.ZObj as DeepCore.GameSlave.ZoneUnit).Info.BodyScale;
            string shadowPath = "/res/effect/ef_marquee.assetbundles";
            var id = FuckAssetObject.GetOrLoad(shadowPath, Path.GetFileNameWithoutExtension(shadowPath), (loader) =>
            {
                if (loader)
                {
                    if (unit.IsDisposed || isDisposed)
                    {
                        loader.Unload();
                        return;
                    }
                    mShadowObject = loader;
                    mShadowObject.transform.parent = unit.bindBehaviour.transform;
                    mShadowObject.transform.localPosition = Vector3.zero;
                    mShadowObject.transform.localEulerAngles = Vector3.zero;
                    mShadowObject.transform.localScale = scale;
                    //Projector[] ps = mShadowObject.transform.GetComponentsInChildren<Projector>(true);
                    //foreach (Projector p in ps)
                    //{
                    //    p.orthographicSize = (mUnit.ZObj as DeepCore.GameSlave.ZoneUnit).Info.BodySize *1.2f;
                    //}
                    mShadowCode = mShadowObject.GetComponent<SelectShadow>();
                }
            });


        }
    }

    public void Dispose()
    {
        if (mShadowObject != null)
        {
            mShadowObject.transform.SetParent(null);
            mShadowObject.Unload();
			mShadowObject = null;
        }
    }
    
    private void OnDestroy()
    {
        isDisposed = true;
    }


    public void DoSelect(bool isSelect)
    {
        if (mShadowCode != null && mUnit != null)
        {
            if (isSelect)
            {
                bool selfForce = !TLBattleScene.Instance.TargetIsEnemy(mUnit);
                mShadowCode.Init(selfForce);
            }
            mShadowCode.DoSelect(isSelect);
        }
    }
}