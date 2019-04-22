using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SmallMapNpc : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			SmallMapNpc o;
			System.Int32 a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.UGUIEditor.UI.HZCanvas a2;
			checkType(l,3,out a2);
			o=new SmallMapNpc(a1,a2);
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
	static public int SetForce(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			System.Byte a1;
			checkType(l,2,out a1);
			self.SetForce(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideMain(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			self.HideMain();
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
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
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
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
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
	static public int GetTemplateId(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			var ret=self.GetTemplateId();
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
	static public int UpdateQuest(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			self.UpdateQuest(a1,a2,a3);
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
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
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
	static public int get_templetaId(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.templetaId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_templetaId(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.templetaId=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_questState(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.questState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_questState(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.questState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Visible(IntPtr l) {
		try {
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
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
			SmallMapNpc self=(SmallMapNpc)checkSelf(l);
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
		getTypeTable(l,"SmallMapNpc");
		addMember(l,SetForce);
		addMember(l,HideMain);
		addMember(l,Clear);
		addMember(l,SetPosition);
		addMember(l,GetTemplateId);
		addMember(l,UpdateQuest);
		addMember(l,SetImage);
		addMember(l,"templetaId",get_templetaId,set_templetaId,true);
		addMember(l,"questState",get_questState,set_questState,true);
		addMember(l,"Visible",get_Visible,set_Visible,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(SmallMapNpc));
	}
}
