using UnityEngine;
using System.Collections.Generic;
using System;
 

using UnityEngine.EventSystems;

using DeepCore.GUI.Data;
using DeepCore.Unity3D.UGUIEditor;
using DeepCore.Unity3D.UGUI;
using DeepCore.Unity3D.UGUIEditor.UI;

public class MenuBase : UIComponent
{

	public string MenuTag { get; set; }
	public string XML_PATH { get; private set; }
    
    public MenuBase RootMenu { get; private set; }
    public MenuBase ParentMenu { get; private set; }
    private readonly List<MenuBase> mSubMenuList = new List<MenuBase>();
	public List<MenuBase> Childrens { get { return mSubMenuList; } }

	protected Dictionary<string, DisplayNode> mParts = new Dictionary<string, DisplayNode>();

	//被隐藏的下层菜单列表.
	public List<MenuBase> HideBackList { get; set; }

	//用来标识打开菜单时如何隐藏下层元素.
	public int ShowType { get; set; }

    //打开关闭时的动画效果.
    //public int AnimeType { get; set; }

    //用于UI缓存时，OnEnter后可取此参数重新初始化UI
    public object[] ExtParam { get; set; }

	public UERoot mRoot { get; protected set; }

	public SLua.LuaTable LuaTable { get; set; }

	public SLua.LuaTable RequireTable { get; set; }

	private SubMenuUListener mSubMenuListener;

	/// <summary>
	/// 缓存级别，0代表不缓存
	/// </summary>
	public int CacheLevel { get; set; }

	public bool IsDestroy { get; private set; }

	public bool IsRunning { get; protected set; }

    public bool IsUGUI { get { return true; } }

	public bool HasParentMenu { get { return ParentMenu != null; } }

	//在所有ui中的序号，值越大，界面存在的时间越长,小于0表示不在界面列表中
	public int LifeIndex { get; set; }

    public DateTime LastUseTime { get; private set; }

    public bool NeedBack { get; set; }

    private int mMenuOrder;
    public int MenuOrder { get
        {
            return mMenuOrder;
        }
        set
        {
            OnMenuOrderChange(value);
        }
    }
    private float mMenuZ;
    public float MenuZ { get
        {
            return mMenuZ;
        }
        set
        {
            mMenuZ = value;
            UILayerMgr.SetPositionZ(this.UnityObject, mMenuZ);
        }
    }

    protected UIComponent mBackGroundComp;

	public static long s_RefCount = 0;

	private string mName;

    protected bool IsPlayingAnime;
    protected bool IsChildRemoving;
    protected bool IsWaitToRemove;

    public MenuBase(int tag)
	: this("MenuBase - " + tag)
	{
		s_RefCount++;
	}

	public MenuBase(string name = "MenuBase")
		: base(name)
	{
		this.mName = name;
		this.MenuTag = UITag();
		this.ShowType = UIShowType.HideHudAndMenu;
        this.mSubMenuListener = MenuMgr.Instance;
        this.SetCompAnime(this, UIAnimeType.Default);

		HZUISystem.SetNodeFullScreenSize(this);
		this.IsInteractive = true;
		this.Enable = true;
		this.EnableChildren = true;
		this.Layout = new UILayout();
		s_RefCount++;
		//OnInit();
	}

	~MenuBase()
	{
		s_RefCount--;
		//GameDebug.Log("-------------- ~MenuBase -------------- " + mName);
	}

	protected virtual bool OnInit()
	{
		return true;
	}

	public static MenuBase Create(string tag, string xmlPath)
	{
		MenuBase ret = new MenuBase(tag);

		if (!string.IsNullOrEmpty(xmlPath))
		{
			if (ret.InitWithXml(xmlPath))
			{
				return ret;
			}
			return null;
		}
		else
		{
			return ret;
		}
	}

	protected override void OnPointerClick(PointerEventData e)
	{
		base.OnPointerClick(e);
		if (mRoot != null && mRoot.GetAttributeAs<string>("touch_close") == "1")
		{
			//读取到此属性且为1时，点击空白关闭此界面
			this.Close();
		}
	}

