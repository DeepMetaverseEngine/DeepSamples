#if UNITY_5_4_OR_NEWER

using Slate.ActionClips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_2017_1_OR_NEWER
using UnityEngine.Animations;
using UnityEngine.Playables;
#else
using UnityEngine.Experimental.Director;
#endif
namespace Slate
{

    public class TLPlayerAnimatorTrack : AnimatorTrack
    {
        private List<Animator> mAnimatorlist;
        private RuntimeAnimatorController mController;
        private bool mRootMotion;
        private AnimatorCullingMode mCullingMode;
        private bool mEnabled;
        [HideInInspector]
        public object userdata;
#if UNITY_2017_1_OR_NEWER
        //private PlayableGraph graph;
        //private AnimationPlayableOutput animationOutput;
        //private Playable mixerPlayable;
        //private Playable baseClipPlayable;
        private List<PlayableGraph> mGraphList = new List<PlayableGraph>();
        private List<AnimationPlayableOutput> mAnimationOutputList = new List<AnimationPlayableOutput>();
        private List<Playable> mMixerPlayableHandleList = new List<Playable>();
        private List<Playable> mBaseClipPlayableHandleList = new List<Playable>();
#elif UNITY_5_6_OR_NEWER
        private List<PlayableGraph> mGraphList = new List<PlayableGraph>();
        private List<AnimationPlayableOutput> mAnimationOutputList = new List<AnimationPlayableOutput>();
        private List<PlayableHandle> mMixerPlayableHandleList = new List<PlayableHandle>();
        private List<PlayableHandle> mBaseClipPlayableHandleList = new List<PlayableHandle>();
#else
        private List<AnimationMixerPlayable> mMixerPlayableList = new List<AnimationMixerPlayable>();
#endif

        protected override bool OnInitialize()
        {
            return base.OnInitialize();
        }
        protected override void OnEnter()
        {

            if (userdata != null)
            {
                mController = animator.runtimeAnimatorController;
                mRootMotion = animator.applyRootMotion;
                mCullingMode = animator.cullingMode;
                mEnabled = animator.enabled;
                if (userdata != null)
                {
                    mAnimatorlist = actor.GetComponentsInChildren<Animator>().ToList();
                }
                StoreSet();
                CreateAndPlayTree();
                animator.runtimeAnimatorController = mController;
                animator.applyRootMotion = mRootMotion;
                animator.cullingMode = mCullingMode;
                animator.enabled = mEnabled;
                if (useRootMotion)
                {
                    NewBakeRootMotion();
                }
            }
            else
            {
                base.OnEnter();
            }

        }

        public void NewBakeRootMotion(){
			useBakedRootMotion = false;
			animator.applyRootMotion = true;
			rmPositions = new List<Vector3>();
			rmRotations = new List<Quaternion>();
			var lastTime = -1f;
			var updateInterval = (1f/ROOTMOTION_FRAMERATE);
			var tempActiveClips = 0;
			for (var i = startTime; i <= endTime + updateInterval; i += updateInterval){
				foreach(var clip in (this as IDirectable).children){

					if (i >= clip.startTime && lastTime < clip.startTime){
						tempActiveClips++;
						clip.Enter();
					}

					if (i >= clip.startTime && i <= clip.endTime){
						clip.Update(i - clip.startTime, i - clip.startTime - updateInterval);
					}

					if ( (i > clip.endTime || i >= this.endTime) && lastTime <= clip.endTime){
						tempActiveClips--;
						clip.Exit();
					}
				}

				if (tempActiveClips > 0){
#if UNITY_5_6_OR_NEWER
                    foreach (var graph in mGraphList)
                    {
                        graph.Evaluate(updateInterval);
                    }
					#else
					animator.Update(updateInterval);
					#endif
				}

               
                rmPositions.Add(animator.transform.localPosition);
                rmRotations.Add(animator.transform.localRotation);
				lastTime = i;
			}
            animator.applyRootMotion = false;
           
			useBakedRootMotion = true;
		}

