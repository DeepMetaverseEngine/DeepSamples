using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLProtocol_Data_AvatarInfoSnap : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap o;
			o=new TLProtocol.Data.AvatarInfoSnap();
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
	static public int get_PartTag(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PartTag);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_PartTag(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			System.Int32 v;
			checkType(l,2,out v);
			self.PartTag=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_FileName(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.FileName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_FileName(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.FileName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultName(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultName);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultName(IntPtr l) {
		try {
			TLProtocol.Data.AvatarInfoSnap self=(TLProtocol.Data.AvatarInfoSnap)checkSelf(l);
			System.String v;
			checkType(l,2,out v);
			self.DefaultName=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"AvatarInfoSnap");
		addMember(l,"PartTag",get_PartTag,set_PartTag,true);
		addMember(l,"FileName",get_FileName,set_FileName,true);
		addMember(l,"DefaultName",get_DefaultName,set_DefaultName,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLProtocol.Data.AvatarInfoSnap));
	}
}
