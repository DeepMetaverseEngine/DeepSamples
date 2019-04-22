using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUI;
using DeepMMO.Data;
using Assets.Scripts;

public class ServerListMenu : MenuBase {

    public delegate void OnEnterServer(ServerInfo server);
    public OnEnterServer EnterServer;

    private HttpRequest mHttpRequest;

    private ServerList mServerList;

    private List<ServerInfo> mCurrentZone;

    private int mSelectedZone = 0;
    private string mCurZoneName;

    public static ServerListMenu Create(ServerList serverList)
    {
        ServerListMenu ret = new ServerListMenu(serverList);
        if (ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }

    public ServerListMenu(ServerList serverList)
    {
        mServerList = serverList;
    }

    protected override bool OnInit()
    {
        InitWithXml("xml/login/login_chooseservers.gui.xml");

        //compmont
        InitCompment();

        return true;
    }

    private void InitCompment()
    {
        this.SetCompAnime(this, UIAnimeType.NoAnime);
        //close button
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("bt_return");
        if (closeBtn != null)
        {
            closeBtn.TouchClick = (sender) => {
                CloseAndDestroy();
            };
        }

        mCurrentZone = mServerList.RecomSrvList;

        SetVisibleUENode("cvs_jiaose", false);
        InitHttp();
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        InitZoneList();

        InitServerList();

        InitDefaultServer();

        mServerList.OnServerListRefresh += this.OnServerListRefresh;
    }

    protected override void OnExit()
    {
        base.OnExit();
        mServerList.OnServerListRefresh -= this.OnServerListRefresh;
    }

    private void InitHttp()
    {
        GameObject obj = GameObject.Find("LoginNode");
        mHttpRequest = obj.AddComponent<HttpRequest>();
    }

    private void InitDefaultServer()
    {
        //显示上次登录服.
        //功能需要修改，暂时注掉
        //InitServerInfo(mRoot.FindChildByEditName<HZCanvas>("cvs_frame2"), mServerList.GetLastLoginServer());

        //显示推荐服.
        //功能需要修改，暂时注掉
        //InitServerInfo(mRoot.FindChildByEditName<HZCanvas>("cvs_frame3"), mServerList.GetRecomServerByIndex(0));
        //InitServerInfo(mRoot.FindChildByEditName<HZCanvas>("cvs_frame4"), mServerList.GetRecomServerByIndex(1));
    }

    private void InitZoneList()
    {
        HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_qu");
        if (scrollPan == null)
            return;

        HZCanvas zoneItem = mRoot.FindChildByEditName<HZCanvas>("cvs_jiaose");
        if(zoneItem == null)
            return;

        zoneItem.Visible = false;
        scrollPan.Initialize(zoneItem.Width, zoneItem.Height, mServerList.ZoneIdList.Count + 1, 1, zoneItem, OnScrollPanUpdateZoneItem);
        scrollPan.Scrollable.event_Scrolled = (sender, e) =>
        {
            RefreshArrow(sender as ScrollablePanel, null, "ib_arrowq");
        };
        RefreshArrow(scrollPan.Scrollable, null, "ib_arrowq");
    }

    public void OnScrollPanUpdateZoneItem(int gx, int gy, DisplayNode obj)
    {
        if (gy >= mServerList.ZoneIdList.Count + 1)
            return;

        HZCanvas cell = obj as HZCanvas;
        string zoneName = "";
        if (gy == 0)
        {
            //SetVisibleUENode(cell, "ib_jiaose", true);
            //zoneName = HZLanguageManager.Instance.GetString("common_existing_role");
            zoneName = HZLanguageManager.Instance.GetString("common_recommend");
            SetLabelText(cell, "lb_qu", zoneName);
        }
        else
        {
            //SetVisibleUENode(cell, "ib_jiaose", false);
            zoneName = mServerList.GetZoneName(gy - 1);
            SetLabelText(cell, "lb_qu", zoneName);
        }
        if (mSelectedZone == gy)
            mCurZoneName = zoneName;

        HZToggleButton btn = cell.FindChildByEditName<HZToggleButton>("tbt_jiaose");
        btn.IsChecked = gy == mSelectedZone;
        btn.TouchClick = (sender) =>
        {
            mSelectedZone = gy;
            mCurZoneName = zoneName;

            HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_qu");
            scrollPan.RefreshShowCell();

            RefreshServerList();
        };
    }

    private void InitServerList()
    {
        HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_severs");
        if (scrollPan == null)
            return;

        HZCanvas serverItem = mRoot.FindChildByEditName<HZCanvas>("cvs_frame1");
        if (serverItem == null)
            return;

        serverItem.Visible = false;
        SetLabelText("lb_area", mCurZoneName);
        scrollPan.Initialize(serverItem.Width, serverItem.Height, (mCurrentZone.Count + 1) / 2, 2, serverItem, OnScrollPanUpdateServerItem);
        scrollPan.Scrollable.event_Scrolled = (sender, e) =>
        {
            RefreshArrow(sender as ScrollablePanel, null, "ib_arrows");
        };
        RefreshArrow(scrollPan.Scrollable, null, "ib_arrows");
    }

    public void OnScrollPanUpdateServerItem(int gx, int gy, DisplayNode obj)
    {
        int itemIndex = gy * 2 + gx;
        if (itemIndex >= mCurrentZone.Count)
        {
            obj.Visible = false;
            return;
        }
        obj.Visible = true;
        
        InitServerInfo(obj as HZCanvas, mCurrentZone[itemIndex]);
    }

    private void RefreshServerList()
    {
        HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_severs");
        if (scrollPan == null)
            return;

        if (mSelectedZone == 0)
            mCurrentZone = mServerList.RecomSrvList;
        else
            mCurrentZone = mServerList.GetServerListByZoneIndex(mSelectedZone - 1);

        SetLabelText("lb_area", mCurZoneName);
        //(scrollPan.Scrollable as CachedGridScrollablePanel).ClearGrid();
        scrollPan.ResetRowsAndColumns((mCurrentZone.Count + 1) / 2, 2);
    }

    private void InitServerInfo(HZCanvas frame, ServerInfo info)
    {
        if (frame == null)
            return;

        if (info == null)
        {
            frame.Visible = false;
            return;
        }

        frame.Visible = true;
        uint textRGBA = (uint)info.view_rgba;
        HZTextButton btn = frame.FindChildByEditName<HZTextButton>("bt_choosesever");
        btn.Text = info.name;
        btn.FontColor = GameUtil.RGBA2Color(textRGBA);
        btn.FocuseFontColor = GameUtil.RGBA2Color(textRGBA);
        btn.Tag = info;
        btn.TouchClick = DoEnterServer;
        //SetVisibleUENode(frame, "ib_recommend", info.ServerType==(byte)ServerInfo.SrvType.kSvrTypeRecommend);
        SetImageBox(frame, "ib_light", info.icon);
        //if (info.RoleNum > 0)
        //{
        //    SetVisibleUENode(frame, "ib_jiaose1", true);
        //    SetVisibleUENode(frame, "ib_jiaose2", true);

        //    SetVisibleUENode(frame, "lb_num", true);
        //    SetLabelText(frame, "lb_num", info.RoleNum.ToString());
        //}
        //else
        {
            SetVisibleUENode(frame, "ib_jiaose1", false);
            SetVisibleUENode(frame, "ib_jiaose2", false);

            SetVisibleUENode(frame, "lb_num", false);
        }
    }

    private void RefreshArrow(ScrollablePanel panel, string upName, string downName)
    {
        HZImageBox up = mRoot.FindChildByEditName<HZImageBox>(upName);
        if (up != null)
        {
            up.Visible = true;
            if (panel.Container.Y > -66) //顶端
            {
                up.Visible = false;
            }
        }
        HZImageBox down = mRoot.FindChildByEditName<HZImageBox>(downName);
        if (down != null)
        {
            down.Visible = true;
            if (panel.Container.Height + panel.Container.Y <= panel.ScrollRect2D.height + 66) //底部
            {
                down.Visible = false;
            }
        }
    }

    private void DoEnterServer(DisplayNode sender)
    {
        ServerInfo serverInfo = (sender as HZTextButton).Tag as ServerInfo;

        if(!serverInfo.is_open)
        {
            
            GameAlertManager.Instance.ShowAlertDialog(AlertDialog.PRIORITY_NORMAL, HZLanguageManager.Instance.GetString("common_server_closed"), "", null, null);
            return;
        }

        if (EnterServer != null)
        {
            EnterServer(serverInfo);
        }
    }

    private void OnServerListRefresh(ServerList serverList)
    {
        InitDefaultServer();
        RefreshServerList();
    }

    protected override void OnDestory()
    {
        if (mHttpRequest != null)
        {
            mHttpRequest.Destroy();
        }
        base.OnDestory();
    }

    protected override string UITag() { return "ServerListMenu"; }
	
}
