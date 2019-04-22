using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.GUI.Display.Text;
using DeepCore.Xml;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using TextAnchor = DeepCore.GUI.Data.TextAnchor;

/// <summary>
/// 游戏提示功能管理类.
/// </summary>
public class FloatingTipsU
{
    private static int NODEMAXCOUNT = 3;
    private static int NODEMAXCHCHECOUNT = 20;

    public FloatingTipsU()
    {
        for (int i = 0; i < NODEMAXCOUNT; i++)
        {
            NodeInfo info = new NodeInfo();
            nodecaches.Add(new NodeCache(info.framenode,info.textnode));
        }
    }
    
    public struct NodeCache
    {
        public HZCanvas frame;
        public HZRichTextPan textnode;

        public NodeCache(HZCanvas frame,HZRichTextPan textnode)
        {
            this.frame = frame;
            this.textnode = textnode;
        }
    }

    
    #region FloatingTipsU(漂浮提示).
    public int HTMLTIPS_HEIGHT = 45;
    
    public static List<NodeCache> nodecaches = new List<NodeCache>();
    public static List<NodeInfo> nodelist = new List<NodeInfo>();
    

    public static int index = 0;
    internal static List<FloatingHelper> mFloatingList = new List<FloatingHelper>();
    internal static List<FloatingHelper> mWaitFloatingList = new List<FloatingHelper>();

    public void Update(float deltaTime)
    {
        for (int i = 0; i < mFloatingList.Count; i++)
        {
            mFloatingList[i].Update();
        }

        if (mFloatingList.Count < 1 && mWaitFloatingList.Count > 0)
        {
            index = 0;
            for (int i = 0; i < 3; i++)
            {
                if (mWaitFloatingList.Count > 0)
                {
                    mWaitFloatingList[0].Index = i;
                    index = i + 1;
                    mFloatingList.Add(mWaitFloatingList[0]);
                    mWaitFloatingList.RemoveAt(0);
                }
            }
        }
    }

    /// <summary>
    /// 对象做飘浮效果.
    /// </summary>
    /// <param name="node">执行飘浮的显示对象.</param>
    /// <param name="distance">飘浮距离.</param>
    /// <param name="parent">显示对象所处的父节点.</param>
    /// <param name="beginGloblePosition">指定飘浮起始位置.</param>
    public void ShowFloatingTips(DisplayNode node, float distance, DisplayNode parent, Vector2 beginPosition, float pause = 20)
    {
        if (parent == null)
        {
            parent = HZUISystem.Instance.GetUIAlertLayer();
        }
        
        FloatingHelper helper = new FloatingHelper(node, distance, parent, beginPosition, pause);

        if (mFloatingList != null && index < 3 && mWaitFloatingList.Count < 1)
        {
            mFloatingList.Add(helper);
            helper.Index = index;
            index += 1;
        }
        else
        {
            if (mWaitFloatingList != null && mWaitFloatingList.Count < NODEMAXCHCHECOUNT)
            {
                mWaitFloatingList.Add(helper);
            }
            else
                helper.Dispose();
        }

    }
    
