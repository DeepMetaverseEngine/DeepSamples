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
        public float duration;
        public bool checkDisable;

        List<ParticleSystem> mParticles = new List<ParticleSystem>();
        List<Animation> mAnimations = new List<Animation>();
        List<Animator> mAnimators = new List<Animator>();
        bool mDestroyed;

        void Start()
        {
            if (Extensions.NearlyZero(duration))
            {
                duration = 3f;
                gameObject.GetComponentsInChildren<ParticleSystem>(mParticles);
                gameObject.GetComponentsInChildren<Animation>(mAnimations);
                gameObject.GetComponentsInChildren<Animator>(mAnimators);
            }
        }



        public static EffectAutoDestroy GetOrAdd(GameObject obj)
        {
            var ret = obj.GetComponent<EffectAutoDestroy>();
            if (ret)
            {
                ret.mDestroyed = false;
                ret.checkDisable = false;
                return ret;
            }
            return obj.AddComponent<EffectAutoDestroy>();
        }

        void OnDisable()
        {
            if (checkDisable)
            {
                DoDestroy();
            }
        }

        void Update()
        {
            if (!mDestroyed)
            {
                duration -= Time.deltaTime;
                if (duration <= 0)
                {
                    DoDestroy();
                }

                TryDestroy();
            }
        }

        public void DoDestroy()
        {
            if (!mDestroyed && aoeHandler != null)
            {
                mDestroyed = true;
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
                if (elem.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                    return;
            }

            DoDestroy();
        }
    }
}