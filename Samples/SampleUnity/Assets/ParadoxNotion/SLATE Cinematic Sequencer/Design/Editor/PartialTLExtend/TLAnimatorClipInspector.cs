#if UNITY_EDITOR && UNITY_5_4_OR_NEWER
using Slate.ActionClips;
using UnityEditor;
using UnityEngine;
namespace Slate{

	[CustomEditor(typeof(TLAnimatePlay))]
	public class TLAnimatorClipInspector : ActionClipInspector<TLAnimatePlay> {

		public override void OnInspectorGUI(){

			base.OnInspectorGUI();
			if (action.BaseAnimationClip != null && GUILayout.Button("Set at Clip Length")){
				action.length = action.BaseAnimationClip.length;	
			}
		}
	}
}

#endif