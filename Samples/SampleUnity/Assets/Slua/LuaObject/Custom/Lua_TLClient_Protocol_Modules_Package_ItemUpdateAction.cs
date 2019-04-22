using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Protocol_Modules_Package_ItemUpdateAction : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			TLClient.Protocol.Modules.Package.ItemUpdateAction o;
			if(argc==5){
				TLClient.Protocol.Modules.Package.BasePackage a1;
				checkType(l,2,out a1);
				TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType a2;
				checkEnum(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				System.Object a4;
				checkType(l,5,out a4);
				o=new TLClient.Protocol.Modules.Package.ItemUpdateAction(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==4){
				TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType a1;
				checkEnum(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Object a3;
				checkType(l,4,out a3);
				o=new TLClient.Protocol.Modules.Package.ItemUpdateAction(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==1){
				o=new TLClient.Protocol.Modules.Package.ItemUpdateAction();
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
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Index(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Index);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Index(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Index=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Type(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Type);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Type(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			TLClient.Protocol.Modules.Package.ItemUpdateAction.ActionType v;
			checkEnum(l,2,out v);
			self.Type=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Param(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Param);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Param(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			System.Object v;
			checkType(l,2,out v);
			self.Param=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Reason(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Reason);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Reason(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.Reason=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TemplateSnap(IntPtr l) {
		try {
			TLClient.Protocol.Modules.Package.ItemUpdateAction self=(TLClient.Protocol.Modules.Package.ItemUpdateAction)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TemplateSnap);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemUpdateAction");
		addMember(l,"Index",get_Index,set_Index,true);
		addMember(l,"Type",get_Type,set_Type,true);
		addMember(l,"Param",get_Param,set_Param,true);
		addMember(l,"Reason",get_Reason,set_Reason,true);
		addMember(l,"TemplateSnap",get_TemplateSnap,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Protocol.Modules.Package.ItemUpdateAction));
	}
}
