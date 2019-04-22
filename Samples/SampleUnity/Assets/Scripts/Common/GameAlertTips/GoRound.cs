using UnityEngine;
 
using System.Collections.Generic;

using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIAction;

public class GoRoundMgr
{
    struct Detail
    {
        public string html;
        public string direction;
        public float duration;
        public Detail(string html, string direction, float duration)
        {
            this.html = "<a size = '20'>" + html + "</a>";
            this.direction = direction;
            this.duration = duration;
        }
    }

    private HZRichTextPan texthtml;
    private UERoot m_root;
    private UEImageBox mgoround;
    private Queue<Detail> mTips = new Queue<Detail>();
    private bool gorounding = false;

    public void SetEnable(bool enable)
    {
        var frame = m_root.FindChildByEditName("frame", false);
        if (frame != null)
        {
            frame.Visible = enable;
        }
    }
    
    public GoRoundMgr()
    {
        m_root = (HZRoot)HZUISystem.CreateFromFile("xml/tips/uigoround.gui.xml");
        //HudManager.Instance.InitAnchorWithNode(m_root, HudManager.HUD_BOTTOM);
        UECanvas csv_msg = m_root.FindChildByEditName("csv_msg", true) as UECanvas;
        mgoround = m_root.FindChildByEditName("bg", true) as UEImageBox;
        UnityEngine.UI.Mask mask = csv_msg.UnityObject.AddComponent<UnityEngine.UI.Mask>();
        mask.showMaskGraphic = false;

        texthtml = new HZRichTextPan();
        texthtml.Position2D = Vector2.zero;
        //texthtml.RichTextLayer.UseBitmapFont = true;
        texthtml.RichTextLayer.SetEnableMultiline(false);
        texthtml.Size2D = csv_msg.Size2D;
        csv_msg.AddChild(texthtml);

        m_root.Visible = false;
        HZUISystem.Instance.GetUIAlertLayer().AddChild(m_root);

    }

    public UIComponent getNode()
    {
        if  (null !=  this.mgoround)
        {
            return this.mgoround;
        }
        return null;
    }

    public void SetLocation(Vector2 p)
    {
        RectTransform rt = this.mgoround.UnityObject.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(p.x, -p.y);
    }

    /**
     *  立即结束一个条目的滚动
     * */
    public void cutover()
    {
        if (true == this.gorounding)
        {
            this.gorounding = false;
            this.texthtml.RemoveAllAction(false);
            this.texthtml.Visible = false;
            this.clear();
        }
    }

    public void clear()
    {
        this.mTips.Clear();
        m_root.Visible = false;
        this.gorounding = false;
    }

    /// <summary>
    /// 跑马灯追加信息
    /// </summary>
    /// <param name="html">文本内容</param>
    /// <param name="direction">滚动方向</param>
    /// <param name="speed">滚动速度——每秒多少像素</param>
    public void addTip(string html, string direction = "h", float duration = 15)
    {
        if (this.mTips.Count > 5)
            return;
        Detail detail = new Detail(html, direction, duration);
        this.mTips.Enqueue(detail);
        if (false == this.gorounding)
        {
            this.gorounding = true;
            this.mgoround.Visible = true;
            this.cycle();
        }
    }

    public void changeBG(string filePath)
    {
        this.mgoround.Layout = HZUISystem.CreateLayoutFromFile(filePath, UILayoutStyle.IMAGE_STYLE_BACK_4, 0);
    }

    public void changeBGXml(string filePath)
    {
        this.mgoround.Layout = HZUISystem.CreateLayoutFromAtlas(filePath, UILayoutStyle.IMAGE_STYLE_BACK_4, 0);
    }

    public void destroy()
    {
        this.mgoround.RemoveAllChildren(true);
        this.mgoround.RemoveFromParent(true);
        this.mTips = null;
    }

    private void cycle()
    {
        if ( 0 == this.mTips.Count)
        {
            this.mgoround.Visible = false;
            this.gorounding = false;
            m_root.Visible = false;
        }
        else
        {
            m_root.Visible = true;
            Detail detail = this.mTips.Dequeue();
            if (false == this.texthtml.Visible)
            {
                this.texthtml.Visible = true;
            }
            if ("h" == detail.direction)
            {
                this.texthtml.XmlText = detail.html;

                //this.texthtml.SetLocation(this.mgoround.Bounds.width + 1, (this.mgoround.Bounds.height - this.texthtml.RichTextLayer.ContentHeight) / 2);

                //public float ContentWidth { get { return mContentWidth; } }
                //public float ContentHeight { get { return mContentHeigh; } }

                this.texthtml.Position2D = new Vector2(
                    this.mgoround.Size2D.x + 1,
                    (this.mgoround.Size2D.y - texthtml.RichTextLayer.ContentHeight) / 2
                );

                MoveAction move = new MoveAction();
                move.TargetX = 0 - 1 - texthtml.RichTextLayer.ContentWidth;
                move.TargetY = (this.mgoround.Size2D.y - texthtml.RichTextLayer.ContentHeight) / 2;
                move.Duration = detail.duration;
                move.ActionFinishCallBack = (sender) =>
                {
                    this.cycle();
                };
                this.texthtml.RemoveAllAction(false);
                this.texthtml.AddAction(move);
            }
            else if ("v" == detail.direction)
            {
                //TODO
            }
            else
            {
            }
        }
    }
}
