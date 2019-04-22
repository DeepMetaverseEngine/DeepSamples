 
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;

public class HZUIBehaviour : UIBehaviour
{
    public UIComponent mfui;
    
    public enum AlignType
    {
        // 左对齐
        Align_Left = 0,
        // 上对齐
        Align_Top = 0,
        // 右对齐
        Align_Right = 3,
        // 下对齐
        Align_Bottom = 3,
        // 居中(包括水平居中和垂直居中)
        Align_Center = 1,
        // 拉伸(包括水平拉伸和垂直拉伸)
        Align_Stretch = 2,
    }

    public class AlignSet
    {
        public DisplayNode ui;
        public AlignType alignx;
        public AlignType aligny;
    }

    Dictionary<DisplayNode, AlignSet> alignsets = new Dictionary<DisplayNode, AlignSet>();
    DisplayNode alignRoot;

    public void SetAlign(AlignType alignx, AlignType aligny, UIComponent relative = null)
    {
        var anchorMin = new Vector2(0, 0);
        var anchorMax = new Vector2(1, 1);
        var pivot = new Vector2(0, 1);

        var sizeDelta = mfui.Root.Transform.rect.size;

        if (alignx == AlignType.Align_Left)
        {
            anchorMax.x = 0;
        }
        else if (alignx == AlignType.Align_Center)
        {
            anchorMin.x = 0.5f;
            anchorMax.x = 0.5f;
            pivot.x = 0.5f;
        }
        else if (alignx == AlignType.Align_Right)
        {
            anchorMin.x = 1;
        }
        else
        {
            sizeDelta.x = 0;
        }
        
        if (aligny == AlignType.Align_Top)
        {
            anchorMin.y = 1;
        }
        else if (aligny == AlignType.Align_Center)
        {
            anchorMin.y = 0.5f;
            anchorMax.y = 0.5f;
            pivot.y = 0.5f;
        }
        else if (aligny == AlignType.Align_Bottom)
        {
            anchorMax.y = 0;
        }
        else
        {
            sizeDelta.y = 0;
        }

        mfui.Transform.anchorMin = anchorMin;
        mfui.Transform.anchorMax = anchorMax;
        mfui.Transform.sizeDelta = sizeDelta;
        mfui.Transform.pivot = pivot;
        
        if (relative != null)
        {
            if (relative != mfui)
            {
                relative.Transform.anchorMin = new Vector2(0, 0);
                relative.Transform.anchorMax = new Vector2(1, 1);
                relative.Transform.sizeDelta = new Vector2(0, 0);
            }
            alignRoot = relative;
        }
        else
        {
            alignRoot = mfui;
        }
    }

    Vector2 oldSize;

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();

        if (mfui == null || mfui.IsDispose) return;

        var rootRect = mfui.Transform.rect;

        if (rootRect.width == 0 || rootRect.height == 0)
        {
            return;
        }

        // 计算UI改变大小
        var xPad = rootRect.width - oldSize.x;
        var yPad = rootRect.height - oldSize.y;

        //适配指定了对齐方式的子控件
        foreach (var kv in this.alignsets)
        {
            var v = kv.Value;
            var rect = v.ui.Bounds2D;

            if (v.alignx == AlignType.Align_Stretch)
                rect.width = rect.width + xPad;
            else if (v.alignx == AlignType.Align_Left)
                rect.x = rect.x;
            else if (v.alignx == AlignType.Align_Right)
                rect.x = rect.x + xPad;
            else if (v.alignx == AlignType.Align_Center)
                rect.x = rect.x + xPad / 2;


            if (v.aligny == AlignType.Align_Stretch)
                rect.height = rect.height + yPad;
            else if (v.aligny == AlignType.Align_Top)
                rect.y = rect.y;
            else if (v.aligny == AlignType.Align_Bottom)
                rect.y = rect.y + yPad;
            else if (v.aligny == AlignType.Align_Center)
                rect.y = rect.y + yPad / 2;

            v.ui.Bounds2D = rect;
        }

        //自动适配未设置对齐方式的子控件
        List<DisplayNode> childs = new List<DisplayNode>();
        mfui.GetAllChild(childs);
        foreach (var child in childs)
        {
            if (!alignsets.ContainsKey(child))
            {
                var x = child.X + child.Width / 2;
                var y = child.Y + child.Height / 2;
                var tx = x * rootRect.width / oldSize.x;
                var ty = y * rootRect.height / oldSize.y;
                child.X = tx - child.Width / 2;
                child.Y = ty - child.Height / 2;
            }
        }

        oldSize = new Vector2(rootRect.width, rootRect.height);
    }

    protected override void Awake()
    {
        base.Awake();

        var node = GetComponent<DisplayNodeBehaviour>();
        mfui = node.Binding as UIComponent;
        oldSize = mfui.Transform.rect.size;

        OnInit();

        if (alignRoot == null && mfui.Parent is DisplayRoot)
        {
            SetAlign(AlignType.Align_Center, AlignType.Align_Center);
        }

        OnRectTransformDimensionsChange();
    }
    
    //设置水平对齐方式
    public void alignH(DisplayNode child, AlignType alignx)
    {
        align(child, alignx, 0);
    }

    //设置垂直对齐方式
    public void alignV(DisplayNode child, AlignType aligny)
    {
        align(child, 0, aligny);
    }

    //设置子控件的对齐方式
    //@param child 要设置的子控件
    //@param alignx 水平对齐方式
    //@param aligny 垂直对齐方式
    public void align(DisplayNode child, AlignType alignx, AlignType aligny)
    {
        var align = new AlignSet();
        align.ui = child;
        align.alignx = alignx;
        align.aligny = aligny;

        alignsets.Add(child, align);
    }

    protected virtual void OnInit()
    {
    }

    public UIComponent GetUI(string name)
    {
        return mfui.FindChildByEditName(name);
    }

    public T BindChild<T>(string name) where T : HZUIBehaviour
    {
        var ui = mfui.FindChildByEditName(name);
        T component = ui.UnityObject.GetComponent<T>();
        if (component == null)
        {
            component = ui.UnityObject.AddComponent<T>();
        }

        return component;
    }
}