    public class NodeInfo
    {
        private static UILayout frameLayout1 = HZUISystem.CreateLayoutFromAtlas("#static/TL_hud/output/TL_hud.xml|TL_hud|232", UILayoutStyle.IMAGE_STYLE_H_012, 33);
        private static UILayout frameLayout2 = HZUISystem.CreateLayoutFromAtlas("#static/TL_hud/output/TL_hud.xml|TL_hud|176", UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
        public HZCanvas framenode;
        public HZRichTextPan textnode;
        public Vector2 beginPosition;
        public AttributedString text = null;
        public string textstring = null;
        public uint rgba = 0;
        public int fontSize = 0;
        public bool isCustom = true;
        public DisplayNode parent;
        
        public NodeInfo(bool iscreate = true)
        {
            if (iscreate)
            {
                parent = HZUISystem.Instance.GetUIAlertLayer();
                textnode = new HZRichTextPan();
                textnode.UnityObject.name = "HZRichTextPan";
                textnode.RichTextLayer.SetEnableMultiline(false);
                textnode.RichTextLayer.SetWidth(parent.Width);
    
                framenode = new HZCanvas();
                framenode.Name = "FloatingTipsU";
                framenode.AddChild(textnode);
                framenode.Visible = false;
                parent.AddChild(framenode);
            }
        }

        public void InitNode()
        {
            if (isCustom)
            {
                textnode.XmlText = textstring;
                uint color = (rgba >> 8 & 0xffffff) | (rgba << 24 & 0xff000000); 
                textstring = "<font size='" + fontSize + "' color='" + color.ToString("X") + "'>" + textstring +
                             "</font>";
                textnode.XmlText = textstring;
                framenode.Layout = frameLayout1;
                float paddingX = 20, paddingY = 1;
                framenode.Size2D = new Vector2(textnode.RichTextLayer.ContentWidth + paddingX * 2, framenode.Layout.PreferredSize.y + paddingY * 2);
                textnode.Size2D = framenode.Size2D;
                textnode.RichTextLayer.Anchor = TextAnchor.C_C;
            }
            else
            {
                textnode.RichTextLayer.SetString(text);
                framenode.Layout = frameLayout2;
                if(framenode.Layout.PreferredSize.x > textnode.RichTextLayer.ContentWidth)
                    framenode.Size2D = new Vector2(framenode.Layout.PreferredSize.x, framenode.Layout.PreferredSize.y);
                else
                    framenode.Size2D = new Vector2(textnode.RichTextLayer.ContentWidth, framenode.Layout.PreferredSize.y);
                textnode.Size2D = new Vector2(textnode.RichTextLayer.ContentWidth, textnode.RichTextLayer.ContentHeight);
                textnode.Y = (framenode.Height - textnode.RichTextLayer.ContentHeight) * 0.5f;
                textnode.X = (framenode.Width - textnode.RichTextLayer.ContentWidth) * 0.5f;
                beginPosition.x -= framenode.Width * 0.5f;
            }
        }
    }

    public void ShowFloatingTips(AttributedString text, float distance)
    {
        if (text == null)
        {
            return;
        }
        if (nodelist.Count>NODEMAXCHCHECOUNT+NODEMAXCOUNT)
            return;
        
        NodeInfo nodeinfo = new NodeInfo(false);
        nodeinfo.beginPosition.y = -120;
        nodeinfo.text = text;
        nodeinfo.isCustom = false;
        nodelist.Add(nodeinfo);
        
        ShowFloatingTips(null, distance, nodeinfo.parent, nodeinfo.beginPosition);
    }


    /// <summary>
    /// 文字飘浮效果.
    /// </summary>
    /// <param name="text">文字内容.</param>
    /// <param name="color">文字颜色.</param>
    /// <param name="distance">上飘距离.</param>
    /// <param name="parent">所属父节点.</param>
    /// <param name="beginPosition">相对于父节点的位置.</param>
    public void ShowFloatingTips(string text, uint rgba, float distance, DisplayNode parent, Vector2 beginPosition, int fontSize = 20)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        if (nodelist.Count>NODEMAXCHCHECOUNT+NODEMAXCOUNT)
            return;
        
        NodeInfo nodeinfo = new NodeInfo(false);
        if (parent != null)
            nodeinfo.parent = parent;
        nodeinfo.isCustom = true;
        nodeinfo.textstring = text;
        nodeinfo.rgba = rgba;
        nodeinfo.fontSize = fontSize;
        nodeinfo.beginPosition = beginPosition;
        nodelist.Add(nodeinfo);

        ShowFloatingTips(null, distance, nodeinfo.parent, nodeinfo.beginPosition);
    }

    /// <summary>
    /// 文字飘浮效果.
    /// </summary>
    /// <param name="text">文字内容.</param>
    public void ShowFloatingTips(string text)
    {
        ShowFloatingTips(text,
                            0xffffffff,
                            70,
                             HZUISystem.Instance.GetUIAlertLayer(),
                             Vector2.zero);
    }
    public void ShowFloatingTips(string text, uint rgba)
    {
        ShowFloatingTips(text,
                            rgba,
                            70,
                            HZUISystem.Instance.GetUIAlertLayer(),
                            Vector2.zero);
    }

    public void ShowFloatingTips(string text, uint rgba, int font)
    {
        ShowFloatingTips(text,
                            rgba,
                            70,
                            HZUISystem.Instance.GetUIAlertLayer(), Vector2.zero, font);
    }

