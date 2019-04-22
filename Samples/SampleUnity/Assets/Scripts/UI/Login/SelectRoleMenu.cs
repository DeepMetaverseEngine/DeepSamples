using UnityEngine;
using System.Collections.Generic;
using DeepMMO.Data;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUIEditor;
using TLProtocol.Data;

public class SelectRoleMenu : MenuBase
{

    public delegate void OnGoBackHandler();
    public OnGoBackHandler OnGoBack;

    public delegate void OnEnterScene(string playerId, int sceneId);
    public OnEnterScene EnterScene;

    private LoginAnimeScene mLoginAnimeScene;

    private List<RoleSnap> mPlayerList;
    private int mSelectIndex;

    public static SelectRoleMenu Create(LoginAnimeScene loginAnimeScene, List<RoleSnap> playerList)
    {
        SelectRoleMenu ret = new SelectRoleMenu(loginAnimeScene, playerList);
        if(ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }

    public SelectRoleMenu(LoginAnimeScene loginAnimeScene, List<RoleSnap> playerList)
    {
        mLoginAnimeScene = loginAnimeScene;
        this.mPlayerList = playerList;
    }

    protected override bool OnInit()
    {
        if(mPlayerList == null)
            return false;

        InitWithXml("xml/login/login_chooserole.gui.xml");

        //compmont
        InitCompment();

        return true;
    }

    protected override void OnEnter()
    {
        string lastId = PlayerPrefs.GetString("lastRole");
        if (!string.IsNullOrEmpty(lastId))
        {
            mSelectIndex = 0;
            for (int i = 0; i < mPlayerList.Count; ++i)
            {
                if (mPlayerList[i].uuid == lastId)
                {
                    mSelectIndex = i;
                    break;
                }
            }
            ChangeRole(mPlayerList[mSelectIndex]);
        }
        else
            ChangeRole(mPlayerList[0]);

        InitRoleList();

        MenuMgr.Instance.MenuRemoveEvent += OnMenuRemove;
    }

    protected override void OnExit()
    {
        MenuMgr.Instance.MenuRemoveEvent -= OnMenuRemove;
    }

    private void OnMenuRemove(MenuBase menu)
    {
        if (menu.MenuTag == "80") //创角.
        {
            HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_see");
            if (scrollPan != null)
            {
                scrollPan.Scrollable.LookAt(Vector2.zero);
                mSelectIndex = 0;
                ChangeRole(mPlayerList[0]);
            }
        }
    }

    private void InitCompment()
    {
        this.SetCompAnime(this, UIAnimeType.NoAnime);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_top"), HudManager.HUD_TOP);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_bottom"), HudManager.HUD_BOTTOM);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_left"), HudManager.HUD_LEFT);
        HudManager.Instance.InitAnchorWithNode(mRoot.FindChildByEditName<HZCanvas>("cvs_right"), HudManager.HUD_RIGHT);

        //close button
        HZTextButton closeBtn = mRoot.FindChildByEditName<HZTextButton>("bt_back");
        if (closeBtn != null)
        {
            closeBtn.TouchClick = (sender) =>
            {
                DoBack();
            };
        }

        //enter game button
        HZTextButton enterGame = mRoot.FindChildByEditName<HZTextButton>("btn_start");
        if(enterGame != null)
        {
            enterGame.TouchClick = (data) =>
            {
                //mLoginAnimeScene.PlayRoleAnime(LoginAnimeScene.RoleAnimeTag.Out, ()=>
                //{
                if (mSelectIndex >= 0 && mSelectIndex < mPlayerList.Count )
                {
                    DoEnterScene(mPlayerList[mSelectIndex].uuid, 0);
                }
                //});
            };
        }

        //delete role button
        HZTextButton deleteRole = mRoot.FindChildByEditName<HZTextButton>("btn_delete");
        if (deleteRole != null)
        {
        }

