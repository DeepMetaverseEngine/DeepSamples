using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_BlendWeights : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOneBone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BlendWeights.OneBone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OneBone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BlendWeights.OneBone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTwoBones(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BlendWeights.TwoBones);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TwoBones(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BlendWeights.TwoBones);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFourBones(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.BlendWeights.FourBones);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FourBones(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.BlendWeights.FourBones);
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
		getTypeTable(l,"UnityEngine.BlendWeights");
		addMember(l,"OneBone",getOneBone,null,false);
		addMember(l,"_OneBone",get_OneBone,null,false);
		addMember(l,"TwoBones",getTwoBones,null,false);
		addMember(l,"_TwoBones",get_TwoBones,null,false);
		addMember(l,"FourBones",getFourBones,null,false);
		addMember(l,"_FourBones",get_FourBones,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.BlendWeights));
	}
}
