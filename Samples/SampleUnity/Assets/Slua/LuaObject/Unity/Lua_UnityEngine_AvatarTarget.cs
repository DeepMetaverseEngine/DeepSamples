using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AvatarTarget : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.Root);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Root(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.Root);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBody(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.Body);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Body(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.Body);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.LeftFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.LeftFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.RightFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.RightFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.LeftHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.LeftHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AvatarTarget.RightHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AvatarTarget.RightHand);
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
		getTypeTable(l,"UnityEngine.AvatarTarget");
		addMember(l,"Root",getRoot,null,false);
		addMember(l,"_Root",get_Root,null,false);
		addMember(l,"Body",getBody,null,false);
		addMember(l,"_Body",get_Body,null,false);
		addMember(l,"LeftFoot",getLeftFoot,null,false);
		addMember(l,"_LeftFoot",get_LeftFoot,null,false);
		addMember(l,"RightFoot",getRightFoot,null,false);
		addMember(l,"_RightFoot",get_RightFoot,null,false);
		addMember(l,"LeftHand",getLeftHand,null,false);
		addMember(l,"_LeftHand",get_LeftHand,null,false);
		addMember(l,"RightHand",getRightHand,null,false);
		addMember(l,"_RightHand",get_RightHand,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AvatarTarget));
	}
}
