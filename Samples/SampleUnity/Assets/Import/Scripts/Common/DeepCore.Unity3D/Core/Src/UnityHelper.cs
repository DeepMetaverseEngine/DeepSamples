using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace DeepCore.Unity3D
{
    public static partial class UnityHelper
    {

        public static bool IsObjectExist(Object val)
        {
            return val != null && !val.Equals(null);
        }

        /// <summary>
        /// 递归查找子节点
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static Transform FindRecursive(this Transform transform, string name, StringComparison comp = StringComparison.Ordinal)
        {
            //先广度遍历
            foreach (Transform t in transform)
            {
                if (t.name.Equals(name, comp))
                {
                    return t;
                }
            }

            //再深度遍历
            return (from Transform t in transform select FindRecursive(t, name, comp)).FirstOrDefault(tmp => tmp != null);
        }


        public static Transform GetChildAtOrDefault(this Transform transform, int index)
        {
            if (transform.childCount > index)
            {
                return transform.GetChild(index);
            }

            return null;
        }

        public static Transform[] GetChildren(this Transform transform)
        {
            var ret = new Transform[transform.childCount];
            var p = 0;
            foreach (Transform t in transform)
            {
                ret[p++] = t;
            }

            return ret;
        }

        public static Quaternion LogicRad2Quaternion(float direct)
        {
            return Quaternion.AngleAxis(direct * Mathf.Rad2Deg + 90, Vector3.up);
        }

        public static float Quaternion2LogicRad(Quaternion rot)
        {
            return (rot.eulerAngles.y - 90) / Mathf.Rad2Deg;
        }

#if UNITY_EDITOR
        private static HashMap<int, string> hash = new HashMap<int, string>();
#else
        private static HashSet<int> hash = new HashSet<int>();
#endif
        public static void Destroy(Object o, float t = 0f)
        {
            if (IsObjectExist(o))
            {
                //                var id = o.GetInstanceID();
                //#if UNITY_EDITOR
                //                if (hash.TryAdd(id, new System.Diagnostics.StackTrace().ToString()))
                //#else
                //                if (hash.Add(id))
                //#endif
                //                {
                //                    Object.Destroy(o, t);
                //                }
                //                else
                //                {
                //#if UNITY_EDITOR
                //                    Debug.LogError("Try to destroy a destroyed object\n" + hash[id], o);
                //#else
                //                    Debug.LogError("Try to destroy a destroyed object\n", o);
                //#endif
                //                }
                Object.Destroy(o, t);
            }
            else
            {
                Debug.LogError("Try to destroy a not exists object", o);
            }
        }

        public static void DestroyImmediate(Object o, bool b = false)
        {
            if (IsObjectExist(o))
            {
                //                var id = o.GetInstanceID();
                //#if UNITY_EDITOR
                //                if (hash.TryAdd(id, new System.Diagnostics.StackTrace().ToString()))
                //#else
                //                if (hash.Add(id))
                //#endif
                //                {
                //                    Object.DestroyImmediate(o, b);
                //                }
                //                else
                //                {
                //                    Debug.LogError("Try to destroy a destroyed object", o);
                //                }
                Object.DestroyImmediate(o, b);
            }
            else
            {
                Debug.LogError("Try to destroy a not exists object", o);
            }
        }
    }
}