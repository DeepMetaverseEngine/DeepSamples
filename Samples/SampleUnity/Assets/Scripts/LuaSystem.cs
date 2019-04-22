using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Setting;
using SLua;
using DeepCore.IO;

public class LuaSystem
{
    private static LuaSystem gInstance;
    public static LuaSystem Instance
    {
        get
        {
            if (gInstance == null)
            {
                gInstance = new LuaSystem();
            }
            return gInstance;
        }
    }

    public LuaTable GetLuaTable(string path)
    {
        if (!IsCompleted) return null;
        return LuaSvr.mainState.getTable(path);
    }

    public void LuaGC()
    {
        
    }
    public object DoFunc(string func, params object[] args)
    {
        if (IsCompleted)
        {
            LuaFunction f = LuaSvr.mainState.getFunction(func);
            if (f != null)
            {
                return f.call(args);
            }
        }
        return null;
    }

    public object LoadString(string chunk)
    {
        return LuaSvr.mainState.doString(chunk);
    }

    private LuaSvr mLuaSvr;
    private bool IsStarted { get; set; }
    public bool IsCompleted { get; private set; }

    public int InitProgress { get; private set; }
    /// <summary>
    /// 初始化完成后此函数会被设置null
    /// </summary>
    public Action OnInitComplete { private get; set; }

    LuaSystem()
    {
    }

    public void Start()
    {
        if (!IsStarted)
        {
            IsStarted = true;

            mLuaSvr = new LuaSvr();
            LuaSvr.mainState.loaderDelegate = delegate(string fn)
            {
                if (!fn.EndsWith(".lua"))
                {
                    fn = fn + ".lua";
                }
                if (!fn.StartsWith(ProjectSetting.LuaPath))
                {
                    fn = ProjectSetting.LuaPath + fn;
                }
                return Resource.LoadData(fn);
            };

            mLuaSvr.init(tick, complete, LuaSvrFlag.LSF_3RDDLL);

            new GameObject("ConsoleRun").AddComponent<ConsoleRun>();
        }
    }

    void tick(int p)
    {
        InitProgress = p;
    }

    void complete()
    {
        if (GameGlobal.Instance.netMode)
        {
            mLuaSvr.start("Main.lua");
        }
       
        if (OnInitComplete != null)
        {
            OnInitComplete();
            OnInitComplete = null;
        }
        IsCompleted = true;
        EventManager.Fire("Event.LuaSystem.InitComplete", EventManager.defaultParam);
    }
}

class ConsoleRun : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && Input.GetKeyDown(KeyCode.R))
        {
            LuaSystem.Instance.LoadString(@"
package.loaded['/ui_edit/lua/ConsoleRun'] = nil
require('ConsoleRun')
");
        }
    }
}