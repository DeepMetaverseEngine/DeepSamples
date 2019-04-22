using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_PracticeScene : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Init(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.Init(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetButtonPressed(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetButtonPressed(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetButtonSelected(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetButtonSelected(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RotateTo(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.RotateTo(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StartRotate(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.StartRotate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopRotate(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			var ret=self.StopRotate();
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
	static public int SetSlider(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetSlider(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetFightPower(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			UnityEngine.Font a4;
			checkType(l,5,out a4);
			self.SetFightPower(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetButtonImage(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.GetButtonImage(a1,a2);
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
	static public int SetLightPoint(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			self.SetLightPoint(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetButtonListener(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Action<System.Int32> a1;
			checkDelegate(l,2,out a1);
			self.SetButtonListener(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnButtonPressed(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.UI.Toggle a1;
			checkType(l,2,out a1);
			self.OnButtonPressed(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowUpEffect(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.ShowUpEffect(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnBeginDrag(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnBeginDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnDrag(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnDrag(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnEndDrag(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnEndDrag(a1);
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
	static public int get_group(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.group);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_group(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.group=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_toggle(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.toggle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_toggle(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.toggle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_toggle2(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.toggle2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_toggle2(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.RectTransform v;
			checkType(l,2,out v);
			self.toggle2=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_slider(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.slider);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_slider(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.UI.Slider v;
			checkType(l,2,out v);
			self.slider=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_center(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.center);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_center(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.center=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_lookat(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.lookat);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_lookat(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.Transform v;
			checkType(l,2,out v);
			self.lookat=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_radius(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.radius);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_radius(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.radius=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_distance(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.distance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_distance(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.distance=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_startAngle(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.startAngle);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_startAngle(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.startAngle=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_count(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.count);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_count(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.count=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_prefabName(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.prefabName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_prefabName(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.prefabName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_rotateSpeed(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.rotateSpeed);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_rotateSpeed(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Single v;
			checkType(l,2,out v);
			self.rotateSpeed=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_sliderPos(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.sliderPos);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_sliderPos(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Single[] v;
			checkArray(l,2,out v);
			self.sliderPos=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_pointgroup(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.pointgroup);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_pointgroup(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.RectTransform[] v;
			checkArray(l,2,out v);
			self.pointgroup=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_effect(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.effect);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_effect(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			UnityEngine.GameObject v;
			checkType(l,2,out v);
			self.effect=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_forceUpdate(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.forceUpdate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_forceUpdate(IntPtr l) {
		try {
			PracticeScene self=(PracticeScene)checkSelf(l);
			System.Boolean v;
			checkType(l,2,out v);
			self.forceUpdate=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"PracticeScene");
		addMember(l,Init);
		addMember(l,SetButtonPressed);
		addMember(l,SetButtonSelected);
		addMember(l,RotateTo);
		addMember(l,StartRotate);
		addMember(l,StopRotate);
		addMember(l,SetSlider);
		addMember(l,SetFightPower);
		addMember(l,GetButtonImage);
		addMember(l,SetLightPoint);
		addMember(l,SetButtonListener);
		addMember(l,OnButtonPressed);
		addMember(l,ShowUpEffect);
		addMember(l,OnBeginDrag);
		addMember(l,OnDrag);
		addMember(l,OnEndDrag);
		addMember(l,"group",get_group,set_group,true);
		addMember(l,"toggle",get_toggle,set_toggle,true);
		addMember(l,"toggle2",get_toggle2,set_toggle2,true);
		addMember(l,"slider",get_slider,set_slider,true);
		addMember(l,"center",get_center,set_center,true);
		addMember(l,"lookat",get_lookat,set_lookat,true);
		addMember(l,"radius",get_radius,set_radius,true);
		addMember(l,"distance",get_distance,set_distance,true);
		addMember(l,"startAngle",get_startAngle,set_startAngle,true);
		addMember(l,"count",get_count,set_count,true);
		addMember(l,"prefabName",get_prefabName,set_prefabName,true);
		addMember(l,"rotateSpeed",get_rotateSpeed,set_rotateSpeed,true);
		addMember(l,"sliderPos",get_sliderPos,set_sliderPos,true);
		addMember(l,"pointgroup",get_pointgroup,set_pointgroup,true);
		addMember(l,"effect",get_effect,set_effect,true);
		addMember(l,"forceUpdate",get_forceUpdate,set_forceUpdate,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(PracticeScene),typeof(UnityEngine.MonoBehaviour));
	}
}
