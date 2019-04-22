
using SLua;
using DeepCore.Unity3D.UGUIEditor.UI;
using System.Collections.Generic;

public class QuestHud
{
    private HZCanvas mRoot;
    private LuaTable mLuaHud = null;

    public QuestHud(HZCanvas root)
    {
        mRoot = root;
        //if (QuestData.QuestModule() != null)
        //    Reset();
        //else
        //    EventManager.Subscribe("Event.Quest.InitNetWork", Reset);
        //mLuaHud = LuaSystem.Instance.LoadString("return require('UI/Hud/QuestHud').create()") as LuaTable;
        //mLuaHud.invoke("init", new object[] { mLuaHud, mRoot });
    }
    
    public void Clear(bool reLogin, bool reConnect = false)
    {

        //if (mLuaHud != null && (reLogin || reConnect))
        //{
        //    mLuaHud.invoke("Clear", new object[] { mLuaHud, reLogin, reConnect });
        //    if (reLogin)
        //    {
        //        mLuaHud.Dispose();
        //        mLuaHud = null;
        //    }
        //}
        //if (reLogin)
        //{
        //    if (mLuaHud != null)
        //    {
        //        mLuaHud.invoke("Clear", new object[] { mLuaHud, reLogin, reConnect });
        //        mLuaHud.Dispose();
        //        mLuaHud = null;
        //    }
        //    //EventManager.Unsubscribe("Event.Quest.InitNetWork", Reset);
        //}
        //else if (reConnect && mLuaHud != null)
        //{
        //        mLuaHud.invoke("Clear", new object[] { mLuaHud, reLogin, reConnect });
                
        //}
        
    }

    public void Update()
    {
        
    }
}
