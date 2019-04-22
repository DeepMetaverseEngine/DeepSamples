using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SettingData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			SettingData o;
			o=new SettingData();
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
	static public int SaveData(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			self.SaveData();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitNetWork(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			self.InitNetWork();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Clear(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.Clear(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AttachLuaObserver(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			SLua.LuaTable a2;
			checkType(l,3,out a2);
			self.AttachLuaObserver(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DetachLuaObserver(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.DetachLuaObserver(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangerData(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			self.ChangerData(a1,a2);
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
			SettingData self=(SettingData)checkSelf(l);
			SettingData.NotifySettingState a1;
			checkEnum(l,2,out a1);
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
	static public int TryGetIntAttribute(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			SettingData.NotifySettingState a1;
			checkEnum(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.TryGetIntAttribute(a1,a2);
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
			SettingData self=(SettingData)checkSelf(l);
			SettingData.NotifySettingState a1;
			checkEnum(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			var ret=self.SetAttribute(a1,a2);
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
	static public int ContainsKey(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			SettingData.NotifySettingState a1;
			checkEnum(l,2,out a1);
			SettingData.NotifySettingState a2;
			checkEnum(l,3,out a2);
			var ret=self.ContainsKey(a1,a2);
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
	static public int Notify(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.Notify(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetQuailty(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			var ret=self.GetQuailty();
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
	static public int IsLowerPower(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			var ret=self.IsLowerPower();
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
	static public int PowerSavingMode(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			var ret=self.PowerSavingMode(a1);
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
	static public int get_bIsSavePower(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.bIsSavePower);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_bIsSavePower(IntPtr l) {
		try {
			SettingData self=(SettingData)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.bIsSavePower=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QUALITY_LOW(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.QUALITY_LOW);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QUALITY_MED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.QUALITY_MED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QUALITY_HIGH(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SettingData.QUALITY_HIGH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"SettingData");
		addMember(l,SaveData);
		addMember(l,InitNetWork);
		addMember(l,Clear);
		addMember(l,AttachLuaObserver);
		addMember(l,DetachLuaObserver);
		addMember(l,ChangerData);
		addMember(l,GetAttribute);
		addMember(l,TryGetIntAttribute);
		addMember(l,SetAttribute);
		addMember(l,ContainsKey);
		addMember(l,Notify);
		addMember(l,GetQuailty);
		addMember(l,IsLowerPower);
		addMember(l,PowerSavingMode);
		addMember(l,"bIsSavePower",get_bIsSavePower,set_bIsSavePower,true);
		addMember(l,"QUALITY_LOW",get_QUALITY_LOW,null,false);
		addMember(l,"QUALITY_MED",get_QUALITY_MED,null,false);
		addMember(l,"QUALITY_HIGH",get_QUALITY_HIGH,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(SettingData));
	}
}
