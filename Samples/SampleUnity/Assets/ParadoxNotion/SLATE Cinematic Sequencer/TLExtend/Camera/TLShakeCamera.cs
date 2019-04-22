using UnityEngine;
using System.Collections;
using Slate;

namespace Slate.ActionClips
{

    [Category("Rendering")]
    [Description("ShakeCamera")]
    public class TLShakeCamera : DirectorActionClip
    {

        private Camera mCamera;
        public Vector3 directionStrength = new Vector3(0, 1, 0);
        public Vector3 RotationStrength = new Vector3(0, 0, 0);
        public float Speed = 1.0f;
        public AnimationCurve PositionCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.13f, 0.4f), new Keyframe(0.33f, -0.33f), new Keyframe(0.5f, 0.17f), new Keyframe(0.71f, -0.12f), new Keyframe(1, 0));
        public AnimationCurve RotationCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1f, 0f));


        public float timer = 1;
        Vector3 shakeRotation;
        Vector3 shakePosition;
        private Vector3 orgPostion;
        private Vector3 orgRotation;
        public override string info
        {
            get { return "TLShakeCamera"; }
        }
        public override float length
        {
            get
            {
                return timer;
            }

            set
            {
                timer = value;
            }
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            mCamera = DirectorCamera.current.cam;
            shakePosition = Vector3.zero;
            shakeRotation = Vector3.zero;
            //orgPostion = mCamera.transform.position;
            //orgRotation = mCamera.transform.rotation.eulerAngles;
        }
        protected override void OnExit()
        {
            base.OnExit();
            shakePosition = Vector3.zero;
            shakeRotation = Vector3.zero;
            //mCamera.transform.position = orgPostion;
            //mCamera.transform.rotation = Quaternion.Euler(orgRotation);
        }
        protected override void OnReverse()
        {
            base.OnReverse();
            shakePosition = Vector3.zero;
            shakeRotation = Vector3.zero;
            //mCamera.transform.position = orgPostion;
            //mCamera.transform.rotation = Quaternion.Euler(orgRotation);
        }
        protected override void OnUpdate(float time)
        {
            base.OnUpdate(time);
            //Debug.Log("timer="+ timer+" time ="+ time);

            float _perTime = time / timer;
            shakePosition = new Vector3(PositionCurve.Evaluate((_perTime) * Speed) * directionStrength.x, PositionCurve.Evaluate((_perTime) * Speed) * directionStrength.y, PositionCurve.Evaluate((_perTime) * Speed) * directionStrength.z);
            mCamera.transform.localPosition = shakePosition;
            shakeRotation = new Vector3(RotationCurve.Evaluate((_perTime) * Speed) * RotationStrength.x, RotationCurve.Evaluate((_perTime) * Speed) * RotationStrength.y, RotationCurve.Evaluate((_perTime) * Speed) * RotationStrength.z);
            mCamera.transform.localRotation = Quaternion.Euler(shakeRotation);

        }
    }
}