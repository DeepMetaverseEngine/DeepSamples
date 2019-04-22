
using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using System;
using System.Collections.Generic;
using ThreeLives.Client.Common.Modules.Quest;
using TLBattle.Common.Plugins;
using UnityEngine;

public class BattleInteractive
{
    private ZoneLayer mLayer;

    private TLAIActor mActor;

    private TLBattleScene mScene;

    private bool IsTalkWithNpc = false;
    private bool IsDialogueTalk = false;
    public enum PickType
    {
        None,
        Quest
    }

    public BattleInteractive(TLBattleScene scene, ZoneLayer layer, TLAIActor actor)
    {
        this.mScene = scene;
        this.mLayer = layer;
        this.mActor = actor;
        this.IsTalkWithNpc = false;
        this.IsDialogueTalk = false;
        EventManager.Subscribe("Event.Npc.NpcTalk", NpcTalkEvent);
        EventManager.Subscribe("Event.Npc.DialogueTalk", DialogueTalkEvent);
        //EventManager.Subscribe("CloseNpcCamera", CloseNpcTalkEvent);
    }


    float time = 0;
    public void Update(float delta)
    {
        if (mLayer != null && mActor != null)
        {
            time += delta;
            if (time >= 0.5f)
            {
                CheckItem(delta);
                CheckNpcTalk(delta);
                time = 0;
            }
        }
    }
    private Vector3 mLastPosition = Vector3.zero;

    private void StopTalkEvent()
    {

        if (mActor != null && mActor.ObjectRoot != null && IsTalkState())
        {
            mLastPosition = mActor.Position;
            mActor.ChangeActorState(new TLAIActor.IdleState(mActor));
        }
        if (!IsTalkState())
        {
            DataMgr.Instance.QuestMangerData.ShowCompleteAnim();
        }
    }

    private void NpcTalkEvent(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object state;
        if (data.TryGetValue("isTalk", out state))
        {
            bool _isTalk = (bool)state;
            this.IsTalkWithNpc = _isTalk;
           
            StopTalkEvent();
        }
    }
    private void DialogueTalkEvent(EventManager.ResponseData res)
    {
        Dictionary<object, object> data = (Dictionary<object, object>)res.data[1];
        object state;
        if (data.TryGetValue("isTalk", out state))
        {
            bool _isTalk = (bool)state;
            this.IsDialogueTalk = _isTalk;
           
            StopTalkEvent();
        }

    }

    //private void CloseNpcTalkEvent(EventManager.ResponseData res)
    //{
    //    mLastPosition = Vector3.zero;
    //    this.IsTalkWithNpc = false;
    //}
    private void CheckNpcTalk(float delta)
    {
        if (mLastPosition != Vector3.zero && IsTalkState())
        {

            float distance = Vector3.Distance(mLastPosition, mActor.Position);
            if (distance >= 1)
            {
                EventManager.Fire("NpcTalkClose", EventManager.defaultParam);
                mLastPosition = Vector3.zero;
            }

        }
    }

    private PickType mPickType = PickType.None;

    private void PickItem()
    {
        if (mActor.CurGState is TLAIActor.PreparePickState)
        {
            (mActor.CurGState as TLAIActor.PreparePickState).PickItem();
        }
    }



    private void CheckItem(float delta)
    {
        if (mActor.CurGState is TLAIActor.PreparePickState
            || mActor.CurrentState != UnitActionStatus.Idle
            || this.IsTalkState())
        {
            return;
        }

        if (mActor != null && mActor.ZActor != null)
        {
            ZoneItem zi = mLayer.GetNearPickableItem(mActor.ZActor,1);
            if (zi != null)
            {
                TLItemProperties zp = zi.Info.Properties as TLItemProperties;
                if (zp.Type == TLItemProperties.ItemType.Task)
                {
                    if (DataMgr.Instance.QuestData.GetQuestConditionTypeIsNotEnough(TLQuestCondition.PickItem, zi.Info.TemplateID))
                    {
                        mActor.ChangeActorState(new TLAIActor.PreparePickState(mActor, zi));
                    }
                }
                else
                {
                    mActor.ChangeActorState(new TLAIActor.PreparePickState(mActor, zi));
                }
            }
        }
    }

    private bool IsTalkState()
    {
        return this.IsTalkWithNpc || this.IsDialogueTalk ;
    }

    public void OnStartPickObject(ZoneUnit unit, TimeExpire<UnitStartPickObjectEvent> start)
    {
        var obj = mScene.GetBattleObject(start.Tag.PickObjectID);
        if (obj == null) return;

        //HudManager.Instance.SkillBar.Visible = false;

        if (obj is TLAIItem)//物品.
        {
            var item = obj as TLAIItem;
            if (item != null)
            {
                mActor.ChangeActorState(new TLAIActor.StartPickState(mActor, item, start));
            }
        }
        else if (obj is TLAIActor)
        {
            if (start.Tag.PickStatus != "tp")
                mActor.ChangeActorState(new TLAIActor.PickSelfState(mActor, start));
            else
            {
                mActor.ChangeDirection(-90 * Mathf.Deg2Rad);
                mActor.PlayAnim("n_jump", false);
                GameSceneMgr.Instance.ReadyToLoading();
            }
        }

    }


    public void OnStopPickObject(ZoneUnit unit, UnitStopPickObjectEvent stop)
    {
        //HudManager.Instance.SkillBar.Visible = true;

        if (!(mActor.CurGState is TLAIActor.AutoRunState))
        {
            mActor.ChangeActorState(new TLAIActor.IdleState(mActor));
        }

    }


    public void Destroy()
    {
        EventManager.Unsubscribe("Event.Npc.NpcTalk", NpcTalkEvent);
        EventManager.Unsubscribe("Event.Npc.DialogueTalk", DialogueTalkEvent);
        //EventManager.Unsubscribe("CloseNpcCamera", CloseNpcTalkEvent);
        HudManager.Instance.Interactive.OnPickBtnClick = null;
        this.mLayer = null;
        this.mActor = null;
        this.IsTalkWithNpc = false;
        this.IsDialogueTalk = false;
        mScene = null;
    }


}