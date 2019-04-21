using DeepCore.GUI.Data;
using DeepCore.GUI.Display.Text;
using DeepCore.Unity3D.Impl;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Xml;
using UnityEngine;


public class UGUI_Canvas : MonoBehaviour
{
    private MyUIEditor editor;
    private DisplayNode root;
    private DisplayNode dp;
    private UIComponent test_ui;
    private GameObject test_obj;
    private long load_time;
    private string jdata_text;

    void Start()
    {
        UnityDriver.SetDirver();
        UIEditor.GlobalUseBitmapText = false;
        UnityDriver.UnityInstance.MAX_WORDS_CHAR_COUNT = 0;
        if (false)
        {
            var mpq = new DeepCore.MPQ.MPQFileSystem();
            var dir = new System.IO.DirectoryInfo(Application.dataPath + @"\..\HTTP\mpq");
            mpq.init(dir);
            UnityDriver.AddFileSystem(mpq);
            TextGraphics.DefaultUnderlineType = TextGraphics.UnderlineStyle.Fill;
            TextGraphics.DefaultUnderlineChar = "＿";
            this.editor = new MyUIEditor("/ui_edit/");
        }
        else
        {
            //this.editor = new MyUIEditor(@"file://H:\projects\morefuntek\Zeus\GameEditors\Http\res\ui_edit\");
            //this.editor = new MyUIEditor(@"file://H:\projects\morefuntek\Zeus\GameEditors\UIEdit\res\ui_edit\");
            this.editor = new MyUIEditor(Application.streamingAssetsPath + "/ui_edit/");
        }


        this.root = create_root();

        if (true)
        {
            reload();
        }
        if (false)
        {
            byte[] jdata = DeepCore.IO.Resource.LoadData(Application.streamingAssetsPath + "/image/java.png");
            byte[] jtext = DeepCore.IO.Resource.LoadData(Application.streamingAssetsPath + "/txt/testjava.txt");
            jdata_text = jdata.Length + " + " + jtext.Length;
        }
        if (false)
        {
            var ui = editor.CreateFromFile("liaotian/ui_liaotian.gui.xml");
            root.AddChild(ui);
        }
        if (false)
        {
            var meta = editor.GetSpriteMeta(@"actor_001010\output\actor.xml", "001010");
            CPJSprite spr = new CPJSprite(meta);
            spr.Controller.SetCurrentAnimate(1);
            spr.Position2D = new Vector2(200, 200);
            root.AddChild(spr);
        }
        if (true)
        {
            var tb = new RichTextBox("rich text");
            var ta = Resources.Load<TextAsset>("Res/RichText3334");

            var atext = new AttributedString();
            {
//                 atext.Append("余华", new TextAttribute(0xffff00ff, 32));
//                 atext.Append("12345", new TextAttribute(0xff0000ff, 32));
//                 atext.Append("余华", new TextAttribute(0xff00ffff, 32));
//                atext.Append(editor.DecodeAttributedString(string.Format("<recipe><font size = '20'><h img='#liaotian/output/liaotian.xml,liaotian,{0}'>TTT</h></font></recipe>", 3)));
//                atext.Append("\n");
//                for (int i = 0; i <= 16; i++)
//                {
//                    atext.Append(editor.DecodeAttributedString(string.Format("<recipe><font size = '20'><h img='#liaotian/output/liaotian.xml,liaotian,{0}'>T</h></font></recipe>", i)));
//                }
//                atext.Append(editor.DecodeAttributedString(string.Format("<recipe><font size = '20'><h img='#liaotian/output/liaotian.xml,liaotian,{0}'>TTT</h></font></recipe>", 3)));
//                 atext.Append("\n");
                 atext.Append(editor.DecodeAttributedString(XmlUtil.FromString(ta.text)));
//                 atext.Append("_ __", new TextAttribute(0xffff00ff, 32));
            }
            tb.Bounds2D = new Rect(10, 10, 800, 500);
            tb.AText = (atext);
            tb.Enable = true;
            tb.IsInteractive = true;
            //tb.Anchor = CommonUI.Data.TextAnchor.C_C;
            tb.PointerClick += new DisplayNode.PointerEventHandler((sender, e) =>
            {
                Vector2 p = sender.ScreenToLocalPoint2D(e);
                //Debug.Log("Pos:" + p);
                RichTextClickInfo click;
                if (tb.TestClick(p, out click))
                {
                    Debug.Log("Click:" + click.mRegion.Index + " " + click.mRegion.Text);
                }
            });
            root.AddChild(tb);
        }
        if (false)
        {
            var ts = new TintSprite("color");
            ts.Color = new Color(0, 0, 0, 0.5f);
            ts.Size2D = root.Size2D;
            root.AddChild(ts);
        }
    }

