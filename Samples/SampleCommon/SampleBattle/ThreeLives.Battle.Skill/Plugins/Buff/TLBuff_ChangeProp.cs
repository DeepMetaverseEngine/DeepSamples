using DeepCore.GameHost.Instance;
using DeepCore.Log;
using TLBattle.Common.Data;
using TLBattle.Common.Plugins;
using TLBattle.Server.Plugins.Virtual;

namespace TLCommonSkill.Plugins.Buff
{
    /// <summary>
    /// 单位属性变更.
    /// </summary>
    public class TLBuff_ChangeProp : TLBuff
    {
        /// <summary>
        /// 属性类型.
        /// </summary>
        public TLPropObject.PropType ChangeType;
        /// <summary>
        /// 变更值类型.
        /// </summary>
        public TLPropObject.ValueType ValueType;
        /// <summary>
        /// 变更量.
        /// </summary>
        public int Value;

        private int OperationID;

        protected override void OnBuffBegin(TLVirtual hitter, TLVirtual attacker, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_ChangeProp begin", null);
            TLPropObject obj = new TLPropObject(ChangeType, ValueType, Value);
            OperationID = hitter.PropModule.AddPropObject(obj);
        }

        protected override void OnBuffEnd(TLVirtual hitter, InstanceUnit.BuffState state)
        {
            TLVirtual.FormatLog(LoggerLevel.INFO, "TLBuff_ChangeProp end", null);

            hitter.PropModule.RemovePropObject(OperationID);
        }

        internal override void Init(TLBuffData bd)
        {
            data = bd as TLBuffData_ChangeProp;
            var d = data as TLBuffData_ChangeProp;
            if (data != null)
            {
                ChangeType = d.ChangeType;
                ValueType = d.ValueType;
                Value = d.Value;
            }
            base.Init(bd);
        }
    }
}
