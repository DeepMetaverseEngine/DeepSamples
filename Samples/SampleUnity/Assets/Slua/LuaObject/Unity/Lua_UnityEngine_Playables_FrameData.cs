﻿using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Playables_FrameData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData o;
			o=new UnityEngine.Playables.FrameData();
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
	static public int get_frameId(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.frameId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_deltaTime(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.deltaTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_weight(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.weight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_effectiveWeight(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.effectiveWeight);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_effectiveParentDelay(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.effectiveParentDelay);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_effectiveParentSpeed(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.effectiveParentSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_effectiveSpeed(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.effectiveSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_evaluationType(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.evaluationType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_seekOccurred(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.seekOccurred);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_timeLooped(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.timeLooped);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_timeHeld(IntPtr l) {
		try {
			UnityEngine.Playables.FrameData self;
			checkValueType(l,1,out self);
			pushValue(l,true);
			pushValue(l,self.timeHeld);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.Playables.FrameData");
		addMember(l,"frameId",get_frameId,null,true);
		addMember(l,"deltaTime",get_deltaTime,null,true);
		addMember(l,"weight",get_weight,null,true);
		addMember(l,"effectiveWeight",get_effectiveWeight,null,true);
		addMember(l,"effectiveParentDelay",get_effectiveParentDelay,null,true);
		addMember(l,"effectiveParentSpeed",get_effectiveParentSpeed,null,true);
		addMember(l,"effectiveSpeed",get_effectiveSpeed,null,true);
		addMember(l,"evaluationType",get_evaluationType,null,true);
		addMember(l,"seekOccurred",get_seekOccurred,null,true);
		addMember(l,"timeLooped",get_timeLooped,null,true);
		addMember(l,"timeHeld",get_timeHeld,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(UnityEngine.Playables.FrameData),typeof(System.ValueType));
	}
}
