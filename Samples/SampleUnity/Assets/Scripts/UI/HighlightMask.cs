using UnityEngine;
using UnityEngine.UI;


public class HighlightMask : MaskableGraphic, ICanvasRaycastFilter, FingerHandlerInterface
{
    public RectTransform arrow;
    private Rect lastRect;
    public Vector2 center = Vector2.zero;
    public Vector2 size = new Vector2(0, 0);
    public bool show = true;

    public delegate void PointDownCallBack(Vector2 fingerpos,string eventName);
    public event PointDownCallBack OnPointDownHandle { add { mPointerDown += value; } remove { mPointerDown -= value; } }
    public PointDownCallBack mPointerDown;
    public void SetArrowTransform(bool show,RectTransform trans = null,float alpha = 0)
    {
        arrow = trans;
        color = new Color(0, 0, 0, alpha);
        lastRect = new Rect();
        if (!trans)
        {
            size = Vector2.zero;
        }
        gameObject.SetActive(show);

        if (show && GameSceneMgr.Instance.UGUI.Rock.HasFingerIndex)
        {
            GameSceneMgr.Instance.UGUI.Rock.Reset(true);
        }
    }

    private bool CheckSpecialObject(Vector2 sp,string eName = null)
    {
        if (!this.gameObject.activeSelf)
        {
            return false;
        }
        bool ret = true;
        if (arrow == GameSceneMgr.Instance.UGUI.Rock.backGround.transform && GameSceneMgr.Instance.UGUI.Rock.HasFingerIndex)
        {
            ret = false;
        }
        else
        {
            ret = !RectTransformUtility.RectangleContainsScreenPoint(arrow, sp, GameSceneMgr.Instance.UICamera);
        }
        if (!ret && eName != null)
        {
            //var p = new Dictionary<string, object>();
            //if(arrow)
            //{
            //    p.Add(name, arrow.name);
            //}
            //DramaHelper.SendMessage("GuideHighLight.Touch."+ eName + "." + arrow.name, EventManager.defaultParam);
        }
        if (ret)
        {
            //TODO 不能点提示
            if (mPointerDown != null)
            {
                mPointerDown.Invoke(sp,eName);
            }
        }
        return ret;
    }
    public bool OnDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        return CheckSpecialObject(fingerPos);
    }

    public bool OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        return CheckSpecialObject(fingerPos,"Down");
    }

    public bool OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        return CheckSpecialObject(fingerPos,"Up");
    }

    public void OnFingerClear()
    {
        
    }


    public void DoUpdate()
    {            
        if (arrow && (lastRect.x != arrow.position.x || lastRect.y != arrow.position.y || lastRect.size != arrow.sizeDelta))
        {
            //Vector3 wp = arrow.parent.TransformPoint(arrow.localPosition);
            Vector3 lp = rectTransform.InverseTransformPoint(arrow.position);
            this.size = arrow.sizeDelta;

            lp.x = lp.x + (0.5f - arrow.pivot.x) * this.size.x;
            lp.y = lp.y + (0.5f - arrow.pivot.y) * this.size.y;
            this.center = lp;
            lastRect = new Rect(arrow.position, arrow.sizeDelta);
            SetAllDirty();
        }
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        bool ret = !RectTransformUtility.RectangleContainsScreenPoint(arrow, sp, eventCamera);
        return ret;
    }

    protected override void OnPopulateMesh(VertexHelper vbo)
    {
        Vector4 outer = new Vector4(-rectTransform.pivot.x * rectTransform.rect.width, -rectTransform.pivot.y * rectTransform.rect.height, (1 - rectTransform.pivot.x) * rectTransform.rect.width, (1 - rectTransform.pivot.y) * rectTransform.rect.height);

        Vector4 inner = new Vector4(center.x - size.x / 2,
                                    center.y - size.y / 2,
                                    center.x + size.x * 0.5f,
                                    center.y + size.y * 0.5f);

        vbo.Clear();

        var vert = UIVertex.simpleVert;

        // left
        vert.position = new Vector2(outer.x, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(outer.x, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.x, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.x, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vbo.AddTriangle(0, 1, 2);
        vbo.AddTriangle(2, 3, 0);

        // top
        vert.position = new Vector2(inner.x, inner.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.x, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.z, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.z, inner.w);
        vert.color = color;
        vbo.AddVert(vert);

        vbo.AddTriangle(4, 5, 6);
        vbo.AddTriangle(6, 7, 4);
        // right
        vert.position = new Vector2(inner.z, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.z, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(outer.z, outer.w);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(outer.z, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vbo.AddTriangle(8, 9, 10);
        vbo.AddTriangle(10, 11, 8);
        // bottom
        vert.position = new Vector2(inner.x, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.x, inner.y);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.z, inner.y);
        vert.color = color;
        vbo.AddVert(vert);

        vert.position = new Vector2(inner.z, outer.y);
        vert.color = color;
        vbo.AddVert(vert);

        vbo.AddTriangle(12, 13, 14);
        vbo.AddTriangle(14, 15, 12);

    }

    private void Update()
    {
        if (show)
        {
            DoUpdate();
        }
    }
}