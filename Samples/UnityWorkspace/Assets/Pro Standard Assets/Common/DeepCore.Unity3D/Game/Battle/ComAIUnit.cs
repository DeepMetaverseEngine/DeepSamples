using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Data;
using DeepCore.GameData.Zone;
using DeepCore.GameData.ZoneClient;
using DeepCore.GameSlave;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public enum WeaponPart
    {
        WEAPON_LEFT,
        WEAPON_RIGHT,
    }
    public partial class ComAIUnit : ComAICell
    {
        private GameObject mAvatarRoot;
        private float mBodyScale = 1;

        protected ZoneUnit ZUnit { get { return ZObj as ZoneUnit; } }
        public bool IsActive { get { return this.ZUnit.IsActive; } }
        public int TemplateID { get { return this.ZUnit.TemplateID; } }
        public string TemplateName { get { return this.ZUnit.TemplateName; } }
        public string PlayerUUID { get { return this.ZUnit.PlayerUUID; } }
        public string DisplayName { get { return this.ZUnit.DisplayName; } }
        public byte Force { get { return this.ZUnit.Force; } }
        public int MaxHP { get { return this.ZUnit.MaxHP; } }
        public int HP { get { return this.ZUnit.HP; } }
        public int MaxMP { get { return this.ZUnit.MaxMP; } }
        public int MP { get { return this.ZUnit.MP; } }
        public int Dummy_0 { get { return this.ZUnit.Dummy_0; } }
        public int Dummy_1 { get { return this.ZUnit.Dummy_1; } }
        public int Dummy_2 { get { return this.ZUnit.Dummy_2; } }
        public int Dummy_3 { get { return this.ZUnit.Dummy_3; } }
        public int Dummy_4 { get { return this.ZUnit.Dummy_4; } }
        public uint CurrentTarget { get { return this.ZUnit.CurrentTarget; } }
        public UnitActionStatus CurrentState { get { return this.ZUnit.CurrentState; } }
        public UnitInfo Info { get { return this.ZUnit.Info; } }
        public SyncUnitInfo SyncInfo { get { return this.ZUnit.SyncInfo; } }

        public T GetZoneUnitObject<T>(string fieldName)
        {
            var f = typeof(ZoneUnit).GetField(fieldName);
            if (f != null)
            {
                return (T)f.GetValue(ZUnit);
            }
            var f1 = typeof(ZoneUnit).GetProperty(fieldName);
            if (f1 != null)
            {
                return (T)f1.GetValue(ZUnit, null);
            }
            return default(T);
        }

        public void SetDirection(float direction, bool smooth = true)
        {
            this.ZUnit.SetDirection(direction, smooth);
        }

        public UnitInfo CloneUnitInfo()
        {
            return this.ZUnit.Info.Clone() as UnitInfo;
        }


        public ComAIUnit(BattleScene battleScene, ZoneUnit obj)
            : base(battleScene, obj)
        {
            mAvatarRoot = new GameObject("AvatarRoot");
            mAvatarRoot.ParentRoot(this.DisplayRoot);

            if (ZUnit.Info != null && ZUnit.Info.BodyScale > 0f)
            {
                mBodyScale = ZUnit.Info.BodyScale;
            }
            this.ObjectRoot.transform.localScale *= mBodyScale;

            InitActionStatus();

            this.ZUnit.OnActionChanged += ZUnit_OnActionChanged;
            this.ZUnit.OnLaunchSkill += ZUnit_OnLaunchSkill;
            this.ZUnit.OnSkillActionChanged += ZUnit_OnSkillActionChanged;
            this.ZUnit.OnSkillActionStart += ZUnit_OnSkillActionStart;

            this.ZUnit.OnBuffAdded += ZUnit_OnBuffAdded;
            this.ZUnit.OnBuffChanged += ZUnit_OnBuffChanged;
            this.ZUnit.OnBuffRemoved += ZUnit_OnBuffRemoved;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            OnLoadModel();
        }

        protected virtual void OnLoadModel()
        {
            FuckAssetLoader.GetOrLoad(GetModelFileName(), System.IO.Path.GetFileNameWithoutExtension(GetModelFileName()), (loader) =>
            {
                if (this.IsDisposed)
                {
                    if (loader.AssetComp)
                    {
                        loader.AssetComp.Unload();
                    }
                    return;
                }
                OnLoadModelFinish(loader.AssetComp);
            });
        }

        protected virtual string GetModelFileName()
        {
            return ZUnit.Info.FileName;
        }

        protected virtual void OnLoadModelFinish(FuckAssetObject aoe)
        {
            if (aoe)
            {
                this.DisplayCell.SetModel(aoe);
                this.animPlayer.AddAnimator(aoe.gameObject.GetComponent<Animator>());
                CorrectDummyNode();
                ChangeAction(this.ZUnit.CurrentState, true);
            }
        }

        protected override void OnDispose()
        {
            this.ObjectRoot.transform.localScale *= 1.0f / mBodyScale;
            this.ZUnit.OnActionChanged -= ZUnit_OnActionChanged;
            this.ZUnit.OnLaunchSkill -= ZUnit_OnLaunchSkill;
            this.ZUnit.OnSkillActionChanged -= ZUnit_OnSkillActionChanged;
            this.ZUnit.OnSkillActionStart -= ZUnit_OnSkillActionStart;

            this.ZUnit.OnBuffAdded -= ZUnit_OnBuffAdded;
            this.ZUnit.OnBuffChanged -= ZUnit_OnBuffChanged;
            this.ZUnit.OnBuffRemoved -= ZUnit_OnBuffRemoved;

            //if (this.ZUnit.Info.RemovedEffect != null)
            //{
            //    PlayEffect(this.ZUnit.Info.RemovedEffect, ObjectRoot.Position(), ObjectRoot.Rotation());
            //}

            foreach (var elem in mBuffs)
            {
                elem.Value.Dispose();
            }
            mBuffs.Clear();

            foreach (var elem in mAvatarStack)
            {
                elem.DisplayCell.Dispose();
            }
            mAvatarStack.Clear();

            base.OnDispose();
        }

        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            UpdateAction(deltaTime);
            UpdateBuff(deltaTime);
        }

        protected virtual void ZUnit_OnLaunchSkill(ZoneUnit unit, ZoneUnit.SkillState skill
            , UnitLaunchSkillEvent evt)
        {
            var act = GetTopActionStatus(UnitActionStatus.Skill) as SkillActionStatus;
            if (act != null)
            {
                act.Skill = skill;
            }
            this.ChangeAction(UnitActionStatus.Skill);
            if (this.CurrentActionStatus is SkillActionStatus)
            {
                (this.CurrentActionStatus as SkillActionStatus).ZUnit_OnLaunchSkill(this, skill, evt);
            }
        }

        protected virtual void ZUnit_OnSkillActionStart(ZoneUnit unit, ZoneUnit.ISkillAction action)
        {
            //if (this.CurrentActionStatus is SkillActionStatus)
            //{
            //    (this.CurrentActionStatus as SkillActionStatus).OnStart(this);
            //}
        }

        protected virtual void ZUnit_OnSkillActionChanged(ZoneUnit unit, ZoneUnit.SkillState skill, byte index)
        {
            if (this.CurrentActionStatus is SkillActionStatus)
            {
                (this.CurrentActionStatus as SkillActionStatus).ZUnit_OnSkillActionChanged(this, index);
            }
        }

        protected virtual void ZUnit_OnActionChanged(ZoneUnit unit, UnitActionStatus status)
        {
            //技能不处理
            if (status != UnitActionStatus.Skill)
            {
                //Debug.LogError("----------" + status);
                this.ChangeAction(status);
            }
        }
    }
}