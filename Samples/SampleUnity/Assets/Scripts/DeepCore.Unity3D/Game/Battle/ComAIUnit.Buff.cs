using DeepCore.Unity3D.Utils;
using DeepCore;
using DeepCore.Concurrent;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using System.Collections.Generic;
using System.IO;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAIUnit
    {
        HashMap<ZoneUnit.BuffState, Buff> mBuffs = new HashMap<ZoneUnit.BuffState, Buff>();


        private void UpdateBuff(float deltaTime)
        {
            foreach (var elem in mBuffs)
            {
                elem.Value.Update(deltaTime);
            }
        }

        protected virtual void ZUnit_OnBuffAdded(ZoneUnit unit, ZoneUnit.BuffState state)
        {
            Buff buff = new Buff(this, state);
            mBuffs.Add(state, buff);
        }

        protected virtual void ZUnit_OnBuffChanged(ZoneUnit unit, ZoneUnit.BuffState state)
        {
            Buff buff = null;
            if (mBuffs.TryGetValue(state, out buff))
            {
                buff.OnChange(state);
            }
        }

        protected virtual void ZUnit_OnBuffRemoved(ZoneUnit unit, ZoneUnit.BuffState state)
        {
            Buff buff = mBuffs.RemoveByKey(state);
            if (buff != null)
            {
                buff.Dispose();
            }
        }

        private void SyncBuffState()
        {
            using (var list = ListObjectPool<ZoneUnit.BuffState>.AllocAutoRelease())
            {
                this.ZUnit.GetBuffStatus(list);
                for (int i = 0 ; i < list.Count ; i++)
                {
                    ZUnit_OnBuffAdded(this.ZUnit, list[i]);
                }
            }
        }

        public class Buff
        {
            #region RefCount

            public static readonly TypeAllocRecorder Alloc = new TypeAllocRecorder(typeof(Buff));
            public static int AllocCount { get { return Alloc.AllocCount; } }
            public static int ActiveCount { get { return Alloc.ActiveCount; } }

            #endregion

            private bool mDisposed = false;
            private List<FuckAssetObject> mBindEffects = new List<FuckAssetObject>();
            private FuckAssetObject mOverlayEffect;
            private Avatar mAvatar;
            internal readonly ComAIUnit mOwner;
            internal readonly ZoneUnit.BuffState mState;


            public bool IsDisposed { get { return mDisposed; } }
            protected ComAIUnit Owner { get { return mOwner; } }
            protected ZoneUnit.BuffState State { get { return State; } }


            internal Buff(ComAIUnit owner, ZoneUnit.BuffState state)
            {
                Alloc.RecordConstructor(GetType());

                this.mOwner = owner;
                this.mState = state;

                if (state.Data.MakeAvatar
                    && !string.IsNullOrEmpty(state.Data.UnitFileName))
                {
                    mAvatar = owner.AddAvatar(state.Data.UnitFileName
                        , Path.GetFileNameWithoutExtension(state.Data.UnitFileName));

                    if (this.mState.Data.BodyScale > 0f)
                    {
                        mOwner.ObjectRoot.transform.localScale *= this.mState.Data.BodyScale;
                    }
                }

                if (state.Data.BindingEffect != null)
                {
                    InitBindingEffect(state.Data.BindingEffect);
                }
                if (state.Data.BindingEffectList != null
                    && state.Data.BindingEffectList.Count > 0)
                {
                    foreach (var elem in state.Data.BindingEffectList)
                    {
                        InitBindingEffect(elem);
                    }
                }
                OnChange(state);
            }
            ~Buff()
            {
                Alloc.RecordDestructor(GetType());
            }

            //只在初始化阶段调用
            private void InitBindingEffect(LaunchEffect eff)
            {
                if (!string.IsNullOrEmpty(eff.Name))
                {
                    var bindDummy = Owner.BuffRoot;
                    if (eff.BindBody && !string.IsNullOrEmpty(eff.BindPartName))
                    {
                        bindDummy = Owner.GetDummyNode(eff.BindPartName).gameObject;
                    }

                    FuckAssetObject.GetOrLoad(eff.Name, Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader)
                        {
                            if (mDisposed)
                            {
                                loader.Unload();
                                return;
                            }

                            mBindEffects.Add(loader);
                            loader.transform.SetParent(bindDummy.transform);
                            loader.transform.localPosition = UnityEngine.Vector3.zero;
                            loader.transform.localRotation = UnityEngine.Quaternion.identity;
                        }
                    });

                }
                if (!string.IsNullOrEmpty(eff.SoundName))
                {
                    SoundManager.Instance.PlaySound(eff.SoundName, mOwner.ObjectRoot.transform, eff.IsLoop);
                }
            }


            //多层叠加
            internal void OnChange(ZoneUnit.BuffState state)
            {
                if (state.OverlayLevel < state.Data.OverlayBindingEffect.Count)
                {
                    var eff = state.Data.OverlayBindingEffect[state.OverlayLevel - 1];

                    if (mOverlayEffect != null)
                    {
                        mOverlayEffect.Unload();
                        mOverlayEffect = null;
                    }

                    FuckAssetObject.GetOrLoad(eff.Name, Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader)
                        {
                            if (mDisposed)
                            {
								loader.Unload();
                                return;
                            }
                            mOverlayEffect = loader;
                            mOverlayEffect.transform.SetParent(Owner.BuffRoot.transform);
                            mOverlayEffect.transform.localPosition = UnityEngine.Vector3.zero;
                            mOverlayEffect.transform.localRotation = UnityEngine.Quaternion.identity;
                        }
                    });

                    if (!string.IsNullOrEmpty(eff.SoundName))
                    {
                        SoundManager.Instance.PlaySound(eff.SoundName, mOwner.ObjectRoot.transform, eff.IsLoop);
                    }
                }
            }

            internal void Dispose()
            {
                if (!mDisposed)
                {
                    Alloc.RecordDispose(GetType());
                    mDisposed = true;
                    OnDispose();
                }
            }

            protected virtual void OnDispose()
            {
                if (mAvatar != null)
                {
                    if (this.mState.Data.BodyScale > 0f)
                    {
                        mOwner.ObjectRoot.transform.localScale *= 1.0f / this.mState.Data.BodyScale;
                    }
                    mOwner.RemoveAvatar(mAvatar);
                }

                foreach (var elem in mBindEffects)
                {
                    elem.Unload();
                }
                mBindEffects.Clear();
 

                if (mOverlayEffect != null)
                {
                    mOverlayEffect.Unload();
                    mOverlayEffect = null;
                }
            }

            internal protected virtual void Update(float deltaTime)
            {

            }
        }
    }
}