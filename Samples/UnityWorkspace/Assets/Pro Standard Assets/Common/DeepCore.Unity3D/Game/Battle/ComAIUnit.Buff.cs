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

        public class Buff
        {
            #region RefCount

            private static AtomicInteger s_alloc_count = new AtomicInteger(0);
            private static AtomicInteger s_active_count = new AtomicInteger(0);

            /// <summary>
            /// 分配实例数量
            /// </summary>
            public static int AllocCount { get { return s_alloc_count.Value; } }
            /// <summary>
            /// 未释放实例数量
            /// </summary>
            public static int ActiveCount { get { return s_active_count.Value; } }

            #endregion

            private bool mDisposed = false;
            private List<DisplayCell> mBindEffects = new List<DisplayCell>();
            private List<Audio> mAudios = new List<Audio>();
            private DisplayCell mOverlayEffect;
            private Audio mOverlayAudio;
            private Avatar mAvatar;
            internal readonly ComAIUnit mOwner;
            internal readonly ZoneUnit.BuffState mState;


            public bool IsDisposed { get { return mDisposed; } }
            protected ComAIUnit Owner { get { return mOwner; } }
            protected ZoneUnit.BuffState State { get { return State; } }


            internal Buff(ComAIUnit owner, ZoneUnit.BuffState state)
            {
                s_alloc_count++;
                s_active_count++;

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

            //只在初始化阶段调用
            private void InitBindingEffect(LaunchEffect eff)
            {
                if (!string.IsNullOrEmpty(eff.Name))
                {
                    var bindDummy = Owner.EffectRoot;
                    if (eff.BindBody && !string.IsNullOrEmpty(eff.BindPartName))
                    {
                        bindDummy = Owner.GetDummyNode(eff.BindPartName).gameObject;
                    }

                    var displayCell = BattleFactory.Instance.CreateDisplayCell(bindDummy);
                    mBindEffects.Add(displayCell);

                    FuckAssetLoader.GetOrLoad(eff.Name, Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader.AssetComp)
                        {
                            if (displayCell.IsDisposed)
                            {
                                loader.AssetComp.Unload();
                                return;
                            }

                            displayCell.SetModel(loader.AssetComp);
                        }
                    });

                }
                if (!string.IsNullOrEmpty(eff.SoundName))
                {
                    if (eff.IsLoop)
                    {
                        Audio audio = BattleFactory.Instance.SoundAdapter.PlaySoundLoop(eff.SoundName, mOwner.ObjectRoot);
                        mAudios.Add(audio);

                    }
                    else
                    {
                        BattleFactory.Instance.SoundAdapter.PlaySound(eff.SoundName, mOwner.ObjectRoot.Position());
                    }
                }
            }

            ~Buff()
            {
                s_alloc_count--;
            }

            //多层叠加
            internal void OnChange(ZoneUnit.BuffState state)
            {
                if (state.OverlayLevel < state.Data.OverlayBindingEffect.Count)
                {
                    var eff = state.Data.OverlayBindingEffect[state.OverlayLevel - 1];

                    if (mOverlayEffect == null)
                    {
                        mOverlayEffect = BattleFactory.Instance.CreateDisplayCell(Owner.EffectRoot);
                    }
                    FuckAssetLoader.GetOrLoad(eff.Name, Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader.AssetComp)
                        {
                            if (mOverlayEffect.IsDisposed)
                            {
                                loader.AssetComp.Unload();
                                return;
                            }
                            mOverlayEffect.SetModel(loader.AssetComp);
                        }
                    });

                    if (!string.IsNullOrEmpty(eff.SoundName))
                    {
                        if (eff.IsLoop)
                        {
                            mOverlayAudio = BattleFactory.Instance.SoundAdapter.PlaySoundLoop(eff.SoundName, mOwner.ObjectRoot);
                        }

                        else
                        {
                            BattleFactory.Instance.SoundAdapter.PlaySound(eff.SoundName, mOwner.ObjectRoot.Position());
                        }
                    }
                }
            }

            internal void Dispose()
            {
                if (!mDisposed)
                {
                    s_active_count--;
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
                    elem.Dispose();
                }
                mBindEffects.Clear();
                foreach (var elem in mAudios)
                {
                    BattleFactory.Instance.SoundAdapter.StopSoundLoop(elem);
                }

                if (mOverlayEffect != null)
                {
                    mOverlayEffect.Dispose();
                    mOverlayEffect = null;
                }

                if (mOverlayAudio != null)
                {
                    BattleFactory.Instance.SoundAdapter.StopSoundLoop(mOverlayAudio);
                }
            }

            internal protected virtual void Update(float deltaTime)
            {

            }
        }
    }
}