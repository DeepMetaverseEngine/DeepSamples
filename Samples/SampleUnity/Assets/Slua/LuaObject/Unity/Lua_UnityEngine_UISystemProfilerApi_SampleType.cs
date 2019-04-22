using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_UISystemProfilerApi_SampleType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLayout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UISystemProfilerApi.SampleType.Layout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Layout(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UISystemProfilerApi.SampleType.Layout);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRender(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.UISystemProfilerApi.SampleType.Render);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Render(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.UISystemProfilerApi.SampleType.Render);
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
		getTypeTable(l,"UnityEngine.UISystemProfilerApi.SampleType");
		addMember(l,"Layout",getLayout,null,false);
		addMember(l,"_Layout",get_Layout,null,false);
		addMember(l,"Render",getRender,null,false);
		addMember(l,"_Render",get_Render,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.UISystemProfilerApi.SampleType));
	}
}
