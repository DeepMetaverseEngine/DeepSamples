using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_LineTextureMode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getStretch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LineTextureMode.Stretch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Stretch(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LineTextureMode.Stretch);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTile(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LineTextureMode.Tile);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tile(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LineTextureMode.Tile);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getDistributePerSegment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LineTextureMode.DistributePerSegment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DistributePerSegment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LineTextureMode.DistributePerSegment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRepeatPerSegment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.LineTextureMode.RepeatPerSegment);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RepeatPerSegment(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.LineTextureMode.RepeatPerSegment);
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
		getTypeTable(l,"UnityEngine.LineTextureMode");
		addMember(l,"Stretch",getStretch,null,false);
		addMember(l,"_Stretch",get_Stretch,null,false);
		addMember(l,"Tile",getTile,null,false);
		addMember(l,"_Tile",get_Tile,null,false);
		addMember(l,"DistributePerSegment",getDistributePerSegment,null,false);
		addMember(l,"_DistributePerSegment",get_DistributePerSegment,null,false);
		addMember(l,"RepeatPerSegment",getRepeatPerSegment,null,false);
		addMember(l,"_RepeatPerSegment",get_RepeatPerSegment,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.LineTextureMode));
	}
}