        protected override void OnUpdate(float time, float previousTime)
        {
            if (mAnimatorlist == null || mAnimatorlist.Count == 0)
            {
                base.OnUpdate(time, previousTime);
                return;
            }
#if UNITY_2017_1_OR_NEWER

            //if (!graph.IsValid())
            //{
            //    return;
            //}

            //baseClipPlayable.SetTime(time * basePlaybackSpeed);
            //graph.Evaluate(0);
            foreach (var graph in mGraphList)
            {
                if (!graph.IsValid())
                {
                    return;
                }
            }
            int i = 0;
            foreach (var graph in mGraphList)
            {
                var baseClipPlayable = mBaseClipPlayableHandleList[i++];
                baseClipPlayable.SetTime(time * basePlaybackSpeed);
                graph.Evaluate(0);
            }

#elif UNITY_5_6_OR_NEWER

            foreach (var graph in mGraphList)
            {
                if (!graph.IsValid())
                {
                    return;
                }
            }
            int i = 0;
            foreach(var graph in mGraphList)
            {
                var baseClipPlayableHandle = mBaseClipPlayableHandleList[i++];
                baseClipPlayableHandle.time = time * basePlaybackSpeed;
                graph.Evaluate(0);
            }

#else
            foreach (var mixerPlayable in mMixerPlayableList)
            {
                if (!mixerPlayable.IsValid())
                {
                    return;
                }
            }
            int i = 0;
            foreach (var animator in mAnimatorlist)
            {
                if (!animator.isInitialized)
                {
                    animator.Play(mMixerPlayableList[i]);
                }
                i++;
            }


            if (baseAnimationClip != null)
            {
                i = 0;
                foreach (var animator in mAnimatorlist)
                {
                    var basePlayable = mMixerPlayableList[i].GetInput(0);
                    basePlayable.time = time * basePlaybackSpeed;
                    mMixerPlayableList[i].SetInput(basePlayable, 0);
                    i++;
                    animator.Update(0);
                }

            }
#endif

        }
        public override void CreateAndPlayTree()
        {
            if (mAnimatorlist == null || mAnimatorlist.Count == 0)
            {
                base.CreateAndPlayTree();
                return;
            }
            NewCreateAndPlayTree();
        }

