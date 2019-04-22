
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;
using System.Collections.Generic;
using TLBattle.Message;
using UnityEngine;

public partial class TLAIUnit
{

    protected override void InitActionStatus()
    {
        base.InitActionStatus();

        RegistAction(UnitActionStatus.Idle, new ActionStatus(UnitActionStatus.Idle, "n_idle", "n_idle", true));
        RegistAction(UnitActionStatus.Move, new ActionStatus(UnitActionStatus.Move, "n_run", "n_run", true));
        RegistAction(UnitActionStatus.Skill, new TLSkillActionStatus(UnitActionStatus.Skill, "TL_unit_skill"));
        RegistAction(UnitActionStatus.Spawn, new ActionStatus(UnitActionStatus.Spawn, "f_out", "f_out", false));
        //RegistAction(UnitActionStatus.Rebirth, new ActionStatus(UnitActionStatus.Rebirth, "f_out", "f_out", false));
    }

    protected virtual void OnCombatStateChange(CombatStateChangeEventB2C.BattleStatus status)
    {
        bindBehaviour.ShowHpBar(IsShowHPBar());
        RefreshCombatState(status);
    }

    protected void ResetActionStatus()
    {
        RegistAction(UnitActionStatus.Idle, new ActionStatus(UnitActionStatus.Idle, "n_idle", "n_idle", true));
        RegistAction(UnitActionStatus.Move, new ActionStatus(UnitActionStatus.Move, "n_run", "n_run", true));
    }

    private void RefreshCombatState(CombatStateChangeEventB2C.BattleStatus status)
    {
        switch (status)
        {
            case CombatStateChangeEventB2C.BattleStatus.None:
                {
                    RegistAction(UnitActionStatus.Idle, new ActionStatus(UnitActionStatus.Idle, "n_idle", "n_idle", true));
                    RegistAction(UnitActionStatus.Move, new ActionStatus(UnitActionStatus.Move, "n_run", "n_run", true));
                }
                break;
            case CombatStateChangeEventB2C.BattleStatus.PVE:
            case CombatStateChangeEventB2C.BattleStatus.PVP:
                {
                    var action = ZUnit.Templates.GetDefinedUnitAction(UnitActionStatus.Idle);
                    if (action != null)
                    {
                        var mySt = new DefinedActionStatus(UnitActionStatus.Idle, UnitActionStatus.Idle.ToString(), action);
                        RegistAction(UnitActionStatus.Idle, mySt);
                    }

                    action = ZUnit.Templates.GetDefinedUnitAction(UnitActionStatus.Move);
                    if (action != null)
                    {
                        var mySt = new DefinedActionStatus(UnitActionStatus.Move, UnitActionStatus.Move.ToString(), action);
                        RegistAction(UnitActionStatus.Move, mySt);
                    }
                }
                break;
            default:
                Debugger.LogError("UNKNOWN BATTLE STATE");
                break;
        }
    }

    protected void RegistPickActionStatus(string pickAction)
    {
        var st = UnitActionStatus.Pick;
        var pick = new TLPickActionStatus(st, pickAction);
        pick.ActionName = pickAction;
        RegistAction(st, pick);
    }

    protected void RegistFishActionStatus(string pickAction, int timeMs, bool isSpecial = false)
    {
        var st = UnitActionStatus.Pick;
        var pick = new TLFishActionStatus(st, pickAction, timeMs, isSpecial);
        pick.ActionName = pickAction;
        RegistAction(st, pick);
    }

    protected void RemovePickActionStatus()
    {
        this.ResetActionStatus();
    }

    protected void RegistRideActionStatus(string actionName)
    {
        var st = UnitActionStatus.Idle;
        var action = ZUnit.Templates.GetDefinedUnitAction(st);
        string name = string.Format(actionName, "idle");
        var idle = new TLRideIdleActionStatus(st, name, action);
        idle.ActionName = name;
        RegistAction(st, idle);

        st = UnitActionStatus.Move;
        action = ZUnit.Templates.GetDefinedUnitAction(st);
        name = string.Format(actionName, "run");
        var run = new TLRideRunActionStatus(st, name, action);
        run.ActionName = name;
        RegistAction(st, run);

    }

