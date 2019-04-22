using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_TLClientCreateRoleExtData_ProType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.ProType.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.ProType.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getYiZu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.ProType.YiZu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_YiZu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.ProType.YiZu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTianGong(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.ProType.TianGong);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TianGong(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.ProType.TianGong);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getKunLun(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.ProType.KunLun);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KunLun(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.ProType.KunLun);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getQinqQiu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.TLClientCreateRoleExtData.ProType.QinqQiu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QinqQiu(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLProtocol.Data.TLClientCreateRoleExtData.ProType.QinqQiu);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int op_Equality(IntPtr l) {
		try {
			System.Object a1;
			checkType(l,1,out a1);
			System.Object a2;
			checkType(l,2,out a2);
			var ret = System.Object.Equals(a1, a2);
			pushValue(l,true);
			pushValue(l,ret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	static public void reg(IntPtr l) {
		getTypeTable(l,"RoleProType");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"YiZu",getYiZu,null,false);
		addMember(l,"_YiZu",get_YiZu,null,false);
		addMember(l,"TianGong",getTianGong,null,false);
		addMember(l,"_TianGong",get_TianGong,null,false);
		addMember(l,"KunLun",getKunLun,null,false);
		addMember(l,"_KunLun",get_KunLun,null,false);
		addMember(l,"QinqQiu",getQinqQiu,null,false);
		addMember(l,"_QinqQiu",get_QinqQiu,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLProtocol.Data.TLClientCreateRoleExtData.ProType));
	}
}
