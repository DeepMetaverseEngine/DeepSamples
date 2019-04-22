using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;

namespace DeepCore.Unity3D.UGUIEditor.UI
{
    public class HZCanvas : UECanvas
    {
        public HZCanvas()
        {
            Enable = false;
            EnableChildren = true;
            IsInteractive = true;
            Layout = new UILayout();
        }

        public TouchClickHandle TouchClick { get; set; }

        protected override void OnDispose()
        {
            base.OnDispose();
            TouchClick = null;
        }

        public static HZCanvas CreateCanvas(UECanvasMeta m)
        {
            if (m == null)
            {
                m = new UECanvasMeta
                {
                    Visible = true
                };
            }
            var e = UIFactory.Instance as UIEditor;
            var l = (HZCanvas) e.CreateFromMeta(m, meta => new HZCanvas());
            return l;
        }

        protected override void OnPointerClick(PointerEventData e)
        {
            base.OnPointerClick(e);
            if (TouchClick != null)
                TouchClick(this);
            var soundKey = GetAttributeAs<string>("sound");
            if (!string.IsNullOrEmpty(soundKey))
                SoundManager.Instance.PlaySoundByKey(soundKey);
        }

        protected override void DecodeEnd(UIEditor.Decoder editor, UIComponentMeta e)
        {
            base.DecodeEnd(editor, e);
            Enable = e.Enable;
            EnableChildren = e.EnableChilds;
        }

        /// <summary>
        ///     使用网格布局
        /// </summary>
        /// <param name="cellSize"></param>
        /// <param name="space"></param>
        /// <param name="count"></param>
        /// e.g
        //HZCanvas cvs = new HZCanvas();
        //cvs.Bounds2D = new Rect(0, 0, 300, 300);
        //mRoot.AddChild(cvs);
        //cvs.Name = "HZCanvas";
        //cvs.SetGridLayout(new Vector2(100, 50), new Vector2(0, 1), HZCanvas.Constraint.FixedColumnCount, 2);
        //for(int i = 0; i < 100; i++)
        //{
        //    HZLabel l = new HZLabel();
        //    l.Size2D = new Vector2(100, 50);
        //    l.Text = "Test" + i.ToString();
        //    cvs.AddChild(l);
        //}
        public void SetGridLayout(Vector2 cellSize, Vector2 space, RectOffset padding, GridLayoutGroup.Constraint cons,
            int count)
        {
            var grid = UnityObject.GetComponent<GridLayoutGroup>();
            if (!grid)
            {
                grid = UnityObject.AddComponent<GridLayoutGroup>();
            }
            grid.enabled = true;
            grid.cellSize = cellSize;
            grid.spacing = space;
            grid.constraint = cons;
            grid.constraintCount = count;
            grid.padding = padding;
        }

        public void DisableGridLayout()
        {
            var grid = UnityObject.GetComponent<GridLayoutGroup>();
            if (!grid)
            {
                grid.enabled = false;
            }
        }


        public void SetContentSizeFitter(ContentSizeFitter.FitMode h, ContentSizeFitter.FitMode v)
        {
            var fitter = UnityObject.GetComponent<ContentSizeFitter>();
            if (!fitter)
                fitter = UnityObject.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = h;
            fitter.verticalFit = v;
        }

        public void SetFlexibleGridLayout(Vector2 cellSize, Vector2 space, RectOffset padding)
        {
            SetGridLayout(cellSize, space, padding, GridLayoutGroup.Constraint.Flexible, 0);
        }

        public void SetCenterScaleMode(DisplayNode templateNode,
            int childCount,
            float scale,
            int startPage,
            Action<int, DisplayNode> initChild, Action<int, DisplayNode> pageChange,
            Action beginMove, Action endMove)
        {
            var css = UnityObject.GetComponent<CenterScaleScrollRect>();
            if (!css)
            {
                css = UnityObject.AddComponent<CenterScaleScrollRect>();
                css.OnPageChange += (index, transform) => { pageChange.Invoke(index, GetChildAt(index)); };
                css.OnBeginMove += () =>
                {
                    if (beginMove != null)
                    {
                        beginMove.Invoke();
                    }
                };
                css.OnEndMove += () =>
                {
                    if (endMove != null)
                    {
                        endMove.Invoke();
                    }
                };
            }

            RemoveAllChildren(true);
            for (var i = 0; i < childCount; i++)
            {
                var node = templateNode.Clone();
                node.Visible = true;
                AddChild(node);
                initChild.Invoke(i, node);
            }
            css.MaxScale = scale;
            css.CellSize = templateNode.Size2D;
            css.Initialize(startPage);
        }

        public int CenterScalePageIndex
        {
            get
            {
                var css = UnityObject.GetComponent<CenterScaleScrollRect>();
                if (!css)
                {
                    Debug.LogError("not in CenterScaleMode");
                    return 0;
                }
                return css.CurrentPage;
            }
        }

        public void ChangeCenterScalePage(int index, bool smooth)
        {
            var css = UnityObject.GetComponent<CenterScaleScrollRect>();
            if (!css)
            {
                Debug.LogError("not in CenterScaleMode");
                return;
            }
            css.MoveTo(index, smooth);
        }
    }
}