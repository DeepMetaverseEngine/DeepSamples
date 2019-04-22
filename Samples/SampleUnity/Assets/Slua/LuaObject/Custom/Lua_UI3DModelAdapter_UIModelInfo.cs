using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UI3DModelAdapter_UIModelInfo : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo o;
			System.String a1;
			checkType(l,2,out a1);
			DeepCore.Unity3D.Battle.DisplayCell a2;
			checkType(l,3,out a2);
			AnimPlayer a3;
			checkType(l,4,out a3);
			o=new UI3DModelAdapter.UIModelInfo(a1,a2,a3);
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
	static public int InitDynamicBone(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			self.InitDynamicBone();
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
	static public int get_Key(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Key);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DC(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RootTrans(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RootTrans);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Anime(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Anime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Callback(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			System.Action<UI3DModelAdapter.UIModelInfo> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.Callback=v;
			else if(op==1) self.Callback+=v;
			else if(op==2) self.Callback-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DynamicBoneEnable(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DynamicBoneEnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DynamicBoneEnable(IntPtr l) {
		try {
			UI3DModelAdapter.UIModelInfo self=(UI3DModelAdapter.UIModelInfo)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.DynamicBoneEnable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UIModelInfo");
		addMember(l,InitDynamicBone);
		addMember(l,"Key",get_Key,null,true);
		addMember(l,"DC",get_DC,null,true);
		addMember(l,"RootTrans",get_RootTrans,null,true);
		addMember(l,"Anime",get_Anime,null,true);
		addMember(l,"Callback",null,set_Callback,true);
		addMember(l,"DynamicBoneEnable",get_DynamicBoneEnable,set_DynamicBoneEnable,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UI3DModelAdapter.UIModelInfo));
	}
}
