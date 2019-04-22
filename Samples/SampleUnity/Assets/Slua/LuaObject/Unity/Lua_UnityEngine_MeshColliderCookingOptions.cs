using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_MeshColliderCookingOptions : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshColliderCookingOptions.None);
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
			pushValue(l,(double)UnityEngine.MeshColliderCookingOptions.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getInflateConvexMesh(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshColliderCookingOptions.InflateConvexMesh);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_InflateConvexMesh(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshColliderCookingOptions.InflateConvexMesh);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getCookForFasterSimulation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshColliderCookingOptions.CookForFasterSimulation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CookForFasterSimulation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshColliderCookingOptions.CookForFasterSimulation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getEnableMeshCleaning(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshColliderCookingOptions.EnableMeshCleaning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableMeshCleaning(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshColliderCookingOptions.EnableMeshCleaning);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWeldColocatedVertices(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.MeshColliderCookingOptions.WeldColocatedVertices);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WeldColocatedVertices(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.MeshColliderCookingOptions.WeldColocatedVertices);
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
		getTypeTable(l,"UnityEngine.MeshColliderCookingOptions");
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,"InflateConvexMesh",getInflateConvexMesh,null,false);
		addMember(l,"_InflateConvexMesh",get_InflateConvexMesh,null,false);
		addMember(l,"CookForFasterSimulation",getCookForFasterSimulation,null,false);
		addMember(l,"_CookForFasterSimulation",get_CookForFasterSimulation,null,false);
		addMember(l,"EnableMeshCleaning",getEnableMeshCleaning,null,false);
		addMember(l,"_EnableMeshCleaning",get_EnableMeshCleaning,null,false);
		addMember(l,"WeldColocatedVertices",getWeldColocatedVertices,null,false);
		addMember(l,"_WeldColocatedVertices",get_WeldColocatedVertices,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.MeshColliderCookingOptions));
	}
}
