using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_RenderTextureCreationFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMipMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.MipMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MipMap(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.MipMap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAutoGenerateMips(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.AutoGenerateMips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AutoGenerateMips(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.AutoGenerateMips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.SRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SRGB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.SRGB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEyeTexture(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.EyeTexture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EyeTexture(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.EyeTexture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnableRandomWrite(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.EnableRandomWrite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableRandomWrite(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.EnableRandomWrite);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCreatedFromScript(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.CreatedFromScript);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CreatedFromScript(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.CreatedFromScript);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAllowVerticalFlip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.AllowVerticalFlip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AllowVerticalFlip(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.AllowVerticalFlip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNoResolvedColorSurface(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.NoResolvedColorSurface);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NoResolvedColorSurface(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.NoResolvedColorSurface);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDynamicallyScalable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.RenderTextureCreationFlags.DynamicallyScalable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DynamicallyScalable(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.RenderTextureCreationFlags.DynamicallyScalable);
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
		getTypeTable(l,"UnityEngine.RenderTextureCreationFlags");
		addMember(l,"MipMap",getMipMap,null,false);
		addMember(l,"_MipMap",get_MipMap,null,false);
		addMember(l,"AutoGenerateMips",getAutoGenerateMips,null,false);
		addMember(l,"_AutoGenerateMips",get_AutoGenerateMips,null,false);
		addMember(l,"SRGB",getSRGB,null,false);
		addMember(l,"_SRGB",get_SRGB,null,false);
		addMember(l,"EyeTexture",getEyeTexture,null,false);
		addMember(l,"_EyeTexture",get_EyeTexture,null,false);
		addMember(l,"EnableRandomWrite",getEnableRandomWrite,null,false);
		addMember(l,"_EnableRandomWrite",get_EnableRandomWrite,null,false);
		addMember(l,"CreatedFromScript",getCreatedFromScript,null,false);
		addMember(l,"_CreatedFromScript",get_CreatedFromScript,null,false);
		addMember(l,"AllowVerticalFlip",getAllowVerticalFlip,null,false);
		addMember(l,"_AllowVerticalFlip",get_AllowVerticalFlip,null,false);
		addMember(l,"NoResolvedColorSurface",getNoResolvedColorSurface,null,false);
		addMember(l,"_NoResolvedColorSurface",get_NoResolvedColorSurface,null,false);
		addMember(l,"DynamicallyScalable",getDynamicallyScalable,null,false);
		addMember(l,"_DynamicallyScalable",get_DynamicallyScalable,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.RenderTextureCreationFlags));
	}
}
