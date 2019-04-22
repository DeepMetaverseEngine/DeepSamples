using DeepCore.Concurrent;
using DeepCore.GameData.Zone;
using DeepCore.GameData.Zone.ZoneEditor;
using DeepCore.GameSlave;
using DeepCore.GameSlave.Client;
using DeepCore.Unity3D.Game.Battle;
using DeepCore.Unity3D.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using DeepCore.GameData.RTS;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class BattleScene : IDisposable, IDebugCameraInterface
    {
        #region RefCount

        public static readonly TypeAllocRecorder Alloc = new TypeAllocRecorder(typeof(BattleScene));
        public static int AllocCount { get { return Alloc.AllocCount; } }
        public static int ActiveCount { get { return Alloc.ActiveCount; } }

        #endregion

        internal class RemovedBattleObject
        {
            public float delayTime;
            public ComAICell battleObject;
        }

        private bool mDisposed = false;
        private AbstractBattle mBattle;
        /// <summary>
        /// 不绑定任何东西的特效会存在在这里
        /// </summary>
        private GameObject mEffectRoot;
        private HashMap<uint, ComAICell> mBattleObjects = new HashMap<uint, ComAICell>();
        private List<RemovedBattleObject> mRemovedBattleObjects = new List<RemovedBattleObject>();
        private IComAIActor mActor;


        public bool IsDisposed { get { return mDisposed; } }
        protected AbstractBattle Battle { get { return mBattle; } }
        public GameObject EffectRoot { get { return mEffectRoot; } }
        public EditorTemplates DataRoot { get { return mBattle.DataRoot; } }
        public ZoneInfo Terrain { get { return mBattle.Layer.TerrainSrc; } }
        public SceneData SceneData { get { return this.Battle.Layer.Data; } }
        public int SceneID { get { return this.Battle.Layer.SceneID; } }

        private bool mIsDebugShowBodysize;
        private bool mIsDebugShowGuard;
        private bool mIsDebugShowAttack;
        //added by chenjie
        public HashMap<uint, ComAICell> BattleObjects
        {
            get
            {
                return mBattleObjects;
            }
        }

        protected BattleScene(AbstractBattle client)
        {
            Alloc.RecordConstructor(GetType());

            mBattle = client;
            mBattle.Layer.LayerInit += Layer_LayerInit;
            mBattle.Layer.ObjectEnter += Layer_ObjectEnter;
            mBattle.Layer.ObjectLeave += Layer_ObjectLeave;
            mBattle.Layer.MessageReceived += Layer_MessageReceived;
            mBattle.Layer.DecorationChanged += Layer_DecorationChanged;
            mBattle.Layer.OnChangeBGM += Layer_OnChangeBGM;

            mEffectRoot = new GameObject("EffectNode");

            RegistAllZoneEvent();
            //HZUnityDebug.GetInstance().SetInterface(this);
        }

        ~BattleScene()
        {
            Alloc.RecordDestructor(GetType());
        }

        public void Dispose()
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
            mActor = null;
            foreach (var elem in mBattleObjects)
            {
                elem.Value.Dispose();
            }
            mBattleObjects.Clear();

            foreach (var elem in mRemovedBattleObjects)
            {
                elem.battleObject.Dispose();
            }
            mRemovedBattleObjects.Clear();

            mBattle.Layer.LayerInit -= Layer_LayerInit;
            mBattle.Layer.ObjectEnter -= Layer_ObjectEnter;
            mBattle.Layer.ObjectLeave -= Layer_ObjectLeave;
            mBattle.Layer.MessageReceived -= Layer_MessageReceived;
            mBattle.Layer.DecorationChanged -= Layer_DecorationChanged;
            mBattle.Layer.OnChangeBGM -= Layer_OnChangeBGM;
            mBattle.Dispose();
            mBattle = null;

            FuckAssetObject[] aoes = mEffectRoot.GetComponentsInChildren<FuckAssetObject>();
            foreach (var elem in aoes)
            {
                var aoe = elem;
                aoe.Unload();
            }
            DeepCore.Unity3D.UnityHelper.Destroy(mEffectRoot);
            DisposeDecoration();
        }

        public void Update(float deltaTime)
        {
            if (!mDisposed)
            {
                OnUpdate(deltaTime);
            }
        }

        protected virtual void OnUpdate(float deltaTime)
        {
            int delta = (int)(deltaTime * 1000);
            foreach (var elem in mBattleObjects)
            {
                elem.Value.BeginUpdate(deltaTime);
                elem.Value.IsDebugShowAttack = mIsDebugShowAttack;
                elem.Value.IsDebugShowBody = mIsDebugShowBodysize;
                elem.Value.IsDebugShowGuard = mIsDebugShowGuard;
            }
            mBattle.BeginUpdate(delta);
            mBattle.Update();
            for (int i = mRemovedBattleObjects.Count - 1; i >= 0; i--)
            {
                var removed = mRemovedBattleObjects[i];
                removed.delayTime -= deltaTime;
                if (removed.delayTime <= 0)
                {
                    mRemovedBattleObjects.RemoveAt(i);
                    removed.battleObject.Dispose();
                }
            }
            foreach (var elem in mBattleObjects)
            {
                elem.Value.Update(deltaTime);
            }
        }

        public T[] FindBattleObjectsAs<T>(Predicate<T> select) where T : ComAICell
        {
            var ret = new List<T>();
            foreach (var entry in mBattleObjects)
            {
                if (entry.Value is T)
                {
                    if (select == null || select((T)entry.Value))
                    {
                        ret.Add((T)entry.Value);
                    }
                }
            }
            return ret.ToArray();
        }


        public T FindBattleObjectAs<T>(Predicate<T> select) where T : ComAICell
        {
            var m = mBattleObjects.FirstOrDefault(entry => entry.Value is T && @select((T)entry.Value));
            return (T)m.Value;
        }

        public ComAICell GetBattleObject(uint id)
        {
            ComAICell outVal = null;
            mBattleObjects.TryGetValue(id, out outVal);
            return outVal;
        }

        public WayPoint FindPath(float sx,float sy, float dx, float dy)
        {
            return mBattle.Layer.FindPath(sx, sy, dx, dy);
        }

        public IComAIActor GetActor()
        {
            return mActor;
        }

        private void AddBattleObject(ComAICell obj)
        {
            if (obj != null)
            {
                mBattleObjects.Add(obj.ObjectID, obj);
                if (obj is IComAIActor)
                {
                    Debug.Log("Actor comming");
                    mActor = obj as IComAIActor;
                    OnComAIActorAdded(mActor);
                }
                OnComAICellAdded(obj);
            }
        }

        private void RemoveBattleObject(uint id)
        {
            ComAICell outVal = null;
            mBattleObjects.TryGetValue(id, out outVal);
            if (outVal != null)
            {
                OnComAICellRemoved(outVal);
                AddRemovedBattleObject(outVal);
            }
            mBattleObjects.RemoveByKey(id);

            //ComAICell outVal = mBattleObjects[id];
            //if (outVal != null)
            //{
            //    OnComAICellRemoved(outVal);
            //    AddRemovedBattleObject(outVal);
            //}

        }

        protected virtual void AddRemovedBattleObject(ComAICell obj)
        {
            if (obj.DisposeDelayMS > 0)
            {
                var removed = new RemovedBattleObject();
                removed.delayTime = obj.DisposeDelayMS / 1000f;
                removed.battleObject = obj;
                mRemovedBattleObjects.Add(removed);
                return;
            }

            obj.Dispose();
        }

        protected virtual void OnComAIActorAdded(IComAIActor actor) { }
        protected virtual void OnComAICellAdded(ComAICell o) { }
        protected virtual void OnComAICellRemoved(ComAICell o) { }

        protected virtual void Layer_LayerInit(ZoneLayer layer)
        {
            //BattleFactory.Instance.TerrainAdapter.Load(layer.Data.FileName, OnMapLoadFinish);
            layer.ActorAutoReady = false;
            InitDecoration();
            SoundManager.Instance.PlayBGM(layer.Data.BGM);
        }

        private void Layer_ObjectEnter(ZoneLayer layer, ZoneObject obj)
        {
            AddBattleObject(BattleFactory.Instance.CreateComAICell(this, obj));
        }

        private void Layer_ObjectLeave(ZoneLayer layer, ZoneObject obj)
        {
            RemoveBattleObject(obj.ObjectID);
        }

        private void Layer_MessageReceived(ZoneLayer layer, DeepCore.Protocol.IMessage msg)
        {
            //Debug.Log("Layer_MessageReceived " + msg);
            if (msg is ZoneEvent)
            {
                Action<ZoneEvent> action = null;
                if (mZoneEvens.TryGetValue(msg.GetType(), out action))
                {
                    action(msg as ZoneEvent);
                }
            }

        }

        private void Layer_OnChangeBGM(ZoneLayer layer, string filename)
        {
            SoundManager.Instance.PlayBGM(filename);
        }

        public virtual void PlayEffectWithZoneCoord(LaunchEffect eff
            , float x, float y, float direct)
        {
            Vector3 pos = Extensions.ZonePos2NavPos(mBattle.Layer.TerrainSrc.TotalHeight
                , x, y, 0);
            Quaternion rot = Extensions.ZoneRot2UnityRot(direct);
            if (eff != null)
            {
                if (!string.IsNullOrEmpty(eff.SoundName))
                {
                    SoundManager.Instance.PlaySound(eff.SoundName, eff.EffectTimeMS, pos, eff.IsLoop);
                }
                //特效
                if (!string.IsNullOrEmpty(eff.Name))
                {
                    FuckAssetObject.GetOrLoad(eff.Name, System.IO.Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader)
                        {
                            if (IsDisposed && eff.BindBody)
                            {
                                loader.Unload();
                                return;
                            }
                            OnLoadEffectSuccess(loader, eff, pos, rot);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eff"></param>
        /// <param name="pos"> unity pos</param>
        /// <param name="rot"> unity rot</param>
        public virtual void PlayEffect(LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            if (eff != null)
            {
                //特效
                if (!string.IsNullOrEmpty(eff.Name))
                {
                    FuckAssetObject.GetOrLoad(eff.Name, System.IO.Path.GetFileNameWithoutExtension(eff.Name), (loader) =>
                    {
                        if (loader)
                        {
                            if (IsDisposed && eff.BindBody)
                            {
                                loader.Unload();
                                return;
                            }
                            OnLoadEffectSuccess(loader, eff, pos, rot);
                        }
                    });

                }
            }
        }

        protected virtual void OnLoadEffectSuccess(FuckAssetObject aoe
            , LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            aoe.gameObject.Parent(mEffectRoot);
            aoe.gameObject.Position(pos);
            aoe.gameObject.Rotation(rot);

            var script = EffectAutoDestroy.GetOrAdd(aoe.gameObject);
            script.aoeHandler = aoe;
            script.duration = eff.EffectTimeMS / 1000f;
            script.IsLoop = eff.IsLoop;

            if (Math.Abs(eff.ScaleToBodySize) > 0.01)
            {
                aoe.transform.localScale = new Vector3(eff.ScaleToBodySize, eff.ScaleToBodySize, eff.ScaleToBodySize);
            }
        }

        public void DoNear()
        {
            //throw new NotImplementedException();
        }

        public void DoFar()
        {
            //throw new NotImplementedException();
        }

        public void DoSub_H()
        {
            //throw new NotImplementedException();
        }

        public void DoAdd_H()
        {
            // throw new NotImplementedException();
        }

        public void DoSub_RX()
        {
            // throw new NotImplementedException();
        }

        public void DoAdd_RX()
        {
            throw new NotImplementedException();
        }

        public void DoSub_RY()
        {
            //throw new NotImplementedException();
        }

        public void DoAdd_RY()
        {
            //throw new NotImplementedException();
        }

        public void ShowBodySize(bool flag)
        {
            // throw new NotImplementedException();
            mIsDebugShowBodysize = flag;
        }

        public void ShowGuard(bool flag)
        {
            //throw new NotImplementedException();
            mIsDebugShowGuard = flag;
        }

        public void ShowAttack(bool flag)
        {
            // throw new NotImplementedException();
            mIsDebugShowAttack = flag;
        }
    }
}