using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_HideFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.None);
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
			pushValue(l,(double)UnityEngine.HideFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHideInHierarchy(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.HideInHierarchy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideInHierarchy(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.HideInHierarchy);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHideInInspector(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.HideInInspector);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideInInspector(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.HideInInspector);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDontSaveInEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.DontSaveInEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontSaveInEditor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.DontSaveInEditor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNotEditable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.NotEditable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NotEditable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.NotEditable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDontSaveInBuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.DontSaveInBuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontSaveInBuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.DontSaveInBuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDontUnloadUnusedAsset(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.DontUnloadUnusedAsset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontUnloadUnusedAsset(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.DontUnloadUnusedAsset);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDontSave(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.DontSave);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontSave(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.DontSave);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHideAndDontSave(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HideFlags.HideAndDontSave);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HideAndDontSave(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HideFlags.HideAndDontSave);
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
		getTypeTable(l,"UnityEngine.HideFlags");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"HideInHierarchy",getHideInHierarchy,null,false);
		addMember(l,"_HideInHierarchy",get_HideInHierarchy,null,false);
		addMember(l,"HideInInspector",getHideInInspector,null,false);
		addMember(l,"_HideInInspector",get_HideInInspector,null,false);
		addMember(l,"DontSaveInEditor",getDontSaveInEditor,null,false);
		addMember(l,"_DontSaveInEditor",get_DontSaveInEditor,null,false);
		addMember(l,"NotEditable",getNotEditable,null,false);
		addMember(l,"_NotEditable",get_NotEditable,null,false);
		addMember(l,"DontSaveInBuild",getDontSaveInBuild,null,false);
		addMember(l,"_DontSaveInBuild",get_DontSaveInBuild,null,false);
		addMember(l,"DontUnloadUnusedAsset",getDontUnloadUnusedAsset,null,false);
		addMember(l,"_DontUnloadUnusedAsset",get_DontUnloadUnusedAsset,null,false);
		addMember(l,"DontSave",getDontSave,null,false);
		addMember(l,"_DontSave",get_DontSave,null,false);
		addMember(l,"HideAndDontSave",getHideAndDontSave,null,false);
		addMember(l,"_HideAndDontSave",get_HideAndDontSave,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.HideFlags));
	}
}
