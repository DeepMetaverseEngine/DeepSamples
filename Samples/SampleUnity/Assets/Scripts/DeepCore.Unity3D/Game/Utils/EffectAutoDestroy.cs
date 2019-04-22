using DeepCore.Unity3D.Battle;
using System.Collections.Generic;
using UnityEngine;

namespace DeepCore.Unity3D.Utils
{
    /// <summary>
    /// 需要保证其他地方不会处理对象de删除操作
    /// </summary>
    public class EffectAutoDestroy : MonoBehaviour
    {
        public FuckAssetObject aoeHandler;

        private float mDuration;
        private bool mNearZero;
        public float duration
        {
            get { return mDuration; }
            set
            {
                mDuration = value;
                if (Extensions.NearlyZero(mDuration))
                {
                    mDuration = 3f;
                    mNearZero = true;
                }
                else
                {
                    mNearZero = false;
                }

            }
        }

        public bool IsLoop { get; set; }

        private void Start()
        {
            gameObject.GetComponentsInChildren<ParticleSystem>(true, mParticles);
            gameObject.GetComponentsInChildren<Animation>(true, mAnimations);
            gameObject.GetComponentsInChildren<Animator>(true, mAnimators);
        }



        public bool checkDisable;

        List<ParticleSystem> mParticles = new List<ParticleSystem>();
        List<Animation> mAnimations = new List<Animation>();
        List<Animator> mAnimators = new List<Animator>();
        bool mDestroyed;
        public bool OnDestroyed = false;

        public static EffectAutoDestroy GetOrAdd(GameObject obj)
        {
            var ret = obj.GetComponent<EffectAutoDestroy>();
            if (ret)
            {
                ret.mDestroyed = false;
                ret.OnDestroyed = false;
                ret.checkDisable = false;
                ret.enabled = true;
                return ret;
            }
            return obj.AddComponent<EffectAutoDestroy>();
        }

        void OnDisable()
        {
            if (checkDisable)
            {
                UnityHelper.WaitForEndOfFrame(DoDestroy);
            }
        }

        void Update()
        {
            if (OnDestroyed) return;
            if (!mDestroyed)
            {
                mDuration -= Time.deltaTime;
                if (mDuration <= 0)
                {
                    DoDestroy();
                }
                //设置了时间， 切IsLoop为ture 时，走duration时间
                if (!mNearZero && !IsLoop)
                {
                    TryDestroy();
                }

            }
        }

        public void SetBeforeDestroy()
        {
            mDestroyed = true;
            enabled = false;
        }

        public void DoDestroy()
        {
            if (OnDestroyed) return;

            foreach (var item in mParticles)
            {
                item.Clear(true);
            }

            if (!mDestroyed && aoeHandler != null)
            {
                SetBeforeDestroy();
                aoeHandler.Unload();
            }
        }

        void TryDestroy()
        {
            foreach (var elem in mParticles)
            {
                if (elem.isPlaying || elem.isPaused)
                    return;
            }

            foreach (var elem in mAnimations)
            {
                if (elem.isPlaying)
                    return;
            }

            foreach (var elem in mAnimators)
            {
                if (elem.runtimeAnimatorController != null && elem.gameObject.activeInHierarchy && elem.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                    return;
            }

            DoDestroy();
        }

        public void OnDestroy()
        {
            if(OnDestroyed)
            {
                Debug.LogError("[EffectAutoDestroy]wtf ", gameObject);
            }
            OnDestroyed = true;
            if (!mDestroyed)
            {
                Debug.LogError("[EffectAutoDestroy]Destroy before call Unload(): "+ aoeHandler.CacheName, gameObject);
            }
        }
    }
}