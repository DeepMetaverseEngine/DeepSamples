using System;
using DeepCore.IO;
using DeepMMO.Protocol;
using SLua;

namespace Localized
{
    public class TLMessageCodeManager : MessageCodeManager
    {
        public TLMessageCodeManager(ISerializerFactory factory) : base(factory)
        {
        }


        public override string GetCodeMessage(Response rsp)
        {
            return GetCodeMessage(rsp.GetType(), rsp.s2c_code);
        }

        public override string GetCodeMessage(Type type, int s2c_code)
        {
            return GetCodeMessage(type.FullName, s2c_code);
        }

        private string mDefaultError;
        private string GetDefaultError()
        {
            if (mDefaultError == null)
            {
                using(var table = (LuaTable) LuaSvr.mainState["MessageCodeAttribute"])
                using (var codeTable = (LuaTable) table["DeepMMO.Protocol.Response"])
                {
                    var ret = codeTable[Response.CODE_ERROR];
                    mDefaultError = ret.ToString();
                }
            }

            return mDefaultError;
        }
        
        public override string GetCodeMessage(string typeName, int s2c_code)
        {
            return string.Empty;
//            if (s2c_code == Response.CODE_OK)
//            {
//                return string.Empty;
//            }
            try
            {
                using(var table = (LuaTable) LuaSvr.mainState["MessageCodeAttribute"])
                using (var codeTable = (LuaTable) table[typeName])
                {
                    var ret = codeTable[s2c_code];
                    return ret != null ? ret.ToString() : GetDefaultError();
                }
                
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}