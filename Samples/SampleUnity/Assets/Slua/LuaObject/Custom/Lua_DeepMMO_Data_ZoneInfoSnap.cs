using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepMMO_Data_ZoneInfoSnap : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap o;
			o=new DeepMMO.Data.ZoneInfoSnap();
			pushValue(l,true);
			pushValue(l,o);
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_lineIndex(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.lineIndex);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_lineIndex(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.lineIndex=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_curPlayerCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.curPlayerCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_curPlayerCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.curPlayerCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_playerMaxCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.playerMaxCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_playerMaxCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.playerMaxCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_playerFullCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.playerFullCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_playerFullCount(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.playerFullCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_uuid(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.uuid);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_uuid(IntPtr l) {
		try {
			DeepMMO.Data.ZoneInfoSnap self=(DeepMMO.Data.ZoneInfoSnap)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.uuid=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ZoneInfoSnap");
		addMember(l,"lineIndex",get_lineIndex,set_lineIndex,true);
		addMember(l,"curPlayerCount",get_curPlayerCount,set_curPlayerCount,true);
		addMember(l,"playerMaxCount",get_playerMaxCount,set_playerMaxCount,true);
		addMember(l,"playerFullCount",get_playerFullCount,set_playerFullCount,true);
		addMember(l,"uuid",get_uuid,set_uuid,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepMMO.Data.ZoneInfoSnap));
	}
}
