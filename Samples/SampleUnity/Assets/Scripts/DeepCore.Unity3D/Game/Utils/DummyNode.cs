using UnityEngine;
using System.Collections;

namespace DeepCore.Unity3D.Utils
{
    public class DummyNode : MonoBehaviour
    {
        private string mDummyName;
        private GameObject mTraceObject;

        public bool IsTrace
        {
            get { return IsGameObjectExists(mTraceObject); }
        }


        public bool Init(string name, GameObject traceObject)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.LogError("string.IsNullOrEmpty(DummyName)");
                return false;
            }
            
            mDummyName = name;
            mTraceObject = traceObject;

            gameObject.name = "DummyNode_" + name;
            Update();
            return true;
        }
        
        // Update is called once per frame
        void Update()
        {
            if (!IsGameObjectExists(mTraceObject))
            {
                mTraceObject = null;
            }
            if (mTraceObject != null && mTraceObject.activeInHierarchy)
            {
                transform.position = mTraceObject.Position();
                transform.rotation = mTraceObject.Rotation();
            }
        }

        public static bool IsGameObjectExists(UnityEngine.GameObject go)
        {
            return go != null && !go.Equals(null);
        }

        public void Unload()
        {
            FuckAssetObject[] aoes = gameObject.GetComponentsInChildren<FuckAssetObject>(true);
            foreach (var elem in aoes)
            {
                var aoe = elem;
                aoe.transform.SetParent(null);
                var script = aoe.gameObject.GetComponent<EffectAutoDestroy>();
                if (script)
                {
                    script.DoDestroy();
                }
                else
                {
                    aoe.Unload();
                }
            }
        }
    }
}