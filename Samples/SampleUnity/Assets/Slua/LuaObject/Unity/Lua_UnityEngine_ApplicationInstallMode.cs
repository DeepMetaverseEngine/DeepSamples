using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ApplicationInstallMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUnknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Unknown(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.Unknown);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStore(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.Store);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Store(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.Store);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDeveloperBuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.DeveloperBuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DeveloperBuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.DeveloperBuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAdhoc(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.Adhoc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Adhoc(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.Adhoc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnterprise(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.Enterprise);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Enterprise(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.Enterprise);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ApplicationInstallMode.Editor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Editor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ApplicationInstallMode.Editor);
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
		getTypeTable(l,"UnityEngine.ApplicationInstallMode");
		addMember(l,"Unknown",getUnknown,null,false);
		addMember(l,"_Unknown",get_Unknown,null,false);
		addMember(l,"Store",getStore,null,false);
		addMember(l,"_Store",get_Store,null,false);
		addMember(l,"DeveloperBuild",getDeveloperBuild,null,false);
		addMember(l,"_DeveloperBuild",get_DeveloperBuild,null,false);
		addMember(l,"Adhoc",getAdhoc,null,false);
		addMember(l,"_Adhoc",get_Adhoc,null,false);
		addMember(l,"Enterprise",getEnterprise,null,false);
		addMember(l,"_Enterprise",get_Enterprise,null,false);
		addMember(l,"Editor",getEditor,null,false);
		addMember(l,"_Editor",get_Editor,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ApplicationInstallMode));
	}
}
