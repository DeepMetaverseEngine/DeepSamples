using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PublicConst : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			PublicConst o;
			o=new PublicConst();
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
	static public int get_ClientVersion(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.ClientVersion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ClientVersion(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			PublicConst.ClientVersion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SVNVersion(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.SVNVersion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SVNVersion(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			PublicConst.SVNVersion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LogicVersion(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.LogicVersion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LogicVersion(IntPtr l) {
		try {
			System.Int32 v;
			checkType(l,2,out v);
			PublicConst.LogicVersion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ClientRegion(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.ClientRegion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ClientRegion(IntPtr l) {
		try {
			System.String v;
			checkType(l,2,out v);
			PublicConst.ClientRegion=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_REAR_BUFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_REAR_BUFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_HEAD_BUFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_HEAD_BUFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_CHEST_BUFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_CHEST_BUFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_FOOT_BUFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_FOOT_BUFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_RIGHT_SPELL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_RIGHT_SPELL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_LEFT_SPELL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_LEFT_SPELL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_RIGHT_HAND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_RIGHT_HAND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_LEFT_HAND(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_LEFT_HAND);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_FIRSTGLOVE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_FIRSTGLOVE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_HEAD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_HEAD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DUMMY_COFFIN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.DUMMY_COFFIN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_crossFadeLength(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.crossFadeLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AccountLength(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.AccountLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PasswordLength(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.PasswordLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RoleNameLength(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.RoleNameLength);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OSType(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,PublicConst.OSType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PublicConst");
		addMember(l,"ClientVersion",get_ClientVersion,set_ClientVersion,false);
		addMember(l,"SVNVersion",get_SVNVersion,set_SVNVersion,false);
		addMember(l,"LogicVersion",get_LogicVersion,set_LogicVersion,false);
		addMember(l,"ClientRegion",get_ClientRegion,set_ClientRegion,false);
		addMember(l,"DUMMY_REAR_BUFF",get_DUMMY_REAR_BUFF,null,false);
		addMember(l,"DUMMY_HEAD_BUFF",get_DUMMY_HEAD_BUFF,null,false);
		addMember(l,"DUMMY_CHEST_BUFF",get_DUMMY_CHEST_BUFF,null,false);
		addMember(l,"DUMMY_FOOT_BUFF",get_DUMMY_FOOT_BUFF,null,false);
		addMember(l,"DUMMY_RIGHT_SPELL",get_DUMMY_RIGHT_SPELL,null,false);
		addMember(l,"DUMMY_LEFT_SPELL",get_DUMMY_LEFT_SPELL,null,false);
		addMember(l,"DUMMY_RIGHT_HAND",get_DUMMY_RIGHT_HAND,null,false);
		addMember(l,"DUMMY_LEFT_HAND",get_DUMMY_LEFT_HAND,null,false);
		addMember(l,"DUMMY_FIRSTGLOVE",get_DUMMY_FIRSTGLOVE,null,false);
		addMember(l,"DUMMY_HEAD",get_DUMMY_HEAD,null,false);
		addMember(l,"DUMMY_COFFIN",get_DUMMY_COFFIN,null,false);
		addMember(l,"crossFadeLength",get_crossFadeLength,null,false);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"AccountLength",get_AccountLength,null,false);
		addMember(l,"PasswordLength",get_PasswordLength,null,false);
		addMember(l,"RoleNameLength",get_RoleNameLength,null,false);
		addMember(l,"OSType",get_OSType,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(PublicConst));
	}
}
