using DeepCore.IO;
using System;
using TLBattle.Common.Plugins;

namespace TLBattle.Common.Data
{
    [MessageType(TLConstants.BATTLE_MSG_B2C_START + 6)]
    public class TLPropObject : IExternalizable
    {
        public enum PropType : byte
        {
            //CurMP = 8,
            //MaxMP = 9,

            curhp = 1,
            maxhp = 2,

            attack = 3,

            defend = 4,
            mdef = 5,

            through = 6,
            block = 7,

            hit = 8,
            dodge = 9,

            crit = 10,
            rescrit = 11,

            cridamageper = 12,
            redcridamageper = 13,

            runspeed = 14,
            autorecoverhp = 15,

            totalreducedamageper = 16,
            totaldamageper = 17,

            thunderdamage = 18,
            winddamage = 19,
            icedamage = 20,
            firedamage = 21,
            soildamage = 22,

            thunderresist = 23,
            windresist = 24,
            iceresist = 25,
            fireresist = 26,
            soilresist = 27,


            allelementdamage = 28,
            allelementresist = 29,

            onhitrecoverhp = 30,
            killrecoverhp = 31,

            extragoldper = 32,
            extraexpper = 33,

            targettomonster = 34,
            targettoplayer = 35,

            goddamage = 36,

            defreduction = 37,
            mdefreduction = 38,
            extracrit = 39,
            //39.
        }

        public enum ValueType : byte
        {
            Value = 1,           //值.
            Percent = 2,         //万分比.
            ValuePercent = 3,    //值万分比
            FinalPropPercent = 4 //最终属性万分比加成.
        }

        public PropType Prop;
        public ValueType Type;
        private int mValue = 0;
        public int Value
        {
            set { mValue = value; }
            get { return GetValue(); }
        }

        public string Tag;

        public TLPropObject() { }

        public TLPropObject(PropType prop, ValueType type, int value, string tag = null)
        {
            Type = type;
            Prop = prop;
            Value = value;
            Tag = tag;
        }

        public static PropType ToPropType(string name)
        {
            return (PropType)Enum.Parse(typeof(PropType), name);
        }


        public TLPropObject(ValueType type, int prop, int value, string tag = null)
        {
            Type = (ValueType)type;
            Prop = (PropType)prop;
            Value = value;
            Tag = tag;
        }

        private int GetValue()
        {
            switch (Type)
            {
                case ValueType.Value:
                    return mValue;
                case ValueType.Percent:
                    return mValue;
                case ValueType.ValuePercent:
                    return mValue;
                case ValueType.FinalPropPercent:
                    return mValue;
                default:
                    return 0;
            }
        }

        public void WriteExternal(IOutputStream output)
        {
            output.PutEnum8(Prop);
            output.PutEnum8(Type);
            output.PutS32(mValue);
            output.PutUTF(Tag);
        }

        public void ReadExternal(IInputStream input)
        {
            Prop = input.GetEnum8<PropType>();
            Type = input.GetEnum8<ValueType>();
            mValue = input.GetS32();
            Tag = input.GetUTF();
        }


    }
}