	protected bool InitWithXml(string path)
	{
		this.XML_PATH = path;
		mRoot = (UERoot)HZUISystem.CreateFromFile(path);
		if (mRoot != null)
		{
			this.AddChild(mRoot);
			return true;
		}
		return false;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="tag"></param>
	/// <param name="re">是否递归查找</param>
	/// <returns></returns>
	public MenuBase FindSubMenu(string tag, bool re)
	{
		for (int i = 0; i < mSubMenuList.Count; i++)
		{
			MenuBase menu = mSubMenuList[i];
			if (tag == menu.MenuTag)
			{
				return menu;
			}
			if (re)
			{
				MenuBase subMenu = menu.FindSubMenu(tag, re);
				if (subMenu != null)
				{
					return subMenu;
				}
			}
		}

		return null;
	}

	public void AddSubMenu(MenuBase child)
	{
		if (child == null)
			return;

		if (child.ParentMenu != null)
		{
			Debugger.LogError("a child menu can not be added more then one parent!!!");
			return;
		}

        int key = MenuMgr.Instance.PushWaitUI(child);
        child.Load((ret) =>
        {
            if (MenuMgr.Instance.PopWaitUI(key) == null)
                return;

            base.AddChildAt(child, NumChildren);
            this.mSubMenuList.Add(child);
            child.SetParent(this);
            child.RootMenu = this.RootMenu == null ? this : this.RootMenu;

            //manager call back
            if (mSubMenuListener != null)
            {
                mSubMenuListener.OnSubMenuAdd(child);
            }

            if (this.IsRunning)
            {
                child.Enter();

                if (child.AnimeComps[child].AnimeType == UIAnimeType.Default && mName == "MenuRoot")
                    child.AnimeComps[child].AnimeType = UIAnimeType.FadeMoveUp;

                foreach (var comp in child.AnimeComps)
                {
                    if (comp.Key.Visible)
                        ShowOpenAnime(comp.Key, comp.Value);
                }
                if(IsWaitToRemove)
                    this.RemoveSubMenu(child);
            }
		});
	}

    public new void AddChild(DisplayNode child)
	{
		this.AddChildAt(child, NumChildren);
	}

	public new void AddChildAt(DisplayNode child, int index)
	{
		if (child is MenuBase)
			this.AddSubMenu(child as MenuBase);
		else
			base.AddChildAt(child, index);
    }

    private void BeforeRemoveSubMenu(MenuBase child)
    {
        int animeCount = 0;
        foreach (var comp in child.AnimeComps)
        {
            if (ShowCloseAnime(comp.Key, comp.Value, () =>
            {
                animeCount--;
                if (animeCount == 0)
                {
                    child.IsPlayingAnime = false;
                    AfterRemoveSubMenu(child);
                }
            }))
            {
                child.IsPlayingAnime = true;
                animeCount++;
            }
        }
    }

    public void RemoveSubMenu(MenuBase child)
	{
		if (child == null)
			return;

        BeforeRemoveSubMenu(child);

        //exit
        if (this.IsRunning)
        {
            child.IsWaitToRemove = true;
            if (!child.RemoveAllSubMenu())
                AfterRemoveSubMenu(child);
		}
	}

    private void AfterRemoveSubMenu(MenuBase child)
    {
        //等待动画播放完毕，并且所有子节点全部移除完毕
        if (!child.IsPlayingAnime && !child.IsChildRemoving)
        {
            //exit
            child.Exit();

            //remove self
            this.mSubMenuList.Remove(child);
            RemoveChild(child, false);
            child.SetParent(null);
            child.RootMenu = null;
            child.IsWaitToRemove = false;

            //manager call back
            if (mSubMenuListener != null)
            {
                mSubMenuListener.OnSubMenuRemove(child);
            }

            OnChildRemoveFinish(child);

            //add cache
            bool cached = false;
            if (child.CacheLevel >= 0)
            {
                //回收进缓存队列
                cached = MenuMgr.Instance.AddCacheUI(child.MenuTag, child);
            }
            if (!cached)
            {
                child.Destory();
            }
            if (child.RequireTable != null)
            {
                child.RequireTable["dont_destroy"] = cached;
            }
        }
    }

    protected void OnChildRemoveFinish(MenuBase child)
    {
        if(mSubMenuList.Count == 0)
        {
            IsChildRemoving = false;
            if (IsWaitToRemove)
            {
                ParentMenu.AfterRemoveSubMenu(this);
            }
        }
    }

	public bool RemoveAllSubMenu()
	{
        if (mSubMenuList.Count > 0)
        {
            IsChildRemoving = true;
            for (int i = mSubMenuList.Count - 1; i >= 0; --i)
            {
                RemoveSubMenu(mSubMenuList[i]);
            }
            return true;
        }
        return false;
	}

    private void OnMenuOrderChange(int menuOrder)
    {
        int changeOrder = menuOrder - this.mMenuOrder;
        foreach (var p in mLayerComps)
        {
            if (p.Key.StartsWith("comps")) //ui
            {
                DisplayNode node = p.Value as DisplayNode;
                GameObject go = node.UnityObject;
                int oldOrder = UILayerMgr.GetLayerOrder(go, true);
                UILayerMgr.SetLayerOrder(go, oldOrder + changeOrder, true);
            }
            else //3d model
            {
                string modelKey = p.Key.Substring("model.".Length);
                if (UI3DModelAdapter.IsModelExist(modelKey))
                {
                    GameObject go = p.Value as GameObject;
                    int oldOrder = UILayerMgr.GetLayerOrder(go, false);
                    UILayerMgr.SetLayerOrder(go, oldOrder + changeOrder, false);
                }
            }
        }

        this.mMenuOrder = menuOrder;
        UILayerMgr.SetLayerOrder(this.UnityObject, menuOrder, true, (int)PublicConst.LayerSetting.UI);

        for (int i = mSubMenuList.Count - 1; i >= 0; --i)
        {
            mSubMenuList[i].MenuOrder = mMenuOrder + UILayerMgr.SubMenuOrderSpace;
        }
    }

    Dictionary<string, object> mLayerComps = new Dictionary<string, object>();

    public void SetUILayer(DisplayNode node)
    {
        SetUILayer(node, (int)(mMenuOrder + UILayerMgr.CompZOrderSpace), UILayerMgr.CompZSpace);
    }

    public void SetUILayer(DisplayNode node, int layerOrder, float posZ)
    {
        string key = "comps." + node.GetHashCode();
        mLayerComps[key] = node;

        GameObject go = node.UnityObject;
        UILayerMgr.SetLayerOrder(go, layerOrder, true);
        UILayerMgr.SetPositionZ(go, posZ);
    }

    public void Set3DModelLayer(string modelKey, GameObject go)
    {
        Set3DModelLayer(modelKey, go, mMenuOrder);
    }

    public void Set3DModelLayer(string modelKey, GameObject go, int layerOrder)
    {
        string key = "model." + modelKey;
        mLayerComps[key] = go;
        
        UILayerMgr.SetLayerOrder(go, layerOrder, false);
    }

    protected void Load(Action<bool> action)
    {
        if (OnLoadEvent != null)
        {
            OnLoadEvent(action);
        }
        else
        {
            this.OnLoad(action);
        }
    }

    protected void Enter()
	{
		IsRunning = true;

        LastUseTime = DateTime.UtcNow;
        if (NeedBack)
        {
            Predicate<string> cb = OnGlobalBackClick;
            LuaSystem.Instance.DoFunc("GlobalHooks.SubscribeGlobalBack", new object[] { MenuTag, cb });
        }

        for (int i = 0; i < mSubMenuList.Count; ++i)
		{
			mSubMenuList[i].Enter();
        }

        this.OnEnter();
		if (OnEnterEvent != null)
		{
			OnEnterEvent();
		}
	}

	/// <summary>
	/// 仅关闭界面，不销毁实例
	/// </summary>
	protected void Exit()
	{
		IsRunning = false;
        if (NeedBack)
        {
            LuaSystem.Instance.DoFunc("GlobalHooks.UnsubscribeGlobalBack", new object[] { MenuTag });
        }

        //for (int i = 0; i < mSubMenuList.Count; ++i)
        //{
        //	mSubMenuList[i].Exit();
        //}

        this.OnExit();
		if (OnExitEvent != null)
		{
			OnExitEvent();
		}
	}

	/// <summary>
	/// 销毁实例
	/// </summary>
	protected virtual void Destory()
	{
        if (IsDestroy)
            return;

		//call back
		OnDestory();

		if (OnDestoryEvent != null)
		{
			OnDestoryEvent();
        }

        ////remove chindren
        //RemoveAllSubMenu();

		//remove self
		if (this.Parent == null)
		{
			this.Dispose();
		}
		else
		{
			RemoveFromParent(true);
		}

		//remove comp ref
		mRoot = null;
		mParts.Clear();
        mLayerComps.Clear();
        if (HideBackList != null)
			HideBackList.Clear();
		if (LuaTable != null)
		{
			foreach (SLua.LuaTable.TablePair p in this.LuaTable)
			{
				LuaTable[(string)p.key] = null;
			}
			LuaTable.Dispose();
			LuaTable = null;

		}
		if (RequireTable != null)
		{
			RequireTable.Dispose();
			RequireTable = null;
		}
		mSubMenuListener = null;
		mBackGroundComp = null;

		CacheLevel = 0;

		IsDestroy = true;
    }

    public virtual void Close()
    {
        if (IsRunning)
        {
            ParentMenu.RemoveSubMenu(this);
            if (OnCloseEvent != null)
            {
                OnCloseEvent();
                OnCloseEvent = null;
            }
        }
        //else if (!IsDestroy)
        //{
        //    this.Destory();
        //}
    }

    /// <summary>
    /// 关闭并强制清空缓存，销毁对象.
    /// </summary>
    public void CloseAndDestroy()
    {
        this.CacheLevel = -1;
        if (IsRunning)
        {
            Close();
        }
        else if (!IsDestroy)
        {
            this.Destory();
        }
    }

    public bool Contains(MenuBase child)
    {
        // check for a recursion
        MenuBase ancestor = child;
        while (ancestor != this && ancestor != null)
            ancestor = ancestor.ParentMenu;
        return ancestor == this;
    }

	protected void SetParent(MenuBase parent)
	{
        if(this.Contains(parent))
            throw new Exception("An object cannot be added as a child to itself or one " +
                                    "of its children (or children's children, etc.)");
        else
            ParentMenu = parent;
	}

    public void SetLuaParams(SLua.LuaTable param)
    {
        object[] objs = new object[param.length()];
        int index = 0;
        foreach (SLua.LuaTable.TablePair p in param)
        {
            object val = p.value;
            objs[index++] = val;
        }
        this.ExtParam = objs;
    }

    public bool HideBack(bool hide)
    {
        if (!this.IsDestroy)
        {
            if (hide)
            {
                if (this.Visible && !IsPlayingAnime)
                {
                    this.Visible = false;
                    if (OnDisableEvent != null)
                        OnDisableEvent();
                    return true;
                }
            }
            else
            {
                if (!this.Visible)
                {
                    this.Visible = true;
                    if (OnEnableEvent != null)
                        OnEnableEvent();
                    return true;
                }
            }
        }
        return false;
    }

    protected Dictionary<DisplayNode, AnimeData> AnimeComps = new Dictionary<DisplayNode, AnimeData>();

    protected class AnimeData
    {
        public int AnimeType { get; set; }
        public Vector2 OriginPos { get; set; }
    }

    public void SetCompAnime(DisplayNode node, int anime)
    {
        AnimeData animeData;
        if (!AnimeComps.TryGetValue(node, out animeData))
        {
            animeData = new AnimeData();
            AnimeComps.Add(node, animeData);
        }
        animeData.AnimeType = anime;
        animeData.OriginPos = node.Position2D;
    }

    private static bool ShowOpenAnime(DisplayNode node, AnimeData animeData, Action callback = null)
    {
        node.Position2D = animeData.OriginPos;
        if (animeData.AnimeType == UIAnimeType.Scale)
        {
            DeepCore.Unity3D.UGUIAction.ScaleAction scale = new DeepCore.Unity3D.UGUIAction.ScaleAction();
            scale.ScaleX = scale.ScaleY = 1;
            node.Scale = Vector2.zero;
            scale.Duration = 0.3f;
            node.AddAction(scale);
            scale.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveUp)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.Y += 200;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 1;
            node.Alpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveDown)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.Y += -100;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 1;
            node.Alpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveLeft)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.X += 200;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 1;
            node.Alpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveRight)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.X += -200;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 1;
            node.Alpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        return false;
    }

    private static bool ShowCloseAnime(DisplayNode node, AnimeData animeData, Action callback = null)
    {
        if (animeData.AnimeType == UIAnimeType.Scale)
        {
            DeepCore.Unity3D.UGUIAction.ScaleAction scale = new DeepCore.Unity3D.UGUIAction.ScaleAction();
            scale.ScaleX = scale.ScaleY = 0;
            scale.Duration = 0.15f;
            node.AddAction(scale);
            scale.ActionFinishCallBack = (a) =>
            {
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveUp)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y + 200;
            move.Duration = 0.15f;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                node.Alpha = 1;
                node.Y += -200;
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveDown)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X;
            move.TargetY = node.Y - 100;
            move.Duration = 0.15f;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                node.Alpha = 1;
                node.Y += 100;
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveLeft)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X + 200;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                node.Alpha = 1;
                node.X += -200;
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        else if (animeData.AnimeType == UIAnimeType.FadeMoveRight)
        {
            DeepCore.Unity3D.UGUIAction.MoveAction move = new DeepCore.Unity3D.UGUIAction.MoveAction();
            move.TargetX = node.X - 200;
            move.TargetY = node.Y;
            move.Duration = 0.15f;
            node.AddAction(move);
            DeepCore.Unity3D.UGUIAction.FadeAction fade = new DeepCore.Unity3D.UGUIAction.FadeAction();
            fade.TargetAlpha = 0;
            fade.Duration = 0.15f;
            node.AddAction(fade);
            fade.ActionFinishCallBack = (a) =>
            {
                node.Alpha = 1;
                node.X += 200;
                if (callback != null)
                    callback.Invoke();
            };
            return true;
        }
        return false;
    }

	/// <summary>
	/// 返回搜索的控件，并存入容器中
	/// </summary>
	/// <param name="name">控件名</param>
	/// <returns>搜索的控件</returns>
	public DisplayNode GetComponent(string name)
	{
		return GetComponent(mRoot, name);
	}

	/// <summary>
	/// 返回搜索的控件，并存入容器中
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <returns>搜索的控件</returns>
	public DisplayNode GetComponent(UIComponent root, string name)
	{
		if (root == null)
		{
			return null;
		}
		string key = root.EditName + '.' + root.GetHashCode() + '.' + name;
		if (!mParts.ContainsKey(key))
		{
			mParts[key] = root.FindChildByEditName<UIComponent>(name);
		}
		return mParts[key];
	}

	public delegate bool ComponentPredicate(DisplayNode node);
	public static DisplayNode FindComponentAs(DisplayNode root, ComponentPredicate predicate, bool recursive = true)
	{
		if (root == null)
		{
			return null;
		}
		return root.FindChildAs<DisplayNode>((it) => { return predicate(it); }, recursive);
	}

	/// <summary>
	/// 以/开头表示从根节点下开始查找，不以/开头为匹配一段路径
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static DisplayNode FindChildComponent(UIComponent root, string path)
	{
		if (string.IsNullOrEmpty(path) || root == null)
		{
			return null;
		}
		var strs = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
		if (strs.Length >= 1)
		{
			UIComponent p = root.FindChildByEditName<UIComponent>(strs[0], !path.StartsWith("/"));
			for (int i = 1; i < strs.Length; i++)
			{
				if (p == null)
				{
					break;
				}
				p = p.FindChildByEditName(strs[i], false);
			}
			return p;
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// 以/开头表示从根节点下开始查找，不以/开头为匹配一段路径
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public DisplayNode FindComponent(string path)
	{
		return FindChildComponent(mRoot, path);
	}
	/// <summary>
	/// 设置UE控件显示
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="visible">是否显示</param>
	public void SetVisibleUENode(string name, bool visible)
	{
		var node = GetComponent(name);
		if (node != null)
		{
			node.Visible = visible;
		}
	}

	/// <summary>
	/// 设置UE控件显示
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="visible">是否显示</param>
	public static void SetVisibleUENode(UIComponent root, string name, bool visible)
	{
		if (root != null)
		{
			UIComponent node = root.FindChildByEditName<UIComponent>(name);
			SetVisibleUENode(node, visible);
		}
	}

	/// <summary>
	/// 设置UE控件显示
	/// </summary>
	/// <param name="node">操作控件</param>
	/// <param name="visible">是否显示</param>
	public static void SetVisibleUENode(UIComponent node, bool visible)
	{
		if (node != null)
		{
			node.Visible = visible;
		}
	}

	/// <summary>
	/// 设置UE控件触摸开关
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="enable">自己是否可触摸</param>
	/// <param name="enableChildren">子节点是否可触摸</param>
	public void SetEnableUENode(string name, bool enable, bool enableChildren)
	{
		var node = GetComponent(name) as UIComponent;
		SetEnableUENode(node, enable, enableChildren);
	}

	/// <summary>
	/// 设置UE控件显示
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="enable">自己是否可触摸</param>
	/// <param name="enableChildren">子节点是否可触摸</param>
	public static void SetEnableUENode(UIComponent root, string name, bool enable, bool enableChildren)
	{
		if (root != null)
		{
			UIComponent node = root.FindChildByEditName<UIComponent>(name);
			SetEnableUENode(node, enable, enableChildren);
		}
	}

	/// <summary>
	/// 设置UE控件显示
	/// </summary>
	/// <param name="node">操作控件</param>
	/// <param name="enable">自己是否可触摸</param>
	/// <param name="enableChildren">子节点是否可触摸</param>
	public static void SetEnableUENode(UIComponent node, bool enable, bool enableChildren)
	{
		if (node != null)
		{
			node.IsInteractive = enable;
			node.Enable = enable;
			node.EnableChildren = enableChildren;
			if (enable && node.Layout == null)
				node.Layout = new UILayout();
		}
	}

	/// <summary>
	/// 设置UE控件置灰
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="gray">是否置灰</param>
	public void SetGrayUENode(string name, bool gray)
	{
		SetGrayUENode(mRoot, name, gray);
	}

	/// <summary>
	/// 设置UE控件置灰
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="gray">是否置灰</param>
	public static void SetGrayUENode(UIComponent root, string name, bool gray)
	{
		if (root != null)
		{
			UIComponent node = root.FindChildByEditName<UIComponent>(name);
			SetGrayUENode(node, gray);
		}
	}

	/// <summary>
	/// 设置UE控件置灰
	/// </summary>
	/// <param name="node">操作控件</param>
	/// <param name="gray">是否置灰</param>
	public static void SetGrayUENode(UIComponent node, bool gray)
	{
		if (node != null)
		{
			node.IsGray = gray;
		}
	}

	/// <summary>
	/// 设置UELabel文本及颜色
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="text">文本</param>
	/// <param name="textRGBA">文字颜色</param>
	/// <param name="borderRGBA">描边色</param>
	public void SetLabelText(string name, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		var label = GetComponent(name) as HZLabel;
		SetLabelText(label, text, textRGBA, borderRGBA);
	}


	/// <summary>
	/// 设置UELabel文本及颜色
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="text">文本</param>
	/// <param name="textRGBA">文字颜色</param>
	/// <param name="borderRGBA">描边色</param>
	public static void SetLabelText(UIComponent root, string name, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		if (root != null)
		{
			HZLabel label = root.FindChildByEditName<HZLabel>(name);
			SetLabelText(label, text, textRGBA, borderRGBA);
		}
	}

    public static void SetLabelText(UIComponent root, string name, string text, UnityEngine.Color textRGB)
    {
        if (root != null)
        {
            HZLabel label = root.FindChildByEditName<HZLabel>(name);
            if (label != null)
            {
                label.FontColor = textRGB;
            }
            SetLabelText(label, text, 0, 0);
        }
    }
    /// <summary>
    /// 设置UELabel文本及颜色
    /// </summary>
    /// <param name="label">操作控件</param>
    /// <param name="text">文本</param>
    /// <param name="textRGBA">文字颜色</param>
    /// <param name="borderRGBA">描边色</param>
    public static void SetLabelText(HZLabel label, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		if (label != null)
		{
			label.Text = text;
			if (textRGBA != 0)
				label.FontColor = GameUtil.RGBA2Color(textRGBA);
			if (borderRGBA != 0)
				label.SetBorder(GameUtil.RGBA2Color(borderRGBA), new Vector2(1, 1));
		}
	}

	/// <summary>
	/// 设置UETextButton文本及颜色
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="text">文本</param>
	/// <param name="textRGBA">文字颜色</param>
	/// <param name="borderRGBA">描边色</param>
	public void SetButtonText(string name, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		SetButtonText(mRoot, name, text, textRGBA, borderRGBA);
	}

	/// <summary>
	/// 设置UETextButton文本及颜色
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="text">文本</param>
	/// <param name="textRGBA">文字颜色</param>
	/// <param name="borderRGBA">描边色</param>
	public static void SetButtonText(UIComponent root, string name, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		if (root != null)
		{
			HZTextButton button = root.FindChildByEditName<HZTextButton>(name);
			SetButtonText(button, text, textRGBA, borderRGBA);
		}
	}

	/// <summary>
	/// 设置UETextButton文本及颜色
	/// </summary>
	/// <param name="btn">操作控件</param>
	/// <param name="text">文本</param>
	/// <param name="textRGBA">文字颜色</param>
	/// <param name="borderRGBA">描边色</param>
	public static void SetButtonText(HZTextButton btn, string text, uint textRGBA = 0, uint borderRGBA = 0)
	{
		if (btn != null)
		{
			btn.Text = text;
			if (textRGBA != 0)
				btn.FontColor = GameUtil.RGBA2Color(textRGBA);
			if (borderRGBA != 0)
				btn.TextSprite.SetBorder(GameUtil.RGBA2Color(borderRGBA), new Vector2(1, 1));
		}
	}

	/// <summary>
	/// 对ImageBox设置图片
	/// </summary>
	/// <param name="name">控件名</param>
	/// <param name="imgPath">图片路径</param>
	public void SetImageBox(string name, string imgPath, UILayoutStyle style = UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, int clipSize = 8)
	{
		var node = GetComponent(name) as UIComponent;
		SetImageBox(node, imgPath, style, clipSize);
	}

	/// <summary>
	/// 对ImageBox设置图片
	/// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
	/// <param name="imgName">图片名称</param>
	/// <param name="imgPath">图片路径</param>
	public static void SetImageBox(UIComponent root, string name, string imgPath, UILayoutStyle style = UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, int clipSize = 8)
	{
		if (root != null)
		{
			UIComponent node = root.FindChildByEditName<UIComponent>(name);
			SetImageBox(node, imgPath, style, clipSize);
		}
	}

	/// <summary>
	/// 对ImageBox设置图片
	/// </summary>
	/// <param name="node">操作控件</param>
	/// <param name="imgName">图片名称</param>
	/// <param name="imgPath">图片路径</param>
	public static void SetImageBox(UIComponent node, string imgPath, UILayoutStyle style = UILayoutStyle.IMAGE_STYLE_BACK_4_CENTER, int clipSize = 8)
	{
		if (node != null)
		{
			UILayout layout = HZUISystem.CreateLayout(imgPath, style, clipSize);
			node.Layout = layout;
		}
    }

    /// <summary>
    /// 对ImageBox设置图片
    /// </summary>
	/// <param name="root">搜索的根结点</param>
	/// <param name="name">控件名</param>
    /// <param name="layout">layout</param>
    public void SetImageBox(UIComponent root, string name, UILayout layout)
    {
        if (root != null)
        {
            UIComponent node = root.FindChildByEditName<UIComponent>(name);
            SetImageBox(node, layout);
        }
    }

    /// <summary>
    /// 对ImageBox设置图片
    /// </summary>
    /// <param name="name">控件名</param>
    /// <param name="layout">layout</param>
    public void SetImageBox(string name, UILayout layout)
    {
        var node = GetComponent(name) as UIComponent;
        SetImageBox(node, layout);
    }

    /// <summary>
    /// 对ImageBox设置图片
    /// </summary>
    /// <param name="node">控件</param>
    /// <param name="layout">layout</param>
    public static void SetImageBox(UIComponent node, UILayout layout)
    {
        node.Layout = layout;
    }

    public Vector2 LocalToUIGlobal(DisplayNode node)
    {
        Vector2 gPos = node.LocalToGlobal();
        Vector2 lPos = mRoot.GlobalToLocal(gPos, true);
        RectTransform rootTrans = mRoot.Transform;
        lPos.x += rootTrans.offsetMin.x + rootTrans.anchorMin.x * rootTrans.rect.width;
        lPos.y += rootTrans.offsetMax.y - (1 - rootTrans.anchorMax.y) * rootTrans.rect.height;
        return lPos;
    }

    public Vector2 LocalToScreenGlobal(DisplayNode node)
    {
        Vector2 gPos = node.LocalToGlobal();
        Vector2 lPos = mRoot.GlobalToLocal(gPos, true);
        RectTransform rootTrans = mRoot.Transform;
        lPos.x += rootTrans.offsetMin.x + rootTrans.anchorMin.x * this.Transform.rect.width;
        lPos.y += rootTrans.offsetMax.y - (1 - rootTrans.anchorMax.y) * this.Transform.rect.height;
        return lPos;
    }

    /// <summary>
    /// 初始化TB组，一个按下后其他的自动弹起（一组TB要放在同一个节点下）
    /// </summary>
    /// <param name="parent">TB组的父节点</param>
    /// <param name="selectFirst">默认选中项</param>
    /// <param name="handler">TB组的TouchBegin回调事件</param>
    public static void InitMultiToggleButton(DisplayNode parent, string selectDefault, TouchClickHandle handler)
	{
		if (parent != null)
		{
			HZToggleButton last = null;
			for (int i = parent.NumChildren - 1; i >= 0; --i)
			{
				HZToggleButton childTB = parent.GetChildAt(i) as HZToggleButton;
				if (childTB != null)
				{
					//设置锁定.
					childTB.SetBtnLockState(HZToggleButton.LockState.eLockSelect);
					//处理触摸事件.
					childTB.Selected = (sender) =>
					{
						if ((sender as HZToggleButton).IsChecked)
						{
							RefreshMultiToggleButton(childTB);
							if (handler != null)
							{
								handler(sender);
							}

							if (!(sender as HZToggleButton).IsChecked && last != null && last != sender)
								last.IsChecked = true;
							else
								last = (HZToggleButton)sender;
						}
					};
					//设置默认选中项.
					if (selectDefault == childTB.EditName)
					{
						RefreshMultiToggleButton(childTB);
						if (handler != null)
						{
							handler(childTB);
						}
					}
				}
			}
		}
	}

	//public static void InitMultiToggleButton(TouchClickHandle handler, string selectDefault, LuaInterface.LuaTable table)
	//{
	//	List<HZToggleButton> nodes = new List<HZToggleButton>();
	//	HZToggleButton defaultNode = null;
	//	foreach (DictionaryEntry v in table)
	//	{
	//		HZToggleButton bt = v.Value as HZToggleButton;
	//		nodes.Add(bt);
	//		if (bt.EditName == selectDefault)
	//		{
	//			defaultNode = bt;
	//		}
	//	}
	//	InitMultiToggleButton(handler, defaultNode, nodes.ToArray());
	//}

	public static void InitMultiToggleButton(TouchClickHandle handler, HZToggleButton selectDefault, params HZToggleButton[] btns)
	{
		HZToggleButton last = null;
		for (int i = 0; i < btns.Length; i++)
		{
			HZToggleButton childTB = btns[i];
			childTB.SetBtnLockState(HZToggleButton.LockState.eLockSelect);
			childTB.Selected = (sender) =>
			{
				if ((sender as HZToggleButton).IsChecked)
				{
					for (int j = 0; j < btns.Length; j++)
					{
						HZToggleButton node = btns[j];
						if (node != sender)
						{
							node.IsChecked = false;
						}
					}
					if (handler != null)
					{
						handler(sender);
					}

					if (!(sender as HZToggleButton).IsChecked && last != null && last != sender)
						last.IsChecked = true;
					else
						last = (HZToggleButton)sender;
				}
			};
			//设置默认选中项.
			if (selectDefault == childTB)
			{
				childTB.IsChecked = true;
				if (handler != null)
				{
					handler(childTB);
				}
			}
			else
			{
				childTB.IsChecked = false;
			}
		}
	}

	/// <summary>
	/// 刷新TB组，一个按下后其他的自动弹起（一组TB要放在同一个节点下）
	/// </summary>
	/// <param name="selectTB">按下的ToggleButton</param>
	public static void RefreshMultiToggleButton(HZToggleButton selectTB)
	{
		if (selectTB != null)
		{
			//把自己设为按下状态.
			if (!selectTB.IsChecked)
				selectTB.IsChecked = true;
			//其余的设为弹起状态.
			if (selectTB.Parent != null)
			{
				for (int i = selectTB.Parent.NumChildren - 1; i >= 0; --i)
				{
					HZToggleButton childTB = selectTB.Parent.GetChildAt(i) as HZToggleButton;
					if (childTB != null && childTB != selectTB)
					{
						childTB.IsChecked = false;
					}
				}
			}
		}
	}

	public void SetFullBackground(UILayout layout)
	{
		if (mBackGroundComp == null)
		{
			mBackGroundComp = new UIComponent("back_ground");
			if (IsRunning)
			{
				InitBackGround();
			}
		}
		mBackGroundComp.Layout = layout;

	}

	private void InitBackGround()
	{
		DisplayNode p = this.Parent;
		DisplayRoot r = null;
		while (p != null)
		{
			if (p is DisplayRoot)
			{
				r = (DisplayRoot)p;
				break;
			}
			p = p.Parent;
		}

		if (r != null)
		{
			mBackGroundComp.Transform.localPosition = r.Transform.localPosition;
			mBackGroundComp.Transform.anchorMin = r.Transform.anchorMin;
			mBackGroundComp.Transform.anchorMax = r.Transform.anchorMax;
			mBackGroundComp.Transform.anchoredPosition = r.Transform.anchoredPosition;
			mBackGroundComp.Transform.offsetMin = r.Transform.offsetMin;
			mBackGroundComp.Transform.offsetMax = r.Transform.offsetMax;

			mBackGroundComp.Size2D = r.Size2D;
			AddChildAt(mBackGroundComp, 0);
		}
	}

	protected virtual void OnEnter()
	{
		if (mBackGroundComp != null && mBackGroundComp.Parent == null)
		{
			InitBackGround();
		}
	}
	protected virtual void OnLoad(Action<bool> callback) { callback(true); }
    protected virtual void OnClose() { }
    protected virtual void OnExit() { }
	protected virtual void OnDestory() { }

    protected virtual string UITag() { return string.Empty; }

    protected virtual bool OnGlobalBackClick(string arg)
    {
        Close();
        return false;
    }

    public Action OnDestoryEvent { get; set; }
    public Action<Action<bool>> OnLoadEvent { get; set; }
    public Action OnEnterEvent { get; set; }
	public Action OnExitEvent { get; set; }
    public Action OnCloseEvent { get; set; }
    public Action OnEnableEvent { get; set; }
    public Action OnDisableEvent { get; set; }
}
