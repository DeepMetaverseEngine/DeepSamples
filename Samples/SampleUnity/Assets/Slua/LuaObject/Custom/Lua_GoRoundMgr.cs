using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_GoRoundMgr : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			GoRoundMgr o;
			o=new GoRoundMgr();
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
	static public int SetEnable(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SetEnable(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNode(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			var ret=self.getNode();
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
	static public int SetLocation(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			self.SetLocation(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int cutover(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			self.cutover();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int clear(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			self.clear();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int addTip(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.addTip(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int changeBG(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.changeBG(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int changeBGXml(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.changeBGXml(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int destroy(IntPtr l) {
		try {
			GoRoundMgr self=(GoRoundMgr)checkSelf(l);
			self.destroy();
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
		getTypeTable(l,"GoRoundMgr");
		addMember(l,SetEnable);
		addMember(l,getNode);
		addMember(l,SetLocation);
		addMember(l,cutover);
		addMember(l,clear);
		addMember(l,addTip);
		addMember(l,changeBG);
		addMember(l,changeBGXml);
		addMember(l,destroy);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(GoRoundMgr));
	}
}
