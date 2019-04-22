using System.Collections.Generic;
using DeepCore.Unity3D.UGUIEditor.UI;


public class LoginNoticeMenu:MenuBase
{
    public struct NoticeType
    {
        public string title;
        public string content;

        public NoticeType(string title,string content)
        {
            if (title != null)
                this.title = title;
            else
                this.title = "";
            if (content != null)
                this.content = content;
            else
                this.content = "";
        }
    }

    public static List<NoticeType> notices;
    public static bool autoShowNotice = false;
    public delegate void OnCloseNotice(bool isAutoLogin);
    public OnCloseNotice CloseUI;
    
    public static LoginNoticeMenu Create()
    {
        LoginNoticeMenu ret = new LoginNoticeMenu();
        if (ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }
    
    public LoginNoticeMenu() : base("LoginNoticeMenu")
    {

    }

    protected override bool OnInit()
    {
        SetCompAnime(this, UIAnimeType.NoAnime);
        InitWithXml("xml/login/login_notice.gui.xml");
        InitNoticeCompment();
        return true;
    }
    
    private void InitNoticeCompment()
    {
        int ischecked = 0;
        HZCanvas cvs_notice = mRoot.FindChildByEditName<HZCanvas>("cvs_notice");
        HZToggleButton tbn_title = cvs_notice.FindChildByEditName<HZToggleButton>("tbt_title");
        HZTextBox tb_text = cvs_notice.FindChildByEditName<HZTextBox>("tb_text");
        HZTextButton btn_close = cvs_notice.FindChildByEditName<HZTextButton>("btn_close");
        HZScrollPan sp_title = cvs_notice.FindChildByEditName<HZScrollPan>("sp_title");

        if (btn_close != null)
            btn_close.TouchClick = (sender) =>
            {
                Close();
            };
        tbn_title.Visible = false;
        tb_text.Scrollable = true;
        sp_title.Initialize(tbn_title.Width, tbn_title.Height,notices.Count, 1, tbn_title, (gx, index, obj) =>
        {
            HZToggleButton node = obj as HZToggleButton;
            node.IsChecked = ischecked == index;
            node.Text = notices[index].title;
            tb_text.UnityRichText = notices[0].content;
            node.TouchClick = (sender) =>
            {
                ischecked = index;
                sp_title.RefreshShowCell();
                tb_text.UnityRichText = notices[index].content;
                (tb_text.TextComponent as DeepCore.Unity3D.UGUI.RichTextBox).Container.Y = 0;
            };
        });
    }

    protected override void OnExit()
    {
        if (CloseUI != null)
        {
            CloseUI(true);
        }
    }
}