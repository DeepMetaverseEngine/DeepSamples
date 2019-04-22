using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_FingerGesturesCtrl : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddFingerHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			FingerHandlerInterface a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.AddFingerHandler(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveFingerHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			FingerHandlerInterface a1;
			checkType(l,2,out a1);
			self.RemoveFingerHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddPinchHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			PinchHandlerInterface a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.AddPinchHandler(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemovePinchHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			PinchHandlerInterface a1;
			checkType(l,2,out a1);
			self.RemovePinchHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddGlobalTouchDownHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			FingerGesturesCtrl.OnGlobalTouchEvent a2;
			checkDelegate(l,3,out a2);
			var ret=self.AddGlobalTouchDownHandler(a1,a2);
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
	static public int AddGlobalTouchUpHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			FingerGesturesCtrl.OnGlobalTouchEvent a2;
			checkDelegate(l,3,out a2);
			var ret=self.AddGlobalTouchUpHandler(a1,a2);
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
	static public int RemoveGlobalTouchDownHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveGlobalTouchDownHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveGlobalTouchUpHandler(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveGlobalTouchUpHandler(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnFingerDown(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.FingerGestures_OnFingerDown(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnFingerDragMove(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			UnityEngine.Vector2 a3;
			checkType(l,4,out a3);
			self.FingerGestures_OnFingerDragMove(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnFingerUp(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.FingerGestures_OnFingerUp(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetTouchEnable(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetTouchEnable(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnFingerDragBegin(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			UnityEngine.Vector2 a3;
			checkType(l,4,out a3);
			self.FingerGestures_OnFingerDragBegin(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnFingerDragEnd(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.FingerGestures_OnFingerDragEnd(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnPinchBegin(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.FingerGestures_OnPinchBegin(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnPinchMove(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.FingerGestures_OnPinchMove(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int FingerGestures_OnPinchEnd(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			UnityEngine.Vector2 a1;
			checkType(l,2,out a1);
			UnityEngine.Vector2 a2;
			checkType(l,3,out a2);
			self.FingerGestures_OnPinchEnd(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Update(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			self.Update();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Up(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			self.Up();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Down(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			self.Down();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Left(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			self.Left();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Right(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			self.Right();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int KeyDown(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			UnityEngine.KeyCode a1;
			checkEnum(l,2,out a1);
			self.KeyDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int KeyUp(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			UnityEngine.KeyCode a1;
			checkEnum(l,2,out a1);
			self.KeyUp(a1);
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
	static public int get_fingerGestures(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.fingerGestures);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_fingerGestures(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			FingerGestures v;
			checkType(l,2,out v);
			self.fingerGestures=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TouchEnable(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TouchEnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FingerCount(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FingerCount);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FingerCount(IntPtr l) {
		try {
			FingerGesturesCtrl self=(FingerGesturesCtrl)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.FingerCount=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"FingerGesturesCtrl");
		addMember(l,AddFingerHandler);
		addMember(l,RemoveFingerHandler);
		addMember(l,AddPinchHandler);
		addMember(l,RemovePinchHandler);
		addMember(l,AddGlobalTouchDownHandler);
		addMember(l,AddGlobalTouchUpHandler);
		addMember(l,RemoveGlobalTouchDownHandler);
		addMember(l,RemoveGlobalTouchUpHandler);
		addMember(l,FingerGestures_OnFingerDown);
		addMember(l,FingerGestures_OnFingerDragMove);
		addMember(l,FingerGestures_OnFingerUp);
		addMember(l,SetTouchEnable);
		addMember(l,FingerGestures_OnFingerDragBegin);
		addMember(l,FingerGestures_OnFingerDragEnd);
		addMember(l,FingerGestures_OnPinchBegin);
		addMember(l,FingerGestures_OnPinchMove);
		addMember(l,FingerGestures_OnPinchEnd);
		addMember(l,Update);
		addMember(l,Up);
		addMember(l,Down);
		addMember(l,Left);
		addMember(l,Right);
		addMember(l,KeyDown);
		addMember(l,KeyUp);
		addMember(l,"fingerGestures",get_fingerGestures,set_fingerGestures,true);
		addMember(l,"TouchEnable",get_TouchEnable,null,true);
		addMember(l,"FingerCount",get_FingerCount,set_FingerCount,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(FingerGesturesCtrl),typeof(FingerControlBase));
	}
}
