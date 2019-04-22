using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_Battle_ComAICell : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell o;
			DeepCore.Unity3D.Battle.BattleScene a1;
			checkType(l,2,out a1);
			DeepCore.GameSlave.ZoneObject a2;
			checkType(l,3,out a2);
			o=new DeepCore.Unity3D.Battle.ComAICell(a1,a2);
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
	static public int OnLoadWarningEffect(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			DeepCore.Unity3D.FuckAssetObject a1;
			checkType(l,2,out a1);
			DeepCore.GameData.Zone.LaunchEffect a2;
			checkType(l,3,out a2);
			UnityEngine.Vector3 a3;
			checkType(l,4,out a3);
			UnityEngine.Quaternion a4;
			checkType(l,5,out a4);
			self.OnLoadWarningEffect(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdateDebugGuard(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			self.UpdateDebugGuard();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdateDebugBody(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			self.UpdateDebugBody();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdateDebugAttack(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			self.UpdateDebugAttack();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoObjectEvent(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			DeepCore.GameData.Zone.ObjectEvent a1;
			checkType(l,2,out a1);
			self.DoObjectEvent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RegistObjectEvent(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			System.Action<DeepCore.GameData.Zone.ObjectEvent> a1;
			checkDelegate(l,2,out a1);
			self.RegistObjectEvent<DeepCore.GameData.Zone.ObjectEvent>(a1);
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
	static public int get_ZObj(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ZObj);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ObjectID(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ObjectID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_X(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.X);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Y(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Y);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Z(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Z);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsEnable(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsEnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Direction(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Direction);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Templates(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Templates);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDebugShowGuard(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDebugShowGuard);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsDebugShowGuard(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsDebugShowGuard=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDebugShowBody(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDebugShowBody);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsDebugShowBody(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsDebugShowBody=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDebugShowAttack(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDebugShowAttack);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsDebugShowAttack(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsDebugShowAttack=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDisposed(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDisposed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPosChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.ComAICell self=(DeepCore.Unity3D.Battle.ComAICell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPosChanged);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ComAICell");
		addMember(l,OnLoadWarningEffect);
		addMember(l,UpdateDebugGuard);
		addMember(l,UpdateDebugBody);
		addMember(l,UpdateDebugAttack);
		addMember(l,DoObjectEvent);
		addMember(l,RegistObjectEvent);
		addMember(l,"ZObj",get_ZObj,null,true);
		addMember(l,"ObjectID",get_ObjectID,null,true);
		addMember(l,"X",get_X,null,true);
		addMember(l,"Y",get_Y,null,true);
		addMember(l,"Z",get_Z,null,true);
		addMember(l,"IsEnable",get_IsEnable,null,true);
		addMember(l,"Direction",get_Direction,null,true);
		addMember(l,"Templates",get_Templates,null,true);
		addMember(l,"IsDebugShowGuard",get_IsDebugShowGuard,set_IsDebugShowGuard,true);
		addMember(l,"IsDebugShowBody",get_IsDebugShowBody,set_IsDebugShowBody,true);
		addMember(l,"IsDebugShowAttack",get_IsDebugShowAttack,set_IsDebugShowAttack,true);
		addMember(l,"IsDisposed",get_IsDisposed,null,true);
		addMember(l,"IsPosChanged",get_IsPosChanged,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.Battle.ComAICell),typeof(DeepCore.Unity3D.Battle.BattleObject));
	}
}
