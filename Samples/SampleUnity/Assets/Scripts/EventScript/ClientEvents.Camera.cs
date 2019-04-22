using DeepCore;
using DeepCore.GameEvent;
using DeepCore.GameEvent.Events;
using DeepCore.Reflection;
using System;
using UnityEngine;
using DG.Tweening;

namespace EventScript.Client.Events
{
    [Event("临时设置主镜头参数", "Camera.Async")]
    public class SetArgumentOnceEvent : CustomEvent
    {
        private static readonly Vector3 InvalidV3 = new Vector3(-999, -999, -999);
        [EventArgument("动态参数", 0)] public SceneCamera.CameraArgument CameraArg;
        [EventArgument("坐标偏移", 1)] public Vector3 PostionOffset = InvalidV3;
        [EventArgument("角度偏移", 2)] public Vector3 AngleOffset = InvalidV3;

        private SceneCamera.CameraArgument mLastArgument;
        private Vector3 mLastOffset;
        private Vector3 mLastAngle;


        protected override void OnStart()
        {
            base.OnStart();
            mLastArgument = GameSceneMgr.Instance.SceneCameraNode.CurrentArgument;
            mLastOffset = GameSceneMgr.Instance.SceneCameraNode.positionNode.localPosition;
            mLastAngle = GameSceneMgr.Instance.SceneCameraNode.pitchNode.localEulerAngles;
            GameSceneMgr.Instance.SceneCameraNode.SetCameraArgument(CameraArg);
            if (!PostionOffset.Equals(InvalidV3))
            {
                GameSceneMgr.Instance.SceneCameraNode.SetCameraOffset(PostionOffset);
            }

            if (!AngleOffset.Equals(InvalidV3))
            {
                GameSceneMgr.Instance.SceneCameraNode.SetCameraAngle(AngleOffset);
            }
        }

        protected override void OnStop()
        {
            GameSceneMgr.Instance.SceneCameraNode.SetCameraOffset(mLastOffset);
            GameSceneMgr.Instance.SceneCameraNode.SetCameraAngle(mLastAngle);
            GameSceneMgr.Instance.SceneCameraNode.SetCameraArgument(mLastArgument);
            base.OnStop();
        }
    }

    [Event("设置主镜头参数", "Camera.Sync")]
    public class SetArgumentEvent : CustomEvent
    {
        private static readonly Vector3 InvalidV3 = new Vector3(-999, -999, -999);
        [EventArgument("动态参数", 0)] public SceneCamera.CameraArgument CameraArg;
        [EventArgument("坐标偏移", 1)] public Vector3 PostionOffset = InvalidV3;
        [EventArgument("角度偏移", 2)] public Vector3 AngleOffset = InvalidV3;


        public SetArgumentEvent()
        {
            CameraArg = GameSceneMgr.Instance.SceneCameraNode.CurrentArgument.Clone();
        }

        protected override void OnStart()
        {
            base.OnStart();
            GameSceneMgr.Instance.SceneCameraNode.SetCameraArgument(CameraArg);
            if (!PostionOffset.Equals(InvalidV3))
            {
                GameSceneMgr.Instance.SceneCameraNode.SetCameraOffset(PostionOffset);
            }

            if (!AngleOffset.Equals(InvalidV3))
            {
                GameSceneMgr.Instance.SceneCameraNode.SetCameraAngle(AngleOffset);
            }

            Stop(true);
        }
    }


