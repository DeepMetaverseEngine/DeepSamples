using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_GUI_Display_Text_AttributedString : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.GUI.Display.Text.AttributedString o;
			if(argc==1){
				o=new DeepCore.GUI.Display.Text.AttributedString();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(argc==3){
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.GUI.Display.Text.TextAttribute a2;
				checkType(l,3,out a2);
				o=new DeepCore.GUI.Display.Text.AttributedString(a1,a2);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			return error(l,"New object failed.");
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clone(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
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
	static public int SetAttribute(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			DeepCore.GUI.Display.Text.TextAttribute a3;
			checkType(l,4,out a3);
			var ret=self.SetAttribute(a1,a2,a3);
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
	static public int AddAttribute(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
				DeepCore.GUI.Display.Text.TextAttribute a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				var ret=self.AddAttribute(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==5){
				DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
				DeepCore.GUI.Display.Text.TextAttribute a1;
				checkType(l,2,out a1);
				System.Int32 a2;
				checkType(l,3,out a2);
				System.Int32 a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.AddAttribute(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function AddAttribute to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Append(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string))){
				DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				var ret=self.Append(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(DeepCore.GUI.Display.Text.AttributedString))){
				DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
				DeepCore.GUI.Display.Text.AttributedString a1;
				checkType(l,2,out a1);
				var ret=self.Append(a1);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==3){
				DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				DeepCore.GUI.Display.Text.TextAttribute a2;
				checkType(l,3,out a2);
				var ret=self.Append(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Append to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DeleteString(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.DeleteString(a1,a2);
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
	static public int ClearString(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			var ret=self.ClearString();
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
	static public int GetChar(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			var ret=self.GetChar(a1);
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
	static public int GetAttribute(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			System.Int32 a1;
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
	static public int get_Length(IntPtr l) {
		try {
			DeepCore.GUI.Display.Text.AttributedString self=(DeepCore.GUI.Display.Text.AttributedString)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Length);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AttributedString");
		addMember(l,Clone);
		addMember(l,SetAttribute);
		addMember(l,AddAttribute);
		addMember(l,Append);
		addMember(l,DeleteString);
		addMember(l,ClearString);
		addMember(l,GetChar);
		addMember(l,GetAttribute);
		addMember(l,"Length",get_Length,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.GUI.Display.Text.AttributedString));
	}
}
