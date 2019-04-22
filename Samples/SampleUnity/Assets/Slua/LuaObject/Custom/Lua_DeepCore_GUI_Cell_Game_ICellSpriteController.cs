using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Cell_Game_ICellSpriteController : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetCurrentAnimate(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(int))){
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
				System.Int32 a1;
				checkType(l,2,out a1);
				self.SetCurrentAnimate(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(string))){
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
				self.NextCycFrame();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
				self.PrewCycFrame();
				pushValue(l,true);
				return 1;
			}
			else if(argc==2){
				DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
	static public int get_CurrentFrame(IntPtr l) {
		try {
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
			DeepCore.GUI.Cell.Game.ICellSpriteController self=(DeepCore.GUI.Cell.Game.ICellSpriteController)checkSelf(l);
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
		getTypeTable(l,"DeepCore.GUI.Cell.Game.ICellSpriteController");
		addMember(l,SetCurrentAnimate);
		addMember(l,SetCurrentFrame);
		addMember(l,NextFrame);
		addMember(l,NextCycFrame);
		addMember(l,PrewFrame);
		addMember(l,PrewCycFrame);
		addMember(l,"CurrentFrame",get_CurrentFrame,null,true);
		addMember(l,"CurrentAnimate",get_CurrentAnimate,null,true);
		addMember(l,"IsEndFrame",get_IsEndFrame,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.GUI.Cell.Game.ICellSpriteController));
	}
}
