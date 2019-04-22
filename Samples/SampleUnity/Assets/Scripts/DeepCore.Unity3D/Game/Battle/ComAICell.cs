using DeepCore.Unity3D.Utils;
using DeepCore.GameData.Zone;
using DeepCore.GameSlave;
using UnityEngine;

namespace DeepCore.Unity3D.Battle
{
    public partial class ComAICell : BattleObject
    {
        private ZoneObject mZObj;

        public ZoneObject ZObj { get { return mZObj; } }
        public uint ObjectID { get { return mZObj.ObjectID; } }
        public float X { get { return mZObj.X; } }
        public float Y { get { return mZObj.Y; } }
        public float Z { get { return mZObj.Z; } }

        //added by chenjie
        public bool IsEnable { get { return mZObj.IsEnable; } }
        public float Direction { get { return mZObj.Direction; } }

        public TemplateManager Templates { get { return this.mZObj.Templates; } }


        public bool IsDebugShowGuard { get; set; }
        public bool IsDebugShowBody { get; set; }
        public bool IsDebugShowAttack { get; set; }

        public override bool IsDisposed
        {
            get
            {
                return mDisposed || ZObj.IsDisposed;
            }
        }

        public ComAICell(BattleScene battleScene, ZoneObject obj)
            : base(battleScene, string.Format("{0}_{1}({2})", obj.GetType().Name, obj.Name, obj.ObjectID))
        {
            mZObj = obj;
            mZObj.OnDoEvent += MZObj_OnDoEvent;
            RegistAllObjectEvent();
        }

        private void MZObj_OnDoEvent(ZoneObject obj, ObjectEvent e)
        {
            DoObjectEvent(e);
        }

        protected override void OnDispose()
        {
            mZObj.OnDoEvent -= MZObj_OnDoEvent;
            base.OnDispose();
            mZObj.Dispose();
        }

        private Vector3 mPrePos;

        /// <summary>
        /// zero means no change, or means move direction
        /// </summary> 
        public Vector3 IsPosChanged { get; private set; }

        private void CheckPosChanged()
        {
            IsPosChanged = mPrePos.Equals(this.Position) ? Vector3.zero : this.Position - mPrePos;
            mPrePos = this.Position;
        }
        float mCheckDelta;
        protected override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            SyncState();
            mCheckDelta += deltaTime;
            if (mCheckDelta > 0.05f)
            {
                mCheckDelta = 0.0f;
                CheckPosChanged();
            }
            
            if (IsDebugShowGuard)
            {
                UpdateDebugGuard();
            }
            if (IsDebugShowBody)
            {
                UpdateDebugBody();
            }
            if (IsDebugShowAttack)
            {
                UpdateDebugAttack();
            }
    }

        protected virtual void SyncState()
        {
            if (ZObj.Parent.TerrainSrc != null)
            {
                this.ObjectRoot.ZonePos2NavPos(ZObj.Parent.TerrainSrc.TotalHeight
                    , ZObj.X, ZObj.Y, ZObj.Z);
            }
            this.ObjectRoot.ZoneRot2UnityRot(ZObj.Direction);
        }

        public override void OnLoadWarningEffect(FuckAssetObject aoe, LaunchEffect eff, Vector3 pos, Quaternion rot)
        {
            base.OnLoadWarningEffect(aoe, eff, pos, rot);
            aoe.gameObject.ZoneRot2UnityRot(Direction);
        }


        #region Debuger相关.

        //预警
        public virtual void UpdateDebugGuard()
        {
          
        }
        public virtual void UpdateDebugBody()
        {

        }
        //预警
        public virtual void UpdateDebugAttack()
        {
           
        }
        #endregion
    }

}