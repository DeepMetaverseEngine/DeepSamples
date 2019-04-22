using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_Battle_DisplayCell : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell o;
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			o=new DeepCore.Unity3D.Battle.DisplayCell(a1,a2);
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
	static public int CrossFade(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Single a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			self.CrossFade(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAnimTime(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetAnimTime(a1);
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
	static public int IsCurrentStatePlayOver(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsCurrentStatePlayOver(a1);
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
	static public int Play(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.Play(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFloat(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				self.SetFloat(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(argc==5){
				DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				self.SetFloat(a1,a2,a3,a4);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetFloat to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Dispose(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			self.Dispose();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetLayer(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetLayer(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int EnableRender(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.EnableRender(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoadModel(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Action<DeepCore.Unity3D.FuckAssetObject> a3;
			checkDelegate(l,4,out a3);
			System.Predicate<DeepCore.Unity3D.FuckAssetObject> a4;
			checkDelegate(l,5,out a4);
			var ret=self.LoadModel(a1,a2,a3,a4);
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
	static public int Parent(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			self.Parent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ParentRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.GameObject a1;
			checkType(l,2,out a1);
			self.ParentRoot(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachPart(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==4){
				DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				AnimPlayer a3;
				checkType(l,4,out a3);
				var ret=self.AttachPart(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.String a2;
				checkType(l,3,out a2);
				DeepCore.Unity3D.Battle.DisplayCell a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.AttachPart(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AttachPart to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachPart(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DetachPart(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int TryDetachPart(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.TryDetachPart(a1);
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
	static public int GetPart(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetPart(a1);
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
	static public int DetachAllPart(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			self.DetachAllPart();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetNode(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetNode(a1);
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
	static public int HasAnim(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.HasAnim(a1);
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
	static public int Update(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.Update(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Unload(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			self.Unload();
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
	static public int get_AllocCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.Battle.DisplayCell.AllocCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ActiveCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.Battle.DisplayCell.ActiveCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDisposed(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
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
	static public int get_speed(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.speed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_speed(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.speed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ObjectRoot(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ObjectRoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Position(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Position);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Position(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.Position=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Rotation(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Rotation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Rotation(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Quaternion v;
			checkType(l,2,out v);
			self.Rotation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_localPosition(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.localPosition);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_localPosition(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.localPosition=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_localScale(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.localScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_localScale(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.localScale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_localRotation(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.localRotation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_localRotation(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Quaternion v;
			checkType(l,2,out v);
			self.localRotation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_localEulerAngles(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.localEulerAngles);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_localEulerAngles(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.localEulerAngles=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_activeSelf(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.activeSelf);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_activeSelf(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.activeSelf=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DontUseCache(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DontUseCache);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DontUseCache(IntPtr l) {
		try {
			DeepCore.Unity3D.Battle.DisplayCell self=(DeepCore.Unity3D.Battle.DisplayCell)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.DontUseCache=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DisplayCell");
		addMember(l,CrossFade);
		addMember(l,GetAnimTime);
		addMember(l,IsCurrentStatePlayOver);
		addMember(l,Play);
		addMember(l,SetFloat);
		addMember(l,Dispose);
		addMember(l,SetLayer);
		addMember(l,EnableRender);
		addMember(l,LoadModel);
		addMember(l,Parent);
		addMember(l,ParentRoot);
		addMember(l,AttachPart);
		addMember(l,DetachPart);
		addMember(l,TryDetachPart);
		addMember(l,GetPart);
		addMember(l,DetachAllPart);
		addMember(l,GetNode);
		addMember(l,HasAnim);
		addMember(l,Update);
		addMember(l,Unload);
		addMember(l,"AllocCount",get_AllocCount,null,false);
		addMember(l,"ActiveCount",get_ActiveCount,null,false);
		addMember(l,"IsDisposed",get_IsDisposed,null,true);
		addMember(l,"speed",get_speed,set_speed,true);
		addMember(l,"ObjectRoot",get_ObjectRoot,null,true);
		addMember(l,"Position",get_Position,set_Position,true);
		addMember(l,"Rotation",get_Rotation,set_Rotation,true);
		addMember(l,"localPosition",get_localPosition,set_localPosition,true);
		addMember(l,"localScale",get_localScale,set_localScale,true);
		addMember(l,"localRotation",get_localRotation,set_localRotation,true);
		addMember(l,"localEulerAngles",get_localEulerAngles,set_localEulerAngles,true);
		addMember(l,"activeSelf",get_activeSelf,set_activeSelf,true);
		addMember(l,"DontUseCache",get_DontUseCache,set_DontUseCache,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.Battle.DisplayCell));
	}
}
