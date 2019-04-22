using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SmallMapUnit : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			SmallMapUnit o;
			DeepCore.Unity3D.UGUIEditor.UI.UECanvas a1;
			checkType(l,2,out a1);
			System.Byte a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			o=new SmallMapUnit(a1,a2,a3);
			pushValue(l,true);
			pushValue(l,o);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFore(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			System.Byte a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetFore(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			self.Clear();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetPosition(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			self.SetPosition(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetImage(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.SetImage(a1);
			pushValue(l,true);
			return 1;
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Visible(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Visible);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Visible(IntPtr l) {
		try {
			SmallMapUnit self=(SmallMapUnit)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.Visible=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"SmallMapUnit");
		addMember(l,SetFore);
		addMember(l,Clear);
		addMember(l,SetPosition);
		addMember(l,SetImage);
		addMember(l,"Visible",get_Visible,set_Visible,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(SmallMapUnit));
	}
}
