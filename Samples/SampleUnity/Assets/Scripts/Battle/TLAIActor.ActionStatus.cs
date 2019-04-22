
using DeepCore.GameData.Data;
using DeepCore.GameSlave;
using DeepCore.Unity3D.Battle;
using DeepCore.Unity3D.Utils;
using SLua;
using System.Collections;
using System.Collections.Generic;
using TLBattle.Common.Plugins;
using UnityEngine;

public partial class TLAIActor
{
    protected override void InitActionStatus()
    {
        base.InitActionStatus();
        RegistAction(UnitActionStatus.Skill, new TLActorSkillActionStatus(UnitActionStatus.Skill, "TL_actor_skill"));
    }

    [DoNotToLua]
    public class TLActorSkillActionStatus : TLSkillActionStatus
    {
        private Vector3 mCameraForward;
        private Quaternion mCameraRot;
        private bool mFaceToTarget;
        private int mPreSkillID;
        private uint mPreTargetUUID;


        public TLActorSkillActionStatus(UnitActionStatus status, string key) : base(status, key)
        {
        }

        protected override void OnStart(ComAIUnit owner)
        {
            base.OnStart(owner);
        }
        protected override void OnStop(ComAIUnit owner)
        {
            base.OnStop(owner);
            var actor = owner as TLAIActor;
            actor.LastSpellTargetUUID = 0;
            if (actor.HumanFocus != null)
                actor.HumanFocus.Foucs = Vector3.zero;
        }
        protected override void OnUpdate(ComAIUnit owner, float deltaTime)
        {
            //mCameraForward = Camera.main.gameObject.Forward();
            //mCameraRot = Camera.main.gameObject.Rotation();

            var actor = owner as TLAIActor;
            //放完技能后角度修正，这个版本不用了，by67
            //if (!actor.IsPosChanged.Equals(Vector3.zero))
            //{
            //    var degree = Quaternion.Angle(mCameraRot, Extensions.ZoneRot2UnityRot(actor.mAngle));
            //    if (degree > 110f && degree < 250f)
            //    {
            //        actor.mFaceto = Extensions.UnityRot2ZoneRot(mCameraRot);
            //    }
            //    else
            //    {
            //        actor.mFaceto = actor.mAngle;
            //    }
            //}
            //else
            //{
            //    actor.mFaceto = Extensions.UnityRot2ZoneRot(mCameraRot);
            //}

            base.OnUpdate(owner, deltaTime);

            if (mFaceToTarget && actor.LastSpellTargetUUID != 0 && actor.HumanFocus != null)
            {
                var unit = actor.BattleScene.GetBattleObject(actor.LastSpellTargetUUID);
                if (unit != null)
                {
                    actor.HumanFocus.Foucs = unit.Position;
                }
                else
                {
                    actor.HumanFocus.Foucs = Vector3.zero;
                }
            }
        }

        public override void ZUnit_OnSkillActionStart(ComAIUnit owner, ZoneUnit.ISkillAction skillAction)
        {
            var Data = skillAction.SkillData;

            if (mPreSkillID != Data.ID)
            {
                mPreSkillID = Data.ID;
            }
            var actor = owner as TLAIActor;
            if (actor.IsPosChanged.Equals(Vector3.zero) && actor.LastSpellTargetUUID != 0)
            {
                if (mPreTargetUUID != actor.LastSpellTargetUUID)
                {

                    mPreTargetUUID = actor.LastSpellTargetUUID;
                }

                //bool targetAlive = false;
                //var targetPos = actor.GetLastSpellTargetPos(out targetAlive);
                //if (targetAlive)
                //{
                //    var toTarget = targetPos - actor.ObjectRoot.transform.position;
                //    toTarget.y = 0;
                //    toTarget = toTarget.normalized;
                //    var camForward = Camera.main.transform.forward;
                //    camForward.y = 0;
                //    camForward = camForward.normalized;
                //    var degree = Vector3.Angle(toTarget, camForward);
                //}
            }

            base.ZUnit_OnSkillActionStart(owner, skillAction);
        }
    }

}
