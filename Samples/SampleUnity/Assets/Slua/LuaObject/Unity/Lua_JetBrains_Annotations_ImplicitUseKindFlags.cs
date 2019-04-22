using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_JetBrains_Annotations_ImplicitUseKindFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAccess(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseKindFlags.Access);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Access(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseKindFlags.Access);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAssign(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseKindFlags.Assign);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Assign(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseKindFlags.Assign);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInstantiatedWithFixedConstructorSignature(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InstantiatedWithFixedConstructorSignature(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature);
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
			pushValue(l,JetBrains.Annotations.ImplicitUseKindFlags.Default);
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
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseKindFlags.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInstantiatedNoFixedConstructorSignature(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InstantiatedNoFixedConstructorSignature(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature);
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
		getTypeTable(l,"JetBrains.Annotations.ImplicitUseKindFlags");
		addMember(l,"Access",getAccess,null,false);
		addMember(l,"_Access",get_Access,null,false);
		addMember(l,"Assign",getAssign,null,false);
		addMember(l,"_Assign",get_Assign,null,false);
		addMember(l,"InstantiatedWithFixedConstructorSignature",getInstantiatedWithFixedConstructorSignature,null,false);
		addMember(l,"_InstantiatedWithFixedConstructorSignature",get_InstantiatedWithFixedConstructorSignature,null,false);
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"InstantiatedNoFixedConstructorSignature",getInstantiatedNoFixedConstructorSignature,null,false);
		addMember(l,"_InstantiatedNoFixedConstructorSignature",get_InstantiatedNoFixedConstructorSignature,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(JetBrains.Annotations.ImplicitUseKindFlags));
	}
}
