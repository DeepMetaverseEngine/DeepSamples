using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UELabel : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.Unity3D.UGUIEditor.UI.UELabel o;
			if(argc==1){
				o=new DeepCore.Unity3D.UGUIEditor.UI.UELabel();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==2){
				System.Boolean a1;
				checkType(l,2,out a1);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UELabel(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==5){
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.Unity3D.UGUIEditor.UILayout a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Cell.CPJAtlas a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				o=new DeepCore.Unity3D.UGUIEditor.UI.UELabel(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UELabel");
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UELabel),typeof(DeepCore.Unity3D.UGUIEditor.UI.UETextComponent));
	}
}
