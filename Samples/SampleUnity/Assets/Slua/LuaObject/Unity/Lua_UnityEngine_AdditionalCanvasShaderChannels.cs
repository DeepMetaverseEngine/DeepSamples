using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AdditionalCanvasShaderChannels : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTexCoord1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.TexCoord1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TexCoord1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.TexCoord1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTexCoord2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.TexCoord2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TexCoord2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.TexCoord2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTexCoord3(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.TexCoord3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TexCoord3(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.TexCoord3);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTangent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AdditionalCanvasShaderChannels.Tangent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tangent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AdditionalCanvasShaderChannels.Tangent);
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
		getTypeTable(l,"UnityEngine.AdditionalCanvasShaderChannels");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"TexCoord1",getTexCoord1,null,false);
		addMember(l,"_TexCoord1",get_TexCoord1,null,false);
		addMember(l,"TexCoord2",getTexCoord2,null,false);
		addMember(l,"_TexCoord2",get_TexCoord2,null,false);
		addMember(l,"TexCoord3",getTexCoord3,null,false);
		addMember(l,"_TexCoord3",get_TexCoord3,null,false);
		addMember(l,"Normal",getNormal,null,false);
		addMember(l,"_Normal",get_Normal,null,false);
		addMember(l,"Tangent",getTangent,null,false);
		addMember(l,"_Tangent",get_Tangent,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AdditionalCanvasShaderChannels));
	}
}
