using DeepCore.Unity3D;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private readonly List<ParticleSystem> mParticles = new List<ParticleSystem>();
    private readonly List<Animation> mAnimations = new List<Animation>();
    private readonly List<Animator> mAnimators = new List<Animator>();


    private MonoBehaviour mComponent;

    public bool IsStarted { get; private set; }

    /// <summary>
    /// 小于等于0时， 表示自动unload
    /// </summary>
    public float Duration;

    public float Timeout;

    public static EffectController GetOrAdd(MonoBehaviour obj, float duration = -1)
    {
        var ret = obj.GetComponent<EffectController>();
        if (ret)
        {
            ret.IsStarted = true;
            ret.enabled = true;
        }
        else
        {
            ret = obj.gameObject.AddComponent<EffectController>();
            ret.IsStarted = true;
            ret.mComponent = obj;
        }
        ret.Duration = duration;
        return ret;
    }


    public void Reset()
    {
        IsStarted = false;
        Duration = 0;
        Timeout = 0;
        enabled = false;
    }


    private void Start()
    {
        GetComponentsInChildren(true, mParticles);
        GetComponentsInChildren(true, mAnimations);
        GetComponentsInChildren(true, mAnimators);
    }

    private void Unload()
    {
        var ul = mComponent as IUnload;
        if (ul != null)
        {
            ul.Unload();
        }
        else
        {
            Debug.LogError("[destroy the fucking effect]");
            DeepCore.Unity3D.UnityHelper.Destroy(mComponent.gameObject);
        }
    }

    private void Update()
    {
        if (!IsStarted)
        {
            return;
        }
        if (Duration > 0)
        {
            Duration -= Time.deltaTime;
            if (Duration <= 0)
            {
                Unload();
            }
        }
        else if (Duration <= 0)
        {
            if (Timeout > 0)
            {
                Timeout -= Time.deltaTime;
                if (Timeout <= 0)
                {
                    Unload();
                }
            }
            foreach (var elem in mParticles)
            {
                if (elem.isPlaying || elem.isPaused)
                {
                    return;
                }
            }
            foreach (var elem in mAnimations)
            {
                if (elem.isPlaying)
                {
                    return;
                }
            }

            foreach (var elem in mAnimators)
            {
                var stateInfo = elem.GetCurrentAnimatorStateInfo(0);
                if (elem.runtimeAnimatorController != null &&
                    elem.gameObject.activeInHierarchy &&
                    (stateInfo.loop || stateInfo.normalizedTime < 1f))
                {
                    return;
                }
            }

            Unload();
        }
    }
}