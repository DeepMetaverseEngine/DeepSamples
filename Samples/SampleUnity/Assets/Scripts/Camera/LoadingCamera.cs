using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LoadingCamera : MonoBehaviour
{

    public GameObject inCamera;
    public GameObject outCamera;
    public float inDuration;
    public float outDuration;

    private float mEffectInTime;
    private float mEffectOutTime;

    private Action mEffectInCB;
    private Action mEffectOutCB;

    public void ShowOutEffect(Action cb)
    {
        outCamera.SetActive(true);
        inCamera.SetActive(false);
        mEffectOutTime = outDuration;
        if (mEffectOutCB != null)
        {
            mEffectOutCB();
        }
        mEffectOutCB = cb;
    }

    public void ShowInEffect(Action cb)
    {
        inCamera.SetActive(true);
        outCamera.SetActive(false);
        mEffectInTime = inDuration;
        if (mEffectInCB != null)
        {
            mEffectInCB();
        }
        mEffectInCB = cb;
    }

    public void Reset()
    {
        outCamera.SetActive(false);
        inCamera.SetActive(false);
    }

    void Update()
    {
        if (mEffectInTime > 0)
        {
            mEffectInTime -= Time.deltaTime;
        }
        else
        {
            if (mEffectInCB != null)
            {
                mEffectInCB();
                mEffectInCB = null;
            }
        }
        if (mEffectOutTime > 0)
        {
            mEffectOutTime -= Time.deltaTime;
        }
        else
        {
            if (mEffectOutCB != null)
            {
                mEffectOutCB();
                mEffectOutCB = null;
            }
        }
    }

}
