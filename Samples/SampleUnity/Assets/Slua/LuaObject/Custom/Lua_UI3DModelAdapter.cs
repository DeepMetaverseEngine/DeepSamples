using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UI3DModelAdapter : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UI3DModelAdapter o;
			o=new UI3DModelAdapter();
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
	static public int AddSingleModel_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,1,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,2,out a2);
			System.Single a3;
			checkType(l,3,out a3);
			System.Int32 a4;
			checkType(l,4,out a4);
			System.String a5;
			checkType(l,5,out a5);
			var ret=UI3DModelAdapter.AddSingleModel(a1,a2,a3,a4,a5);
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
	static public int AddAvatar_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(UnityEngine.Vector2),typeof(float),typeof(int),typeof(DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo>),typeof(int))){
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Int32 a4;
				checkType(l,4,out a4);
				DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a5;
				checkType(l,5,out a5);
				System.Int32 a6;
				checkType(l,6,out a6);
				var ret=UI3DModelAdapter.AddAvatar(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(DeepCore.Unity3D.UGUI.DisplayNode),typeof(UnityEngine.Vector2),typeof(float),typeof(int),typeof(SLua.LuaTable),typeof(int))){
				DeepCore.Unity3D.UGUI.DisplayNode a1;
				checkType(l,1,out a1);
				UnityEngine.Vector2 a2;
				checkType(l,2,out a2);
				System.Single a3;
				checkType(l,3,out a3);
				System.Int32 a4;
				checkType(l,4,out a4);
				SLua.LuaTable a5;
				checkType(l,5,out a5);
				System.Int32 a6;
				checkType(l,6,out a6);
				var ret=UI3DModelAdapter.AddAvatar(a1,a2,a3,a4,a5,a6);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddAvatar to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReleaseModel_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			UI3DModelAdapter.ReleaseModel(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsModelExist_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			var ret=UI3DModelAdapter.IsModelExist(a1);
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
	static public int SetLoadCallback_s(IntPtr l) {
		try {
			System.String a1;
			checkType(l,1,out a1);
			System.Action<UI3DModelAdapter.UIModelInfo> a2;
			checkDelegate(l,2,out a2);
			UI3DModelAdapter.SetLoadCallback(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadAvatar_s(IntPtr l) {
		try {
			DeepCore.HashMap<System.Int32,TLBattle.Common.Plugins.TLAvatarInfo> a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			System.Action<UI3DModelAdapter.UIModelInfo> a3;
			checkDelegate(l,3,out a3);
			var ret=UI3DModelAdapter.LoadAvatar(a1,a2,a3);
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
		getTypeTable(l,"UI3DModelAdapter");
		addMember(l,AddSingleModel_s);
		addMember(l,AddAvatar_s);
		addMember(l,ReleaseModel_s);
		addMember(l,IsModelExist_s);
		addMember(l,SetLoadCallback_s);
		addMember(l,LoadAvatar_s);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UI3DModelAdapter));
	}
}
