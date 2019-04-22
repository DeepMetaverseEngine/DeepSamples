using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_SpriteAlignment : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.Center);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Center(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.Center);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTopLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.TopLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TopLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.TopLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTopCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.TopCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TopCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.TopCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTopRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.TopRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TopRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.TopRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.LeftCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.LeftCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.RightCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.RightCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBottomLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.BottomLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BottomLeft(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.BottomLeft);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBottomCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.BottomCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BottomCenter(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.BottomCenter);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBottomRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.BottomRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BottomRight(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.BottomRight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCustom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.SpriteAlignment.Custom);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Custom(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.SpriteAlignment.Custom);
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
		getTypeTable(l,"UnityEngine.SpriteAlignment");
		addMember(l,"Center",getCenter,null,false);
		addMember(l,"_Center",get_Center,null,false);
		addMember(l,"TopLeft",getTopLeft,null,false);
		addMember(l,"_TopLeft",get_TopLeft,null,false);
		addMember(l,"TopCenter",getTopCenter,null,false);
		addMember(l,"_TopCenter",get_TopCenter,null,false);
		addMember(l,"TopRight",getTopRight,null,false);
		addMember(l,"_TopRight",get_TopRight,null,false);
		addMember(l,"LeftCenter",getLeftCenter,null,false);
		addMember(l,"_LeftCenter",get_LeftCenter,null,false);
		addMember(l,"RightCenter",getRightCenter,null,false);
		addMember(l,"_RightCenter",get_RightCenter,null,false);
		addMember(l,"BottomLeft",getBottomLeft,null,false);
		addMember(l,"_BottomLeft",get_BottomLeft,null,false);
		addMember(l,"BottomCenter",getBottomCenter,null,false);
		addMember(l,"_BottomCenter",get_BottomCenter,null,false);
		addMember(l,"BottomRight",getBottomRight,null,false);
		addMember(l,"_BottomRight",get_BottomRight,null,false);
		addMember(l,"Custom",getCustom,null,false);
		addMember(l,"_Custom",get_Custom,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.SpriteAlignment));
	}
}
