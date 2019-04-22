using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUIEditor_UI_UEFileNode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEFileNode o;
			o=new DeepCore.Unity3D.UGUIEditor.UI.UEFileNode();
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
	static public int get_FileNodeName(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEFileNode self=(DeepCore.Unity3D.UGUIEditor.UI.UEFileNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FileNodeName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FileNodeRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUIEditor.UI.UEFileNode self=(DeepCore.Unity3D.UGUIEditor.UI.UEFileNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FileNodeRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UEFileNode");
		addMember(l,"FileNodeName",get_FileNodeName,null,true);
		addMember(l,"FileNodeRoot",get_FileNodeRoot,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUIEditor.UI.UEFileNode),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
