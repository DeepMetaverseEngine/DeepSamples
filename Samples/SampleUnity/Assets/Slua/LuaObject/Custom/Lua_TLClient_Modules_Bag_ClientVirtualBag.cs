using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLClient_Modules_Bag_ClientVirtualBag : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag o;
			System.Byte a1;
			checkType(l,2,out a1);
			DeepCore.FuckPomeloClient.PomeloClient a2;
			checkType(l,3,out a2);
			o=new TLClient.Modules.Bag.ClientVirtualBag(a1,a2);
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
	static public int AddAction(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a2;
			checkDelegate(l,3,out a2);
			self.AddAction(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAction(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a2;
			checkDelegate(l,3,out a2);
			self.RemoveAction(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SubscribSilver(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.SubscribSilver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribSilver(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.UnSubscribSilver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SubscribCopper(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.SubscribCopper(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribCopper(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.UnSubscribCopper(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SubscribDiamond(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.SubscribDiamond(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribDiamond(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.UnSubscribDiamond(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SubscribFate(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.SubscribFate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UnSubscribFate(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			TLClient.Modules.Bag.ClientVirtualBag.ModifyActionHandler a1;
			checkDelegate(l,2,out a1);
			self.UnSubscribFate(a1);
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
	static public int get_Silver(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Silver);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Copper(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Copper);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Diamond(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Diamond);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Fate(IntPtr l) {
		try {
			TLClient.Modules.Bag.ClientVirtualBag self=(TLClient.Modules.Bag.ClientVirtualBag)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Fate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLClient.Modules.Bag.ClientVirtualBag");
		addMember(l,AddAction);
		addMember(l,RemoveAction);
		addMember(l,SubscribSilver);
		addMember(l,UnSubscribSilver);
		addMember(l,SubscribCopper);
		addMember(l,UnSubscribCopper);
		addMember(l,SubscribDiamond);
		addMember(l,UnSubscribDiamond);
		addMember(l,SubscribFate);
		addMember(l,UnSubscribFate);
		addMember(l,"Silver",get_Silver,null,true);
		addMember(l,"Copper",get_Copper,null,true);
		addMember(l,"Diamond",get_Diamond,null,true);
		addMember(l,"Fate",get_Fate,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLClient.Modules.Bag.ClientVirtualBag),typeof(TLClient.Modules.Bag.ClientBag));
	}
}