    protected void RemoveRideActionStatus()
    {
        this.ResetActionStatus();
    }

    public class TLPickActionStatus : ActionStatus
    {
        public TLPickActionStatus(UnitActionStatus status, string key) : base(status, key)
        {

        }
    }

    public class TLFishActionStatus : ActionStatus
    {
        public class Fish
        {
            public string name;
            public int TimeMS;

            public Fish(string name, int time)
            {
                this.name = name;
                this.TimeMS = time;
            }
        }

        public int mCurrentPassTime = 0;

        protected Queue<Fish> mActionQueue = new Queue<Fish>();

        protected Fish mCurrentAction;

        public int timeMs = 0;

        private bool isSpecial = false;

        public TLFishActionStatus(UnitActionStatus status, string key, int timeMs, bool isSpecial = false) : base(status, key)
        {
            this.timeMs = timeMs;

            this.isSpecial = isSpecial;
        }

        protected override void OnStart(ComAIUnit owner)
        {
            mActionQueue.Clear();

            Fish prepare = new Fish("n_fish01", 600);
            this.mActionQueue.Enqueue(prepare);

            var fishTimes = this.timeMs - 600 - 1100;
            Fish fishing = new Fish("n_fish02", fishTimes);
            this.mActionQueue.Enqueue(fishing);

            if (this.isSpecial == false)
            {
                Fish finish = new Fish("n_fish03", 1100);
                this.mActionQueue.Enqueue(finish);
            }

            this.mCurrentPassTime = 0;
            NextAction(owner);
        }

        protected override void OnUpdate(ComAIUnit owner, float deltaTime)
        {
            this.mCurrentPassTime += (int)((owner.ZObj as ZoneUnit).Parent.CurrentIntervalMS);

            if (mActionQueue.Count > 0 && mCurrentAction != null)
            {
                if (mCurrentPassTime >= mCurrentAction.TimeMS)
                {
                    NextAction(owner);
                }
            }

        }

        protected virtual void NextAction(ComAIUnit owner)
        {
            if (mActionQueue.Count > 0)
            {
                mCurrentAction = mActionQueue.Dequeue();
                if (mCurrentAction != null)
                {
                    owner.animPlayer.Play(mCurrentAction.name);
                }
            }
            mCurrentPassTime = 0;
        }


    }

    public class TLSpeicalActionStatus : ActionStatus
    {
        private float SpeicalAnimTime = 0;
        private System.Action mAction;
        private bool isLoop = false;
        public TLSpeicalActionStatus(UnitActionStatus status,
            string key, string animName, bool crossFade = true, bool isLoop = false,
            float speed = 1,
            System.Action action = null) : base(status, key, animName, crossFade, speed)
        {
            this.mAction = action;
            isLoop = this.isLoop;
        }
        public override int CompareTo(ActionStatus other)
        {
            return base.CompareTo(other);
        }
        protected override void OnUpdate(ComAIUnit owner, float deltaTime)
        {
            base.OnUpdate(owner, deltaTime);
            if (!isLoop)
            {
                SpeicalAnimTime -= deltaTime;
                if (SpeicalAnimTime <= 0)
                {
                    SpeicalAnimTime = 0;
                    OnStop(owner);
                }
            }

        }
        protected override void OnStop(ComAIUnit owner)
        {
            base.OnStop(owner);
            owner.RemoveAction(UnitActionStatus, Key);
            if (mAction != null)
            {
                mAction.Invoke();
            }

        }
        protected override void OnStart(ComAIUnit owner)
        {
            base.OnStart(owner);
            SpeicalAnimTime = owner.animPlayer.GetAnimTime(ActionName);
        }
    }

    public class TLSkillActionStatus : SkillActionStatus
    {
        public TLSkillActionStatus(UnitActionStatus status, string key) : base(status, key)
        {
        }

        protected override void OnStart(ComAIUnit owner)
        {
            base.OnStart(owner);
        }

