using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_CameraType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getGame(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraType.Game);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Game(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraType.Game);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSceneView(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraType.SceneView);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SceneView(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraType.SceneView);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getPreview(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraType.Preview);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Preview(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraType.Preview);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraType.VR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraType.VR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getReflection(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.CameraType.Reflection);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Reflection(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.CameraType.Reflection);
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
		getTypeTable(l,"UnityEngine.CameraType");
		addMember(l,"Game",getGame,null,false);
		addMember(l,"_Game",get_Game,null,false);
		addMember(l,"SceneView",getSceneView,null,false);
		addMember(l,"_SceneView",get_SceneView,null,false);
		addMember(l,"Preview",getPreview,null,false);
		addMember(l,"_Preview",get_Preview,null,false);
		addMember(l,"VR",getVR,null,false);
		addMember(l,"_VR",get_VR,null,false);
		addMember(l,"Reflection",getReflection,null,false);
		addMember(l,"_Reflection",get_Reflection,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.CameraType));
	}
}
