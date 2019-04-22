using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CollisionFlags : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollidedSides(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.CollidedSides);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CollidedSides(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.CollidedSides);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSides(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.Sides);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Sides(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.Sides);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollidedAbove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.CollidedAbove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CollidedAbove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.CollidedAbove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAbove(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.Above);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Above(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.Above);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCollidedBelow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.CollidedBelow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CollidedBelow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.CollidedBelow);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getBelow(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CollisionFlags.Below);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Below(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CollisionFlags.Below);
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
		getTypeTable(l,"UnityEngine.CollisionFlags");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"CollidedSides",getCollidedSides,null,false);
		addMember(l,"_CollidedSides",get_CollidedSides,null,false);
		addMember(l,"Sides",getSides,null,false);
		addMember(l,"_Sides",get_Sides,null,false);
		addMember(l,"CollidedAbove",getCollidedAbove,null,false);
		addMember(l,"_CollidedAbove",get_CollidedAbove,null,false);
		addMember(l,"Above",getAbove,null,false);
		addMember(l,"_Above",get_Above,null,false);
		addMember(l,"CollidedBelow",getCollidedBelow,null,false);
		addMember(l,"_CollidedBelow",get_CollidedBelow,null,false);
		addMember(l,"Below",getBelow,null,false);
		addMember(l,"_Below",get_Below,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CollisionFlags));
	}
}
