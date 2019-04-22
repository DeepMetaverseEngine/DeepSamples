using DeepCore;
using System;
using System.Collections.Generic;

namespace DeepCore.Unity3D.Utils
{
    internal static class TPool<T> where T : class, new()
    {
        private static Dictionary<Type, ObjectPool<T>> gTPool = new Dictionary<Type, ObjectPool<T>>();

        public static T Get()
        {
            Type t = typeof(T);
            ObjectPool<T> p = null;
            if (!gTPool.TryGetValue(t, out p))
            {
                p = new ObjectPool<T>();
                gTPool.Add(t, p);
            }
            return p.Get();
        }

        public static void Release(ref T v)
        {
#if !UNITY_IOS
            if (v != null)
            {
                Type t = typeof(T);
                ObjectPool<T> p = null;
                if (gTPool.TryGetValue(t, out p))
                {
                    p.Release(v);
                }
            }
#endif
            v = null;
        }
    }

}
