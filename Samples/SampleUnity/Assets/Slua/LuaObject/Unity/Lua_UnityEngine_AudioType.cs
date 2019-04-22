using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_AudioType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUNKNOWN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.UNKNOWN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UNKNOWN(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.UNKNOWN);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getACC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.ACC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_ACC(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.ACC);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAIFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.AIFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AIFF(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.AIFF);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getIT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.IT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_IT(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.IT);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMOD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.MOD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MOD(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.MOD);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getMPEG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.MPEG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_MPEG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.MPEG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getOGGVORBIS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.OGGVORBIS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_OGGVORBIS(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.OGGVORBIS);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getS3M(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.S3M);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_S3M(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.S3M);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getWAV(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.WAV);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_WAV(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.WAV);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.XM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XM(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.XM);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getXMA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.XMA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_XMA(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.XMA);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getVAG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.VAG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_VAG(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.VAG);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAUDIOQUEUE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.AudioType.AUDIOQUEUE);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_AUDIOQUEUE(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.AudioType.AUDIOQUEUE);
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
	static public void reg(IntPtr l) {
		getTypeTable(l,"UnityEngine.AudioType");
		addMember(l,"UNKNOWN",getUNKNOWN,null,false);
		addMember(l,"_UNKNOWN",get_UNKNOWN,null,false);
		addMember(l,"ACC",getACC,null,false);
		addMember(l,"_ACC",get_ACC,null,false);
		addMember(l,"AIFF",getAIFF,null,false);
		addMember(l,"_AIFF",get_AIFF,null,false);
		addMember(l,"IT",getIT,null,false);
		addMember(l,"_IT",get_IT,null,false);
		addMember(l,"MOD",getMOD,null,false);
		addMember(l,"_MOD",get_MOD,null,false);
		addMember(l,"MPEG",getMPEG,null,false);
		addMember(l,"_MPEG",get_MPEG,null,false);
		addMember(l,"OGGVORBIS",getOGGVORBIS,null,false);
		addMember(l,"_OGGVORBIS",get_OGGVORBIS,null,false);
		addMember(l,"S3M",getS3M,null,false);
		addMember(l,"_S3M",get_S3M,null,false);
		addMember(l,"WAV",getWAV,null,false);
		addMember(l,"_WAV",get_WAV,null,false);
		addMember(l,"XM",getXM,null,false);
		addMember(l,"_XM",get_XM,null,false);
		addMember(l,"XMA",getXMA,null,false);
		addMember(l,"_XMA",get_XMA,null,false);
		addMember(l,"VAG",getVAG,null,false);
		addMember(l,"_VAG",get_VAG,null,false);
		addMember(l,"AUDIOQUEUE",getAUDIOQUEUE,null,false);
		addMember(l,"_AUDIOQUEUE",get_AUDIOQUEUE,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.AudioType));
	}
}
