using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    /// <summary>
    /// 控制的所有animator
    /// </summary>
    private List<Animator> _animators = new List<Animator>();
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
                foreach (var elem in _animators)
                {
                    if (elem != null)
                    {
                        elem.speed = _speed;
                    }
                }
            }
        }
    }

    public void AddAnimator(Animator animator)
    {
        if (animator != null)
        {
            foreach (var elem in _animators)
            {
                if (elem == animator)
                {
                    return;
                }
            }

            animator.speed = _speed;
            _animators.Add(animator);

            if (!string.IsNullOrEmpty(_lastStateName))
            {
                Play(_lastStateName);
            }
        }
    }

    public void RemoveAnimator(Animator animator)
    {
        if(animator != null)
        _animators.Remove(animator);
    }

    public void CrossFade(string stateName, float transDuration)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    elem.CrossFade(_lastStateName, transDuration);
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }
    }

    public void CrossFade(string stateName, float transDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    elem.CrossFade(_lastStateName, transDuration, layer, normalizedTime);
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }
    }

    public void Play(string stateName)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    elem.Play(_lastStateName);
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }
    }

    public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
    {
        if (!string.IsNullOrEmpty(stateName))
        {
            _lastStateName = stateName;

            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    elem.Play(_lastStateName, layer, normalizedTime);
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }
                }
            }
        }
    }

    public void SetFloat(string name, float value)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    if (name != "Blend")
                    {
                        elem.SetFloat(name, value);
                    }
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }
                        
                }
            }
        }
    }

    public void SetFloat(string name, float value, float dampTime, float deltaTime)
    {
        if (!string.IsNullOrEmpty(name))
        {
            foreach (var elem in _animators)
            {
                if (elem != null && elem.gameObject.activeInHierarchy)
                {
                    if (name != "Blend")
                    {
                        elem.SetFloat(name, value, dampTime, deltaTime);
                    }
                   
                }
                else
                {
                    if (elem.gameObject.activeInHierarchy)
                    {
                        Debug.LogError("have null value");
                    }
                    else
                    {
                        Debug.Log("animplay is activeInHierarchy ");
                    }

                }
            }
        }
    }
}
