using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UnityHelper : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsObjectExist_s(IntPtr l) {
		try {
			UnityEngine.Object a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UnityHelper.IsObjectExist(a1);
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
	static public int FindRecursive_s(IntPtr l) {
		try {
			UnityEngine.Transform a1;
			checkType(l,1,out a1);
			System.String a2;
			checkType(l,2,out a2);
			System.StringComparison a3;
			checkEnum(l,3,out a3);
			var ret=DeepCore.Unity3D.UnityHelper.FindRecursive(a1,a2,a3);
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
	static public int GetChildAtOrDefault_s(IntPtr l) {
		try {
			UnityEngine.Transform a1;
			checkType(l,1,out a1);
			System.Int32 a2;
			checkType(l,2,out a2);
			var ret=DeepCore.Unity3D.UnityHelper.GetChildAtOrDefault(a1,a2);
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
	static public int GetChildren_s(IntPtr l) {
		try {
			UnityEngine.Transform a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UnityHelper.GetChildren(a1);
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
	static public int LogicRad2Quaternion_s(IntPtr l) {
		try {
			System.Single a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UnityHelper.LogicRad2Quaternion(a1);
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
	static public int Quaternion2LogicRad_s(IntPtr l) {
		try {
			UnityEngine.Quaternion a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UnityHelper.Quaternion2LogicRad(a1);
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
	static public int Destroy_s(IntPtr l) {
		try {
			UnityEngine.Object a1;
			checkType(l,1,out a1);
			System.Single a2;
			checkType(l,2,out a2);
			DeepCore.Unity3D.UnityHelper.Destroy(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DestroyImmediate_s(IntPtr l) {
		try {
			UnityEngine.Object a1;
			checkType(l,1,out a1);
			System.Boolean a2;
			checkType(l,2,out a2);
			DeepCore.Unity3D.UnityHelper.DestroyImmediate(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Init_s(IntPtr l) {
		try {
			DeepCore.Unity3D.UnityHelper.Init();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WaitForEndOfFrame_s(IntPtr l) {
		try {
			System.Action a1;
			checkDelegate(l,1,out a1);
			DeepCore.Unity3D.UnityHelper.WaitForEndOfFrame(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int WaitForSeconds_s(IntPtr l) {
		try {
			System.Single a1;
			checkType(l,1,out a1);
			System.Action a2;
			checkDelegate(l,2,out a2);
			DeepCore.Unity3D.UnityHelper.WaitForSeconds(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StartCoroutine_s(IntPtr l) {
		try {
			System.Collections.IEnumerator a1;
			checkType(l,1,out a1);
			var ret=DeepCore.Unity3D.UnityHelper.StartCoroutine(a1);
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
	static public int MainThreadInvoke_s(IntPtr l) {
		try {
			System.Action a1;
			checkDelegate(l,1,out a1);
			DeepCore.Unity3D.UnityHelper.MainThreadInvoke(a1);
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
	static public int get_DisableParent(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UnityHelper.DisableParent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityHelper");
		addMember(l,IsObjectExist_s);
		addMember(l,FindRecursive_s);
		addMember(l,GetChildAtOrDefault_s);
		addMember(l,GetChildren_s);
		addMember(l,LogicRad2Quaternion_s);
		addMember(l,Quaternion2LogicRad_s);
		addMember(l,Destroy_s);
		addMember(l,DestroyImmediate_s);
		addMember(l,Init_s);
		addMember(l,WaitForEndOfFrame_s);
		addMember(l,WaitForSeconds_s);
		addMember(l,StartCoroutine_s);
		addMember(l,MainThreadInvoke_s);
		addMember(l,"DisableParent",get_DisableParent,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UnityHelper));
	}
}
