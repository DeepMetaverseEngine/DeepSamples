using UnityEngine;

namespace DeepCore.Unity3D
{
    public class CacheRootComponent : MonoBehaviour
    {
        private void OnDestroy()
        {
            UnityObjectCacheCenter.ClearAll();
        }

    }
}
