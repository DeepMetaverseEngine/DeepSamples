using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_QuadItemShow : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddNodeConfig(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.AddNodeConfig(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveNodeConfig(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.RemoveNodeConfig(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetNodeConfig(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetNodeConfig(a1);
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
	static public int VisibleConfigNode(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			QuadItemShow.NodeConfig a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.VisibleConfigNode(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetNodeConfigVal(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.SetNodeConfigVal(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ClearExternChildren(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			self.ClearExternChildren();
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
	static public int get_BackgroundLockunlock(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuadItemShow.BackgroundLockunlock);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BackgroundNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuadItemShow.BackgroundNone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigBackground(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuadItemShow.ConfigBackground);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ConfigIcon(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,QuadItemShow.ConfigIcon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TouchClick(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			DeepCore.Unity3D.UGUIEditor.UI.TouchClickHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.TouchClick=v;
			else if(op==1) self.TouchClick+=v;
			else if(op==2) self.TouchClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AtlasPath(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AtlasPath);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AtlasName(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AtlasName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowBackground(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowBackground);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ShowBackground(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.ShowBackground=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Status(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Status);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Status(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			QuadItemShow.ItemStatus v;
			checkEnum(l,2,out v);
			self.Status=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableTouch(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableTouch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableTouch(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableTouch=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Quality(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Quality);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Quality(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.Quality=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Icon(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Icon);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Icon(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Icon=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IconDir(IntPtr l) {
		try {
			QuadItemShow self=(QuadItemShow)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IconDir);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"QuadItemShow");
		addMember(l,AddNodeConfig);
		addMember(l,RemoveNodeConfig);
		addMember(l,GetNodeConfig);
		addMember(l,VisibleConfigNode);
		addMember(l,SetNodeConfigVal);
		addMember(l,ClearExternChildren);
		addMember(l,"BackgroundLockunlock",get_BackgroundLockunlock,null,false);
		addMember(l,"BackgroundNone",get_BackgroundNone,null,false);
		addMember(l,"ConfigBackground",get_ConfigBackground,null,false);
		addMember(l,"ConfigIcon",get_ConfigIcon,null,false);
		addMember(l,"TouchClick",null,set_TouchClick,true);
		addMember(l,"AtlasPath",get_AtlasPath,null,true);
		addMember(l,"AtlasName",get_AtlasName,null,true);
		addMember(l,"ShowBackground",get_ShowBackground,set_ShowBackground,true);
		addMember(l,"Status",get_Status,set_Status,true);
		addMember(l,"EnableTouch",get_EnableTouch,set_EnableTouch,true);
		addMember(l,"Quality",get_Quality,set_Quality,true);
		addMember(l,"Icon",get_Icon,set_Icon,true);
		addMember(l,"IconDir",get_IconDir,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(QuadItemShow),typeof(DeepCore.Unity3D.UGUIEditor.UIComponent));
	}
}
