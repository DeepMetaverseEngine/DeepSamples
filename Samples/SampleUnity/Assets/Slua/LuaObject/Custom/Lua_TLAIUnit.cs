using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIUnit : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsActor(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			var ret=self.IsActor();
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
	static public int AddBubbleChat(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Single a3;
			checkType(l,4,out a3);
			self.AddBubbleChat(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Name(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			var ret=self.Name();
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
	static public int RemoveUnit(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			self.RemoveUnit();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Level(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			var ret=self.Level();
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
	static public int CheckShowHPBanner(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.CheckShowHPBanner(a1);
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
			TLAIUnit self=(TLAIUnit)checkSelf(l);
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
			TLAIUnit self=(TLAIUnit)checkSelf(l);
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
			TLAIUnit self=(TLAIUnit)checkSelf(l);
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
	static public int PlayIdleSpeicalAnimation(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PlayIdleSpeicalAnimation(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlaySpeicalAnimationByScript(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			DeepCore.Unity3D.Battle.ComAIUnit.ActionStatus a1;
			checkType(l,2,out a1);
			self.PlaySpeicalAnimationByScript(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlayAnim(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			UnityEngine.WrapMode a3;
			checkEnum(l,4,out a3);
			System.Single a4;
			checkType(l,5,out a4);
			var ret=self.PlayAnim(a1,a2,a3,a4);
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
	static public int EulerAngles(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			var ret=self.EulerAngles();
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
	static public int TransformDirection(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			UnityEngine.Vector3 a1;
			checkType(l,2,out a1);
			var ret=self.TransformDirection(a1);
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
	static public int AddBubbleTalkInfo(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			self.AddBubbleTalkInfo(a1,a2,a3);
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
	static public int get_Vehicle(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Vehicle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Vehicle(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			RenderVehicle v;
			checkType(l,2,out v);
			self.Vehicle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_lastAnimName(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.lastAnimName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_lastAnimName(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.lastAnimName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SpeicalAnimTime(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SpeicalAnimTime);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SpeicalAnimTime(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.SpeicalAnimTime=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_HeadTransform(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.HeadTransform);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_HeadTransform(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.HeadTransform=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SkillT(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SkillT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FaceToDirect(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FaceToDirect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FaceToDirect(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.FaceToDirect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_StopFaceToDirect(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.StopFaceToDirect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_StopFaceToDirect(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.StopFaceToDirect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BuffInfoList(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.BuffInfoList);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ShowModel(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ShowModel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsPlayIdleSpeicalAnim(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.IsPlayIdleSpeicalAnim);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_IsPlayIdleSpeicalAnim(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.IsPlayIdleSpeicalAnim=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DynamicBoneEnable(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DynamicBoneEnable);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DynamicBoneEnable(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.DynamicBoneEnable=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_OnPositonChange(IntPtr l) {
		try {
			TLAIUnit self=(TLAIUnit)checkSelf(l);
			System.Action<UnityEngine.Vector3> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnPositonChange=v;
			else if(op==1) self.OnPositonChange+=v;
			else if(op==2) self.OnPositonChange-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLAIUnit");
		addMember(l,IsActor);
		addMember(l,AddBubbleChat);
		addMember(l,Name);
		addMember(l,RemoveUnit);
		addMember(l,Level);
		addMember(l,CheckShowHPBanner);
		addMember(l,UpdateDebugGuard);
		addMember(l,UpdateDebugBody);
		addMember(l,UpdateDebugAttack);
		addMember(l,PlayIdleSpeicalAnimation);
		addMember(l,PlaySpeicalAnimationByScript);
		addMember(l,PlayAnim);
		addMember(l,EulerAngles);
		addMember(l,TransformDirection);
		addMember(l,AddBubbleTalkInfo);
		addMember(l,"Vehicle",get_Vehicle,set_Vehicle,true);
		addMember(l,"lastAnimName",get_lastAnimName,set_lastAnimName,true);
		addMember(l,"SpeicalAnimTime",get_SpeicalAnimTime,set_SpeicalAnimTime,true);
		addMember(l,"HeadTransform",get_HeadTransform,set_HeadTransform,true);
		addMember(l,"SkillT",get_SkillT,null,true);
		addMember(l,"FaceToDirect",get_FaceToDirect,set_FaceToDirect,true);
		addMember(l,"StopFaceToDirect",get_StopFaceToDirect,set_StopFaceToDirect,true);
		addMember(l,"BuffInfoList",get_BuffInfoList,null,true);
		addMember(l,"ShowModel",get_ShowModel,null,true);
		addMember(l,"IsPlayIdleSpeicalAnim",get_IsPlayIdleSpeicalAnim,set_IsPlayIdleSpeicalAnim,true);
		addMember(l,"DynamicBoneEnable",get_DynamicBoneEnable,set_DynamicBoneEnable,true);
		addMember(l,"OnPositonChange",null,set_OnPositonChange,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(TLAIUnit),typeof(DeepCore.Unity3D.Battle.ComAIUnit));
	}
}
