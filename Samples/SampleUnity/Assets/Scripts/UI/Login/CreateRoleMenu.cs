
using DeepMMO.Data;
using DeepCore.Unity3D.UGUIEditor.UI;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.GUI.Data;
using TLProtocol.Data;
using DeepCore.Unity3D;
using System;

public class CreateRoleMenu : MenuBase {

    public delegate void OnGoBackHandler();
    public OnGoBackHandler OnGoBack;

    public delegate void OnEnterScene(string playerId, int sceneId);
    public OnEnterScene EnterScene;

    private LoginAnimeScene mLoginAnimeScene;

    private int mDefaultPro;
    private int mCurSelPro;
    private int mCurSelGen;

    private HZToggleButton mCurProBtn;
    private HZToggleButton mCurGenBtn;
    private bool mAnimePlayering;

    private RoleSnap mCreatedRoleInfo;

    public const float SWIPE_SPEED = 0.5f;

    public static CreateRoleMenu Create(LoginAnimeScene loginAnimeScene)
    {
        CreateRoleMenu ret = new CreateRoleMenu(loginAnimeScene);
        if (ret != null && ret.OnInit())
        {
            return ret;
        }
        return null;
    }

    public CreateRoleMenu(LoginAnimeScene loginAnimeScene)
    {
        mLoginAnimeScene = loginAnimeScene;
        mCurSelPro = 0;
        mCurSelGen = 0;
    }

    protected override bool OnInit()
    {
        if (!InitWithXml("xml/login/login_create.gui.xml"))
            return false;

        //compmont
        InitCompment();

        return true;
    }

