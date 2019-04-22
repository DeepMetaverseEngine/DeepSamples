using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_TLAIPlayer : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			TLAIPlayer o;
			DeepCore.Unity3D.Battle.BattleScene a1;
			checkType(l,2,out a1);
			DeepCore.GameSlave.ZoneUnit a2;
			checkType(l,3,out a2);
			o=new TLAIPlayer(a1,a2);
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
	static public int GetAvatarMap(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			var ret=self.GetAvatarMap();
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
	static public int AddAvatar(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			var ret=self.AddAvatar(a1,a2);
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
	static public int TestLoadAvatar(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			System.Boolean a1;
			checkType(l,2,out a1);
			SLua.LuaTable a2;
			checkType(l,3,out a2);
			self.TestLoadAvatar(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeFish(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			System.Action<System.Boolean> a1;
			checkDelegate(l,2,out a1);
			self.ChangeFish(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveFishhModel(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			self.RemoveFishhModel();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SoundImportant(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			var ret=self.SoundImportant();
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
	static public int GetX(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			var ret=self.GetX();
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
	static public int GetY(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			var ret=self.GetY();
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
	static public int set_OnTeamMemberListChange(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			System.Action<TLAIPlayer> v;
			int op=checkDelegate(l,2,out v);
			if(op==0) self.OnTeamMemberListChange=v;
			else if(op==1) self.OnTeamMemberListChange+=v;
			else if(op==2) self.OnTeamMemberListChange-=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlayerVirtual(IntPtr l) {
		try {
			TLAIPlayer self=(TLAIPlayer)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlayerVirtual);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"TLAIPlayer");
		addMember(l,GetAvatarMap);
		addMember(l,AddAvatar);
		addMember(l,TestLoadAvatar);
		addMember(l,ChangeFish);
		addMember(l,RemoveFishhModel);
		addMember(l,SoundImportant);
		addMember(l,GetX);
		addMember(l,GetY);
		addMember(l,"OnTeamMemberListChange",null,set_OnTeamMemberListChange,true);
		addMember(l,"PlayerVirtual",get_PlayerVirtual,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(TLAIPlayer),typeof(TLAIUnit));
	}
}
