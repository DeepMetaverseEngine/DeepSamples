using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CustomRenderTextureUpdateZoneSpace : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormalized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureUpdateZoneSpace.Normalized);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normalized(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureUpdateZoneSpace.Normalized);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPixel(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureUpdateZoneSpace.Pixel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Pixel(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureUpdateZoneSpace.Pixel);
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
		getTypeTable(l,"UnityEngine.CustomRenderTextureUpdateZoneSpace");
		addMember(l,"Normalized",getNormalized,null,false);
		addMember(l,"_Normalized",get_Normalized,null,false);
		addMember(l,"Pixel",getPixel,null,false);
		addMember(l,"_Pixel",get_Pixel,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CustomRenderTextureUpdateZoneSpace));
	}
}
