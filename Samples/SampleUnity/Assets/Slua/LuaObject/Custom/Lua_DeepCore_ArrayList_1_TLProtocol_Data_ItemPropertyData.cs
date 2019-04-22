using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_ArrayList_1_TLProtocol_Data_ItemPropertyData : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			DeepCore.ArrayList<TLProtocol.Data.ItemPropertyData> o;
			if(argc==1){
				o=new DeepCore.ArrayList<TLProtocol.Data.ItemPropertyData>();
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(IEnumerable<TLProtocol.Data.ItemPropertyData>))){
				System.Collections.Generic.IEnumerable<TLProtocol.Data.ItemPropertyData> a1;
				checkType(l,2,out a1);
				o=new DeepCore.ArrayList<TLProtocol.Data.ItemPropertyData>(a1);
				pushValue(l,true);
				pushValue(l,o);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(int))){
				System.Int32 a1;
				checkType(l,2,out a1);
				o=new DeepCore.ArrayList<TLProtocol.Data.ItemPropertyData>(a1);
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
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"DeepCore.ArrayList<<TLProtocol.Data.ItemPropertyData, ThreeLives.Client.Protocol, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null>>");
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.ArrayList<TLProtocol.Data.ItemPropertyData>),typeof(System.Collections.Generic.List<TLProtocol.Data.ItemPropertyData>));
	}
}
