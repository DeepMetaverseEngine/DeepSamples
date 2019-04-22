using System;
using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D;
using DeepCore.Unity3D.Battle;
using UnityEngine;

public class AnimPlayer
{
    /// <summary>
    /// 控制的所有animator
    /// </summary>
    private List<DisplayCell> animators
    {
        get
        {
            //CheckFucked();
            return _animators;
        }
    }
    private List<DisplayCell> _animators = new List<DisplayCell>();
    private float _speed = 1f;
    private string _lastStateName;

    public float speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (_speed != value)
            {
                _speed = value;
                foreach (var elem in animators)
                {
                    if (elem != null)
                    {
                        elem.speed = _speed;
                    }
                }
            }
        }
    }

    public void AddAnimator(DisplayCell animator)
    {
        if (animator != null)
        {

            foreach (var elem in animators)
            {
                if (elem == animator)
                {
                    return;
                }
            }

            animator.speed = _speed;
            animators.Add(animator);



            if (!string.IsNullOrEmpty(_lastStateName))
            {
                Play(_lastStateName);
            }
        }
    }

    public void RemoveAnimator(DisplayCell animator)
    {
        if (animator != null)
            animators.Remove(animator);
    }

    public void CrossFade(string stateName, float transDuration)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in animators)
            {
                if (elem != null)
                {
                    elem.CrossFade(stateName, transDuration);
                }
            }
        }
    }

    public void CrossFade(string stateName, float transDuration, int layer = 0, float normalizedTime = 0)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in animators)
            {
                if (elem != null)
                {
                    elem.CrossFade(stateName, transDuration, layer, normalizedTime);
                }
            }
        }
    }

    public float GetAnimTime(string stateName)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            if (animators.Count >= 1 && animators[0] != null)
            {
                return animators[0].GetAnimTime(stateName);
            }
        }
        return 0;

    }

    public bool IsCurrentStatePlayOver(string name)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            var elem = animators[i];
            elem.IsCurrentStatePlayOver(name);

        }
        return false;
    }


    public string GetCurrentStateName()
    {
        return _lastStateName;
    }

    public void Play(string stateName, int layer = 0, float normalizedTime = 0)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            //if (_lastStateName == "m_idle01")
            {
            }

            foreach (var elem in animators)
            {
                elem.Play(stateName, layer, normalizedTime);
            }
        }
    }

    public void SetFloat(string name, float value)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var elem in animators)
            {
                elem.SetFloat(name, value);
            }
        }
    }

    public void SetFloat(string name, float value, float dampTime, float deltaTime)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var elem in animators)
            {
                elem.SetFloat(name, value, dampTime, deltaTime);
            }
        }
    }
}