        void NewCreateAndPlayTree()
        {
#if UNITY_2017_1_OR_NEWER

            //var clipActions = actions.OfType<PlayAnimatorClip>().ToList();
            //var inputCount = 1 + clipActions.Count;
            //ports = new Dictionary<PlayAnimatorClip, int>();
            //graph = PlayableGraph.Create();
            //mixerPlayable = AnimationMixerPlayable.Create(graph, inputCount, true);
            //mixerPlayable.SetInputWeight(0, 1f);
            //baseClipPlayable = AnimationClipPlayable.Create(graph, baseAnimationClip);
            //baseClipPlayable.SetPlayState(PlayState.Paused);
            //graph.Connect(baseClipPlayable, 0, mixerPlayable, 0);

            //var index = 1; //0 is baseclip
            //foreach (var playAnimClip in clipActions)
            //{
            //    var clipPlayable = AnimationClipPlayable.Create(graph, playAnimClip.animationClip);
            //    graph.Connect(clipPlayable, 0, mixerPlayable, index);
            //    mixerPlayable.SetInputWeight(index, 0f);
            //    ports[playAnimClip] = index;
            //    clipPlayable.SetPlayState(PlayState.Paused);
            //    index++;
            //}

            //animationOutput = AnimationPlayableOutput.Create(graph, "Animation", animator);
            //animationOutput.SetSourcePlayable(mixerPlayable);
            //mixerPlayable.SetPlayState(PlayState.Paused);
            //graph.Play();

            var clipActions = actions.OfType<TLPlayAnimatorClip>().ToList();
            var inputCount = 1 + clipActions.Count;
            ports = new Dictionary<PlayAnimatorClip, int>();
            mGraphList.Clear();
            mAnimationOutputList.Clear();
            mMixerPlayableHandleList.Clear();
            mBaseClipPlayableHandleList.Clear();
            foreach (var animator in mAnimatorlist)
            {
                var graph = PlayableGraph.Create();
                var mixerPlayableHandle = AnimationMixerPlayable.Create(graph,inputCount, true);
                mixerPlayableHandle.SetInputWeight(0, 1f);
                var baseClipPlayableHandle = AnimationClipPlayable.Create(graph,NewGetAnimationClip(animator, baseAnimationClip));
                baseClipPlayableHandle.SetPlayState(PlayState.Paused);
                graph.Connect(baseClipPlayableHandle, 0, mixerPlayableHandle, 0);
                var index = 1; //0 is baseclip
                foreach (var playAnimClip in clipActions)
                {
                    var clipPlayableHandle = AnimationClipPlayable.Create(graph,NewGetAnimationClip(animator, playAnimClip.animationClip));
                    graph.Connect(clipPlayableHandle, 0, mixerPlayableHandle, index);
                    mixerPlayableHandle.SetInputWeight(index, 0f);
                    clipPlayableHandle.SetPlayState(PlayState.Paused);
                    ports[playAnimClip] = index;
                    index++;
                }
                var animationOutput = AnimationPlayableOutput.Create(graph, "Animation", animator);
                animationOutput.SetSourcePlayable (mixerPlayableHandle);
                mixerPlayableHandle.SetPlayState(PlayState.Paused);
                graph.Play();
                mGraphList.Add(graph);
                mMixerPlayableHandleList.Add(mixerPlayableHandle);
                mBaseClipPlayableHandleList.Add(baseClipPlayableHandle);
            }

#elif      UNITY_5_6_OR_NEWER
            var clipActions = actions.OfType<TLPlayAnimatorClip>().ToList();
			var inputCount = 1 + clipActions.Count;
			ports = new Dictionary<PlayAnimatorClip, int>();
            mGraphList.Clear();
            mAnimationOutputList.Clear();
            mMixerPlayableHandleList.Clear();
            mBaseClipPlayableHandleList.Clear();
            foreach (var animator in mAnimatorlist)
            {
                var graph = PlayableGraph.CreateGraph();
                var mixerPlayableHandle = graph.CreateAnimationMixerPlayable(inputCount, true);
                mixerPlayableHandle.SetInputWeight(0, 1f);
                var baseClipPlayableHandle = graph.CreateAnimationClipPlayable(NewGetAnimationClip(animator,baseAnimationClip));
                baseClipPlayableHandle.playState = PlayState.Paused;
                graph.Connect(baseClipPlayableHandle, 0, mixerPlayableHandle, 0);
                var index = 1; //0 is baseclip
                foreach (var playAnimClip in clipActions)
                {
                    var clipPlayableHandle = graph.CreateAnimationClipPlayable(NewGetAnimationClip(animator,playAnimClip.animationClip));
                    graph.Connect(clipPlayableHandle, 0, mixerPlayableHandle, index);
                    mixerPlayableHandle.SetInputWeight(index, 0f);
                    clipPlayableHandle.playState = PlayState.Paused;
                    ports[playAnimClip] = index;
                    index++;
                }
                var animationOutput = graph.CreateAnimationOutput("Animation", animator);
                animationOutput.sourcePlayable = mixerPlayableHandle;
                mixerPlayableHandle.playState = PlayState.Paused;
                graph.Play();
                mGraphList.Add(graph);
                mMixerPlayableHandleList.Add(mixerPlayableHandle);
                mBaseClipPlayableHandleList.Add(baseClipPlayableHandle);
            }
			

			
			

#else

            ports = new Dictionary<PlayAnimatorClip, int>();

            mMixerPlayableList.Clear();
            foreach (var animator in mAnimatorlist)
            {
                var mixerPlayerable = AnimationMixerPlayable.Create();
                mMixerPlayableList.Add(mixerPlayerable);
            }

            int i = 0;
            foreach (var mixerPlayable in mMixerPlayableList)
            {
                var basePlayableClip = AnimationClipPlayable.Create(NewGetAnimationClip(mAnimatorlist[i], baseAnimationClip));
                basePlayableClip.state = PlayState.Paused;
                mixerPlayable.AddInput(basePlayableClip);
                i++;
            }

            foreach (var playAnimClip in actions.OfType<TLPlayAnimatorClip>())
            {
                i = 0;
                int index = 0;
                foreach (var mixerPlayable in mMixerPlayableList)
                {
                    var playableClip = AnimationClipPlayable.Create(NewGetAnimationClip(mAnimatorlist[i], playAnimClip.animationClip));
                    playableClip.state = PlayState.Paused;
                    index = mixerPlayable.AddInput(playableClip);
                    mixerPlayable.SetInputWeight(index, 0f);
                    i++;
                }
                ports[playAnimClip] = index;
            }
            i = 0;
            foreach (var animator in mAnimatorlist)
            {
                animator.SetTimeUpdateMode(DirectorUpdateMode.Manual);
                var mixerPlayable = mMixerPlayableList[i];
                animator.Play(mixerPlayable);
                mixerPlayable.state = PlayState.Paused;
                i++;
            }
#endif

            // GraphVisualizerClient.Show(graph, animator.name);
        }

