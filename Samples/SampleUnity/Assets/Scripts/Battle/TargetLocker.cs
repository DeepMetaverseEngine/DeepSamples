

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DeepCore.Unity3D.Battle;

/// <summary>
/// 目标锁定器 主要用于锁怪操作
/// </summary>

public class TargetLocker : IDisposable
{
    private bool mDisposed;
    private ComAICell mOwner;
    private ComAICell mTarget;
    private float mMaxGuardRaduis;

    public TargetLocker(ComAICell owner)
    {
        if (owner != null)
        {
            Owner = owner;
        }
    }
    
    public ComAICell Owner
    {
        get { return mOwner; }
        private set
        {
            // do something
            mOwner = value;
        }
    }

    public ComAICell Target
    {
        get { return mTarget; }
        set
        {
            if (!mDisposed)
            {
                //UI设置 设置锁怪的UI
                mTarget = value;
            }
        }
    }

    public float MaxGuardRaduis
    {
        get { return mMaxGuardRaduis; }
        set { mMaxGuardRaduis = value; }
    }

    public void Update(float deltaTime)
    {
        if (!mDisposed)
        {
            OnUpdate(deltaTime);
        }
    }

    private void OnUpdate(float deltaTime)
    {
        if (Owner != null && !Owner.IsDisposed
            && Target != null && !Target.IsDisposed)
        {
            float dist = Vector3.Distance(Owner.Position, Target.Position);
            if (dist <= MaxGuardRaduis)
            {
                //设置相机朝向
                //Vector3 direct = Target.Position - Owner.Position;
                //float degree = Vector3.Angle(CameraManager.Instance.transform.forward, direct);
                //if (degree > 30)
                //{
                //    BattleFactroy.Instance.Camera.RotateToWithFocus2D(direct, 0.5f);
                //}
                return;
            }
        }

        if (Target != null)
        {
            Target = null;
        }
    }

    public void Dispose()
    {
        if (!mDisposed)
        {
            mDisposed = true;
            Owner = null;
            Target = null;
        }
    }
}
