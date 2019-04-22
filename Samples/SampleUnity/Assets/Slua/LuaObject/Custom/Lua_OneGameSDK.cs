using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_OneGameSDK : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int InitSDK(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.InitSDK();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetUserData(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			var ret=self.GetUserData();
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
	static public int GetPlatformData(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			var ret=self.GetPlatformData();
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
	static public int Login(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(matchType(l,argc,2,typeof(string))){
				OneGameSDK self=(OneGameSDK)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.Login(a1);
				pushValue(l,true);
				return 1;
			}
			else if(matchType(l,argc,2,typeof(bool))){
				OneGameSDK self=(OneGameSDK)checkSelf(l);
				System.Boolean a1;
				checkType(l,2,out a1);
				self.Login(a1);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function Login to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Logout(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.Logout();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SwitchAccount(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.SwitchAccount();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int LoginState(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			var ret=self.LoginState();
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
	static public int SaveOrder(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.SaveOrder(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveOrder(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.RemoveOrder(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetOrderList(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.GetOrderList(a1);
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
	static public int Pay(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SLua.LuaTable a1;
			checkType(l,2,out a1);
			System.Action<System.Int32> a2;
			checkDelegate(l,3,out a2);
			self.Pay(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PayItem(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKBaseData a1;
			checkType(l,2,out a1);
			self.PayItem(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowPersonCenter(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.ShowPersonCenter();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HidePersonCenter(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.HidePersonCenter();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowToolBar(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.ShowToolBar();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int HideToolbar(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.HideToolbar();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int UpdatePlayerInfo(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.UpdatePlayerInfo();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ShowShare(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKBaseData a1;
			checkType(l,2,out a1);
			self.ShowShare(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CallCopyClipboard(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKBaseData a1;
			checkType(l,2,out a1);
			self.CallCopyClipboard(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int IsHasRequest(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			var ret=self.IsHasRequest(a1);
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
	static public int Destory(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.Destory();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ExitGame(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.ExitGame();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoAnyFunction(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			SDKBaseData a2;
			checkType(l,3,out a2);
			self.DoAnyFunction(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int DoPhoneInfo(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			var ret=self.DoPhoneInfo();
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
	static public int AddLocalPush(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKBaseData a1;
			checkType(l,2,out a1);
			self.AddLocalPush(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveLocalPush(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.RemoveLocalPush(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveAllLocalPush(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.RemoveAllLocalPush();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetUserFriends(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.GetUserFriends();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int CreateCredentialProvider(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.String a3;
			checkType(l,4,out a3);
			System.String a4;
			checkType(l,5,out a4);
			System.String a5;
			checkType(l,6,out a5);
			System.String a6;
			checkType(l,7,out a6);
			System.Int64 a7;
			checkType(l,8,out a7);
			System.Int64 a8;
			checkType(l,9,out a8);
			self.CreateCredentialProvider(a1,a2,a3,a4,a5,a6,a7,a8);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Upload(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			System.Action<System.String,System.String> a3;
			checkDelegate(l,4,out a3);
			self.Upload(a1,a2,a3);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Cancel(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			self.Cancel();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int OpenAlbum(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Int32 a2;
			checkType(l,3,out a2);
			System.Int32 a3;
			checkType(l,4,out a3);
			System.Action<System.String> a4;
			checkDelegate(l,5,out a4);
			self.OpenAlbum(a1,a2,a3,a4);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddEventDelegate(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKEventType a1;
			checkEnum(l,2,out a1);
			U3DTypeEventDelegate a2;
			checkDelegate(l,3,out a2);
			self.AddEventDelegate(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int RemoveEventDelegate(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			SDKEventType a1;
			checkEnum(l,2,out a1);
			self.RemoveEventDelegate(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SendEvent(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				OneGameSDK self=(OneGameSDK)checkSelf(l);
				SDKEventType a1;
				checkEnum(l,2,out a1);
				self.SendEvent(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				OneGameSDK self=(OneGameSDK)checkSelf(l);
				SDKEventType a1;
				checkEnum(l,2,out a1);
				SDKBaseData a2;
				checkType(l,3,out a2);
				self.SendEvent(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function SendEvent to call");
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,OneGameSDK.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AppId(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.AppId);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Channel(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.Channel);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PlatformID(IntPtr l) {
		try {
			OneGameSDK self=(OneGameSDK)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.PlatformID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"OneGameSDK");
		addMember(l,InitSDK);
		addMember(l,GetUserData);
		addMember(l,GetPlatformData);
		addMember(l,Login);
		addMember(l,Logout);
		addMember(l,SwitchAccount);
		addMember(l,LoginState);
		addMember(l,SaveOrder);
		addMember(l,RemoveOrder);
		addMember(l,GetOrderList);
		addMember(l,Pay);
		addMember(l,PayItem);
		addMember(l,ShowPersonCenter);
		addMember(l,HidePersonCenter);
		addMember(l,ShowToolBar);
		addMember(l,HideToolbar);
		addMember(l,UpdatePlayerInfo);
		addMember(l,ShowShare);
		addMember(l,CallCopyClipboard);
		addMember(l,IsHasRequest);
		addMember(l,Destory);
		addMember(l,ExitGame);
		addMember(l,DoAnyFunction);
		addMember(l,DoPhoneInfo);
		addMember(l,AddLocalPush);
		addMember(l,RemoveLocalPush);
		addMember(l,RemoveAllLocalPush);
		addMember(l,GetUserFriends);
		addMember(l,CreateCredentialProvider);
		addMember(l,Upload);
		addMember(l,Cancel);
		addMember(l,OpenAlbum);
		addMember(l,AddEventDelegate);
		addMember(l,RemoveEventDelegate);
		addMember(l,SendEvent);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"AppId",get_AppId,null,true);
		addMember(l,"Channel",get_Channel,null,true);
		addMember(l,"PlatformID",get_PlatformID,null,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(OneGameSDK),typeof(Notification));
	}
}