        SetEnableUENode(mRoot, true, true);
        mRoot.event_PointerMove = (sender, e) =>
        {
            mLoginAnimeScene.ModelRotate(-e.delta.x);
        };
    }

    private void InitRoleList()
    {
        HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_see");
        if (scrollPan == null)
            return;

        HZCanvas cell = mRoot.FindChildByEditName<HZCanvas>("cvs_role");
        if (cell == null)
            return;

        cell.Visible = false;
        int listLen = mPlayerList.Count > scrollPan.UserTag ? mPlayerList.Count : scrollPan.UserTag;    //userTag表示可创建角色的数量，由策划控制.
        scrollPan.Initialize(cell.Width, cell.Height, listLen, 1, cell, OnScrollPanUpdate);
    }

    public void OnScrollPanUpdate(int gx, int gy, DisplayNode obj)
    {
        HZCanvas cell = obj as HZCanvas;
        if (gy < mPlayerList.Count)
        {
            SetVisibleUENode(cell, "cvs_choose", true);
            SetVisibleUENode(cell, "cvs_create", false);
            TLRoleSnap data = (TLRoleSnap)mPlayerList[gy];
            SetLabelText(cell, "lb_name", data.name, 0, mSelectIndex == gy ? 0xd78b1eff : 0x3e3e3eff);
            SetLabelText(cell, "lb_lv", data.level.ToString());
            SetVisibleUENode(cell, "lb_lvdi2", mSelectIndex == gy);
            UILayout layout = HZUISystem.CreateLayout(string.Format("static/target/{0}_{1}.png", data.UnitPro, data.Gender), UILayoutStyle.IMAGE_STYLE_BACK_4, 8);
            SetImageBox(cell, "ib_jobhead", layout);
        }
        else
        {
            SetVisibleUENode(cell, "cvs_choose", false);
            SetVisibleUENode(cell, "cvs_create", true);
        }
        HZToggleButton tbt = cell.FindChildByEditName<HZToggleButton>("btn_role");
        if (tbt != null)
        {
            tbt.SetBtnLockState(HZToggleButton.LockState.eLockSelect);
            tbt.IsChecked = gy < mPlayerList.Count && gy == mSelectIndex ? true : false;
            tbt.TouchClick = (sender) =>
            {
                if (tbt.IsLongClick)
                {
                    return;
                }
                mSelectIndex = gy;

                if (gy < mPlayerList.Count)
                {
                    ChangeRole(mPlayerList[gy]);
                }
                else
                {
                    HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_see");
                    scrollPan.RefreshShowCell();

                    //create role
                    CreateRoleMenu createRole = CreateRoleMenu.Create(mLoginAnimeScene);
                    createRole.EnterScene = DoEnterScene;
                    createRole.OnGoBack = () =>
                    {
                        
                    };
                    MenuMgr.Instance.AddMenu(createRole, UIShowType.HideBackMenu);
                }
            };
            //tbt.event_LongPoniterDown = (sender, e) =>
            //{
            //    mSelectIndex = gy;
            //    if (mSelectIndex >= 0 && mSelectIndex < mPlayerList.Count)
            //    {
            //        DoEnterScene(mPlayerList[mSelectIndex].id, mPlayerList[mSelectIndex].sceneId);
            //    }
            //};
            obj.X = tbt.IsChecked ? 32 : 0;
        }
    }

    private void ChangeRole(RoleSnap info)
    {
        HZScrollPan scrollPan = mRoot.FindChildByEditName<HZScrollPan>("sp_see");
        scrollPan.RefreshShowCell();
        this.Visible = false;
        mLoginAnimeScene.SwitchAvatar(info.name, ((TLRoleSnap)info).AvatarInfo, ((TLRoleSnap)info).UnitPro, ()=>
        {
            this.Visible = true;
        });
    }

    private void DoEnterScene(string playerId, int sceneId)
    {
        MenuMgr.Instance.MenuRemoveEvent -= OnMenuRemove;
        if (EnterScene != null)
        {
            EnterScene(playerId, sceneId);
        }
    }

    public void DoBack()
    {
        if (OnGoBack != null)
        {
            OnGoBack();
            OnGoBack = null;
        }
    }

    protected override string UITag() { return "0"; }

}