        public override void ZUnit_OnSkillActionStart(ComAIUnit owner, ZoneUnit.ISkillAction skillAction)
        {
            if (owner.IsPosChanged.Equals(Vector3.zero))
            {
                owner.animPlayer.SetFloat("Blend", 0.5f);
            }
            else
            {
                var tmp = owner.IsPosChanged;
                tmp.y = 0;
                var tmp2 = owner.Forward;
                tmp2.y = 0;
                if (Vector3.Angle(tmp, tmp2) > 90)
                {
                    owner.animPlayer.SetFloat("Blend", 1f);
                }
                else
                {
                    owner.animPlayer.SetFloat("Blend", 0f);
                }
            }
            base.ZUnit_OnSkillActionStart(owner, skillAction);
        }

        protected override void OnUpdate(ComAIUnit owner, float delteTime)
        {
            base.OnUpdate(owner, delteTime);

            if (owner.IsPosChanged.Equals(Vector3.zero))
            {
                owner.animPlayer.SetFloat("Blend", 0.5f, 0.15f, delteTime);
            }
            else
            {
                var tmp = owner.IsPosChanged;
                tmp.y = 0;
                var tmp2 = owner.Forward;
                tmp2.y = 0;

                float value = Vector3.Angle(tmp, tmp2) > 90 ? 1f : 0f;
                owner.animPlayer.SetFloat("Blend", value, 0.15f, delteTime);
            }
        }
    }

    public class TLRideIdleActionStatus : DefinedActionStatus
    {
        public TLRideIdleActionStatus(UnitActionStatus status, string key, UnitActionDefinitionMap.UnitAction data) : base(status, key, data)
        {
        }

        protected override void NextAction(ComAIUnit owner)
        {
            if (mActionQueue.Count > 0)
            {
                mCurrentAction = mActionQueue.Dequeue();
                if (mCurrentAction != null)
                {
                    this.CrossFade = mCurrentAction.CrossFade;
                    this.Speed = mCurrentAction.Speed;


                    if (owner is TLAIUnit)
                    {
                        owner.animPlayer.speed = Speed;
                        if (CrossFade)
                        {
                            //owner.animPlayer.CrossFade(this.ActionName, 0.15f);
                            owner.animPlayer.Play(this.ActionName);
                        }
                        else
                        {
                            owner.animPlayer.Play(this.ActionName);
                        }
                    }
                }
            }
            mCurrentPassTime = 0;
        }
    }

    public class TLRideRunActionStatus : DefinedActionStatus
    {
        public TLRideRunActionStatus(UnitActionStatus status, string key, UnitActionDefinitionMap.UnitAction data) : base(status, key, data)
        {
        }

        protected override void NextAction(ComAIUnit owner)
        {
            if (mActionQueue.Count > 0)
            {
                mCurrentAction = mActionQueue.Dequeue();
                if (mCurrentAction != null)
                {
                    this.CrossFade = mCurrentAction.CrossFade;
                    this.Speed = mCurrentAction.Speed;


                    if (owner is TLAIUnit)
                    {
                        owner.animPlayer.speed = Speed;
                        if (CrossFade)
                        {
                            //owner.animPlayer.CrossFade(this.ActionName, 0.15f);
                            owner.animPlayer.Play(this.ActionName);
                        }
                        else
                        {
                            owner.animPlayer.Play(this.ActionName);
                        }
                    }
                }
            }
            mCurrentPassTime = 0;
        }

        protected override void OnUpdate(ComAIUnit owner, float deltaTime)
        {
            NextAction(owner);
        }

    }

    public class TLPickIdleActionStatus : DefinedActionStatus
    {
        private CombatStateChangeEventB2C.BattleStatus mBattleStatus;

        public TLPickIdleActionStatus(UnitActionStatus status, string key, UnitActionDefinitionMap.UnitAction data) : base(status, key, data)
        {
        }

        protected override void NextAction(ComAIUnit owner)
        {
            if (mActionQueue.Count > 0)
            {
                mCurrentAction = mActionQueue.Dequeue();
                if (mCurrentAction != null)
                {
                    this.CrossFade = mCurrentAction.CrossFade;
                    this.Speed = mCurrentAction.Speed;

                    owner.animPlayer.speed = Speed;
                    if (CrossFade)
                    {
                        owner.animPlayer.CrossFade(this.ActionName, 0.15f);
                    }
                    else
                    {
                        owner.animPlayer.Play(this.ActionName);
                    }
                }
            }
            mCurrentPassTime = 0;
        }
    }

}