    protected override void OnEnter()
    {
        int rdm = UnityEngine.Random.Range(0, 3);
        int[] a = {2, 3 ,4 };
        mDefaultPro = a[rdm];
        int rdm2 = UnityEngine.Random.Range(0, 2);
        int[] a2 = { 0,1 };
        mCurSelGen = a2[rdm2];
        //初始选项
        ChangePro(mDefaultPro); //性别控件的位置依赖职业控件，所以要先初始化职业
        ChangeGender(mCurSelGen, false);

        DoRandomName(null);
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
                if (OnGoBack != null)
                {
                    OnGoBack();
                }
                CloseAndDestroy();
            };
        }

        HZTextButton rndNameBtn = mRoot.FindChildByEditName<HZTextButton>("bt_dice");
        if (rndNameBtn != null)
        {
            rndNameBtn.TouchClick = DoRandomName;
        }

        HZTextInput nameInput = mRoot.FindChildByEditName<HZTextInput>("ti_nameinput");
        if (nameInput != null)
        {
            //nameInput.Input.characterLimit = PublicConst.RoleNameLength;
            nameInput.event_endEdit = (sender, text) =>
            {
                if (text.Length > PublicConst.RoleNameLength)
                {
                    nameInput.Input.Text = text.Substring(0, PublicConst.RoleNameLength);
                }
            };
        }

        HZTextButton goBtn = mRoot.FindChildByEditName<HZTextButton>("btn_go");
        if (goBtn != null)
        {
            goBtn.TouchClick = (sender) =>
            {
                if (nameInput != null)
                {
                    if (string.IsNullOrEmpty(nameInput.Text))
                    {
                        GameAlertManager.Instance.ShowNotify("please input name!");
                        return;
                    }
                    else if (nameInput.Text.Length > PublicConst.RoleNameLength)
                    {
                        GameAlertManager.Instance.ShowNotify("the length of name is too long!");
                        return;
                    }
                }
                SetEnableUENode(this, false, false);
                mAnimePlayering = true;
                //mLoginAnimeScene.PlayRoleAnime(LoginAnimeScene.RoleAnimeTag.Out, ()=>
                //{
                    SetEnableUENode(this, false, true);
                    mAnimePlayering = false;
                //});
                DoCreateRole();
            };
        }

        //pro
        for (int i = 2; i <= 5; ++i)
        {
            HZToggleButton tbt = mRoot.FindChildByEditName<HZToggleButton>("btn_job" + i);
            if (tbt != null)
            {
                tbt.SetBtnLockState(HZToggleButton.LockState.eLockSelect);
                tbt.TouchClick = (sender) =>
                {
                    ChangePro(tbt.UserTag);
                    DoRandomName(null);
                };
            }
        }

        //gen
        HZToggleButton tbt_male = mRoot.FindChildByEditName<HZToggleButton>("tbt_male");
        HZToggleButton tbt_female = mRoot.FindChildByEditName<HZToggleButton>("tbt_female");
        tbt_male.TouchClick = (sender) =>
        {
            ChangeGender((int)TLClientCreateRoleExtData.GenderType.Man);
            DoRandomName(null);
        };
        tbt_female.TouchClick = (sender) =>
        {
            ChangeGender((int)TLClientCreateRoleExtData.GenderType.Woman);
            DoRandomName(null);
        };

        SetEnableUENode(mRoot, true, true);
        mRoot.event_PointerMove = (sender, e) =>
        {
            mLoginAnimeScene.ModelRotate(-e.delta.x);
        };
    }

    private void DoRandomName(DisplayNode sender)
    {
        DataMgr.Instance.LoginData.RequestRandomName(mCurSelPro,(byte)mCurSelGen,(result) =>
        {
            HZTextInput nameInput = mRoot.FindChildByEditName<HZTextInput>("ti_nameinput");
            if (nameInput != null)
            {
                nameInput.Input.Text = result.s2c_name;
            }
        });
    }

    private void DoCreateRole()
    {
        HZTextInput nameInput = mRoot.FindChildByEditName<HZTextInput>("ti_nameinput");
        if (nameInput == null)
            return;
        
        DataMgr.Instance.LoginData.RequestCreateRole(mCurSelPro, mCurSelGen, nameInput.Input.Text, (result) =>
        {
            mCreatedRoleInfo = result.s2c_role;
            int create_time = (int)result.s2c_role.create_time.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var localUserData = OneGameSDK.Instance.GetUserData();
            localUserData.SetData(SDKAttName.DATE_TYPE, RoleDateType.createRole);
            localUserData.SetData(SDKAttName.ROLE_ID, result.s2c_role.uuid);
            localUserData.SetData(SDKAttName.ROLE_NAME, result.s2c_role.name);
            localUserData.SetData(SDKAttName.ROLE_LEVEL, result.s2c_role.level);
            localUserData.SetData(SDKAttName.ROLE_CREATE_TIME, create_time);
            localUserData.SetData(SDKAttName.ZONE_ID, DataMgr.Instance.AccountData.CurSrvInfo.id);
            localUserData.SetData(SDKAttName.ZONE_NAME, DataMgr.Instance.AccountData.CurSrvInfo.view_realm_name);
            localUserData.SetData(SDKAttName.SERVER_ID, DataMgr.Instance.AccountData.CurSrvInfo.id);
            localUserData.SetData(SDKAttName.ZONE_NAME, DataMgr.Instance.AccountData.CurSrvInfo.name);
            OneGameSDK.Instance.UpdatePlayerInfo();

            //自定义事件
            var eEvent = new SDKBaseData();
            eEvent.SetData(SDKAttName.CUSTOM_EVENT_NAME, SDKAttName.CUSTOM_EVENT_CHARACTER_NAME);
            OneGameSDK.Instance.DoAnyFunction(SDKAttName.CUSTOM_EVENT, eEvent);
        });
    }

    private void ChangePro(int pro)
    {
        //tbt
        int ITEM_PADDING = 0;// 40;
        if (mCurProBtn != null)
        {
            mCurProBtn.IsChecked = false;
            mCurProBtn.Parent.X -= ITEM_PADDING;
            //SetVisibleUENode("ib_job" + mCurSelPro, false);
        }
        mCurProBtn = mRoot.FindChildByEditName<HZToggleButton>("btn_job" + pro);
        mCurProBtn.IsChecked = true;
        mCurProBtn.Parent.X += ITEM_PADDING;
        //SetVisibleUENode("ib_job" + pro, true);
        UILayout layout = HZUISystem.CreateLayoutFromAtlasKey("$dynamic/TL_login/output/TL_login.xml|TL_login|protx_" + pro, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
        SetImageBox("ib_job", layout);

        UILayout layout1 = HZUISystem.CreateLayoutFromAtlasKey("$dynamic/TL_login/output/TL_login.xml|TL_login|prold_" + pro, UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, 8);
        SetImageBox("ib_job1", layout1);

        //name
        //SetLabelText("lb_job", PublicConst.GetProName(pro));

        //desc
        //HZTextBox proDesc = mRoot.FindChildByEditName<HZTextBox>("tb_describe");
        //proDesc.Text = ConfigMgr.Instance.TxtCfg.GetTextByKey(TextConfig.Type.LOGIN, "proDesc" + pro);

        //sex
        HZCanvas cvs_sex = mRoot.FindChildByEditName<HZCanvas>("cvs_sex");
        cvs_sex.Y = mCurProBtn.Parent.Y;

        mCurSelPro = pro;
        
        this.Visible = false;
        mLoginAnimeScene.SwitchRoleWithPro(pro, mCurSelGen, () =>
        {
            this.Visible = true;
        });
    }

    private void ChangeGender(int gender, bool changeModel = true)
    {
        mCurSelGen = gender;
        HZCanvas cvs_sex = mRoot.FindChildByEditName<HZCanvas>("cvs_sex");
        cvs_sex.Y = mCurProBtn.Parent.Y;
        if(mCurGenBtn != null)
        {
            mCurGenBtn.IsChecked = false;
        }
        mCurGenBtn = mRoot.FindChildByEditName<HZToggleButton>(gender == (int)TLClientCreateRoleExtData.GenderType.Man ? "tbt_male" : "tbt_female");
        mCurGenBtn.IsChecked = true;
        if(changeModel)
            mLoginAnimeScene.SwitchRoleWithGen(mCurSelPro, gender);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (mCreatedRoleInfo != null && !mAnimePlayering)
        {
            if (EnterScene != null)
            {
                EnterScene(mCreatedRoleInfo.uuid, 0);
            }
            mCreatedRoleInfo = null;
        }
    }

    protected override string UITag() { return "80"; }
	
}
