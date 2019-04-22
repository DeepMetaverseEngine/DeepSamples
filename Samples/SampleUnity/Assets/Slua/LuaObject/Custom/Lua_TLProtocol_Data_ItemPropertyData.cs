using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_ItemPropertyData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData o;
			o=new TLProtocol.Data.ItemPropertyData();
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
	static public int GetIsLocked(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			var ret=self.GetIsLocked();
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
	static public int SetIsLocked(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			self.SetIsLocked(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetColorRGB(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			var ret=self.GetColorRGB();
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
	static public int SetColorRGB(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.SetColorRGB(a1);
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
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
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
	static public int CompareTo(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			TLProtocol.Data.ItemPropertyData a1;
			checkType(l,2,out a1);
			var ret=self.CompareTo(a1);
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
	static public int WriteExternal_s(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData a1;
			checkType(l,1,out a1);
			DeepCore.IO.TextOutputStream a2;
			checkType(l,2,out a2);
			TLProtocol.Data.ItemPropertyData.WriteExternal(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ReadExternal_s(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData a1;
			checkType(l,1,out a1);
			DeepCore.IO.TextInputStream a2;
			checkType(l,2,out a2);
			TLProtocol.Data.ItemPropertyData.ReadExternal(a1,a2);
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
	static public int get_FixedAttributeTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.FixedAttributeTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ExtraAttributeTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.ExtraAttributeTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GridGemAttributeTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.GridGemAttributeTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_GridRefineAttributeTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.GridRefineAttributeTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WingAttributeTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.WingAttributeTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ExtraBuffTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.ExtraBuffTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IsLockedTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.IsLockedTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ColorRGBTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.ColorRGBTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FateTag(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,TLProtocol.Data.ItemPropertyData.FateTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Index(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Index);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Index(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Index=v;
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
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
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
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.String v;
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
	static public int get_ID(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ID(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ID=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ValueType(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.ValueType);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_ValueType(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.ValueType=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Value(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Value);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_Value(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.Value=v;
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
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
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
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.String v;
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
	static public int get_SubAttributes(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.SubAttributes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_SubAttributes(IntPtr l) {
		try {
			TLProtocol.Data.ItemPropertyData self=(TLProtocol.Data.ItemPropertyData)checkSelf(l);
			System.Collections.Generic.List<TLProtocol.Data.ItemPropertyData> v;
			checkType(l,2,out v);
			self.SubAttributes=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"ItemPropertyData");
		addMember(l,GetIsLocked);
		addMember(l,SetIsLocked);
		addMember(l,GetColorRGB);
		addMember(l,SetColorRGB);
		addMember(l,Clone);
		addMember(l,CompareTo);
		addMember(l,WriteExternal_s);
		addMember(l,ReadExternal_s);
		addMember(l,"FixedAttributeTag",get_FixedAttributeTag,null,false);
		addMember(l,"ExtraAttributeTag",get_ExtraAttributeTag,null,false);
		addMember(l,"GridGemAttributeTag",get_GridGemAttributeTag,null,false);
		addMember(l,"GridRefineAttributeTag",get_GridRefineAttributeTag,null,false);
		addMember(l,"WingAttributeTag",get_WingAttributeTag,null,false);
		addMember(l,"ExtraBuffTag",get_ExtraBuffTag,null,false);
		addMember(l,"IsLockedTag",get_IsLockedTag,null,false);
		addMember(l,"ColorRGBTag",get_ColorRGBTag,null,false);
		addMember(l,"FateTag",get_FateTag,null,false);
		addMember(l,"Index",get_Index,set_Index,true);
		addMember(l,"Name",get_Name,set_Name,true);
		addMember(l,"ID",get_ID,set_ID,true);
		addMember(l,"ValueType",get_ValueType,set_ValueType,true);
		addMember(l,"Value",get_Value,set_Value,true);
		addMember(l,"Tag",get_Tag,set_Tag,true);
		addMember(l,"SubAttributes",get_SubAttributes,set_SubAttributes,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.ItemPropertyData));
	}
}
