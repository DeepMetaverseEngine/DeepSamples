using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIActor_AutoMoveType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNormal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Normal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.Normal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMapTouch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.MapTouch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MapTouch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.MapTouch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getFollowNpc(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.FollowNpc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FollowNpc(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.FollowNpc);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPickItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.PickItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PickItem(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.PickItem);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnterGuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.EnterGuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnterGuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.EnterGuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnterOtherGuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.EnterOtherGuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnterOtherGuild(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.EnterOtherGuild);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSmallMapTouch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.SmallMapTouch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SmallMapTouch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.SmallMapTouch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTransPort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLAIActor.AutoMoveType.TransPort);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TransPort(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)TLAIActor.AutoMoveType.TransPort);
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
		getTypeTable(l,"AutoMoveType");
		addMember(l,"Normal",getNormal,null,false);
		addMember(l,"_Normal",get_Normal,null,false);
		addMember(l,"MapTouch",getMapTouch,null,false);
		addMember(l,"_MapTouch",get_MapTouch,null,false);
		addMember(l,"FollowNpc",getFollowNpc,null,false);
		addMember(l,"_FollowNpc",get_FollowNpc,null,false);
		addMember(l,"PickItem",getPickItem,null,false);
		addMember(l,"_PickItem",get_PickItem,null,false);
		addMember(l,"EnterGuild",getEnterGuild,null,false);
		addMember(l,"_EnterGuild",get_EnterGuild,null,false);
		addMember(l,"EnterOtherGuild",getEnterOtherGuild,null,false);
		addMember(l,"_EnterOtherGuild",get_EnterOtherGuild,null,false);
		addMember(l,"SmallMapTouch",getSmallMapTouch,null,false);
		addMember(l,"_SmallMapTouch",get_SmallMapTouch,null,false);
		addMember(l,"TransPort",getTransPort,null,false);
		addMember(l,"_TransPort",get_TransPort,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLAIActor.AutoMoveType));
	}
}
