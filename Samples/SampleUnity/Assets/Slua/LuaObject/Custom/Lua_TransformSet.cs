using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TransformSet : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TransformSet o;
			o=new TransformSet();
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
	static public int get_InvalidLayerOrder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TransformSet.InvalidLayerOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Parent(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Parent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Parent(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.Parent=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Clip(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Clip);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Clip(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.Clip=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DisableToUnload(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DisableToUnload);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DisableToUnload(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.DisableToUnload=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Pos(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Pos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Pos(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.Pos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Scale(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Scale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Scale(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.Scale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Deg(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Deg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Deg(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.Vector3 v;
			checkType(l,2,out v);
			self.Deg=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Visible(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Visible);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Visible(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.Visible=v;
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
			TransformSet self=(TransformSet)checkSelf(l);
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
			TransformSet self=(TransformSet)checkSelf(l);
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
	static public int get_Vectormove(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Vectormove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Vectormove(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Vectormove=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Layer(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Layer);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Layer(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Layer=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AnimatorState(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AnimatorState);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_AnimatorState(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.AnimatorState=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LayerOrder(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.LayerOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_LayerOrder(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.LayerOrder=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Name(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Name);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Name(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.Name=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FScale(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FScale);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FScale(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.FScale=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasLayerOrder(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasLayerOrder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasDeg(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasDeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasQuaternion(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasQuaternion);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HasVectormove(IntPtr l) {
		try {
			TransformSet self=(TransformSet)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HasVectormove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TransformSet");
		addMember(l,"InvalidLayerOrder",get_InvalidLayerOrder,null,false);
		addMember(l,"Parent",get_Parent,set_Parent,true);
		addMember(l,"Clip",get_Clip,set_Clip,true);
		addMember(l,"DisableToUnload",get_DisableToUnload,set_DisableToUnload,true);
		addMember(l,"Pos",get_Pos,set_Pos,true);
		addMember(l,"Scale",get_Scale,set_Scale,true);
		addMember(l,"Deg",get_Deg,set_Deg,true);
		addMember(l,"Visible",get_Visible,set_Visible,true);
		addMember(l,"Rotation",get_Rotation,set_Rotation,true);
		addMember(l,"Vectormove",get_Vectormove,set_Vectormove,true);
		addMember(l,"Layer",get_Layer,set_Layer,true);
		addMember(l,"AnimatorState",get_AnimatorState,set_AnimatorState,true);
		addMember(l,"LayerOrder",get_LayerOrder,set_LayerOrder,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"FScale",get_FScale,set_FScale,true);
		addMember(l,"HasLayerOrder",get_HasLayerOrder,null,true);
		addMember(l,"HasDeg",get_HasDeg,null,true);
		addMember(l,"HasQuaternion",get_HasQuaternion,null,true);
		addMember(l,"HasVectormove",get_HasVectormove,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TransformSet));
	}
}
