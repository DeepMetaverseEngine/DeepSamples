using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Cell_Game_CSpriteController : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController o;
			DeepCore.GUI.Cell.Game.CSpriteMeta a1;
			checkType(l,2,out a1);
			o=new DeepCore.GUI.Cell.Game.CSpriteController(a1);
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
	static public int Update(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			var ret=self.Update();
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
	static public int Dispose(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
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
	static public int SetCurrentAnimate(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(int))){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				self.SetCurrentAnimate(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string))){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.SetCurrentAnimate(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SetCurrentAnimate to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetCurrentFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.SetCurrentFrame(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int NextFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			var ret=self.NextFrame();
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
	static public int NextCycFrame(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				self.NextCycFrame();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				self.NextCycFrame(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function NextCycFrame to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PrewFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			var ret=self.PrewFrame();
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
	static public int PrewCycFrame(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==1){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				self.PrewCycFrame();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				self.PrewCycFrame(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function PrewCycFrame to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayAnimate(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string),typeof(int),typeof(DeepCore.GUI.Cell.Game.CSpriteEventHandler))){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Cell.Game.CSpriteEventHandler a3;
				checkDelegate(l,4,out a3);
				self.PlayAnimate(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(int),typeof(int),typeof(DeepCore.GUI.Cell.Game.CSpriteEventHandler))){
				DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				DeepCore.GUI.Cell.Game.CSpriteEventHandler a3;
				checkDelegate(l,4,out a3);
				self.PlayAnimate(a1,a2,a3);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function PlayAnimate to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopAnimate(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.StopAnimate(a1);
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
	static public int get_IsAutoPlay(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsAutoPlay);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsAutoPlay(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.IsAutoPlay=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Meta(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Meta);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentFrame);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CurrentAnimate(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.CurrentAnimate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsEndFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.CSpriteController self=(DeepCore.GUI.Cell.Game.CSpriteController)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsEndFrame);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"CSpriteController");
		addMember(l,Update);
		addMember(l,Dispose);
		addMember(l,SetCurrentAnimate);
		addMember(l,SetCurrentFrame);
		addMember(l,NextFrame);
		addMember(l,NextCycFrame);
		addMember(l,PrewFrame);
		addMember(l,PrewCycFrame);
		addMember(l,PlayAnimate);
		addMember(l,StopAnimate);
		addMember(l,"IsAutoPlay",get_IsAutoPlay,set_IsAutoPlay,true);
		addMember(l,"Meta",get_Meta,null,true);
		addMember(l,"CurrentFrame",get_CurrentFrame,null,true);
		addMember(l,"CurrentAnimate",get_CurrentAnimate,null,true);
		addMember(l,"IsEndFrame",get_IsEndFrame,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GUI.Cell.Game.CSpriteController));
	}
}
