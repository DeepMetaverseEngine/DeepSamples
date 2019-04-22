using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace DeepCore.Unity3D
{
    public  static partial class UnityHelper
    {
        private static GameObject sCacheNode;

        private static UnityTotalComponent sHelper;

        public static void Init()
        {
            if (!sHelper)
            {
                var obj = new GameObject("UnityHelperObject");
                sHelper = obj.AddComponent<UnityTotalComponent>();
                Object.DontDestroyOnLoad(obj);
            }
        }

        public static Transform DisableParent
        {
            get
            {
                if (sCacheNode == null)
                {
                    sCacheNode = new GameObject("_CacheNode_");
                    Object.DontDestroyOnLoad(sCacheNode);
                    sCacheNode.AddComponent<CacheRootComponent>();
                    sCacheNode.SetActive(false);
                }

                return sCacheNode.transform;
            }
        }

        public static void WaitForEndOfFrame(Action cb)
        {
            WaitForSeconds(0, cb);
        }

        public static void WaitForSeconds(float sec, Action cb)
        {
            sHelper.DelayInvoke.Delay(sec, cb);
        }

        public static void WaitForEndOfFrame<T>(T obj, Action<T> cb)
        {
            WaitForSeconds(0, obj, cb);
        }

        public static void WaitForSeconds<T>(float sec, T obj, Action<T> cb)
        {
            sHelper.DelayInvoke.Delay(sec, obj, cb);
        }

        public static Coroutine StartCoroutine(IEnumerator ator)
        {
            return sHelper.DelayInvoke.StartCoroutine(ator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="act"></param>
        public static void MainThreadInvoke(Action act)
        {
            sHelper.MainThreadDispatcher.Enqueue(act);
        }
    }
}