using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UI_Image_FillMethod : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHorizontal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Image.FillMethod.Horizontal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Horizontal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Image.FillMethod.Horizontal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVertical(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Image.FillMethod.Vertical);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Vertical(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Image.FillMethod.Vertical);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRadial90(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Image.FillMethod.Radial90);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Radial90(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Image.FillMethod.Radial90);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRadial180(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Image.FillMethod.Radial180);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Radial180(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Image.FillMethod.Radial180);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRadial360(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UI.Image.FillMethod.Radial360);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Radial360(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UI.Image.FillMethod.Radial360);
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
		getTypeTable(l,"UnityEngine.UI.Image.FillMethod");
		addMember(l,"Horizontal",getHorizontal,null,false);
		addMember(l,"_Horizontal",get_Horizontal,null,false);
		addMember(l,"Vertical",getVertical,null,false);
		addMember(l,"_Vertical",get_Vertical,null,false);
		addMember(l,"Radial90",getRadial90,null,false);
		addMember(l,"_Radial90",get_Radial90,null,false);
		addMember(l,"Radial180",getRadial180,null,false);
		addMember(l,"_Radial180",get_Radial180,null,false);
		addMember(l,"Radial360",getRadial360,null,false);
		addMember(l,"_Radial360",get_Radial360,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UI.Image.FillMethod));
	}
}
