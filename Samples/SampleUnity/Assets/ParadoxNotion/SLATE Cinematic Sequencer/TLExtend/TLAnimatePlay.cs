using UnityEngine;
namespace Slate.ActionClips{

    [Attachable(typeof(MecanimTrack))]
    [Description("Play animate")]
    public class TLAnimatePlay : MecanimBaseClip
    {

        [SerializeField] [HideInInspector]
		private float _length = 1f;
        
        
        
        public bool IsCrossFade = false;
        public float GrossFadeTime = 0.15f;
        public int Layer;
        public float Speed = 1f;
        [Range(0,1)]
        public float weight = 1;
        public AnimationClip BaseAnimationClip;


        private AnimationClip lastAnimationClip;
        private bool lastIsCrossFade;
        private float lastGrossFadeTime;
        private int lastLayer;
        private float lastweight;
        private float lastSpeed = 1;

        public override bool isValid
        {
            get {
                return base.isValid;
            }
        }

        public override string info
        {
            get {
                if (BaseAnimationClip == null)
                {
                    return string.Format("缺少动画文件");
                }
                return string.Format("'{0}' ", BaseAnimationClip.name);
            }
        }

        public override float length{
			get {return _length;}
			set {_length = value;}
		}

       

        public bool PlayAnim()
        {
            if (actor != null && Layer >= 0)
            {
                actor.SetLayerWeight(Layer, weight);

                if (IsCrossFade)
                {
                    actor.CrossFade(BaseAnimationClip.name, GrossFadeTime, Layer,0);
                }
                else
                {
                    actor.Play(BaseAnimationClip.name, Layer,0);
                }


                return true;
            }
            return false;
        }
        protected override void OnEnter(){
            lastAnimationClip = BaseAnimationClip;
            lastIsCrossFade = IsCrossFade;
            lastGrossFadeTime = GrossFadeTime;
            lastLayer = Layer;
            lastweight = weight;
            lastSpeed = Speed;
            PlayAnim();
        }

		protected override void OnUpdate(float time){
            //actor.SetBool(parameterName, value);
            //stateName = laststateName;
            //IsCrossFade = lastIsCrossFade;
            //GrossFadeTime = lastGrossFadeTime;
            //Layer = lastLayer;
            //weight = lastweight;
            if (actor.enabled)
            {
                actor.SetLayerWeight(Layer, Mathf.Lerp(weight, weight, GetClipWeight(time)));
                actor.speed = Speed;
            }
        }

		protected override void OnExit(){
			if (length > 0){
                BaseAnimationClip = lastAnimationClip;
                IsCrossFade = lastIsCrossFade;
                GrossFadeTime = lastGrossFadeTime;
                Layer = lastLayer;
                weight = lastweight;
                Speed = lastSpeed;
                actor.SetLayerWeight(Layer, weight);
                actor.speed = Speed;
                //actor.SetBool(parameterName, lastValue);
            }
		}

		protected override void OnReverse(){
			if (Application.isPlaying){
                actor.SetLayerWeight(lastLayer, lastweight);
                actor.speed = Speed;
                //actor.SetBool(parameterName, lastValue);
            }
		}
    }
}