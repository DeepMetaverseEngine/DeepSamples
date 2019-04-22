using DeepCore;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using System;
using System.Collections.Generic;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAIUnit
    {
        private HashMap<UnitActionStatus, ActionStack> mActionStatus = new HashMap<UnitActionStatus, ActionStack>();
        private ActionStatus mCurrentActionStatus;
        private ActionStatus mLockActionStatus;

        public ActionStatus CurrentActionStatus
        {
            get { return mCurrentActionStatus; }
        }

        //注册动作控制对象//
        protected virtual void InitActionStatus()
        {
            //从配置表初始化动作列表//
            foreach (UnitActionStatus st in Enum.GetValues(typeof(UnitActionStatus)))
            {
                var action = ZUnit.Templates.GetDefinedUnitAction(st);
                if (action != null)
                {
                    RegistAction(st, new DefinedActionStatus(st, st.ToString(), action));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="st"></param>
        /// <param name="action">If return True , break</param>
        public bool ForeachAction(UnitActionStatus st, Predicate<ActionStatus> action)
        {
            ActionStack stack;
            if (mActionStatus.TryGetValue(st, out stack))
            {
                return stack.Foreach(action);
            }
            return false;
        }

        public ActionStatus ReplaceAction(UnitActionStatus st, ActionStatus dst)
        {
            ActionStack stack;
            if (mActionStatus.TryGetValue(st, out stack))
            {
                var ret = stack.Remove(dst.Key);
                if (ret != null)
                {
                    stack.Add(dst);
                    ChangeAction(ZUnit.CurrentState);
                }
                return ret;
            }
            return null;
        }

        public ActionStatus RegistAction(UnitActionStatus st, ActionStatus status)
        {
            ActionStack stack;
            if (mActionStatus.TryGetValue(st, out stack) == false)
            {
                stack = new ActionStack(status);
                mActionStatus.Add(st, stack);
            }
			else
			{
				stack.Add(status);
			}
            ChangeAction(ZUnit.CurrentState);
            return status;
        }

        public ActionStatus RemoveAction(UnitActionStatus st, string key)
        {
            ActionStack stack;
            if (mActionStatus.TryGetValue(st, out stack))
            {
                var status = stack.Remove(key);
                if (status != null)
                {
                    ChangeAction(ZUnit.CurrentState);
                }
                return status;
            }
            return null;
        }

        public ActionStatus GetTopActionStatus(UnitActionStatus st)
        {
            ActionStack stack;
            if (mActionStatus.TryGetValue(st, out stack))
            {
                return stack.Top;
            }
            return null;
        }

        public void SetLockActionStatus(ActionStatus status)
        {
            if (status != null)
            {
                mLockActionStatus = status;
                if (status != mCurrentActionStatus)
                {
                    if (mCurrentActionStatus != null)
                    {
                        mCurrentActionStatus.Stop(this);
                    }
                    mCurrentActionStatus = status;
                    if (mCurrentActionStatus != null)
                    {
                        mCurrentActionStatus.Start(this);
                    }
                }
            }
            else
            {
                mLockActionStatus = null;
                ChangeAction(ZUnit.CurrentState);
            }
        }

        protected virtual void ChangeAction(UnitActionStatus st, bool bForce = false)
        {
            if (mLockActionStatus == null)
            {
                var newAction = GetTopActionStatus(st);
                if (newAction != null && (bForce || mCurrentActionStatus != newAction))
                {
                    if (mCurrentActionStatus != null)
                    {
                        mCurrentActionStatus.Stop(this);
                    }
                    mCurrentActionStatus = newAction;
                    if (mCurrentActionStatus != null)
                    {
                        mCurrentActionStatus.Start(this);
                    }
                }
            }
        }

        protected virtual void UpdateAction(float deltaTime)
        {
            if (mCurrentActionStatus != null)
            {
                mCurrentActionStatus.Update(this, deltaTime);
            }
        }

        internal class ActionStack
        {
            readonly List<ActionStatus> List = new List<ActionStatus>();

            public ActionStatus Top
            {
                get
                {
                    if (List.Count > 0) return List[List.Count - 1];
                    return null;
                }
            }

            internal ActionStack(ActionStatus status)
            {
                List.Add(status);
            }

            public bool Foreach(Predicate<ActionStatus> action)
            {
                using (var list = ListObjectPool<ActionStatus>.AllocAutoRelease(List))
                {
                    foreach (var a in list)
                    {
                        if (action(a))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            public ActionStatus Add(ActionStatus status)
            {
                var tmp = Get(status.Key);
                if (tmp != null)
                {
                    List.Remove(tmp);
                }
                else
                {
                    tmp = status;
                }
                List.Add(tmp);
                //List.Sort();

                return tmp;
            }

            public ActionStatus Get(string key)
            {
                return List.Find((st) =>
                {
                    if (st.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                        return true;
                    return false;
                });
            }

            public ActionStatus Remove(string key)
            {
                for (var i= 0; i < List.Count; i++)
                {
                    var tmp = List[i];
                    if (tmp.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        List.RemoveAt(i);
                        return tmp;
                    }
                }

                return null;
            }
        }

        public class ActionStatus : IComparable<ActionStatus>
        {
            protected static ActionStatus gPrevActionStatus { get; private set; }

            private readonly string mKey;
            public UnitActionStatus UnitActionStatus { get; private set; }
            public string Key { get { return mKey; } }
            public int Priority { get; set; }
            public string ActionName { get; set; }
            public bool CrossFade { get; set; }
            public float Speed { get; set; }

            public ActionStatus(UnitActionStatus status, string key, string animName = "", bool crossFade = false, float speed = 1f)
            {
                this.UnitActionStatus = status;
                this.mKey = key;
                this.ActionName = animName;
                this.CrossFade = crossFade;
                this.Speed = speed;
            }

            public void Start(ComAIUnit owner)
            {
                OnStart(owner);
            }

            protected virtual void OnStart(ComAIUnit owner)
            {
                owner.animPlayer.speed = Speed;
                if (CrossFade)
                {
                    owner.animPlayer.CrossFade(ActionName, 0.15f);
                }
                else
                {
                    owner.animPlayer.Play(ActionName);
                }
            }

            public void Stop(ComAIUnit owner)
            {
                gPrevActionStatus = this;
                OnStop(owner);
            }

            protected virtual void OnStop(ComAIUnit owner) { }

            public void Update(ComAIUnit owner, float deltaTime)
            {
                OnUpdate(owner, deltaTime);
            }

            protected virtual void OnUpdate(ComAIUnit owner, float deltaTime) { }

            public virtual int CompareTo(ActionStatus other)
            {
                return this.Priority - other.Priority;
            }
        }

        public class DefinedActionStatus : ActionStatus
        {
            protected UnitActionDefinitionMap.UnitAction mData;
            protected Queue<UnitActionDefinitionMap.UnitActionKeyFrame> mActionQueue = new Queue<UnitActionDefinitionMap.UnitActionKeyFrame>();
            protected UnitActionDefinitionMap.UnitActionKeyFrame mCurrentAction;
            protected int mCurrentPassTime;

            public DefinedActionStatus(UnitActionStatus status, string key, UnitActionDefinitionMap.UnitAction data)
                : base(status, key)
            {
                this.mData = data;
                if (data.ActionQueue.Count > 0)
                {
                    var a = data.ActionQueue[0];
                    this.ActionName = a.ActionName;
                    this.CrossFade = a.CrossFade;
                    this.Speed = a.Speed;
                }
            }

            protected override void OnStart(ComAIUnit owner)
            {
                mActionQueue.Clear();
                for (int i = 0 ; i < mData.ActionQueue.Count ; i++)
                {
                    this.mActionQueue.Enqueue(mData.ActionQueue[i]);
                }
                NextAction(owner);
            }

            protected override void OnUpdate(ComAIUnit owner, float deltaTime)
            {
                this.mCurrentPassTime += (int)(owner.ZUnit.Parent.CurrentIntervalMS);
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
                        this.ActionName = mCurrentAction.ActionName;
                        this.CrossFade = mCurrentAction.CrossFade;
                        this.Speed = mCurrentAction.Speed;
                        owner.animPlayer.speed = Speed;
                        if (CrossFade)
                        {
                            owner.animPlayer.CrossFade(ActionName, 0.15f);
                        }
                        else
                        {
                            owner.animPlayer.Play(ActionName);
                        }
                    }
                }
                mCurrentPassTime = 0;
            }
        }

        public class SkillActionStatus : ActionStatus
        {
            private ZoneUnit.ISkillAction skillAction;

            public SkillActionStatus(UnitActionStatus status, string key) : base(status, key)
            {
            }

            public ZoneUnit.ISkillAction SkillAction
            {
                get { return skillAction; }
            }

            public SkillTemplate Data
            {
                get { return skillAction.SkillData; }
            }

            protected override void OnStart(ComAIUnit owner)
            {
            }

            protected override void OnStop(ComAIUnit owner)
            {
                base.OnStop(owner);
                skillAction = null;
            }

            protected override void OnUpdate(ComAIUnit owner, float delteTime)
            {
            }

            public virtual void ZUnit_OnSkillActionStart(ComAIUnit owner, ZoneUnit.ISkillAction skillAction)
            {
                this.skillAction = skillAction;
                this.ActionName = skillAction.CurrentActionName;
                if (CrossFade)
                {
                    owner.animPlayer.CrossFade(this.ActionName, 0.15f, -1, 0f);
                }
                else
                {
                    owner.animPlayer.Play(this.ActionName, -1, 0f);
                }
            }
        }
    }

}