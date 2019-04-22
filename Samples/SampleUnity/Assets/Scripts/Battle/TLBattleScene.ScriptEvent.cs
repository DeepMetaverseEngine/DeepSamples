using System;
using Assets.Scripts.Cinema;


using UnityEngine;
using SLua;
using System.Collections.Generic;
using DeepCore.GameData.Zone;
using DeepCore.Unity3D.Battle;

public partial class TLBattleScene
{
    private bool IsInitFinish { get; set; }
    private List<string> CurrentLuaScriptNameList { get; set; }
    private bool bIsBeginScript {get;set;}
    [DoNotToLua]
    public void RegistScriptEvent()
    {
        IsInitFinish = false;
        CurrentLuaScriptNameList = new List<string>();
        RegistZoneEvent<ScriptCommandEvent>(ZoneEvent_ScriptCommandEvent);
        RegistZoneEvent<BubbleTalkEvent>(ZoneEvent_BubbleTalkEvent);
        RegistZoneEvent<ChatEvent>(ZoneEvent_ChatEvent);
        EventManager.Subscribe("Event.Scene.ChangeFinish", FirstInitFinish);
        EventManager.Subscribe("DirectorEventFinish", LuaEventFinish);


    }

    private void LuaEventFinish(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object fileName;
        if (data.TryGetValue("fileName",out fileName))
        {
            string _fileName = (string)fileName;
            bIsBeginScript = false;
            CurrentLuaScriptNameList.Remove(_fileName);

        }

    }

    [DoNotToLua]
    public void UnRegistScriptEvent()
    {
        EventManager.Unsubscribe("Event.Scene.ChangeFinish", FirstInitFinish);
        EventManager.Unsubscribe("DirectorEventFinish", LuaEventFinish);
    }


    private void FirstInitFinish(EventManager.ResponseData res)
    {
        IsInitFinish = true;
        bIsBeginScript = false;
        string filePath = "lua:map_" + DataMgr.Instance.UserData.MapTemplateId;
        if (DoParseLuaEvent(filePath))
        {
            DramaUIManage.Instance.highlightMask.SetArrowTransform(true, null, 1);
        }

    }

    private void ZoneEvent_ChatEvent(ChatEvent obj)
    {
        
            switch (obj.To)
            {
                case ChatMessageType.SystemToPlayer:

                    if (this.Actor != null && string.Equals(obj.ToPlayerUUID, this.Actor.PlayerUUID))
                    {
                        DoParseLuaEvent(obj.Message);
                    }
                    break;
                case ChatMessageType.SystemToForce:
                    if (this.Actor != null &&this.Actor.Force == obj.Force)
                    {
                        DoParseLuaEvent(obj.Message);
                    }
                    break;
                case ChatMessageType.SystemToAll:
                    DoParseLuaEvent(obj.Message);
                    break;
            }
    }

    private void ZoneEvent_BubbleTalkEvent(BubbleTalkEvent obj)
    {
        foreach(var talkinfo in obj.TalkInfos)
        {
            if (talkinfo.TalkUnit == 0)
            {
               AddStoryTip(talkinfo.TalkContent,talkinfo.TalkKeepTimeMS);
            }
            else
            {

                ComAICell unit;
                if (BattleObjects.TryGetValue(talkinfo.TalkUnit,out unit))
                {
                    if (unit is TLAIUnit)
                    {
                        var _unit = unit as TLAIUnit;
                        _unit.AddBubbleChat(talkinfo.TalkContent, talkinfo.TalkActionType,talkinfo.TalkKeepTimeMS);
                    }
                }
               
            }
        }
    }

    private void ZoneEvent_ScriptCommandEvent(ScriptCommandEvent obj)
    {
        //DoParseCgEvent(obj.message);
        DoParseLuaEvent(obj.message);
    }
    [DoNotToLua]
    public bool DoParseLuaEvent(string message)
    {
        if (message.IndexOf("lua") == -1)
            return false;
        string[] str = message.Split(':');
        if (str.Length > 1)
        {
            string filepath = (string)LuaSystem.Instance.DoFunc("GlobalHooks.Drama.GetScriptFile", str[1]);
            if (!string.IsNullOrEmpty(filepath))
            {
                CurrentLuaScriptNameList.Add(filepath);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debugger.LogError("DoParseLuaEvent is Error = " + message);
        }
        return false;
    }
    [DoNotToLua]
    public void DoParseLuaEventByFileName(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
           LuaSystem.Instance.DoFunc("GlobalHooks.Drama.Start", fileName);
        }
    }
    [DoNotToLua]
    public void StopLuaEventByMessage(string message)
    {
        string[] str = message.Split(':');
        if (str.Length > 1)
        {
            string fileName = (string)LuaSystem.Instance.DoFunc("GlobalHooks.Drama.GetScriptFile", str[1]);
            StopLuaEventByFileName(fileName);
        }
        else
        {
            Debugger.LogError("StopLuaEventByMessage is Error = " + message);
        }
    }
    [DoNotToLua]
    public void StopLuaEventByFileName(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            LuaSystem.Instance.DoFunc("GlobalHooks.Drama.Stop", fileName);
        }
        else
        {
            Debugger.LogError("StopLuaEventByFileName is Empty");
        }
       
    }
    protected void UpdateScriptEvent(float deltaTime)
    {
        if (IsInitFinish && CurrentLuaScriptNameList.Count >0 && !bIsBeginScript)
        {
            //DoParseLuaEventByFileName(CurrentLuaScriptName);
            //CurrentLuaScriptName = null;
            var path = CurrentLuaScriptNameList[0];
            //if (!path.StartsWith("effect/"))
            //{
            //    Dictionary<string, object> param = new Dictionary<string, object>();
            //    param.Add("PlayCG", true);
            //    EventManager.Fire("EVENT_PLAYCG_START", param);
            //}
            DoParseLuaEventByFileName(CurrentLuaScriptNameList[0]);
            bIsBeginScript = true;
        }
    }
}

