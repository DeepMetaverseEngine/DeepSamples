using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SkinQuality : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAuto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SkinQuality.Auto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Auto(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SkinQuality.Auto);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBone1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SkinQuality.Bone1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bone1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SkinQuality.Bone1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBone2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SkinQuality.Bone2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bone2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SkinQuality.Bone2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBone4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SkinQuality.Bone4);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Bone4(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SkinQuality.Bone4);
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
		getTypeTable(l,"UnityEngine.SkinQuality");
		addMember(l,"Auto",getAuto,null,false);
		addMember(l,"_Auto",get_Auto,null,false);
		addMember(l,"Bone1",getBone1,null,false);
		addMember(l,"_Bone1",get_Bone1,null,false);
		addMember(l,"Bone2",getBone2,null,false);
		addMember(l,"_Bone2",get_Bone2,null,false);
		addMember(l,"Bone4",getBone4,null,false);
		addMember(l,"_Bone4",get_Bone4,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SkinQuality));
	}
}
