using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_SDKAttName : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			SDKAttName o;
			o=new SDKAttName();
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
	static public int get_APP_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.APP_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_APP_KEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.APP_KEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_APP_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.APP_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_REDIRECT_URI(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.REDIRECT_URI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SECRET_KEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SECRET_KEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CHANNEL_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CHANNEL_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_KEYSTORE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_KEYSTORE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_PASSWORD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_PASSWORD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CP_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CP_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SDK_CP_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SDK_CP_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BUGLY_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.BUGLY_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PLATFORM_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PLATFORM_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SDK_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SDK_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SDK_VERSION(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SDK_VERSION);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PLATFORM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PLATFORM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VERSION(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.VERSION);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BUNDLE_INDENTIFLER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.BUNDLE_INDENTIFLER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BUNDLE_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.BUNDLE_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRODUCT_PACKAGE_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PRODUCT_PACKAGE_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRODUCE_KEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PRODUCE_KEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PRODUCT_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PRODUCT_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AUTH_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.AUTH_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BUNDLE_DISPLAY_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.BUNDLE_DISPLAY_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_CALL_BACK_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_CALL_BACK_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_BASE_RATE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_BASE_RATE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_BASE_VALUE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_BASE_VALUE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXTRA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.EXTRA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_LANDSPACE_GAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_LANDSPACE_GAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_SUPPORT_ROATED(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_SUPPORT_ROATED);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_SUPPORT_EXIT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_SUPPORT_EXIT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_SHOW_LOG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_SHOW_LOG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_LONG_COMET(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_LONG_COMET);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_OPEN_RECHARGE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_OPEN_RECHARGE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_LOGOUT_AUTO_LOGIN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_LOGOUT_AUTO_LOGIN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IS_DEBUG_MODEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IS_DEBUG_MODEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CLOSE_RECHARGE_MSG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CLOSE_RECHARGE_MSG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_PASS_WORD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_PASS_WORD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_TOKEN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_TOKEN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_SESSION_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_SESSION_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_HEAD_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_HEAD_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_USER_HEAD_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.USER_HEAD_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DATE_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.DATE_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_LEVEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_LEVEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_CREATE_TIME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_CREATE_TIME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_LEVELUP_TIME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_LEVELUP_TIME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZONE_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ZONE_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ZONE_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ZONE_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SERVER_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SERVER_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SERVER_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SERVER_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_AMOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_AMOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_ORDERID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_ORDERID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_SIGNATURE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_SIGNATURE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_ACCOUNTID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_ACCOUNTID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_SELLID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_SELLID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_PRODUCTNAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_PRODUCTNAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_PRODUCTDESC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_PRODUCTDESC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROLE_BALANCE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROLE_BALANCE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VIP_LEVEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.VIP_LEVEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PARTY_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PARTY_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_REAL_PRICE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.REAL_PRICE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ORGIN_PRICE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ORGIN_PRICE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DISCOUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.DISCOUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ITEM_COUNT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ITEM_COUNT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ITEM_LOCAL_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ITEM_LOCAL_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ITEM_SERVER_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ITEM_SERVER_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ITEM_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ITEM_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ITEM_DESC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ITEM_DESC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_BILL_NUMBER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.BILL_NUMBER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NEED_SIGN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.NEED_SIGN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_QUERY_ORDER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.QUERY_ORDER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_RESULT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_RESULT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_RESULT_REASON(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_RESULT_REASON);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_RESULT_DATA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_RESULT_DATA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_TARGET_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_TARGET_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_IMG_LOCAL_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_IMG_LOCAL_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_VIDEO_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_VIDEO_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_SENDER_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_SENDER_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_SENDER_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_SENDER_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_RECEIVER_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_RECEIVER_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_RECEIVER_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_RECEIVER_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_INFO_TITLE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_INFO_TITLE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_INFO_CONTENT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_INFO_CONTENT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_IMG_URL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_IMG_URL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SHARE_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SHARE_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_REQUEST_INIT_WITH_SEVER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.REQUEST_INIT_WITH_SEVER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SUPPORT_SHARE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SUPPORT_SHARE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SUPPORT_PERSON_CENTER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SUPPORT_PERSON_CENTER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NOT_ALLOW_PUSH_NOTIFY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.NOT_ALLOW_PUSH_NOTIFY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_SPLASH_IMAGE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_SPLASH_IMAGE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_TUTORIAL_START(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_TUTORIAL_START);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_TUTORIAL_COMPLETE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_TUTORIAL_COMPLETE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_LOAD_START(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_LOAD_START);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_LOAD_COMPLETE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_LOAD_COMPLETE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_CHARACTER_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_CHARACTER_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_CALL_LOGIN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_CALL_LOGIN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_UPDATE_START(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_UPDATE_START);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_UPDATE_COMPLETE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_UPDATE_COMPLETE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CUSTOM_EVENT_ONE_CHAPTER1(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CUSTOM_EVENT_ONE_CHAPTER1);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_TYPE_DATA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_TYPE_DATA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_TITLE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_TITLE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_INFO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_INFO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_REPEAT_INTERVAL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_REPEAT_INTERVAL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_ALERT_DATE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_ALERT_DATE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_NEED_NOTIFY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_NEED_NOTIFY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_RECEIVE_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_RECEIVE_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PUSH_RECEIVE_INFO(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PUSH_RECEIVE_INFO);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_APP_VERSION_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.APP_VERSION_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CURRENT_TIMEZONE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CURRENT_TIMEZONE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CURRENT_TIME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CURRENT_TIME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CURRENT_LANGUAGE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CURRENT_LANGUAGE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SIM_OPERATOR_NAME(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SIM_OPERATOR_NAME);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_NETWORK_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.NETWORK_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PHONE_IP(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PHONE_IP);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PHONE_MODEL(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PHONE_MODEL);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PHONE_PRODUCTOR(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PHONE_PRODUCTOR);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_CPU_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.CPU_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SYSTEM_VERSION(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SYSTEM_VERSION);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SCREEN_HEIGHT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SCREEN_HEIGHT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SCREEN_WIDTH(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SCREEN_WIDTH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ROOT_AHTH(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ROOT_AHTH);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MEMORY_TOTAL_MB(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.MEMORY_TOTAL_MB);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MAC_ADDRESS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.MAC_ADDRESS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IMEI(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.IMEI);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SIM_SERIAL_NUMBER(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SIM_SERIAL_NUMBER);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ANDROID_ID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.ANDROID_ID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RESULT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.RESULT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_KEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.KEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DATA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.DATA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SDK_NAME_QQ(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SDK_NAME_QQ);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_SDK_NAME_WX(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.SDK_NAME_WX);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_TECENT_TYPE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.TECENT_TYPE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OPENID(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.OPENID);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OPENKEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.OPENKEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PFKEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PFKEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_PAY_TOKEN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.PAY_TOKEN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXTERN_FUNCTION_KEY(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.EXTERN_FUNCTION_KEY);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXTERN_FUNCTION_VALUE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.EXTERN_FUNCTION_VALUE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_EXTERN_FUNCTION_VALUE_2(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,SDKAttName.EXTERN_FUNCTION_VALUE_2);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"SDKAttName");
		addMember(l,"APP_NAME",get_APP_NAME,null,false);
		addMember(l,"APP_KEY",get_APP_KEY,null,false);
		addMember(l,"APP_ID",get_APP_ID,null,false);
		addMember(l,"REDIRECT_URI",get_REDIRECT_URI,null,false);
		addMember(l,"SECRET_KEY",get_SECRET_KEY,null,false);
		addMember(l,"CHANNEL_ID",get_CHANNEL_ID,null,false);
		addMember(l,"PAY_KEYSTORE",get_PAY_KEYSTORE,null,false);
		addMember(l,"PAY_PASSWORD",get_PAY_PASSWORD,null,false);
		addMember(l,"CP_ID",get_CP_ID,null,false);
		addMember(l,"SDK_CP_ID",get_SDK_CP_ID,null,false);
		addMember(l,"BUGLY_ID",get_BUGLY_ID,null,false);
		addMember(l,"PLATFORM_ID",get_PLATFORM_ID,null,false);
		addMember(l,"SDK_NAME",get_SDK_NAME,null,false);
		addMember(l,"SDK_VERSION",get_SDK_VERSION,null,false);
		addMember(l,"PLATFORM",get_PLATFORM,null,false);
		addMember(l,"VERSION",get_VERSION,null,false);
		addMember(l,"BUNDLE_INDENTIFLER",get_BUNDLE_INDENTIFLER,null,false);
		addMember(l,"BUNDLE_NAME",get_BUNDLE_NAME,null,false);
		addMember(l,"PRODUCT_PACKAGE_NAME",get_PRODUCT_PACKAGE_NAME,null,false);
		addMember(l,"PRODUCE_KEY",get_PRODUCE_KEY,null,false);
		addMember(l,"PRODUCT_ID",get_PRODUCT_ID,null,false);
		addMember(l,"AUTH_URL",get_AUTH_URL,null,false);
		addMember(l,"BUNDLE_DISPLAY_NAME",get_BUNDLE_DISPLAY_NAME,null,false);
		addMember(l,"PAY_CALL_BACK_URL",get_PAY_CALL_BACK_URL,null,false);
		addMember(l,"PAY_BASE_RATE",get_PAY_BASE_RATE,null,false);
		addMember(l,"PAY_BASE_VALUE",get_PAY_BASE_VALUE,null,false);
		addMember(l,"EXTRA",get_EXTRA,null,false);
		addMember(l,"IS_LANDSPACE_GAME",get_IS_LANDSPACE_GAME,null,false);
		addMember(l,"IS_SUPPORT_ROATED",get_IS_SUPPORT_ROATED,null,false);
		addMember(l,"IS_SUPPORT_EXIT",get_IS_SUPPORT_EXIT,null,false);
		addMember(l,"IS_SHOW_LOG",get_IS_SHOW_LOG,null,false);
		addMember(l,"IS_LONG_COMET",get_IS_LONG_COMET,null,false);
		addMember(l,"IS_OPEN_RECHARGE",get_IS_OPEN_RECHARGE,null,false);
		addMember(l,"IS_LOGOUT_AUTO_LOGIN",get_IS_LOGOUT_AUTO_LOGIN,null,false);
		addMember(l,"IS_DEBUG_MODEL",get_IS_DEBUG_MODEL,null,false);
		addMember(l,"CLOSE_RECHARGE_MSG",get_CLOSE_RECHARGE_MSG,null,false);
		addMember(l,"USER_NAME",get_USER_NAME,null,false);
		addMember(l,"USER_PASS_WORD",get_USER_PASS_WORD,null,false);
		addMember(l,"USER_TOKEN",get_USER_TOKEN,null,false);
		addMember(l,"USER_SESSION_ID",get_USER_SESSION_ID,null,false);
		addMember(l,"USER_ID",get_USER_ID,null,false);
		addMember(l,"USER_HEAD_ID",get_USER_HEAD_ID,null,false);
		addMember(l,"USER_HEAD_URL",get_USER_HEAD_URL,null,false);
		addMember(l,"DATE_TYPE",get_DATE_TYPE,null,false);
		addMember(l,"ROLE_ID",get_ROLE_ID,null,false);
		addMember(l,"ROLE_NAME",get_ROLE_NAME,null,false);
		addMember(l,"ROLE_LEVEL",get_ROLE_LEVEL,null,false);
		addMember(l,"ROLE_CREATE_TIME",get_ROLE_CREATE_TIME,null,false);
		addMember(l,"ROLE_LEVELUP_TIME",get_ROLE_LEVELUP_TIME,null,false);
		addMember(l,"ZONE_ID",get_ZONE_ID,null,false);
		addMember(l,"ZONE_NAME",get_ZONE_NAME,null,false);
		addMember(l,"SERVER_ID",get_SERVER_ID,null,false);
		addMember(l,"SERVER_NAME",get_SERVER_NAME,null,false);
		addMember(l,"PAY_AMOUNT",get_PAY_AMOUNT,null,false);
		addMember(l,"PAY_ORDERID",get_PAY_ORDERID,null,false);
		addMember(l,"PAY_SIGNATURE",get_PAY_SIGNATURE,null,false);
		addMember(l,"PAY_ACCOUNTID",get_PAY_ACCOUNTID,null,false);
		addMember(l,"PAY_SELLID",get_PAY_SELLID,null,false);
		addMember(l,"PAY_PRODUCTNAME",get_PAY_PRODUCTNAME,null,false);
		addMember(l,"PAY_PRODUCTDESC",get_PAY_PRODUCTDESC,null,false);
		addMember(l,"ROLE_TYPE",get_ROLE_TYPE,null,false);
		addMember(l,"ROLE_BALANCE",get_ROLE_BALANCE,null,false);
		addMember(l,"VIP_LEVEL",get_VIP_LEVEL,null,false);
		addMember(l,"PARTY_NAME",get_PARTY_NAME,null,false);
		addMember(l,"REAL_PRICE",get_REAL_PRICE,null,false);
		addMember(l,"ORGIN_PRICE",get_ORGIN_PRICE,null,false);
		addMember(l,"DISCOUNT",get_DISCOUNT,null,false);
		addMember(l,"ITEM_COUNT",get_ITEM_COUNT,null,false);
		addMember(l,"ITEM_LOCAL_ID",get_ITEM_LOCAL_ID,null,false);
		addMember(l,"ITEM_SERVER_ID",get_ITEM_SERVER_ID,null,false);
		addMember(l,"ITEM_NAME",get_ITEM_NAME,null,false);
		addMember(l,"ITEM_DESC",get_ITEM_DESC,null,false);
		addMember(l,"BILL_NUMBER",get_BILL_NUMBER,null,false);
		addMember(l,"NEED_SIGN",get_NEED_SIGN,null,false);
		addMember(l,"QUERY_ORDER",get_QUERY_ORDER,null,false);
		addMember(l,"PAY_RESULT",get_PAY_RESULT,null,false);
		addMember(l,"PAY_RESULT_REASON",get_PAY_RESULT_REASON,null,false);
		addMember(l,"PAY_RESULT_DATA",get_PAY_RESULT_DATA,null,false);
		addMember(l,"SHARE_ID",get_SHARE_ID,null,false);
		addMember(l,"SHARE_TARGET_URL",get_SHARE_TARGET_URL,null,false);
		addMember(l,"SHARE_IMG_LOCAL_URL",get_SHARE_IMG_LOCAL_URL,null,false);
		addMember(l,"SHARE_VIDEO_URL",get_SHARE_VIDEO_URL,null,false);
		addMember(l,"SHARE_SENDER_ID",get_SHARE_SENDER_ID,null,false);
		addMember(l,"SHARE_SENDER_NAME",get_SHARE_SENDER_NAME,null,false);
		addMember(l,"SHARE_RECEIVER_ID",get_SHARE_RECEIVER_ID,null,false);
		addMember(l,"SHARE_RECEIVER_NAME",get_SHARE_RECEIVER_NAME,null,false);
		addMember(l,"SHARE_INFO_TITLE",get_SHARE_INFO_TITLE,null,false);
		addMember(l,"SHARE_INFO_CONTENT",get_SHARE_INFO_CONTENT,null,false);
		addMember(l,"SHARE_IMG_URL",get_SHARE_IMG_URL,null,false);
		addMember(l,"SHARE_TYPE",get_SHARE_TYPE,null,false);
		addMember(l,"REQUEST_INIT_WITH_SEVER",get_REQUEST_INIT_WITH_SEVER,null,false);
		addMember(l,"SUPPORT_SHARE",get_SUPPORT_SHARE,null,false);
		addMember(l,"SUPPORT_PERSON_CENTER",get_SUPPORT_PERSON_CENTER,null,false);
		addMember(l,"NOT_ALLOW_PUSH_NOTIFY",get_NOT_ALLOW_PUSH_NOTIFY,null,false);
		addMember(l,"CUSTOM_EVENT",get_CUSTOM_EVENT,null,false);
		addMember(l,"CUSTOM_EVENT_NAME",get_CUSTOM_EVENT_NAME,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_SPLASH_IMAGE",get_CUSTOM_EVENT_ONE_SPLASH_IMAGE,null,false);
		addMember(l,"CUSTOM_EVENT_TUTORIAL_START",get_CUSTOM_EVENT_TUTORIAL_START,null,false);
		addMember(l,"CUSTOM_EVENT_TUTORIAL_COMPLETE",get_CUSTOM_EVENT_TUTORIAL_COMPLETE,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_LOAD_START",get_CUSTOM_EVENT_ONE_LOAD_START,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_LOAD_COMPLETE",get_CUSTOM_EVENT_ONE_LOAD_COMPLETE,null,false);
		addMember(l,"CUSTOM_EVENT_CHARACTER_NAME",get_CUSTOM_EVENT_CHARACTER_NAME,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_CALL_LOGIN",get_CUSTOM_EVENT_ONE_CALL_LOGIN,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_UPDATE_START",get_CUSTOM_EVENT_ONE_UPDATE_START,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_UPDATE_COMPLETE",get_CUSTOM_EVENT_ONE_UPDATE_COMPLETE,null,false);
		addMember(l,"CUSTOM_EVENT_ONE_CHAPTER1",get_CUSTOM_EVENT_ONE_CHAPTER1,null,false);
		addMember(l,"PUSH_TYPE",get_PUSH_TYPE,null,false);
		addMember(l,"PUSH_TYPE_DATA",get_PUSH_TYPE_DATA,null,false);
		addMember(l,"PUSH_ID",get_PUSH_ID,null,false);
		addMember(l,"PUSH_TITLE",get_PUSH_TITLE,null,false);
		addMember(l,"PUSH_INFO",get_PUSH_INFO,null,false);
		addMember(l,"PUSH_REPEAT_INTERVAL",get_PUSH_REPEAT_INTERVAL,null,false);
		addMember(l,"PUSH_ALERT_DATE",get_PUSH_ALERT_DATE,null,false);
		addMember(l,"PUSH_NEED_NOTIFY",get_PUSH_NEED_NOTIFY,null,false);
		addMember(l,"PUSH_RECEIVE_TYPE",get_PUSH_RECEIVE_TYPE,null,false);
		addMember(l,"PUSH_RECEIVE_INFO",get_PUSH_RECEIVE_INFO,null,false);
		addMember(l,"APP_VERSION_NAME",get_APP_VERSION_NAME,null,false);
		addMember(l,"CURRENT_TIMEZONE",get_CURRENT_TIMEZONE,null,false);
		addMember(l,"CURRENT_TIME",get_CURRENT_TIME,null,false);
		addMember(l,"CURRENT_LANGUAGE",get_CURRENT_LANGUAGE,null,false);
		addMember(l,"SIM_OPERATOR_NAME",get_SIM_OPERATOR_NAME,null,false);
		addMember(l,"NETWORK_TYPE",get_NETWORK_TYPE,null,false);
		addMember(l,"PHONE_IP",get_PHONE_IP,null,false);
		addMember(l,"PHONE_MODEL",get_PHONE_MODEL,null,false);
		addMember(l,"PHONE_PRODUCTOR",get_PHONE_PRODUCTOR,null,false);
		addMember(l,"CPU_TYPE",get_CPU_TYPE,null,false);
		addMember(l,"SYSTEM_VERSION",get_SYSTEM_VERSION,null,false);
		addMember(l,"SCREEN_HEIGHT",get_SCREEN_HEIGHT,null,false);
		addMember(l,"SCREEN_WIDTH",get_SCREEN_WIDTH,null,false);
		addMember(l,"ROOT_AHTH",get_ROOT_AHTH,null,false);
		addMember(l,"MEMORY_TOTAL_MB",get_MEMORY_TOTAL_MB,null,false);
		addMember(l,"MAC_ADDRESS",get_MAC_ADDRESS,null,false);
		addMember(l,"IMEI",get_IMEI,null,false);
		addMember(l,"SIM_SERIAL_NUMBER",get_SIM_SERIAL_NUMBER,null,false);
		addMember(l,"ANDROID_ID",get_ANDROID_ID,null,false);
		addMember(l,"RESULT",get_RESULT,null,false);
		addMember(l,"KEY",get_KEY,null,false);
		addMember(l,"DATA",get_DATA,null,false);
		addMember(l,"SDK_NAME_QQ",get_SDK_NAME_QQ,null,false);
		addMember(l,"SDK_NAME_WX",get_SDK_NAME_WX,null,false);
		addMember(l,"TECENT_TYPE",get_TECENT_TYPE,null,false);
		addMember(l,"OPENID",get_OPENID,null,false);
		addMember(l,"OPENKEY",get_OPENKEY,null,false);
		addMember(l,"PF",get_PF,null,false);
		addMember(l,"PFKEY",get_PFKEY,null,false);
		addMember(l,"PAY_TOKEN",get_PAY_TOKEN,null,false);
		addMember(l,"EXTERN_FUNCTION_KEY",get_EXTERN_FUNCTION_KEY,null,false);
		addMember(l,"EXTERN_FUNCTION_VALUE",get_EXTERN_FUNCTION_VALUE,null,false);
		addMember(l,"EXTERN_FUNCTION_VALUE_2",get_EXTERN_FUNCTION_VALUE_2,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(SDKAttName));
	}
}
