using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_UGUI_TextLayerInputField : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnDeselect(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnDeselect(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSelect(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnSelect(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerClick(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerClick(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnPointerDown(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.PointerEventData a1;
			checkType(l,2,out a1);
			self.OnPointerDown(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ProcessEvent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.Event a1;
			checkType(l,2,out a1);
			self.ProcessEvent(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnUpdateSelected(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnUpdateSelected(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ActivateInputField(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			self.ActivateInputField();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DeactivateInputField(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			self.DeactivateInputField();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GraphicUpdateComplete(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			self.GraphicUpdateComplete();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LayoutComplete(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			self.LayoutComplete();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OnSubmit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.EventSystems.BaseEventData a1;
			checkType(l,2,out a1);
			self.OnSubmit(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Rebuild(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.CanvasUpdate a1;
			checkEnum(l,2,out a1);
			self.Rebuild(a1);
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
	static public int set_event_EndEdit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			System.Action<System.String> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_EndEdit=v;
			else if(op==1) self.event_EndEdit+=v;
			else if(op==2) self.event_EndEdit-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_event_ValueChanged(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			System.Action<System.String> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.event_ValueChanged=v;
			else if(op==1) self.event_ValueChanged+=v;
			else if(op==2) self.event_ValueChanged-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Text);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Text(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			string v;
			checkType(l,2,out v);
			self.Text=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_isFocused(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isFocused);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_inputType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.inputType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_inputType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.InputField.InputType v;
			checkEnum(l,2,out v);
			self.inputType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_contentType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.contentType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_contentType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.InputField.ContentType v;
			checkEnum(l,2,out v);
			self.contentType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_lineType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.lineType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_lineType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.InputField.LineType v;
			checkEnum(l,2,out v);
			self.lineType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_multiLine(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.multiLine);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_keyboardType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.keyboardType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_keyboardType(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.TouchScreenKeyboardType v;
			checkEnum(l,2,out v);
			self.keyboardType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_characterLimit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.characterLimit);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_characterLimit(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			int v;
			checkType(l,2,out v);
			self.characterLimit=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_characterValidation(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.characterValidation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_characterValidation(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.InputField.CharacterValidation v;
			checkEnum(l,2,out v);
			self.characterValidation=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_asteriskChar(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.asteriskChar);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_asteriskChar(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			System.Char v;
			checkType(l,2,out v);
			self.asteriskChar=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_isShowCaret(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.isShowCaret);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_isShowCaret(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			bool v;
			checkType(l,2,out v);
			self.isShowCaret=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_caretBlinkRate(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.caretBlinkRate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_caretBlinkRate(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.caretBlinkRate=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TextComponent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.TextComponent);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_TextComponent(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			DeepCore.Unity3D.UGUI.ITextComponent v;
			checkType(l,2,out v);
			self.TextComponent=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Placeholder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Placeholder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Placeholder(IntPtr l) {
		try {
			DeepCore.Unity3D.UGUI.TextLayerInputField self=(DeepCore.Unity3D.UGUI.TextLayerInputField)checkSelf(l);
			UnityEngine.UI.Graphic v;
			checkType(l,2,out v);
			self.Placeholder=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TextLayerInputField");
		addMember(l,OnDeselect);
		addMember(l,OnSelect);
		addMember(l,OnPointerClick);
		addMember(l,OnPointerDown);
		addMember(l,ProcessEvent);
		addMember(l,OnUpdateSelected);
		addMember(l,ActivateInputField);
		addMember(l,DeactivateInputField);
		addMember(l,GraphicUpdateComplete);
		addMember(l,LayoutComplete);
		addMember(l,OnSubmit);
		addMember(l,Rebuild);
		addMember(l,"event_EndEdit",null,set_event_EndEdit,true);
		addMember(l,"event_ValueChanged",null,set_event_ValueChanged,true);
		addMember(l,"Text",get_Text,set_Text,true);
		addMember(l,"isFocused",get_isFocused,null,true);
		addMember(l,"inputType",get_inputType,set_inputType,true);
		addMember(l,"contentType",get_contentType,set_contentType,true);
		addMember(l,"lineType",get_lineType,set_lineType,true);
		addMember(l,"multiLine",get_multiLine,null,true);
		addMember(l,"keyboardType",get_keyboardType,set_keyboardType,true);
		addMember(l,"characterLimit",get_characterLimit,set_characterLimit,true);
		addMember(l,"characterValidation",get_characterValidation,set_characterValidation,true);
		addMember(l,"asteriskChar",get_asteriskChar,set_asteriskChar,true);
		addMember(l,"isShowCaret",get_isShowCaret,set_isShowCaret,true);
		addMember(l,"caretBlinkRate",get_caretBlinkRate,set_caretBlinkRate,true);
		addMember(l,"TextComponent",get_TextComponent,set_TextComponent,true);
		addMember(l,"Placeholder",get_Placeholder,set_Placeholder,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(DeepCore.Unity3D.UGUI.TextLayerInputField),typeof(DeepCore.Unity3D.UGUI.DisplayNodeInteractive));
	}
}
