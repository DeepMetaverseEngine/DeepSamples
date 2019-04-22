using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_Playables_DataStreamType : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAnimation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DataStreamType.Animation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Animation(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DataStreamType.Animation);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getAudio(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DataStreamType.Audio);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Audio(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DataStreamType.Audio);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getTexture(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DataStreamType.Texture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Texture(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DataStreamType.Texture);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.Playables.DataStreamType.None);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_None(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.Playables.DataStreamType.None);
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
		getTypeTable(l,"UnityEngine.Playables.DataStreamType");
		addMember(l,"Animation",getAnimation,null,false);
		addMember(l,"_Animation",get_Animation,null,false);
		addMember(l,"Audio",getAudio,null,false);
		addMember(l,"_Audio",get_Audio,null,false);
		addMember(l,"Texture",getTexture,null,false);
		addMember(l,"_Texture",get_Texture,null,false);
		addMember(l,"None",getNone,null,false);
		addMember(l,"_None",get_None,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.Playables.DataStreamType));
	}
}
