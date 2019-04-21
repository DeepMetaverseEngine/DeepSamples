using UnityEngine;
using System.Collections;

namespace DeepCore.Unity3D.Utils
{
    public class DummyNode : MonoBehaviour
    {
        private string mDummyName;
        private GameObject mTraceObject;


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
            if (mTraceObject != null && mTraceObject.activeInHierarchy)
            {
                transform.position = mTraceObject.Position();
                transform.rotation = mTraceObject.Rotation();
            }
        }
    }
}