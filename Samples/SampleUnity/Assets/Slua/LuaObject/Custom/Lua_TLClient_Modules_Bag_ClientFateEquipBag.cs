using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_Bag_ClientFateEquipBag : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientFateEquipBag o;
			System.Byte a1;
			checkType(l,2,out a1);
			DeepCore.FuckPomeloClient.PomeloClient a2;
			checkType(l,3,out a2);
			o=new TLClient.Modules.Bag.ClientFateEquipBag(a1,a2);
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
	static public int UnEquip(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientFateEquipBag self=(TLClient.Modules.Bag.ClientFateEquipBag)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Action<System.Boolean> a2;
			checkDelegate(l,3,out a2);
			self.UnEquip(a1,a2);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Modules.Bag.ClientFateEquipBag");
		addMember(l,UnEquip);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Modules.Bag.ClientFateEquipBag),typeof(TLClient.Modules.Bag.ClientBag));
	}
}
