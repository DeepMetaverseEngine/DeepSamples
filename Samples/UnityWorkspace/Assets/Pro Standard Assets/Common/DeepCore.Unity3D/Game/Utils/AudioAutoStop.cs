using UnityEngine;
using System.Collections;

namespace DeepCore.Unity3D.Utils
{
    public class AudioAutoStop : MonoBehaviour
    {

        public DefaultAudio Auido;
        public float Duration;
        public GameObject TraceTarget;
        private bool mStoped;
        private System.Action<DefaultAudio> mCallBack;
        
        public void setCallBack(System.Action<DefaultAudio> CallBack)
        {
            this.mCallBack = CallBack;
        }
        // Update is called once per frame
        void Update()
        {
            if (!mStoped)
            {
                this.Duration -= Time.deltaTime;
                if (this.Duration <= 0)
                {
                    mStoped = true;
                    this.Auido.Stop();
                    if (mCallBack != null)
                    {
                        mCallBack(Auido);
                    }
                    Destroy(this);
                    return;
                }

                if (TraceTarget != null)
                {
                    transform.position = TraceTarget.Position();
                }
            }
        }
    }

}
