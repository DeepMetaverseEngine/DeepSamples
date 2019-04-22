using DeepCore;
using DeepCore.Unity3D;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public sealed class AssetGameObject : MonoBehaviour, IUnload
{
    private static readonly HashMap<int, AssetGameObject> sAll = new HashMap<int, AssetGameObject>();

    public int ID { get; private set; }

    public string BundleName { get; private set; }

    public bool DontMoveToCache { get; set; }
    public bool IsDestroy { get; private set; }
    public bool IsInCache { get; private set; }
    public bool DisableToUnload { get; set; }

    public bool IsUnload
    {
        get { return IsInCache || IsDestroy; }
    }

    private EffectController mEffect;

    public void SetAsEffect(float duration = -1)
    {
        mEffect = EffectController.GetOrAdd(this, duration);
    }

    public delegate void OnUnloadHandler(AssetGameObject id, bool isToCache);

    public event OnUnloadHandler OnBeforeUnload;

    public static AssetGameObject GetAssetObject(int id)
    {
        return sAll.Get(id);
    }

    public override string ToString()
    {
        return string.Format("{0}-{1}", BundleName, ID);
    }

    ~AssetGameObject()
    {
        sAll.Remove(ID);
    }

    private Animator mAnimator;

    private void CheckInvalid()
    {
        if (IsUnload)
        {
            throw new Exception(this + " Invalid");
        }
    }

    public string AnimStateNameFormat { get; set; }

    private IEnumerable<Animator> GetAnimatorsInChildren()
    {
        var aos = GetComponentsInChildren<AssetGameObject>();
        return (from ao in aos where ao.mAnimator select ao.mAnimator);
    }

    private void Destroy()
    {
        IsDestroy = true;
        DeepCore.Unity3D.UnityHelper.Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (!IsUnload && DisableToUnload)
        {
            UnityHelper.WaitForEndOfFrame(Unload);
        }
    }

    public float GetAnimTime(string stateName)
    {
        CheckInvalid();
        if (!mAnimator || string.IsNullOrEmpty(stateName))
        {
            return 0;
        }

        var listanimatorclip = mAnimator.runtimeAnimatorController.animationClips;
        if (listanimatorclip != null)
        {
            return (from clip in listanimatorclip where clip.name.Equals(stateName) select clip.length).FirstOrDefault();
        }

        return 0;
    }

    public void CrossFade(string stateName, float transDuration, int layer, float normalizedTime, bool deep)
    {
        CheckInvalid();
        if (deep)
        {
            var animators = GetAnimatorsInChildren();
            foreach (var animator in animators)
            {
                animator.CrossFade(stateName, transDuration, layer, normalizedTime);
            }
        }
        else if (mAnimator)
        {
            mAnimator.CrossFade(stateName, transDuration, layer, normalizedTime);
        }
    }

    internal void SwapID(AssetGameObject aoe)
    {
        var otherid = aoe.ID;
        var curid = ID;
        ID = otherid;
        aoe.ID = curid;
        sAll[ID] = this;
        sAll[aoe.ID] = aoe;
    }

    internal void SetNewID()
    {
        sAll.Remove(ID);
        ID = FuckAssetLoader.GenID();
        sAll[ID] = this;
    }

    public void CrossFade(string stateName, float transDuration)
    {
        CrossFade(stateName, transDuration, 0, 0, true);
    }

    public void Play(string stateName)
    {
        Play(stateName, true);
    }

    public void Play(string stateName, bool deep)
    {
        Play(stateName, 0, 0, deep);
    }

    public void Play(string stateName, int layer, float normalizedTime, bool deep)
    {
        CheckInvalid();
        if (deep)
        {
            var animators = GetAnimatorsInChildren();
            foreach (var animator in animators)
            {
                animator.Play(stateName, layer, normalizedTime);
            }
        }
        else if (mAnimator)
        {
            mAnimator.Play(stateName, layer, normalizedTime);
        }
    }

    public static AssetGameObject FromCache(string fileName)
    {
        var ao = UnityObjectCacheCenter.GetTypeCache<AssetGameObject>().Pop(fileName);
        if (ao)
        {
            ao.transform.localPosition = Vector3.zero;
            ao.transform.localScale = Vector3.one;
            ao.transform.rotation = Quaternion.identity;
            ao.IsInCache = false;
            ao.DisableToUnload = false;
        }

        return ao;
    }

    public static AssetGameObject Create(FuckAssetLoader loader)
    {
        if (!loader.IsSuccess || !loader.IsGameObject)
        {
            return null;
        }

        var obj = (GameObject) Instantiate(loader.AssetObject);
        var ao = obj.AddComponent<AssetGameObject>();
        ao.ID = loader.ID;
        ao.BundleName = loader.BundleName;
        ao.mAnimator = obj.GetComponent<Animator>();
        HZUnityAssetBundleManager.GetInstance().AddAssetRef(ao.BundleName);
        sAll.Add(ao.ID, ao);
        return ao;
    }

    private void OnDestroy()
    {
        HZUnityAssetBundleManager.GetInstance().RemoveAssetRef(BundleName);
        IsDestroy = true;
        OnBeforeUnload = null;
    }

    private void Unload(bool deep)
    {
        if (IsUnload)
        {
            return;
        }

        if (deep)
        {
            var aos = GetComponentsInChildren<AssetGameObject>(true);
            foreach (var ao in aos)
            {
                if (ao != this)
                {
                    ao.Unload(false);
                }
            }
        }

        if (DontMoveToCache)
        {
            if (OnBeforeUnload != null)
            {
                OnBeforeUnload.Invoke(this, false);
            }

            Destroy();
        }
        else
        {
            if (OnBeforeUnload != null)
            {
                OnBeforeUnload.Invoke(this, true);
            }

            if (mEffect)
            {
                mEffect.Reset();
            }

            UnityObjectCacheCenter.GetTypeCache<AssetGameObject>().Push(BundleName, this);
            IsInCache = true;
        }

        OnBeforeUnload = null;
    }

    public void Unload()
    {
        Unload(true);
    }

    private HashMap<string, GameObject> mGameObjects;

    public GameObject FindNode(string childName)
    {
        CheckInvalid();
        GameObject ret = null;
        if (mGameObjects != null)
        {
            ret = mGameObjects.Get(childName);
        }

        if (ret == null)
        {
            var t = transform.FindRecursive(childName, StringComparison.OrdinalIgnoreCase);
            if (t)
            {
                ret = t.gameObject;
                if (mGameObjects == null)
                {
                    mGameObjects = new HashMap<string, GameObject>();
                }

                mGameObjects[childName] = ret;
            }
        }

        return ret;
    }
}