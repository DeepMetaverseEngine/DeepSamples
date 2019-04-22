using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace uTools {
	[AddComponentMenu("uTools/Tween/Tween PositionX(uTools)")]
	public class uTweenPositionX : uTweener {
		
		public float from;
		public float to;
        public float randomRange = 0;
		
		RectTransform mRectTransform;

        public RectTransform cachedRectTransform { get { if (mRectTransform == null) mRectTransform = GetComponent<RectTransform>(); return mRectTransform;}}
		public float value {
			get { return cachedRectTransform.anchoredPosition.x;}
			set { cachedRectTransform.anchoredPosition = new Vector3(value, cachedRectTransform.anchoredPosition.y);}
		}

        protected override void OnStart()
        {
            float i = Random.Range(-1, 1);
            int mul = 1;
            if (randomRange == 0)
                mul = 0;
            else if (i < 0)
                mul = -1;
            float offset = mul * randomRange;
            from = from + offset;
            to = to + offset;
        }

        protected override void OnUpdate (float factor, bool isFinished)
		{
			value = from + factor * (to - from);
		}
		
		public static uTweenPositionX Begin(GameObject go, float from, float to, float duration = 1f, float delay = 0f) {
			uTweenPositionX comp = uTweener.Begin<uTweenPositionX>(go, duration);
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
