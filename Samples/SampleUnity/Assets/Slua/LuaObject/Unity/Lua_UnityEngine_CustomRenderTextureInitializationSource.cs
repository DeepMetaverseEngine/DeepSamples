﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CustomRenderTextureInitializationSource : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTextureAndColor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureInitializationSource.TextureAndColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextureAndColor(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureInitializationSource.TextureAndColor);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMaterial(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CustomRenderTextureInitializationSource.Material);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Material(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CustomRenderTextureInitializationSource.Material);
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
		getTypeTable(l,"UnityEngine.CustomRenderTextureInitializationSource");
		addMember(l,"TextureAndColor",getTextureAndColor,null,false);
		addMember(l,"_TextureAndColor",get_TextureAndColor,null,false);
		addMember(l,"Material",getMaterial,null,false);
		addMember(l,"_Material",get_Material,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CustomRenderTextureInitializationSource));
	}
}