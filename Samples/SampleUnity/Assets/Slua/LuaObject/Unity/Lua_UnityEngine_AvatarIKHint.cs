using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AvatarIKHint : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftKnee(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarIKHint.LeftKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftKnee(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarIKHint.LeftKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightKnee(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarIKHint.RightKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightKnee(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarIKHint.RightKnee);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftElbow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarIKHint.LeftElbow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftElbow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarIKHint.LeftElbow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightElbow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarIKHint.RightElbow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightElbow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarIKHint.RightElbow);
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
		getTypeTable(l,"UnityEngine.AvatarIKHint");
		addMember(l,"LeftKnee",getLeftKnee,null,false);
		addMember(l,"_LeftKnee",get_LeftKnee,null,false);
		addMember(l,"RightKnee",getRightKnee,null,false);
		addMember(l,"_RightKnee",get_RightKnee,null,false);
		addMember(l,"LeftElbow",getLeftElbow,null,false);
		addMember(l,"_LeftElbow",get_LeftElbow,null,false);
		addMember(l,"RightElbow",getRightElbow,null,false);
		addMember(l,"_RightElbow",get_RightElbow,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AvatarIKHint));
	}
}