    void Update()
    {
        //var dnb = this.GetComponent<DisplayNodeBehaviour>();

        var com = this.GetComponent<Transform>();

        //         if (Input.GetMouseButtonDown(0))
        //         {
        //             if (EventSystem.current.IsPointerOverGameObject())
        //             {
        //                 Debug.Log("触摸在UI上");
        //                 return;
        //             }
        //             else
        //             {
        //                 Debug.Log("没有触摸在UI上");
        //             }
        //         }
    }


    void OnGUI()
    {
     var sx = new DeepCore.Concurrent.AtomicFloat(1);
        GUI.Label(new Rect(10, sx.GetAndAdd(20), Screen.width - 40, 20), "runtime_root = " + this.editor.Root);
        GUI.Label(new Rect(10, sx.GetAndAdd(20), Screen.width - 40, 20), "display node ref count = " + DisplayNode.RefCount);
        GUI.Label(new Rect(10, sx.GetAndAdd(20), Screen.width - 40, 20), "load time = " + load_time + "ms");
        GUI.Label(new Rect(10, sx.GetAndAdd(20), Screen.width - 40, 20), "a⚽😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄😄b");
        if (GUI.Button(new Rect(10, sx.GetAndAdd(40), 100, 40), "reload"))
        {
            reload();
        }
        if (GUI.Button(new Rect(10, sx.GetAndAdd(40), 100, 40), "gc"))
        {
            System.GC.Collect();
        }
        if (test_ui != null)
        {
            if (GUI.Button(new Rect(10, sx.GetAndAdd(40), 100, 40), test_ui.UnityObject.activeSelf ? "hide" : "show"))
            {
                test_ui.UnityObject.SetActive(!test_ui.UnityObject.activeSelf);
            }
            if (GUI.Button(new Rect(10, sx.GetAndAdd(40), 100, 40), dp == null ? "add dp" : "remove dp"))
            {
                add_dp();
            }
            if (test_ui.Name == "test_gray" && GUI.Button(new Rect(10, sx.GetAndAdd(40), 100, 40), "test gray"))
            {
                this.test_ui.Layout = UILayout.CreateUILayoutImage(
                       UILayoutStyle.IMAGE_STYLE_BACK_4,
                       editor.GetImage("liaotian/output/dongzuo.etc.m3z"),
                       0);
            }
        }
    }

    private DisplayNode create_root()
    {
        var ueroot = new DisplayRoot(GetComponent<UnityEngine.Canvas>(), "ueroot");
        //ueroot.IsGray = true;
        //ueroot.Alpha = 0.2f;
        ueroot.UnityObject.transform.SetAsFirstSibling();
        //         var root = new DisplayCanvas("root_canvas");
        //         root.Size2D = ueroot.Size2D;
        //         ueroot.AddChild(root);
        //         return root;
        return ueroot;
    }

    private void add_dp()
    {
        if (dp == null)
        {
            dp = new DisplayNode("dp------------------------------------------");
            test_ui.AddChild(dp);
        }
        else
        {
            test_ui.UnityObject.SetActive(false);
            dp.RemoveFromParent(true);
            dp = null;
        }
    }

