using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_ColorGamut : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getsRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.sRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_sRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.sRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRec709(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.Rec709);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rec709(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.Rec709);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRec2020(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.Rec2020);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rec2020(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.Rec2020);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDisplayP3(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.DisplayP3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DisplayP3(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.DisplayP3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHDR10(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.HDR10);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HDR10(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.HDR10);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDolbyHDR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.ColorGamut.DolbyHDR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DolbyHDR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.ColorGamut.DolbyHDR);
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
		getTypeTable(l,"UnityEngine.ColorGamut");
		addMember(l,"sRGB",getsRGB,null,false);
		addMember(l,"_sRGB",get_sRGB,null,false);
		addMember(l,"Rec709",getRec709,null,false);
		addMember(l,"_Rec709",get_Rec709,null,false);
		addMember(l,"Rec2020",getRec2020,null,false);
		addMember(l,"_Rec2020",get_Rec2020,null,false);
		addMember(l,"DisplayP3",getDisplayP3,null,false);
		addMember(l,"_DisplayP3",get_DisplayP3,null,false);
		addMember(l,"HDR10",getHDR10,null,false);
		addMember(l,"_HDR10",get_HDR10,null,false);
		addMember(l,"DolbyHDR",getDolbyHDR,null,false);
		addMember(l,"_DolbyHDR",get_DolbyHDR,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.ColorGamut));
	}
}