        public void NewEnableClip(TLPlayAnimatorClip playAnimClip)
        {

            if (mAnimatorlist == null || mAnimatorlist.Count == 0)
            {
                EnableClip(playAnimClip);
                return;
            }

#if UNITY_5_6_OR_NEWER

            foreach (var graph in mGraphList)
            {
                if (!graph.IsValid())
                { 
                    return;
                }
            }
#else

            foreach (var mixerPlayable in mMixerPlayableList)
            {
                if (!mixerPlayable.IsValid())
                {
                    return;
                }
            }
#endif

            activeClips++;
            var index = ports[playAnimClip];
            var weight = playAnimClip.GetClipWeight();

#if UNITY_5_6_OR_NEWER
            foreach (var mixerPlayableHandle in mMixerPlayableHandleList)
            {
                mixerPlayableHandle.SetInputWeight(0, activeClips == 2 ? 0 : 1 - weight);
                mixerPlayableHandle.SetInputWeight(index, weight);
            }
               
#else
            foreach (var mixerPlayable in mMixerPlayableList)
            {
                mixerPlayable.SetInputWeight(0, activeClips == 2 ? 0 : 1 - weight);
                mixerPlayable.SetInputWeight(index, weight);
            }

#endif
        }


        private AnimationClip NewGetAnimationClip(Animator animator, AnimationClip clip)
        {
            if (animator != null && animator.runtimeAnimatorController != null && clip != null)
            {
                AnimationClip[] pairclips = animator.runtimeAnimatorController.animationClips;
                foreach (var newclip in pairclips)
                {
                    if (newclip.name == clip.name)
                    {
                        return newclip;
                    }
                }
                Debugger.LogWarning("AnimationclipName = [" + clip.name + "] is null" );
            }
            return clip;
        }
        public override void UpdateClip(PlayAnimatorClip playAnimClip, float clipTime, float clipPrevious, float weight)
        {

            //if (AnimatorTrackHandle != null)
            if (mAnimatorlist != null && mAnimatorlist.Count > 0)
            {
                //AnimatorTrackHandle(actor, userdata, playAnimClip.animationClip, clipTime, clipPrevious, weight);
                //if (animator == null)
                //{
                //    return;
                //}

#if UNITY_5_6_OR_NEWER
                foreach (var graph in mGraphList)
                {
                    if (!graph.IsValid())
                    {
                        return;
                    }
                }
#else
                foreach (var mixerPlayable in mMixerPlayableList)
                {
                    if (!mixerPlayable.IsValid())
                    {
                        return;
                    }
                }

#endif

                var index = ports[playAnimClip];
      
#if UNITY_5_6_OR_NEWER
                foreach (var graph in mGraphList)
                {
                    if (!graph.IsValid())
                    {
                        return;
                    }
                }
                foreach (var mixerPlayableHandle in mMixerPlayableHandleList)
                {
                    var clipPlayable = mixerPlayableHandle.GetInput(index);
#if UNITY_2017_1_OR_NEWER
                    clipPlayable.SetTime(clipTime);
#else 
                    clipPlayable.time = clipTime;
#endif
                    mixerPlayableHandle.SetInputWeight(index, weight);
                    mixerPlayableHandle.SetInputWeight(0, activeClips == 2 ? 0 : 1 - weight);
                }
                  
#else
                foreach (var mixerPlayable in mMixerPlayableList)
                {
                    var clipPlayable = mixerPlayable.GetInput(index);
                    clipPlayable.time = clipTime;
                    mixerPlayable.SetInput(clipPlayable, index);
                    mixerPlayable.SetInputWeight(index, weight);
                    mixerPlayable.SetInputWeight(0, activeClips == 2 ? 0 : 1 - weight);
                }

#endif
                }
            else
            {
                base.UpdateClip(playAnimClip, clipTime, clipPrevious, weight);
            }

        }

        public override void DisableClip(PlayAnimatorClip playAnimClip)
        {
            if (mAnimatorlist == null || mAnimatorlist.Count == 0)
            {
                base.DisableClip(playAnimClip);
                return;
            }
            if (animator == null)
            {
                return;
            }

#if UNITY_5_6_OR_NEWER
            foreach (var graph in mGraphList)
            {
                if (!graph.IsValid())
                {
                    return;
                }
            }
#else
            foreach (var mixerPlayable in mMixerPlayableList)
            {
                if (!mixerPlayable.IsValid())
                {
                    return;
                }
            }
#endif


            activeClips--;
            activeClips = Math.Max(activeClips, 0);
            var index = ports[playAnimClip];

#if UNITY_5_6_OR_NEWER
            foreach (var mixerPlayableHandle in mMixerPlayableHandleList)
            {
                mixerPlayableHandle.SetInputWeight(0, activeClips == 0 ? 1 : 0);
                mixerPlayableHandle.SetInputWeight(index, 0);
            }

#else
            foreach (var mixerPlayable in mMixerPlayableList)
            {
                mixerPlayable.SetInputWeight(0, activeClips == 0 ? 1 : 0);
                mixerPlayable.SetInputWeight(index, 0);
            }
#endif
        }

    }

}

#endif