    private void reload()
    {
        if (test_ui != null) { test_ui.Dispose(); test_ui = null; }
        if (test_obj != null) { GameObject.Destroy(test_obj); test_obj = null; }
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            {
                //this.test_ui = editor.CreateFromFile("gridcell.gui.xml");
                //this.test_ui = editor.CreateFromFile("test.gui.xml");
                this.test_ui = editor.CreateFromFile("paged.gui.xml");

                // test gray
                if (false)
                {
                    this.test_ui = new UIComponent("test_gray");
                    this.test_ui.Size2D = new Vector2(300, 200);
                    this.test_ui.IsGray = true;
                    this.test_ui.Layout = UILayout.CreateUILayoutImage(
                        UILayoutStyle.IMAGE_STYLE_BACK_4,
                        editor.GetImage("dynamic/loading_bg.etc.m3z"),
                        0);
                }
            }
            sw.Stop();
            this.load_time = sw.ElapsedMilliseconds;
        }
        if (test_ui != null)
        {
            this.root.AddChildAt(this.test_ui, 0);

            if (test_ui.Name == "paged.gui.xml")
            {
                var panel = test_ui.FindChildByEditName<UECanvas>("panel");
                if (panel != null)
                {
                    if (false)
                    {
                        var atlas = editor.GetAtlas(@"actor_001010\output\actor.xml", "actor_001010");
                        var quads = new BatchQuadsSprite("quads");
                        quads.Position2D = new Vector2(100, 100);
                        quads.Atlas = atlas;
                        quads.AddQuad(1, ImageAnchor.C_C, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(-1, 1), 0);
                        quads.AddQuadImageFont("123456", ImageAnchor.C_C, new Vector2(0.5f, 0.5f), Vector2.zero, new Vector2(1.5f, 2f), 30);
                        //quads.AddQuad(1, Vector2.zero, new Vector2(-1, 1), 0);
                        //quads.AddQuad(123);
                        //quads.AddQuad(105);
                        panel.AddChild(quads);
                    }
                    else
                    {
                        var meta = editor.GetUIMeta("test.gui.xml");
                        var paged = new PagedScrollablePanel("paged_scrollable");
                        paged.Bounds2D = new Rect(0, 0, panel.Size2D.x, panel.Size2D.y);
                        paged.Initialize(5, new Vector2(meta.Width, meta.Height), (p, i) =>
                        {
                            var page = editor.CreateFromMeta(meta);
                            //                        var sub_canvas = new DisplayCanvas("sub_canvas");
                            //                         sub_canvas.Bounds2D = new Rect(0, 0, panel.Size2D.x, panel.Size2D.y);
                            //                         sub_canvas.AddChild(page);
                            //                         return sub_canvas;
                            return page;
                        });
                        panel.AddChild(paged);
                    }
                }
            }
            else if (test_ui.Name == "gridcell.gui.xml")
            {
                var panel = test_ui.FindChildByEditName<UECanvas>("panel");
                if (panel != null)
                {
                    var grid = new CachedGridScrollablePanel("grid_scrollable");
                    var meta = editor.GetUIMeta("gridcell.gui.xml").FindChildByName<UIComponentMeta>("bt");
                    grid.event_CreateCell = (p) =>
                    {
                        var btn = editor.CreateFromMeta(meta) as UETextButton;
                        btn.EditTextAnchor = DeepCore.GUI.Data.TextAnchor.L_C;
                        btn.TextOffset = new Vector2(40, 0);
                        return btn;
                    };
                    grid.event_ShowCell = (p, gx, gy, cell) =>
                    {
                        var btn = cell as UETextButton;
                        btn.Text = string.Format(" {0},{1} : {2}", gx, gy, Random.Range(0, 100));
                    };
                    grid.Bounds2D = new Rect(8, 8, panel.Size2D.x - 16, panel.Size2D.y - 16);
                    grid.Initialize(4, Random.Range(100, 1000), new Vector2(meta.Width, meta.Height));
                    grid.Scroll.horizontal = false;
                    panel.AddChild(grid);

                    var bttt = test_ui.FindChildByEditName<UETextButton>("bt");
                    if (bttt != null)
                    {
                        bttt.PointerClick += (sender, e) =>
                        {
                            grid.Reset(4, Random.Range(100, 1000));
                            grid.Scroll.horizontal = false;
                        };
                    }
                }
            }
        }
    }

}

public class MyUIEditor : UIEditor
{
    public MyUIEditor(string root)
        : base(root)
    {
        //         StringBuilder sb = new StringBuilder();
        //         foreach (var fontName in Font.GetOSInstalledFontNames())
        //         {
        //             sb.AppendLine(fontName);
        //         }
        //         System.IO.File.WriteAllText(Application.persistentDataPath + "/font_list.txt", sb.ToString());
        //         if (UnityDriver.IsAndroid)
        //         {
        //             this.DefaultFont = Font.CreateDynamicFontFromOSFont("Droid Serif", 16);
        //         }
        //         else if (UnityDriver.IsIOS)
        //         {
        //             this.DefaultFont = Font.CreateDynamicFontFromOSFont("Helvetica-Bold", 16);
        //         }
        //         else if (UnityDriver.IsWin32)
        //         {
        //             //this.DefaultFont = Font.CreateDynamicFontFromOSFont("Segoe UI Symbol", 16);
        //             this.DefaultFont = Resources.Load<Font>("Font/seguisym");
        //         }
        //         else
        //         {
        //             this.DefaultFont = Resources.Load<Font>("Font/DroidSansFallback");
        //             //this.DefaultFont = Resources.Load<Font>("Font/seguisym");
        //         }
        this.DefaultFont = Resources.Load<Font>("Font/FZLJ");
        this.DefaultFontBestFitMin = (int)(DefaultFont.fontSize * 0.75);
        this.DefaultFontBestFitMax = (int)(DefaultFont.fontSize * 1.25);
        UnityDriver.UnityInstance.RedirectImage = this.RedirectImage_ETC;
    }
    public override Font CreateFont(string fontName)
    {
        return DefaultFont;
    }
    protected override UIComponent CreateComponent(UIComponentMeta meta)
    {
        var ret = base.CreateComponent(meta);
        if (ret is UETextBoxBase)
        {
            (ret as UETextBoxBase).Scrollable = true;
        }
        return ret;
    }
    protected override UIComponent CreateFromMeta(UIComponentMeta meta, Decoder decoder)
    {
        var ret = base.CreateFromMeta(meta, decoder);
        if (ret is UETextBoxBase)
        {
            (ret as UETextBoxBase).TextComponent.Anchor = DeepCore.GUI.Data.TextAnchor.L_C;
        }
        return ret;
    }

}
