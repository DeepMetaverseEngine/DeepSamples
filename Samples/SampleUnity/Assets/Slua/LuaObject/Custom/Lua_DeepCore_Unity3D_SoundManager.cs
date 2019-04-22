using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_DeepCore_Unity3D_SoundManager : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int constructor(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager o;
			o=new DeepCore.Unity3D.SoundManager();
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
	static public int PlayBGM(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==2){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				self.PlayBGM(a1);
				pushValue(l,true);
				return 1;
			}
			else if(argc==3){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				self.PlayBGM(a1,a2);
				pushValue(l,true);
				return 1;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function PlayBGM to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PushBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.PushBGM(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PopBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.PopBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int GetCurrentBGMBundleName(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			var ret=self.GetCurrentBGMBundleName();
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
	static public int SetCurrentBGMVol(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.SetCurrentBGMVol(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			self.ChangeBGM(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PauseBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.PauseBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResumeBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.ResumeBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopBGM(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.StopBGM();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int AddSoundKey(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.AddSoundKey(a1,a2);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PlaySoundByKey(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.String a1;
			checkType(l,2,out a1);
			System.Boolean a2;
			checkType(l,3,out a2);
			var ret=self.PlaySoundByKey(a1,a2);
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
	static public int PlaySound(IntPtr l) {
		try {
			int argc = LuaDLL.lua_gettop(l);
			if(argc==3){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Boolean a2;
				checkType(l,3,out a2);
				var ret=self.PlaySound(a1,a2);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Vector3),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Vector3 a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				var ret=self.PlaySound(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Transform),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Transform a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				var ret=self.PlaySound(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Boolean a3;
				checkType(l,4,out a3);
				var ret=self.PlaySound(a1,a2,a3);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Transform),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Transform a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.PlaySound(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.PlaySound(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(UnityEngine.Vector3),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				UnityEngine.Vector3 a2;
				checkType(l,3,out a2);
				System.Single a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.PlaySound(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(UnityEngine.Transform),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				UnityEngine.Transform a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.PlaySound(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(UnityEngine.Vector3),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				UnityEngine.Vector3 a3;
				checkType(l,4,out a3);
				System.Boolean a4;
				checkType(l,5,out a4);
				var ret=self.PlaySound(a1,a2,a3,a4);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(UnityEngine.Transform),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				UnityEngine.Transform a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				System.Boolean a5;
				checkType(l,6,out a5);
				var ret=self.PlaySound(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(matchType(l,argc,2,typeof(string),typeof(float),typeof(UnityEngine.Vector3),typeof(float),typeof(bool))){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				UnityEngine.Vector3 a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				System.Boolean a5;
				checkType(l,6,out a5);
				var ret=self.PlaySound(a1,a2,a3,a4,a5);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			else if(argc==9){
				DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
				System.String a1;
				checkType(l,2,out a1);
				System.Single a2;
				checkType(l,3,out a2);
				UnityEngine.Transform a3;
				checkType(l,4,out a3);
				System.Single a4;
				checkType(l,5,out a4);
				System.Single a5;
				checkType(l,6,out a5);
				System.Single a6;
				checkType(l,7,out a6);
				System.Boolean a7;
				checkType(l,8,out a7);
				DeepCore.Unity3D.AssetAudio.AudioType a8;
				checkEnum(l,9,out a8);
				var ret=self.PlaySound(a1,a2,a3,a4,a5,a6,a7,a8);
				pushValue(l,true);
				pushValue(l,ret);
				return 2;
			}
			pushValue(l,false);
			LuaDLL.lua_pushstring(l,"No matched override function PlaySound to call");
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopSound(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.StopSound(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int PauseSound(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.PauseSound(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ResumeSound(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			self.ResumeSound(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Pause(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.Pause();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int Resume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.Resume();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int StopAllSound(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			self.StopAllSound();
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int SetSoundVolume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Single a1;
			checkType(l,2,out a1);
			self.SetSoundVolume(a1);
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int ChangeSoundResource(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			System.Int32 a1;
			checkType(l,2,out a1);
			System.String a2;
			checkType(l,3,out a2);
			self.ChangeSoundResource(a1,a2);
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
	static public int get_Instance(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,DeepCore.Unity3D.SoundManager.Instance);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultBGMVolume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultBGMVolume);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultBGMVolume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.DefaultBGMVolume=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_DefaultSoundVolume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			pushValue(l,true);
			pushValue(l,self.DefaultSoundVolume);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int set_DefaultSoundVolume(IntPtr l) {
		try {
			DeepCore.Unity3D.SoundManager self=(DeepCore.Unity3D.SoundManager)checkSelf(l);
			float v;
			checkType(l,2,out v);
			self.DefaultSoundVolume=v;
			pushValue(l,true);
			return 1;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[UnityEngine.Scripting.Preserve]
	static public void reg(IntPtr l) {
		getTypeTable(l,"SoundManager");
		addMember(l,PlayBGM);
		addMember(l,PushBGM);
		addMember(l,PopBGM);
		addMember(l,GetCurrentBGMBundleName);
		addMember(l,SetCurrentBGMVol);
		addMember(l,ChangeBGM);
		addMember(l,PauseBGM);
		addMember(l,ResumeBGM);
		addMember(l,StopBGM);
		addMember(l,AddSoundKey);
		addMember(l,PlaySoundByKey);
		addMember(l,PlaySound);
		addMember(l,StopSound);
		addMember(l,PauseSound);
		addMember(l,ResumeSound);
		addMember(l,Pause);
		addMember(l,Resume);
		addMember(l,StopAllSound);
		addMember(l,SetSoundVolume);
		addMember(l,ChangeSoundResource);
		addMember(l,"Instance",get_Instance,null,false);
		addMember(l,"DefaultBGMVolume",get_DefaultBGMVolume,set_DefaultBGMVolume,true);
		addMember(l,"DefaultSoundVolume",get_DefaultSoundVolume,set_DefaultSoundVolume,true);
		addMember(l,op_Equality);
		createTypeMetatable(l,constructor, typeof(DeepCore.Unity3D.SoundManager));
	}
}