    public void ShowFloatingTipsImage(string imageName, string imagePath)
    {
        //DisplayNode parent = HZUISystem.Instance.GetUIAlertLayer();
        //Vector2 beginPosition = new Vector2(parent.Width * 0.5f, parent.Height * 0.5f - 100);
        HZImageBox image = new HZImageBox();
        UILayout layout = HZUISystem.CreateLayoutFromFile(imagePath + imageName + ".png", UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
        if (layout != null)
        {
            image.Layout = layout;
        }
        ShowFloatingTips(image, 180, null, Vector2.zero);
    }

    public void ClearAllFloatingTips()
    {
        if (mFloatingList != null)
        {
            foreach (FloatingHelper fh in mFloatingList)
            {
                fh.Dispose();
            }

            mFloatingList.Clear();
        }

        if (nodelist != null)
        {
            nodelist.Clear();
        }
        if (mWaitFloatingList != null)
        {
            mWaitFloatingList.Clear();
        }
    }

    internal class FloatingHelper
    {
        public int Index
        {
            set
            {
                mIndex = value;
                mDelta = value * 2;
            }
        }

        public int mIndex;
        private int cacheindex;
        private int mDelta;
        private bool mUpdata1finish = false;
        public float mPause = 0;
        private float mSpeed = 280f;
        private float minSpeed = 80f;
        private bool mStart = false;
        private float mDistance = 0;
        private bool mIsFinish = false;
        public NodeInfo mNodeinfo = null;
        public NodeCache nodecache;
        public DisplayNode mNode = null;
        private DisplayNode mParent = null;
        public Vector2 mBeginPosition = Vector2.zero;

        public float MinSpeed
        {
            get
            {
                return minSpeed;
            }

            set
            {
                minSpeed = value;
            }
        }

        internal FloatingHelper(DisplayNode node, float distance, DisplayNode parent, Vector2 beginPosition, float pause = 0)
        {
            mNode = node;
            mParent = parent;
            mDistance = distance;
            mBeginPosition = beginPosition;
            
            mPause = pause;
        }

        public void Start()
        {
            //what???bug !AddChild will reset y position 
            if (mParent.IsDispose)
            {
                mIsFinish = true;
            }
            else
            {
                mParent.AddChild(mNode);
            }

            mNode.X = mBeginPosition.x;
            mNode.Y = mBeginPosition.y;
            mNode.Alpha = 0;
            mStart = true;
        }


        public virtual void Update()
        {
            
            
            if (mDelta > 0)
            {
                mDelta--;
                return;
            }
            if (!mStart)
            {
                if (mNode == null)
                {
                    if (nodecaches.Count>0)
                    {
                        nodelist[0].framenode = nodecaches[0].frame;
                        nodelist[0].textnode = nodecaches[0].textnode;
                        nodelist[0].InitNode();
                        mNode = nodelist[0].framenode;
                        nodecache = nodecaches[0];
                        nodecaches.RemoveAt(0);
                        mNodeinfo = nodelist[0];
                        nodelist.RemoveAt(0);
                        mNode.Visible = true;
                        if (mBeginPosition == Vector2.zero)
                        {
                            mBeginPosition = new Vector2(-mNode.Width * 0.5f, -180);
                        }
                    }
                }
                mBeginPosition.y += (mNode.Size2D[1] - 5) * mIndex; 
                Start();
            }

            if (mUpdata1finish)
            {
                if (mPause > 0)
                {
                    mPause -= 1;
                    return;
                }

                float speed = mSpeed * (1 - (mBeginPosition.y - mNode.Y) / mDistance);
                if (speed < MinSpeed)
                {
                    speed = MinSpeed;
                }

                mNode.Y -= Time.deltaTime * speed;

                if (mNode.Y < mBeginPosition.y - mDistance)
                {
                    UpdateAlpha2();
                }
            }
            else
            {
                UpdateAlpha1();
            }
        }

        private void UpdateAlpha1()
        {
            mNode.Y -= Time.deltaTime * 80;
            mNode.Alpha += Time.deltaTime * 4;
            if (mNode.Alpha >= 1)
            {
                mUpdata1finish = true;
            }
        }

        private void UpdateAlpha2()
        {
            mNode.Alpha -= Time.deltaTime * 1.5f;
            if (mNode.Alpha <= 0)
            {
                mIsFinish = true;
                Dispose();
                mFloatingList.Remove(this);
                
                
                if (mFloatingList.Count < 1)
                {
                    FloatingTipsU.index = 0;
                    if (mWaitFloatingList.Count < 1)
                    {
                        nodelist.Clear();
                    }
                }
            }
        }

        public void Dispose()
        {
            if (mNodeinfo == null)
            {
                mNode = null;
            }
            if (nodecache.frame != null)
            {
                mNode.Visible = false;
                nodecache.frame.Alpha = 0;
                nodecaches.Add(nodecache);
            }
            mParent = null;
            mBeginPosition = Vector2.zero;
        }

    }

    #endregion
}
