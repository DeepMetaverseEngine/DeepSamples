
using DeepCore;
using DeepCore.Unity3D.Impl;
using DeepCore.Unity3D.UGUIEditor;
using SLua;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuMgr : SubMenuUListener
{

	private MenuRoot mMenuRoot;
	private MenuBase mMsgBoxRoot;
    private MenuBase mHudMenuRoot;

    private readonly List<MenuBase> mChildList = new List<MenuBase>();
	private readonly List<MenuBase> mAllMenuList = new List<MenuBase>();

	private HashMap<string, List<MenuBase>> mCacheMenu = new HashMap<string, List<MenuBase>>();
	private HashMap<string, int> mCacheLevel = new HashMap<string, int>();
	private const int UICACHE_MAX = 30;

    private HashMap<int, MenuBase> mWaitMenu = new HashMap<int, MenuBase>();

    private bool mProcessing = false;
    private readonly Queue<MenuBase> mWaitQueue = new Queue<MenuBase>();

    public delegate void OnMenuAdd(MenuBase menu);
	public delegate void OnMenuRemove(MenuBase menu);
	public event OnMenuAdd MenuAddEvent;
	public event OnMenuRemove MenuRemoveEvent;

	public MenuBase LastAddMenu { get; private set; }

	public LuaFunction OnMenuListChange { get; set; }

	private int mHideSceneCount;
	private int mHideHudCount;

	private static MenuMgr mInstance = null;
	public static MenuMgr Instance
	{
		get
		{
			if (mInstance == null)
			{
				new MenuMgr();
			}
			return mInstance;
		}
	}

	public MenuMgr()
	{
		mInstance = this;
		mMenuRoot = new MenuRoot("MenuRoot", 1000);
		HZUISystem.Instance.UILayerAddChild(mMenuRoot);
		mMsgBoxRoot = new MenuRoot("MsgBoxRoot", 1500);
        HZUISystem.Instance.UILayerAddChild(mMsgBoxRoot);
        mMsgBoxRoot.Transform.localPosition = new Vector3(0, 0, -10000);
        UILayerMgr.SetLayerOrder(mMsgBoxRoot.UnityObject, 1500, true, (int)PublicConst.LayerSetting.UI);

        mHudMenuRoot = new MenuRoot("HudMenuRoot", 100);
        //HudManager.Instance.AddChild(mHudMenuRoot);
        HZUISystem.Instance.HUDLayerAddChild(mHudMenuRoot);
    }

	public void PrintRefCount()
	{
		GC.Collect();
		foreach (var item in mAllMenuList)
		{
			Debugger.Log("[BUG] " + item.ToString() + " RefCount: " + MenuBase.s_RefCount.ToString());
		}

		Debugger.Log("[BUG]cache " + mCacheMenu.Count.ToString());
	}

    /// <summary>
    /// 从Hud根节点添加一个菜单
    /// </summary>
    /// <param name="menu">菜单实例</param>
    public void AddHudMenu(MenuBase menu)
    {
        if ((menu.ShowType & UIShowType.HideBackHud) != 0)
        {
            menu.ShowType = UIShowType.Cover;
        }
        mHudMenuRoot.AddSubMenu(menu);
    }
	
	/// <summary>
	/// 从MsgBox根节点添加一个消息菜单
	/// </summary>
	/// <param name="menu">消息菜单实例</param>
	public void AddMsgBox(MenuBase menu)
	{
		mMsgBoxRoot.AddSubMenu(menu);
	}
	
	/// <summary>
	/// 从UI根节点添加一个菜单
	/// </summary>
	/// <param name="menu">菜单实例</param>
	public void AddMenu(MenuBase menu)
	{
		AddMenu(menu, menu.ShowType);
	}
	
	/// <summary>
	/// 从UI根节点添加一个菜单
	/// </summary>
	/// <param name="menu">菜单实例</param>
	/// <param name="showType">是否隐藏下层菜单或3d场景</param>
	public void AddMenu(MenuBase menu, int showType)
	{
		if (menu != null)
		{
            if (mProcessing)
            {
                menu.ShowType = showType;
                mWaitQueue.Enqueue(menu);
                return;
            }
            mProcessing = true;
            
            menu.ShowType = showType;

			mMenuRoot.AddSubMenu(menu);

            mProcessing = false;
            if (mWaitQueue.Count > 0)
            {
                MenuBase next = mWaitQueue.Dequeue();
                AddMenu(next, next.ShowType);
            }
        }
	}

	private bool mNeedNotifyMenuList = false;
	public void CheckNotifyMenuList()
	{
		if(mNeedNotifyMenuList)
		{
			mNeedNotifyMenuList = false;

			//if (OnMenuListChange != null)
			//{
			//	OnMenuListChange.Call(mAllMenuList.ToArray());
			//}

			//LuaScriptMgr.Instance.LuaGC();
			//GC.Collect();
		}
	}

    private void InitMenuLayer(MenuBase menu)
    {
        int order;
        float z;
        MenuBase rootMenu = menu.ParentMenu == mMenuRoot ? mMenuRoot : menu.ParentMenu == mMsgBoxRoot ? mMsgBoxRoot : menu.Parent == mHudMenuRoot ? mHudMenuRoot : null;
        if (rootMenu != null)   //一级菜单
        {
            List<MenuBase> childrens = rootMenu.Childrens;
            int selfIndex = childrens.IndexOf(menu);
            order = selfIndex > 0 ? childrens[selfIndex - 1].MenuOrder + UILayerMgr.MenuOrderSpace : rootMenu.MenuOrder;
            z = selfIndex > 0 ? childrens[selfIndex - 1].MenuZ + UILayerMgr.MenuZSpace : rootMenu.MenuZ;
        }
        else
        {
            order = menu.ParentMenu.MenuOrder + UILayerMgr.SubMenuOrderSpace;
            z = UILayerMgr.SubMenuZSpace;
        }
        menu.MenuOrder = order;
        menu.MenuZ = z;

        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("tag", menu.MenuTag);
        EventManager.Fire("Event.UI.ResetOrder", param);
    }

	public void OnSubMenuAdd(MenuBase menu)
	{
		if(menu != null)
		{
            InitMenuLayer(menu);

            if (mAllMenuList.Count > 0)
			{
				menu.LifeIndex = mAllMenuList[mAllMenuList.Count - 1].LifeIndex + 1;
			}
			else
			{
				menu.LifeIndex = 0;
			}

            mAllMenuList.Add(menu);
			if ((menu.ShowType & UIShowType.HideBackScene) != 0)	//打开首个菜单并且不是覆盖模式，就隐藏场景 
			{
			   mMenuRoot.ShowBackground(true);
			}

			if (menu.ParentMenu == mMenuRoot && (menu.ShowType & UIShowType.HideBackMenu) != 0)   //隐藏所有下层菜单.
			{
				List<MenuBase> backList = new List<MenuBase>();
				for (int i = mChildList.Count - 1; i >= 0; --i)
				{
					MenuBase backMenu = mChildList[i];
					if (backMenu.HideBack(true))
					{
						backList.Add(backMenu);
					}
				}
				menu.HideBackList = backList;
			}
			if ((menu.ShowType & UIShowType.HideBackScene) != 0) //隐藏场景元素.
			{
				HideScene(true);
			}
			if ((menu.ShowType & UIShowType.HideBackHud) != 0)
			{
				HideHud(true);
			}

            if(menu.ParentMenu == mMenuRoot)
                mChildList.Add(menu);

            mNeedNotifyMenuList = true;
			LastAddMenu = menu;
			if(MenuAddEvent != null)
			{
				MenuAddEvent(menu);
			}
			NotifyMenuObserver(menu, MenuState.Enter);

			if((menu.Enable || (menu.Root != null && menu.Root.Enable)) && HudManager.Instance.Rocker != null)
				HudManager.Instance.Rocker.Reset(true);
        }
	}

    public void OnSubMenuRemove(MenuBase menu)
	{
		if(menu != null)
		{
			if ((menu.ShowType & UIShowType.HideBackMenu) != 0)
			{
				if (menu.HideBackList != null)  //还原被隐藏的下层菜单
				{
					for (int i = menu.HideBackList.Count - 1; i >= 0; --i)
					{
						MenuBase backMenu = menu.HideBackList[i];
                        if (backMenu != null)
                        {
                            backMenu.HideBack(false);
						}
					}
				}
			}
			if ((menu.ShowType & UIShowType.HideBackScene) != 0)
			{
				HideScene(false);
			}
			if ((menu.ShowType & UIShowType.HideBackHud) != 0)
			{
				HideHud(false);
			}
			////回收进缓存队列
			//if (menu.CacheLevel >= 0)
			//{
			//	AddCacheUI(menu.Tag, menu);
			//}

			NotifyMenuObserver(menu, MenuState.Exit);
            mAllMenuList.Remove(menu);
            int menuIndex = mChildList.IndexOf(menu);
            if(menuIndex != -1)
            {
                mChildList.Remove(menu);
                if (menuIndex < mChildList.Count)
                {
                    for (int i = menuIndex; i < mChildList.Count; ++i)
                    {
                        InitMenuLayer(mChildList[i]);
                    }
                }
            }
			if ((menu.ShowType & UIShowType.HideBackScene) != 0)	//关闭最后一个菜单，还原场景 
			{
				mMenuRoot.ShowBackground(false);
			}
			menu.LifeIndex = -1;
			mNeedNotifyMenuList = true;
			if(MenuRemoveEvent != null)
			{
				MenuRemoveEvent(menu);
			}

			Dictionary<string, string> param = new Dictionary<string, string>();
			param.Add("tag", menu.MenuTag.ToString());
			EventManager.Fire("Event.Menu.Close", param);
        }
	}
	
	public void HideMenu(bool hide)
	{
		mMenuRoot.Visible = !hide;
    }

	/// <summary>
	/// 关闭指定tag的界面
	/// </summary>
	/// <param name="tag"></param>
	public void CloseMenuByTag(string tag)
	{
		MenuBase menu = FindMenuByTag(tag);
		if (menu != null)
		{
			menu.Close();
		}
	}

	/// <summary>
	/// 关闭LifeIndex大于index的所有界面
	/// </summary>
	/// <param name="index"></param>
	public void CloseMenuGtLifeIndex(int index)
	{
		for (int i = mAllMenuList.Count - 1; i >= 0; --i)
		{
			MenuBase menu = mAllMenuList[i];
			if(menu.LifeIndex > index)
			{
				menu.Close();
			}
		}
	}

	public void CloseAllMenu()
	{
		for(int i = mChildList.Count - 1; i >= 0; --i)
		{
			mChildList[i].Close();
		}
		//mFrameAnime.Clear();
	}

	/// <summary>
	/// 关闭所有包含隐藏属性的界面
	/// </summary>
	public void CloseAllHideMenu()
	{
		for (int i = mChildList.Count - 1; i >= 0; --i)
		{
			var menu = mChildList[i];
			if (menu.ShowType != UIShowType.Cover)
			{
				menu.Close();
			}
		}
	}

	public bool NeedHideScene
	{
		get
		{
			for (int i = mChildList.Count - 1; i >= 0; --i)
			{
				var menu = mChildList[i]; 
				if ((menu.ShowType & UIShowType.HideBackScene) > 0)
				{
					return true;
				}
			}
			return false;
		}
	}

	public void CloseAllMsgBox()
	{
		for (int i = 0; i < mMsgBoxRoot.NumChildren; ++i)
		{
			MenuBase child = mMsgBoxRoot.GetChildAt(i) as MenuBase;
			if (child != null)
			{
				child.Close();
			}
		}
    }

    public void CloseAllHudMenu()
    {
        for (int i = 0; i < mHudMenuRoot.NumChildren; ++i)
        {
            MenuBase child = mHudMenuRoot.GetChildAt(i) as MenuBase;
            if (child != null)
            {
                child.Close();
            }
        }
    }

    /// <summary>
    /// 获取最上层的菜单（当前正在显示的）
    /// </summary>
    /// <returns></returns>
    public MenuBase GetTopMenu()
	{
		if (mAllMenuList.Count > 0)
		{
            int index = mAllMenuList.Count - 1;
            while (index >= 0)
            {
                MenuBase cur = mAllMenuList[index--];
                if (cur.RootMenu == mMenuRoot)
                    return cur;
            }
		}
		return null;
    }

    public MenuBase GetTopMsgBox()
    {
        if (mAllMenuList.Count > 0)
        {
            int index = mAllMenuList.Count - 1;
            while (index >= 0)
            {
                MenuBase cur = mAllMenuList[index--];
                if (cur.RootMenu == mMsgBoxRoot)
                    return cur;
            }
        }
        return null;
    }

    public MenuBase FindMenuByTag(string tag)
	{
		for (int i = mAllMenuList.Count - 1; i >= 0; --i)
		{
			if (mAllMenuList[i].MenuTag == tag)
			{
				return mAllMenuList[i];
			}
		}
		return null;
	}

	public MenuBase FindMenuByXml(string name)
	{
		for (int i = mAllMenuList.Count - 1; i >= 0; --i)
		{
			if (mAllMenuList[i].XML_PATH == name)
			{
				return mAllMenuList[i];
			}
		}
		return null;
	}


	public List<MenuBase> FindMenusByTag(string tag)
	{
		var ret = new List<MenuBase>();
		for (int i = mAllMenuList.Count - 1; i >= 0; --i)
		{
			if (mAllMenuList[i].MenuTag == tag)
			{
				ret.Add(mAllMenuList[i]);
			}
		}
		return ret;
	}

    public MenuBase GetCurrentMenu()
    {
        if (mChildList.Count > 0)
        {
            return mChildList[mChildList.Count - 1];
        }
        return null;

    }

    public void CloseCurrentMenu()
    {
        MenuBase menBase = GetCurrentMenu();
        if (menBase != null)
        {
            menBase.Close();
        }
    }

    /// <summary>
    /// 根据tag打开一个界面
    /// </summary>
    /// <param name="uiTag">UI标识</param>
    /// <param name="cacheLevel">缓存级别（0表示不使用缓存）</param>
    public void OpenUIByTag(string uiTag, int cacheLevel = 0)
	{
		OpenUIByTag(uiTag, cacheLevel, null);
	}

	/// <summary>
	/// 根据tag打开一个界面
	/// </summary>
	/// <param name="uiTag">UI标识</param>
	/// <param name="cacheLevel">缓存级别（0表示不使用缓存）</param>
	/// <param name="param">自定义参数</param>
	public void OpenUIByTag(string uiTag, int cacheLevel, object[] param)
	{
        Dictionary<string, object> args = new Dictionary<string, object>();
        args.Add("tag", uiTag);
        args.Add("cacheLevel", cacheLevel);
        args.Add("params", param);
        EventManager.Fire("Event.OpenUI.Open", args);
    }

	/// <summary>
	/// 根据tag创建并返回一个界面
	/// </summary>
	/// <param name="uiTag">UI标识</param>
	/// <param name="cacheLevel">缓存级别（0表示不使用缓存）</param>
	/// <returns>MenuBase实例</returns>
	public MenuBase CreateUIByTag(string uiTag, int cacheLevel = 0)
	{
		return CreateUIByTag(uiTag, cacheLevel, "");
	}

	/// <summary>
	/// 根据tag创建并返回一个界面
	/// </summary>
	/// <param name="uiTag">UI标识</param>
	/// <param name="cacheLevel">缓存级别（0表示不使用缓存）</param>
	/// <param name="param">自定义参数</param>
	/// <returns>MenuBase实例</returns>
	public MenuBase CreateUIByTag(string uiTag, int cacheLevel, string param)
	{
		object[] args = { uiTag, cacheLevel, param };
        object r = LuaSystem.Instance.DoFunc("GlobalHooks.UI.CreateUI", args);
  //      LuaState ls_state = new LuaState();
		//LuaTable table = ls_state.getTable("UI");
		//object r = table.invoke("CreateUI", args);
		if (r != null)
		{
			if (r != null && r is MenuBase)
			{
				return r as MenuBase;
			}
		}
		return null;
	}

	public void UICacheInit(LuaTable t)
	{
		mCacheLevel.Clear();
		foreach (SLua.LuaTable.TablePair p in t)
		{
			int val = (int)Convert.ToInt64(p.value);
            string key = p.key.ToString();
            mCacheLevel.Add(key, val);
		}
	}

	private void RefreshCacheData(string tag)
	{
		if (!mCacheLevel.ContainsKey(tag))
		{
			mCacheLevel.Add(tag, 0);
		}
		if (mCacheLevel[tag] >= 0)
		{
			mCacheLevel[tag]++;
		}
	}

	public void CreateCacheUI(string uiTag)
	{
		if (!mCacheMenu.ContainsKey(uiTag) && mCacheMenu.Count < UICACHE_MAX)
		{
			MenuBase menu = CreateUIByTag(uiTag);
			AddCacheUI(uiTag, menu);
		}
	}

    private void BeforeAddToCache(MenuBase menu)
    {
        if (menu.LuaTable != null && menu.LuaTable["OnMoveToCache"] != null)
        {
            var fn = menu.LuaTable["OnMoveToCache"] as LuaFunction;
            if (fn != null)
            {
                fn.call(menu.LuaTable);
            }
        }
    }

	public bool AddCacheUI(string uiTag, MenuBase menu)
	{
		if (menu == null)
			return false;

		RefreshCacheData(uiTag);
		if (menu.CacheLevel == 0)
		{
			menu.CacheLevel = mCacheLevel[uiTag];
		}
		if (menu.CacheLevel < 0)
		{
			return false;
		}

		if (mCacheMenu.Count < UICACHE_MAX)
		{
			if (!mCacheMenu.ContainsKey(uiTag))
				mCacheMenu.Add(uiTag, new List<MenuBase>());
		    BeforeAddToCache(menu);
            mCacheMenu[uiTag].Add(menu);
			return true;
		}
		else if (UICACHE_MAX > 0)	//缓存池已满，根据优先级进行替换.
		{
			int minValue = int.MaxValue;
			string minTag = string.Empty;
			//找出缓存池中优先级最低的节点.
			foreach (var key in mCacheMenu.Keys)
			{
				int curLv = mCacheLevel.ContainsKey(key) ? mCacheLevel[key] : 0;
				if (curLv < minValue)
				{
					minValue = curLv;
					minTag = key;
				}
			}
			//如果目标的优先级高于最低的缓存节点，删掉老的添加新的.
			if (mCacheLevel[uiTag] > minValue)
			{
				RemoveCacheUIByTag(minTag, minValue);
				if (!mCacheMenu.ContainsKey(uiTag))
					mCacheMenu.Add(uiTag, new List<MenuBase>());
			    BeforeAddToCache(menu);
                mCacheMenu[uiTag].Add(menu);
				return true;
			}
		}

		return false;
	}

	public MenuBase GetCacheUIByXml(string name)
	{
		foreach(var m in mCacheMenu)
		{
			List<MenuBase> menuList = m.Value;
			if (menuList.Count > 0)
			{
				MenuBase menu = menuList[menuList.Count - 1];
				if (menu.XML_PATH == name)
				{
					menuList.RemoveAt(menuList.Count - 1);
					if (!menu.IsDestroy)
					{
						return menu;
					}
				}
			}
		}
		return null;
	}

	public MenuBase GetCacheUIByTag(string uiTag)
	{
		if (mCacheMenu.ContainsKey(uiTag) && mCacheMenu[uiTag] != null)
		{
			List<MenuBase> menuList = mCacheMenu[uiTag];
			if (menuList.Count > 0)
			{
				MenuBase menu = menuList[menuList.Count - 1];
				menuList.RemoveAt(menuList.Count - 1);
				if(menuList.Count == 0)
				{
					mCacheMenu.RemoveByKey(uiTag);
				}
				if (!menu.IsDestroy)
				{
					return menu;
				}
			}
		}
		return null;
	}

	/// <summary>
	/// 根据级别清除单个UI的缓存
	/// </summary>
	/// <param name="uiTag">UI标识</param>
	/// <param name="cacheLevel">清除小于等于这个级别的缓存</param>
	public void RemoveCacheUIByTag(string uiTag, int cacheLevel)
	{
		if (mCacheMenu.ContainsKey(uiTag) && mCacheMenu[uiTag] != null)
		{
			List<MenuBase> menuList = mCacheMenu[uiTag];
			for (int i = menuList.Count - 1; i >= 0; --i)
			{
				MenuBase menu = menuList[i];
				if (menu.CacheLevel <= cacheLevel)
				{
					menu.CloseAndDestroy();
				}
			}
			menuList.Clear();
			mCacheMenu.RemoveByKey(uiTag);
            mCacheLevel.RemoveByKey(uiTag);

        }
    }

    /// <summary>
    /// 清理长时间不用的UI缓存，并重新评估缓存利用率
    /// </summary>
    private void TryToClearCacheUI()
    {
        foreach (var p in mCacheMenu)
        {
            var uiTag = p.Key.ToString();
            var list = p.Value;
            if(list.Count > 0)
            {
                int passRate = (int)((DateTime.UtcNow - list[0].LastUseTime).TotalMinutes / 10); //每经过10分钟，使用率评分减1
                mCacheLevel[uiTag] -= passRate;
                if(mCacheLevel[uiTag] <= 0)
                {
                    for (int i = list.Count - 1; i >= 0; --i)
                    {
                        MenuBase menu = list[i];
                        menu.CloseAndDestroy();
                    }
                    list.Clear();
                    mCacheLevel.RemoveByKey(uiTag);
                }
            }
        }
    }

    /// <summary>
    /// 根据级别清空所有UI缓存
    /// </summary>
    /// <param name="cacheLevel">清除小于等于此级别的缓存</param>
    public void ClearAllCacheUI(int cacheLevel)
	{
		foreach (KeyValuePair<string, List<MenuBase>> p in mCacheMenu)
		{
			var uiTag = p.Key.ToString();
			if (mCacheMenu.ContainsKey(uiTag) && mCacheMenu[uiTag] != null)
			{
				List<MenuBase> menuList = mCacheMenu[uiTag];
				for (int i = menuList.Count - 1; i >= 0; --i)
				{
					MenuBase menu = menuList[i];
					if (menu.CacheLevel <= cacheLevel)
					{
						menu.CloseAndDestroy();
					}
				}
				menuList.Clear();
			}
		}
        mCacheMenu.Clear();
        mCacheLevel.Clear();
    }

    public int PushWaitUI(MenuBase menu)
    {
        int key = menu.GetHashCode();
        mWaitMenu.Add(key, menu);
        return key;
    }

    public MenuBase PopWaitUI(int key)
    {
        MenuBase menu;
        if (mWaitMenu.TryGetValue(key, out menu)) {
            mWaitMenu.Remove(key);
        }
        return menu;
    }

    public void ClearWaitUI()
    {
        foreach (var menu in mWaitMenu.Values)
        {
            menu.CloseAndDestroy();
        }
        mWaitMenu.Clear();
    }

    public void PreLoadUI(bool immediately)
	{
		if (immediately)
		{
			bool preLoadFinish = false;
			while (!preLoadFinish)
			{
				preLoadFinish = PreLoadingUI();
			}
		}
		else
		{
			GameGlobal.Instance.StartCoroutine(StartPreLoadUI());
		}
	}

	private int mPreLoadProcess = 0;
	System.Collections.IEnumerator StartPreLoadUI()
	{
		mPreLoadProcess = 0;
		bool preLoadFinish = false;
		while (!preLoadFinish)
		{
			preLoadFinish = PreLoadingUI();
			yield return 1;
		}
	}

	private bool PreLoadingUI()
	{
		switch (mPreLoadProcess)
		{
			//case 0:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIRoleAvatarEquip"));
			//	break;
			//case 1:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIRoleBagItem"));
			//	break;
			//case 2:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUINPCTalk"));
			//	break;
			//case 3:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIMount"));
			//	break;
			//case 4:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIActivityMain"));
			//	break;
			//case 5:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIWings"));
			//	break;
			//case 6:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUISkillMain"));
			//	break;
			//case 7:
			//	MenuMgr.Instance.CreateCacheUI(UITAG.Key2Tag("GameUIChatMainSecond"));
				//break;
		}

		if (++mPreLoadProcess > 7)
		{
			mPreLoadProcess = 7;
			return true;
		}

		return false;
	}

	/// <summary>
	/// 隐藏3D场景、UGUI界面
	/// </summary>
	/// <param name="isHide"></param>
	public void HideScene(bool isHide)
	{
		mHideSceneCount = isHide ? mHideSceneCount + 1 : mHideSceneCount - 1;
		mHideSceneCount = Math.Max(0, mHideSceneCount);
		GameSceneMgr.Instance.HideScene(mHideSceneCount > 0);
	}

	/// <summary>
	/// 隐藏HUD界面
	/// </summary>
	/// <param name="isHide"></param>
	public void HideHud(bool isHide)
	{
		mHideHudCount = isHide ? mHideHudCount + 1 : mHideHudCount - 1;
		mHideHudCount = Math.Max(0, mHideHudCount);
        bool hide = mHideHudCount > 0;
        if(HudManager.Instance.Visible == hide)
            HudManager.Instance.HideAllHud(hide);
		GameSceneMgr.Instance.UGUI.HideExtendUI(hide);
        BattleNumberManager.Instance.gameObject.transform.localScale = hide ? Vector3.zero : Vector3.one;
        mHudMenuRoot.Visible = !hide;
    }

	public void Clear(bool reLogin, bool reConnect)
	{
		CloseAllMenu();
		CloseAllMsgBox();
        CloseAllHudMenu();
        ClearWaitUI();
        TryToClearCacheUI();

        if (reConnect || reLogin || PublicConst.OSType == 5)
        {
            ClearAllCacheUI(int.MaxValue);
        }

		if (reLogin)
        {
            ClearMenuObserver();

            MenuAddEvent = null;
            MenuRemoveEvent = null;
            if (OnMenuListChange != null)
            {
                OnMenuListChange.Dispose();
                OnMenuListChange = null;
            }
            HideMenu(false);
            HideHud(false);
        }
	}

    public class MenuRoot : MenuBase
	{
		private Texture2D mT2d;
		private int mShowBgRef;
		public MenuRoot(string name, int order)
			: base(name)
		{
			this.IsRunning = true;
			this.Enable = false;
			this.EnableChildren = true;
			HZUISystem.SetNodeFullScreenSize(this);
            MenuOrder = order;
            MenuZ = 0;
		}
		protected override void OnUpdate() 
		{
			base.OnUpdate();
			MenuMgr.Instance.CheckNotifyMenuList();
		}
		protected override string UITag() { return string.Empty; }

		public void ShowBackground(bool show)
		{
            //if (show)
            //{
            //	mShowBgRef++;
            //	if (mT2d == null && GameSceneMgr.Instance.SceneCamera != null && GameSceneMgr.Instance.SceneCamera.isActiveAndEnabled)
            //	{
            //		mT2d = GameUtil.CaptureCamera(GameSceneMgr.Instance.SceneCamera, new Rect(0, 0, Screen.width/2, Screen.height/2), ~(1 << (int)PublicConst.LayerSetting.CharacterUnlit));
            //		//mT2d = Resources.Load<Texture2D>("ugui/Screenshot");
            //		UnityImage image = new UnityImage(mT2d, "SceneBg");
            //		UILayout layout = UILayout.CreateUILayoutImage(DeepCore.GUI.Data.UILayoutStyle.IMAGE_STYLE_BACK_4, image, 8);
            //		this.Layout = layout;
            //		this.Graphics.IsShowUILayout = false;
            //	}
            //}
            //else
            //{
            //	mShowBgRef--;
            //	if (mShowBgRef == 0)
            //	{
            //		if (mT2d != null)
            //		{
            //			DeepCore.Unity3D.UnityHelper.Destroy(mT2d);
            //			mT2d = null;
            //		}
            //		this.Layout = null;
            //	}
            //}
        }
    }

	#region MenuObserver

	private Dictionary<string, HashSet<MenuObserver>> mMenuObservers = new Dictionary<string, HashSet<MenuObserver>>();
	private Dictionary<string, Dictionary<string, LuaTable>> mLuaMenuObservers = new Dictionary<string, Dictionary<string, LuaTable>>();

	private HashSet<MenuObserver> mMenuObserversGlobal = new HashSet<MenuObserver>();

	private enum MenuState
	{
		Enter,
		Exit
	}

	/// <summary>
	/// 注册menu观察者.
	/// </summary>
	/// <param name="uiTag">被观察menu的tag</param>
	/// <param name="ob">观察者C#实例</param>
	public void AttachObserver(string uiTag, MenuObserver ob)
	{
		if (!mMenuObservers.ContainsKey(uiTag))
		{
			mMenuObservers.Add(uiTag, new HashSet<MenuObserver>());
		}
		HashSet<MenuObserver> obs = mMenuObservers[uiTag];
		obs.Add(ob);
	}
	/// <summary>
	/// 注册menu观察者.
	/// </summary>
	/// <param name="ob">观察者C#实例</param>
	public void AttachObserver(MenuObserver ob)
	{
		mMenuObserversGlobal.Add(ob);
	}

	/// <summary>
	/// 注销menu观察者.
	/// </summary>
	/// <param name="uiTag">被观察menu的tag</param>
	/// <param name="ob">观察者C#实例</param>
	public void DetachObserver(string uiTag, MenuObserver ob)
	{
		if (mMenuObservers.ContainsKey(uiTag))
		{
			HashSet<MenuObserver> obs = mMenuObservers[uiTag];
			if (obs != null)
			{
				obs.Remove(ob);
			}
		}
	}
	/// <summary>
	/// 注销menu观察者.
	/// </summary>
	/// <param name="ob">观察者C#实例</param>
	public void DetachObserver(MenuObserver ob)
	{
		mMenuObserversGlobal.Remove(ob);
	}

	public MenuObserver FindGlobalObserverAs(System.Predicate<MenuObserver> select)
	{
		foreach(var ob in mMenuObserversGlobal)
		{
			if (select(ob))
			{
				return ob;
			}
		}
		return null;
	}

	/// <summary>
	/// lua注册menu观察者.
	/// </summary>
	/// <param name="uiTag">被观察menu的tag</param>
	/// <param name="key">观察者的唯一标识，推荐用观察者自己的uiTag</param>
	/// <param name="t">lua观察者的table（OnMenuEnter和OnMenuExit两个lua func）</param>
	public void AttachLuaObserver(string uiTag, string key, LuaTable t)
	{
		if (!mLuaMenuObservers.ContainsKey(uiTag))
		{
			mLuaMenuObservers.Add(uiTag, new Dictionary<string, LuaTable>());
		}
		var obs = mLuaMenuObservers[uiTag];
		if (obs.ContainsKey(key))
		{
			Debugger.LogError("MenuMgr.AttachLuaObserver: can not attach a exist key! uiTag=" + uiTag + ", key=" + key);
			return;
		}
		obs.Add(key, t);
	}

	/// <summary>
	/// lua注销menu观察者.
	/// </summary>
	/// <param name="uiTag">被观察menu的tag</param>
	/// <param name="key">观察者的唯一标识，推荐用观察者自己的uiTag</param>
	public void DetachLuaObserver(string uiTag, string key)
	{
		if (mLuaMenuObservers.ContainsKey(uiTag))
		{
			var obs = mLuaMenuObservers[uiTag];
			if (obs != null)
			{
				obs.Remove(key);
			}
		}
	}

	private void NotifyMenuObserver(MenuBase menu, MenuState state)
	{
		var uiTag = menu.MenuTag;
		//C# notify
		if (mMenuObservers.ContainsKey(uiTag))
		{
			var obs  = mMenuObservers[uiTag];
			if (obs != null && obs.Count > 0)
			{
				foreach (MenuObserver ob in obs)
				{
					switch (state)
					{
						case MenuState.Enter:
							ob.OnMenuEnter(uiTag);
							break;
						case MenuState.Exit:
							ob.OnMenuExit(uiTag);
							break;
					}
				}
			}
		}

		//C# global notify
		foreach (MenuObserver ob in mMenuObserversGlobal)
		{
			switch (state)
			{
				case MenuState.Enter:
					ob.OnMenuEnter(uiTag);
					break;
				case MenuState.Exit:
					ob.OnMenuExit(uiTag);
					break;
			}
		}

		//Lua notify
		if (mLuaMenuObservers.ContainsKey(uiTag))
		{
			var obs = mLuaMenuObservers[uiTag];
			if (obs != null && obs.Count > 0)
			{
				foreach (var ob in obs)
				{
					string funcName = "";
					switch (state)
					{
						case MenuState.Enter:
							funcName = "OnMenuEnter";
							break;
						case MenuState.Exit:
							funcName = "OnMenuExit";
							break;
					}
					ob.Value.invoke(funcName, new object[] { uiTag });
				}
			}
		}
	}

	private void ClearMenuObserver()
	{
		mMenuObservers.Clear();
		mLuaMenuObservers.Clear();
	}

	#endregion

}

public interface SubMenuUListener
{
	void OnSubMenuAdd(MenuBase menu);
	void OnSubMenuRemove(MenuBase menu);
}

public interface MenuObserver
{
	void OnMenuEnter(string tag);
	void OnMenuExit(string tag);
}
