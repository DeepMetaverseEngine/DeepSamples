using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_HZToggleButton_LockState : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int geteNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eNone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_eNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eNone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int geteLockSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eLockSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_eLockSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eLockSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int geteLockUnSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eLockUnSelect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_eLockUnSelect(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState.eLockUnSelect);
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
		getTypeTable(l,"HZToggleButton.LockState");
		addMember(l,"eNone",geteNone,null,false);
		addMember(l,"_eNone",get_eNone,null,false);
		addMember(l,"eLockSelect",geteLockSelect,null,false);
		addMember(l,"_eLockSelect",get_eLockSelect,null,false);
		addMember(l,"eLockUnSelect",geteLockUnSelect,null,false);
		addMember(l,"_eLockUnSelect",get_eLockUnSelect,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUIEditor.UI.HZToggleButton.LockState));
	}
}
