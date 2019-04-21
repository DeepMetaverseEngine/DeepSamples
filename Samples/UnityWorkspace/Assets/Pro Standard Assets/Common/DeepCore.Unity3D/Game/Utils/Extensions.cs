using System;
using UnityEngine;
using DeepCore.Unity3D.Battle;
using System.ComponentModel;

namespace DeepCore.Unity3D.Utils
{
    public static class Extensions
    {
        public static int ArrayLength<T>(T[] array)
        {
            return array == null ? 0 : array.Length;
        }

        public static bool IsNaN(this Vector3 vect)
        {
            return float.IsNaN(vect.x) || float.IsNaN(vect.y) || float.IsNaN(vect.z);
        }

        public static bool IsNaN(this Quaternion quat)
        {
            return float.IsNaN(quat.w) || float.IsNaN(quat.x) || float.IsNaN(quat.y) || float.IsNaN(quat.z);
        }

        public static GameObject FindChild(this GameObject go, string name, StringComparison comp = StringComparison.Ordinal)
        {
            if (go.name.Equals(name,comp))
                return go;

            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject tmp = go.transform.GetChild(i).gameObject;
                tmp = FindChild(tmp, name);
                if (tmp != null)
                {
                    return tmp;
                }
            }
            return null;
        }

        public static GameObject Parent(this GameObject go)
        {
            if (go.transform.parent != null)
            {
                return go.transform.parent.gameObject;
            }
            return null;
        }

        public static Vector3 Position(this GameObject go)
        {
            return go.transform.position;
        }

        public static Quaternion Rotation(this GameObject go)
        {
            return go.transform.rotation;
        }

        public static Vector3 Forward(this GameObject go)
        {
            return go.transform.forward;
        }

        public static GameObject Position(this GameObject go, Vector3 position)
        {
            go.transform.position = position;
            return go;
        }

        public static GameObject Rotation(this GameObject go, Quaternion rotation)
        {
            go.transform.rotation = rotation;
            return go;
        }

        public static GameObject Forward(this GameObject go, Vector3 forward)
        {
            if (forward != Vector3.zero)
                go.transform.forward = forward;
            return go;
        }

        private static bool IsObjectExists(UnityEngine.Object go)
        {
            return go != null && !go.Equals(null);
        }

        public static GameObject Parent(this GameObject go, GameObject parent)
        {
            if (IsObjectExists(go))
            {
                go.transform.parent = IsObjectExists(parent)? parent.transform : null;
            }
            else
            {
                return null;
            }
            return go;
        }

        public static GameObject ParentRoot(this GameObject go, GameObject parent)
        {
            go.transform.parent = parent.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.transform.localRotation = Quaternion.identity;
            return go;
        }

        public static GameObject LookAt(this GameObject go, GameObject target)
        {
            go.transform.LookAt(target.transform);
            return go;
        }

        public static GameObject LookAt(this GameObject go, Transform target)
        {
            go.transform.LookAt(target);
            return go;
        }

        public static GameObject LookAt(this GameObject go, Vector3 worldPosition, [DefaultValue("Vector3.up")]Vector3 worldUp)
        {
            go.transform.LookAt(worldPosition, worldUp);
            return go;
        }

        public static Transform LookAt(this Transform go, GameObject target)
        {
            go.LookAt(target.transform);
            return go;
        }

        public static GameObject ZonePos2UnityPos(this GameObject go, float totalHeight
            , float x, float y, float z = 0)
        {
            go.Position(ZonePos2UnityPos(totalHeight, x, y, z));
            return go;
        }

        public static GameObject ZonePos2NavPos(this GameObject go, float totalHeight
            , float x, float y, float z = 0)
        {
            go.Position(ZonePos2NavPos(totalHeight, x, y, z));
            return go;
        }

        public static GameObject ZoneRot2UnityRot(this GameObject go, float direct)
        {
            go.Rotation(ZoneRot2UnityRot(direct));
            return go;
        }

        public static bool NearlyZero(float v)
        {
            return System.Math.Abs(v) < 0.0001f;
        }

        //找个地方保存地图信息 优化掉参数1
        public static Vector2 UnityPos2ZonePos(float totalHeight, Vector3 pos)
        {
            return new Vector2(pos.x, totalHeight - pos.z);
        }

        public static Vector3 NavRayHit(Vector3 pos)
        {
            RaycastHit rayHit;
            if (Physics.Raycast(pos + Vector3.up * 100, Vector3.down, out rayHit
                , Mathf.Infinity, 1 << BattleFactory.Instance.StageNavLay))
            {
                pos.y = rayHit.point.y;
            }
            return pos;
        }

        public static bool NavRayHit(Vector3 pos, out Vector3 hit)
        {
            hit = pos;
            RaycastHit rayHit;
            if (Physics.Raycast(pos + Vector3.up * 100, Vector3.down, out rayHit
                , Mathf.Infinity, 1 << BattleFactory.Instance.StageNavLay))
            {
                hit.y = rayHit.point.y;
                return true;
            }
            return false;
        }

        //找个地方保存地图信息 优化掉参数1
        public static Vector3 ZonePos2UnityPos(float totalHeight, float x, float y, float z = 0)
        {
            Vector3 tmp = Vector3.zero;
            tmp.x = x;
            tmp.y = z;
            tmp.z = totalHeight - y;
            return tmp;
        }

        //找个地方保存地图信息 优化掉参数1
        public static Vector3 ZonePos2NavPos(float totalHeight, float x, float y, float z = 0)
        {
            Vector3 tmp = ZonePos2UnityPos(totalHeight, x, y, z);
            NavRayHit(tmp, out tmp);
            return tmp;
        }

        public static Quaternion ZoneRot2UnityRot(float direct)
        {
            return Quaternion.AngleAxis(direct * Mathf.Rad2Deg + 90, Vector3.up);
        }

        public static float UnityRot2ZoneRot(Quaternion rot)
        {
            return (rot.eulerAngles.y - 90) / Mathf.Rad2Deg;
        }
    }
}