    [Event("重置主镜头参数", "Camera.Sync")]
    public class ResetArgumentEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            GameSceneMgr.Instance.SceneCameraNode.SetCameraArgument(GameSceneMgr.Instance.SceneCameraNode.SourceArgument);
            Stop(true);
        }
    }

    [Event("重置主镜头参数", "Camera.Sync")]
    public class SetLocationEvent : CustomEvent
    {
        [EventArgument("坐标", 0)] public Vector3 Pos;
        [EventArgument("角度", 1)] public Vector3 Angle = Vector3.zero;
        [EventArgument("缩放", 2)] public Vector3 Scale = Vector3.one;

        protected override void OnStart()
        {
            base.OnStart();
            GameSceneMgr.Instance.SceneCameraNode.SetCameraLocation(Pos, Angle, Scale);
            Stop(true);
        }
    }

    [Event("停止跟随主角", "Camera.Sync")]
    public class StopFollowActorEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(null);
            Stop(true);
        }
    }


    [Event("重置主镜头参数", "Camera.Sync")]
    public class ResetLocationEvent : CustomEvent
    {
        [EventArgument("坐标", 0)] public Vector3 Pos;
        [EventArgument("角度", 1)] public Vector3 Angle = Vector3.zero;
        [EventArgument("缩放", 2)] public Vector3 Scale = Vector3.one;

        protected override void OnStart()
        {
            base.OnStart();
            GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(TLBattleScene.Instance.Actor.ObjectRoot.transform, true);
            GameSceneMgr.Instance.SceneCameraNode.Reset();
            Stop(true);
        }
    }

    [Event("镜头移动到", "Camera.Async")]
    public class MoveToEvent : CustomEvent
    {
        [EventArgument("Pos", 0)] public Vector3 TargetPos;
        [EventArgument("Duration", 1)] public float Duration;

        private Tweener mTweener;

        protected override void OnStart()
        {
            mTweener = GameSceneMgr.Instance.SceneCameraNode.DoMoveTo(TargetPos, Duration);
            mTweener.OnComplete(MoveComplete);
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!mTweener.IsComplete())
            {
                mTweener.Complete(false);
            }
        }

        private void MoveComplete()
        {
            Stop(true);
        }
    }

    [Event("镜头移动和设置", "Camera.Async")]
    public class MoveToAndSetEvent : CustomEvent
    {
        [EventArgument("Pos", 0)] public Vector3 TargetPos;
        [EventArgument("Duration", 1)] public float Duration;

        private Tweener mTweener;

        protected override void OnStart()
        {
            mTweener = GameSceneMgr.Instance.SceneCameraNode.DoMoveToAndSet(TargetPos, Duration);
            mTweener.OnComplete(MoveComplete);
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!mTweener.IsComplete())
            {
                mTweener.Complete(false);
            }
        }

        private void MoveComplete()
        {
            Stop(true);
        }
    }

    [Event("镜头旋转", "Camera.Async")]
    public class RotateToEvent : CustomEvent
    {
        [EventArgument("Pos", 0)] public Vector3 TargetPos;
        [EventArgument("Duration", 1)] public float Duration;

        private Tweener mTweener;

        protected override void OnStart()
        {
            mTweener = GameSceneMgr.Instance.SceneCameraNode.DoRotate(TargetPos, Duration);
            mTweener.OnComplete(MoveComplete);
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!mTweener.IsComplete())
            {
                mTweener.Complete(false);
            }
        }

        private void MoveComplete()
        {
            Stop(true);
        }
    }

    [Event("镜头旋转和设置", "Camera.Async")]
    public class RotateToAndSetEvent : CustomEvent
    {
        [EventArgument("Pos", 0)] public Vector3 TargetPos;
        [EventArgument("Duration", 1)] public float Duration;

        private Tweener mTweener;

        protected override void OnStart()
        {
            mTweener = GameSceneMgr.Instance.SceneCameraNode.DoRotateAndSet(TargetPos, Duration);
            mTweener.OnComplete(MoveComplete);
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (!mTweener.IsComplete())
            {
                mTweener.Complete(false);
            }
        }

        private void MoveComplete()
        {
            Stop(true);
        }
    }


    [Event("主镜头跟随某单位", "Camera.Sync")]
    public class FollowCGUnitEvent : CustomEvent
    {
        [EventArgument("ID", 0)] public int EntityID;

        protected override void OnStart()
        {
            base.OnStart();
            var context = Mgr.GetObject<DramaContext>("Context");
            if (context == null)
            {
                Stop(false, "context null");
                return;
            }

            var entity = context.GetEntity(EntityID);
            if (entity == null || entity.UnityObject == null)
            {
                Stop(false, "Entity model not found");
                return;
            }

            GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(entity.UnityObject.Obj.transform, true);
            Stop(true);
        }
    }

    [Event("主镜头跟随某玩家", "Camera.Sync")]
    public class FollowPlayerEvent : CustomEvent
    {
        [EventArgument("PlayerUUID", 0)] public string PlayerUUID;

        protected override void OnStart()
        {
            base.OnStart();
            var p = TLBattleScene.Instance.GetAIPlayer(PlayerUUID);
            if (p != null)
            {
                GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(p.ObjectRoot.transform, true);
            }

            Stop(true);
        }
    }

    [Event("主镜头跟随某UNIT", "Camera.Sync")]
    public class FollowTargetUnitEvent : CustomEvent
    {
        [EventArgument("ObjectID", 0)] public uint ObjectID;

        protected override void OnStart()
        {
            base.OnStart();
            var p = TLBattleScene.Instance.GetBattleObject(ObjectID);
            if (p != null)
            {
                GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(p.ObjectRoot.transform, true);
            }

            Stop(true);
        }
    }

    [Event("主镜头跟随某玩家", "Camera.Sync")]
    public class FollowActorEvent : CustomEvent
    {
        protected override void OnStart()
        {
            base.OnStart();
            if (TLBattleScene.Instance != null && TLBattleScene.Instance.Actor != null)
            {
                GameSceneMgr.Instance.SceneCameraNode.SetFollowTarget(TLBattleScene.Instance.Actor.ObjectRoot.transform, true);
            }

            Stop(true);
        }
    }
}