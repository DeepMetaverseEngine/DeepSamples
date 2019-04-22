using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RenderTextureFormat : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGB32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGB32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGB32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGB32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDepth(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.Depth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Depth(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.Depth);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGBHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGBHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGBHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGBHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getShadowmap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.Shadowmap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Shadowmap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.Shadowmap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGB565(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGB565);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGB565(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGB565);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGB4444(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGB4444);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGB4444(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGB4444);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGB1555(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGB1555);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGB1555(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGB1555);
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
			pushValue(l,UnityEngine.RenderTextureFormat.Default);
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
			pushValue(l,(double)UnityEngine.RenderTextureFormat.Default);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGB2101010(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGB2101010);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGB2101010(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGB2101010);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDefaultHDR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.DefaultHDR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultHDR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.DefaultHDR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGB64(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGB64);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGB64(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGB64);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGBFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGBFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGBFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGBFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RFloat(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RFloat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RHalf(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RHalf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getR8(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.R8);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_R8(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.R8);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getARGBInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.ARGBInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ARGBInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.ARGBInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RInt(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RInt);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBGRA32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.BGRA32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BGRA32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.BGRA32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGB111110Float(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGB111110Float);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGB111110Float(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGB111110Float);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRG32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RG32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RG32(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RG32);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRGBAUShort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RGBAUShort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RGBAUShort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RGBAUShort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRG16(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.RG16);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RG16(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.RG16);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBGRA10101010_XR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.BGRA10101010_XR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BGRA10101010_XR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.BGRA10101010_XR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBGR101010_XR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureFormat.BGR101010_XR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BGR101010_XR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureFormat.BGR101010_XR);
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
		getTypeTable(l,"UnityEngine.RenderTextureFormat");
		addMember(l,"ARGB32",getARGB32,null,false);
		addMember(l,"_ARGB32",get_ARGB32,null,false);
		addMember(l,"Depth",getDepth,null,false);
		addMember(l,"_Depth",get_Depth,null,false);
		addMember(l,"ARGBHalf",getARGBHalf,null,false);
		addMember(l,"_ARGBHalf",get_ARGBHalf,null,false);
		addMember(l,"Shadowmap",getShadowmap,null,false);
		addMember(l,"_Shadowmap",get_Shadowmap,null,false);
		addMember(l,"RGB565",getRGB565,null,false);
		addMember(l,"_RGB565",get_RGB565,null,false);
		addMember(l,"ARGB4444",getARGB4444,null,false);
		addMember(l,"_ARGB4444",get_ARGB4444,null,false);
		addMember(l,"ARGB1555",getARGB1555,null,false);
		addMember(l,"_ARGB1555",get_ARGB1555,null,false);
		addMember(l,"Default",getDefault,null,false);
		addMember(l,"_Default",get_Default,null,false);
		addMember(l,"ARGB2101010",getARGB2101010,null,false);
		addMember(l,"_ARGB2101010",get_ARGB2101010,null,false);
		addMember(l,"DefaultHDR",getDefaultHDR,null,false);
		addMember(l,"_DefaultHDR",get_DefaultHDR,null,false);
		addMember(l,"ARGB64",getARGB64,null,false);
		addMember(l,"_ARGB64",get_ARGB64,null,false);
		addMember(l,"ARGBFloat",getARGBFloat,null,false);
		addMember(l,"_ARGBFloat",get_ARGBFloat,null,false);
		addMember(l,"RGFloat",getRGFloat,null,false);
		addMember(l,"_RGFloat",get_RGFloat,null,false);
		addMember(l,"RGHalf",getRGHalf,null,false);
		addMember(l,"_RGHalf",get_RGHalf,null,false);
		addMember(l,"RFloat",getRFloat,null,false);
		addMember(l,"_RFloat",get_RFloat,null,false);
		addMember(l,"RHalf",getRHalf,null,false);
		addMember(l,"_RHalf",get_RHalf,null,false);
		addMember(l,"R8",getR8,null,false);
		addMember(l,"_R8",get_R8,null,false);
		addMember(l,"ARGBInt",getARGBInt,null,false);
		addMember(l,"_ARGBInt",get_ARGBInt,null,false);
		addMember(l,"RGInt",getRGInt,null,false);
		addMember(l,"_RGInt",get_RGInt,null,false);
		addMember(l,"RInt",getRInt,null,false);
		addMember(l,"_RInt",get_RInt,null,false);
		addMember(l,"BGRA32",getBGRA32,null,false);
		addMember(l,"_BGRA32",get_BGRA32,null,false);
		addMember(l,"RGB111110Float",getRGB111110Float,null,false);
		addMember(l,"_RGB111110Float",get_RGB111110Float,null,false);
		addMember(l,"RG32",getRG32,null,false);
		addMember(l,"_RG32",get_RG32,null,false);
		addMember(l,"RGBAUShort",getRGBAUShort,null,false);
		addMember(l,"_RGBAUShort",get_RGBAUShort,null,false);
		addMember(l,"RG16",getRG16,null,false);
		addMember(l,"_RG16",get_RG16,null,false);
		addMember(l,"BGRA10101010_XR",getBGRA10101010_XR,null,false);
		addMember(l,"_BGRA10101010_XR",get_BGRA10101010_XR,null,false);
		addMember(l,"BGR101010_XR",getBGR101010_XR,null,false);
		addMember(l,"_BGR101010_XR",get_BGR101010_XR,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RenderTextureFormat));
	}
}
