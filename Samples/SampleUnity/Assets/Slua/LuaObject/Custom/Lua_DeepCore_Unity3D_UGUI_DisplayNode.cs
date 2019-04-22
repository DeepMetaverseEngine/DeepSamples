using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_DisplayNode : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode o;
			System.String a1;
			checkType(l,2,out a1);
			o=new DeepCore.Unity3D.UGUI.DisplayNode(a1);
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
	static public int Dispose(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
	static public int Clone(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			var ret=self.Clone();
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
	static public int AddComponent(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				var ret=self.AddComponent<UnityEngine.Component>();
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==2){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				System.Type a1;
				checkType(l,2,out a1);
				var ret=self.AddComponent(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddComponent to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetAnchor(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			self.SetAnchor(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ScreenToLocalPoint2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			var ret=self.ScreenToLocalPoint2D(a1);
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
	static public int LocalToGlobal(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			var ret=self.LocalToGlobal();
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
	static public int GlobalToLocal(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.GlobalToLocal(a1,a2);
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
	static public int AddChildAt(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.AddChildAt(a1,a2);
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
	static public int RemoveChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.RemoveChild(a1,a2);
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
	static public int ContainsChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			var ret=self.ContainsChild(a1);
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
	static public int AddChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			self.AddChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveChildByName(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.RemoveChildByName(a1,a2);
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
	static public int RemoveChildAt(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.RemoveChildAt(a1,a2);
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
	static public int RemoveChildren(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Boolean a3;
			checkType(l,4,out a3);
			self.RemoveChildren(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAllChildren(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.RemoveAllChildren(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveFromParent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.RemoveFromParent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetChildAt(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetChildAt(a1);
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
	static public int GetAllChild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Collections.Generic.List<DeepCore.Unity3D.UGUI.DisplayNode> a1;
			checkType(l,2,out a1);
			self.GetAllChild(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetParentIndex(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetParentIndex(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetChildIndex(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.SetChildIndex(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetChildIndex(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode a1;
			checkType(l,2,out a1);
			var ret=self.GetChildIndex(a1);
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
	static public int FindChildByName(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.FindChildByName<DeepCore.Unity3D.UGUI.DisplayNode>(a1,a2);
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
	static public int FindChildAs(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Predicate<DeepCore.Unity3D.UGUI.DisplayNode> a1;
			checkDelegate(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.FindChildAs<DeepCore.Unity3D.UGUI.DisplayNode>(a1,a2);
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
	static public int ForEachChilds(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Action<DeepCore.Unity3D.UGUI.DisplayNode> a1;
			checkDelegate(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.ForEachChilds<DeepCore.Unity3D.UGUI.DisplayNode>(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetChildsContentSize(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			var ret=self.GetChildsContentSize();
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
	static public int IsAttribute(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsAttribute(a1);
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
	static public int SetAttribute(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Object a2;
			checkType(l,3,out a2);
			self.SetAttribute(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAttribute(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetAttribute(a1);
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
	static public int AddAction(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUIAction.IAction a1;
			checkType(l,2,out a1);
			self.AddAction(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAction(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(bool))){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				self.RemoveAction(a1,a2);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.Unity3D.UGUIAction.IAction),typeof(bool))){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				DeepCore.Unity3D.UGUIAction.IAction a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				self.RemoveAction(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function RemoveAction to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HasAction(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string))){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.HasAction(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.Unity3D.UGUIAction.IAction))){
				DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
				DeepCore.Unity3D.UGUIAction.IAction a1;
				checkType(l,2,out a1);
				var ret=self.HasAction(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function HasAction to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetAction(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetAction(a1);
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
	static public int RemoveAllAction(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.RemoveAllAction(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdateAction(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.UpdateAction(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AsDisplayNode_s(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,1,typeof(UnityEngine.Component))){
				UnityEngine.Component a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUI.DisplayNode.AsDisplayNode(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,1,typeof(UnityEngine.GameObject))){
				UnityEngine.GameObject a1;
				checkType(l,1,out a1);
				var ret=DeepCore.Unity3D.UGUI.DisplayNode.AsDisplayNode(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AsDisplayNode to call");
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
	static public int set_event_ChildAdded(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.ChildEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ChildAdded=v;
			else if(op==1) self.event_ChildAdded+=v;
			else if(op==2) self.event_ChildAdded-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_ChildRemoved(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.ChildEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ChildRemoved=v;
			else if(op==1) self.event_ChildRemoved+=v;
			else if(op==2) self.event_ChildRemoved-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.PointerEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerDown=v;
			else if(op==1) self.event_PointerDown+=v;
			else if(op==2) self.event_PointerDown-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerUp(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.PointerEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerUp=v;
			else if(op==1) self.event_PointerUp+=v;
			else if(op==2) self.event_PointerUp-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerMove(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.PointerEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerMove=v;
			else if(op==1) self.event_PointerMove+=v;
			else if(op==2) self.event_PointerMove-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_PointerClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.PointerEventHandler v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_PointerClick=v;
			else if(op==1) self.event_PointerClick+=v;
			else if(op==2) self.event_PointerClick-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_disposed(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			DeepCore.Unity3D.UGUI.DisplayNode.DiposeEventHandle v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_disposed=v;
			else if(op==1) self.event_disposed+=v;
			else if(op==2) self.event_disposed-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RefCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUI.DisplayNode.RefCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AliveCount(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.UGUI.DisplayNode.AliveCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UnityObject(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UnityObject);
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
	static public int get_Transform(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Transform);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Root(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Root);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsDispose(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsDispose);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NumChildren(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.NumChildren);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UserData(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UserData);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UserData(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.UserData=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UserTag(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.UserTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_UserTag(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.UserTag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Tag(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Tag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Tag(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			System.Object v;
			checkType(l,2,out v);
			self.Tag=v;
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			string v;
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
	static public int get_Visible(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
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
	static public int get_VisibleInParent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.VisibleInParent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_VisibleInParent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.VisibleInParent=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableOutMove(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableOutMove);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableOutMove(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableOutMove=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Enable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Enable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Enable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.Enable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableChildren(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableChildren);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_EnableChildren(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.EnableChildren=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsInteractive(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsInteractive);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsInteractive(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsInteractive=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EnableTouchInParents(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.EnableTouchInParents);
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
	static public int set_X(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.X=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Y(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
	static public int set_Y(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.Y=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Width(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Width);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Width(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.Width=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Height(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Height);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Height(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.Height=v;
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
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
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Vector2 v;
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
	static public int get_Bounds2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Bounds2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Bounds2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Rect v;
			checkValueType(l,2,out v);
			self.Bounds2D=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Position2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Position2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Position2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Position2D=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Size2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Size2D);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Size2D(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			UnityEngine.Vector2 v;
			checkType(l,2,out v);
			self.Size2D=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Alpha(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Alpha);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Alpha(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.Alpha=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RealAlpha(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.RealAlpha);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsGray(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsGray);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsGray(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsGray=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsAlphaDirty(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsAlphaDirty);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsAlphaDirty(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsAlphaDirty=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsGrayDirty(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsGrayDirty);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsGrayDirty(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsGrayDirty=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Selectable(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Selectable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPressed(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.DisplayNode self=(DeepCore.Unity3D.UGUI.DisplayNode)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPressed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DisplayNode");
		addMember(l,Dispose);
		addMember(l,Clone);
		addMember(l,AddComponent);
		addMember(l,SetAnchor);
		addMember(l,ScreenToLocalPoint2D);
		addMember(l,LocalToGlobal);
		addMember(l,GlobalToLocal);
		addMember(l,AddChildAt);
		addMember(l,RemoveChild);
		addMember(l,ContainsChild);
		addMember(l,AddChild);
		addMember(l,RemoveChildByName);
		addMember(l,RemoveChildAt);
		addMember(l,RemoveChildren);
		addMember(l,RemoveAllChildren);
		addMember(l,RemoveFromParent);
		addMember(l,GetChildAt);
		addMember(l,GetAllChild);
		addMember(l,SetParentIndex);
		addMember(l,SetChildIndex);
		addMember(l,GetChildIndex);
		addMember(l,FindChildByName);
		addMember(l,FindChildAs);
		addMember(l,ForEachChilds);
		addMember(l,GetChildsContentSize);
		addMember(l,IsAttribute);
		addMember(l,SetAttribute);
		addMember(l,GetAttribute);
		addMember(l,AddAction);
		addMember(l,RemoveAction);
		addMember(l,HasAction);
		addMember(l,GetAction);
		addMember(l,RemoveAllAction);
		addMember(l,UpdateAction);
		addMember(l,AsDisplayNode_s);
		addMember(l,"event_ChildAdded",null,set_event_ChildAdded,true);
		addMember(l,"event_ChildRemoved",null,set_event_ChildRemoved,true);
		addMember(l,"event_PointerDown",null,set_event_PointerDown,true);
		addMember(l,"event_PointerUp",null,set_event_PointerUp,true);
		addMember(l,"event_PointerMove",null,set_event_PointerMove,true);
		addMember(l,"event_PointerClick",null,set_event_PointerClick,true);
		addMember(l,"event_disposed",null,set_event_disposed,true);
		addMember(l,"RefCount",get_RefCount,null,false);
		addMember(l,"AliveCount",get_AliveCount,null,false);
		addMember(l,"UnityObject",get_UnityObject,null,true);
		addMember(l,"Parent",get_Parent,null,true);
		addMember(l,"Transform",get_Transform,null,true);
		addMember(l,"Root",get_Root,null,true);
		addMember(l,"IsDispose",get_IsDispose,null,true);
		addMember(l,"NumChildren",get_NumChildren,null,true);
		addMember(l,"UserData",get_UserData,set_UserData,true);
		addMember(l,"UserTag",get_UserTag,set_UserTag,true);
		addMember(l,"Tag",get_Tag,set_Tag,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"Visible",get_Visible,set_Visible,true);
		addMember(l,"VisibleInParent",get_VisibleInParent,set_VisibleInParent,true);
		addMember(l,"EnableOutMove",get_EnableOutMove,set_EnableOutMove,true);
		addMember(l,"Enable",get_Enable,set_Enable,true);
		addMember(l,"EnableChildren",get_EnableChildren,set_EnableChildren,true);
		addMember(l,"IsInteractive",get_IsInteractive,set_IsInteractive,true);
		addMember(l,"EnableTouchInParents",get_EnableTouchInParents,null,true);
		addMember(l,"X",get_X,set_X,true);
		addMember(l,"Y",get_Y,set_Y,true);
		addMember(l,"Width",get_Width,set_Width,true);
		addMember(l,"Height",get_Height,set_Height,true);
		addMember(l,"Scale",get_Scale,set_Scale,true);
		addMember(l,"Bounds2D",get_Bounds2D,set_Bounds2D,true);
		addMember(l,"Position2D",get_Position2D,set_Position2D,true);
		addMember(l,"Size2D",get_Size2D,set_Size2D,true);
		addMember(l,"Alpha",get_Alpha,set_Alpha,true);
		addMember(l,"RealAlpha",get_RealAlpha,null,true);
		addMember(l,"IsGray",get_IsGray,set_IsGray,true);
		addMember(l,"IsAlphaDirty",get_IsAlphaDirty,set_IsAlphaDirty,true);
		addMember(l,"IsGrayDirty",get_IsGrayDirty,set_IsGrayDirty,true);
		addMember(l,"Selectable",get_Selectable,null,true);
		addMember(l,"IsPressed",get_IsPressed,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.UGUI.DisplayNode));
	}
}
