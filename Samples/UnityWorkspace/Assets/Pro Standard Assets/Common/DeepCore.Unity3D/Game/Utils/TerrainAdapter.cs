using UnityEngine;

namespace DeepCore.Unity3D.Utils
{
    public abstract class TerrainAdapter
    {
        public abstract void Load(string assetBundleName, System.Action<bool, GameObject> callback);

    }
}