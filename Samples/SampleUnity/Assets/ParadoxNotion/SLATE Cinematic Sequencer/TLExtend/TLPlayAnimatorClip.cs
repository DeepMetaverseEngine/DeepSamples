#if UNITY_5_4_OR_NEWER

using UnityEngine;
using System.Collections;

namespace Slate.ActionClips{

    [Name("TLAnimation Clip")]
    [Attachable(typeof(TLPlayerAnimatorTrack))]
    public class TLPlayAnimatorClip : PlayAnimatorClip
    {
        private TLPlayerAnimatorTrack track { get { return (TLPlayerAnimatorTrack)parent; } }
        
        private Animator animator { get { return track.animator; } }

        protected override void OnEnter()
        {
            //wasRotation = (Vector2)animator.transform.GetLocalEulerAngles();
            track.NewEnableClip(this);
        }
        public override float blendIn
        {
            get
            {
                return base.blendIn;
            }

            set
            {
                base.blendIn = value;
            }
        }

        public override float blendOut
        {
            get
            {
                return base.blendOut;
            }

            set
            {
                base.blendOut = value;
            }
        }
        protected override void OnReverseEnter() 
		{
            track.NewEnableClip(this);
        }

        protected override void OnUpdate(float time, float previousTime)
        {

            //if (track.useRootMotion && steerLocalRotation != default(Vector2))
            //{
            //    var rot = wasRotation + (Vector3)steerLocalRotation;
            //    animator.transform.SetLocalEulerAngles(rot);
            //}

            track.UpdateClip(this, (time - clipOffset) * playbackSpeed, (previousTime - clipOffset) * playbackSpeed, GetClipWeight(time));
        }

        protected override void OnExit()
        {
            track.DisableClip(this);
        }
        protected override void OnReverse()
        {
            track.DisableClip(this);
        }


        ////////////////////////////////////////
        ///////////GUI AND EDITOR STUFF/////////
        ////////////////////////////////////////
#if UNITY_EDITOR

        protected override void OnClipGUI(Rect rect)
        {
            if (animationClip != null)
            {
                EditorTools.DrawLoopedLines(rect, animationClip.length / playbackSpeed, this.length, clipOffset);
            }
        }

#endif
    }
}

#endif