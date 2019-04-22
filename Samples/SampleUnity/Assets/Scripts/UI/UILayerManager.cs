
using System;
using UnityEngine;

public class UILayerMgr
{

    public const int MenuOrderSpace = 100;
    public const int SubMenuOrderSpace = 10;
    public const float CompZOrderSpace = 1;
    public const float MenuZSpace = -3000;
    public const float SubMenuZSpace = -1200;
    public const float CompZSpace = -500;

    //设置深度
    public static void SetLayerOrder(GameObject go, int layerOrder, bool isUI, int layer = 5)
    {
        SetLayer(go, layer);
        if (isUI)
        {
            if (go.GetComponent<UnityEngine.UI.GraphicRaycaster>() == null)
            {
                go.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            }
            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = go.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = layerOrder;
        }
        else
        {
            Renderer[] renders = go.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer render in renders)
            {
                render.sortingOrder = layerOrder;
            }
        }
    }

    public static void SetPositionZ(GameObject go, float posZ)
    {
        Transform trans = go.transform;
        Vector3 pos = trans.localPosition;
        trans.localPosition = new Vector3(pos.x, pos.y, posZ);
    }

    public static void SetUILayer(DeepCore.Unity3D.UGUI.DisplayNode node, int layerOrder, float posZ)
    {
        GameObject go = node.UnityObject;
        SetLayerOrder(go, layerOrder, true);
        SetPositionZ(go, posZ);
    }

    public static int GetLayerOrder(GameObject go, bool isUI)
    {
        int sortingOrder = 0;
        if (isUI)
        {
            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas != null)
            {
                sortingOrder = canvas.sortingOrder;
            }
        }
        else
        {
            Renderer[] renders = go.GetComponentsInChildren<Renderer>(true);
            foreach (Renderer render in renders)
            {
                if (render.sortingOrder > sortingOrder)
                {
                    sortingOrder = render.sortingOrder;
                }
            }
        }
        return sortingOrder;
    }

    public static int GetParentLayerOrder(GameObject go)
    {
        var parent = go.transform.parent;
        if (parent != null)
        {
            var cvs = parent.gameObject.GetComponentInParent<Canvas>();
            if (cvs != null)
                return cvs.sortingOrder;
        }
        return 0;
    }

    public static void SetLocalLayerOrder(GameObject go, int layerOrder, bool isUI, int layer = 5)
    {
        int order = GetParentLayerOrder(go);
        SetLayerOrder(go, order + layerOrder, isUI, layer);
    }

    //设置层级
    public static void SetLayer(GameObject go, int layer)
    {
        if (go != null)
        {
            go.layer = layer;
            Transform[] translist = go.GetComponentsInChildren<Transform>(true);
            foreach (var o in translist)
            {
                o.gameObject.layer = layer;
            }
        }
    }

}
