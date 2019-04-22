using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_JetBrains_Annotations_ImplicitUseTargetFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getItself(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseTargetFlags.Itself);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Itself(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseTargetFlags.Itself);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefault(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseTargetFlags.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Default(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseTargetFlags.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMembers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseTargetFlags.Members);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Members(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseTargetFlags.Members);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWithMembers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseTargetFlags.WithMembers);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WithMembers(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseTargetFlags.WithMembers);
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
		getTypeTable(l,"JetBrains.Annotations.ImplicitUseTargetFlags");
		addMember(l,"Itself",getItself,null,false);
		addMember(l,"_Itself",get_Itself,null,false);
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"Members",getMembers,null,false);
		addMember(l,"_Members",get_Members,null,false);
		addMember(l,"WithMembers",getWithMembers,null,false);
		addMember(l,"_WithMembers",get_WithMembers,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(JetBrains.Annotations.ImplicitUseTargetFlags));
	}
}
