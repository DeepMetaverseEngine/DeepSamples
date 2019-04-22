using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween Position(uTools)")]
	public class uTweenPosition : uTweener {
		
		public Vector3 from;
		public Vector3 to;
        public Vector3 randomRange = Vector3.zero;
		
		RectTransform mRectTransform;

        public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform;}}
		public Vector3 value {
			get { return cachedRectTransform.localPosition;}
			set { cachedRectTransform.localPosition = value;}
		}

        protected override void OnStart()
        {
            float i = Random.Range(-1, 1);
            int mul = 1;
            if (randomRange == Vector3.zero)
                mul = 0;
            else if (i < 0)
                mul = -1;
            Vector3 offset = new Vector3(mul * randomRange.x, mul * randomRange.y, mul * randomRange.z);
            from = from + offset;
            to = to + offset;
        }

        protected override void OnUpdate (float factor, bool isFinished)
		{
			value = from + factor * (to - from);
		}
		
		public static uTweenPosition Begin(GameObject go, Vector3 from, Vector3 to, float duration = 1f, float delay = 0f) {
			uTweenPosition comp = uTweener.Begin<uTweenPosition>(go, duration);
            comp.from = from;
            comp.to = to;
            comp.duration = duration;
            comp.delay = delay;
			if (duration <= 0) {
				comp.Sample(1, true);
				comp.enabled = false;
			}
			return comp;
		}

		[ContextMenu("Set 'From' to current value")]
		public override void SetStartToCurrentValue () { from = value; }
		
		[ContextMenu("Set 'To' to current value")]
		public override void SetEndToCurrentValue () { to = value; }
		
		[ContextMenu("Assume value of 'From'")]
		public override void SetCurrentValueToStart () { value = from; }
		
		[ContextMenu("Assume value of 'To'")]
		public override void SetCurrentValueToEnd () { value = to; }

	}
}
