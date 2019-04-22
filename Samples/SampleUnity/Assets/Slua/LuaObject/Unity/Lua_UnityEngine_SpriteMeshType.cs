using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SpriteMeshType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFullRect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteMeshType.FullRect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FullRect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteMeshType.FullRect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteMeshType.Tight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteMeshType.Tight);
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
		getTypeTable(l,"UnityEngine.SpriteMeshType");
		addMember(l,"FullRect",getFullRect,null,false);
		addMember(l,"_FullRect",get_FullRect,null,false);
		addMember(l,"Tight",getTight,null,false);
		addMember(l,"_Tight",get_Tight,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SpriteMeshType));
	}
}
