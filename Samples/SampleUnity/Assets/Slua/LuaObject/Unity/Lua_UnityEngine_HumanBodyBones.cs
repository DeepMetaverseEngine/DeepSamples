using System;
using SLua;
using System.Collections.Generic;
[UnityEngine.Scripting.Preserve]
public class Lua_UnityEngine_HumanBodyBones : LuaObject {
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHips(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Hips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Hips(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Hips);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftUpperLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftUpperLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftUpperLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftUpperLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightUpperLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightUpperLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightUpperLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightUpperLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftLowerLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftLowerLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftLowerLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftLowerLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightLowerLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightLowerLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightLowerLeg(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightLowerLeg);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightFoot(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightFoot);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getSpine(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Spine);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Spine(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Spine);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getChest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Chest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Chest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Chest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getNeck(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Neck);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Neck(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Neck);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getHead(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Head);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Head(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Head);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftShoulder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftShoulder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftShoulder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftShoulder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightShoulder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightShoulder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightShoulder(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightShoulder);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftUpperArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftUpperArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightUpperArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightUpperArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightUpperArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftLowerArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftLowerArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftLowerArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftLowerArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightLowerArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightLowerArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightLowerArm(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightLowerArm);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightHand(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightHand);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftToes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftToes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftToes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftToes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightToes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightToes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightToes(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightToes);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftEye(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftEye(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightEye(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightEye(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightEye);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getJaw(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.Jaw);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_Jaw(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.Jaw);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftThumbProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftThumbProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftThumbProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftThumbProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftThumbIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftThumbIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftThumbIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftThumbIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftThumbDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftThumbDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftThumbDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftThumbDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftIndexProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftIndexProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftIndexProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftIndexProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftIndexIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftIndexIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftIndexIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftIndexIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftIndexDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftIndexDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftIndexDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftIndexDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftMiddleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftMiddleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftMiddleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftMiddleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftMiddleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftMiddleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftMiddleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftMiddleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftMiddleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftMiddleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftMiddleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftMiddleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftRingProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftRingProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftRingProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftRingProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftRingIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftRingIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftRingIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftRingIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftRingDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftRingDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftRingDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftRingDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftLittleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftLittleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftLittleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftLittleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftLittleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftLittleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftLittleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftLittleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLeftLittleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LeftLittleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LeftLittleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LeftLittleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightThumbProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightThumbProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightThumbProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightThumbProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightThumbIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightThumbIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightThumbIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightThumbIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightThumbDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightThumbDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightThumbDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightThumbDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightIndexProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightIndexProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightIndexProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightIndexProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightIndexIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightIndexIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightIndexIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightIndexIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightIndexDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightIndexDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightIndexDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightIndexDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightMiddleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightMiddleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightMiddleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightMiddleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightMiddleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightMiddleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightMiddleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightMiddleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightMiddleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightMiddleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightMiddleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightMiddleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightRingProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightRingProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightRingProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightRingProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightRingIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightRingIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightRingIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightRingIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightRingDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightRingDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightRingDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightRingDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightLittleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightLittleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightLittleProximal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightLittleProximal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightLittleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightLittleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightLittleIntermediate(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightLittleIntermediate);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getRightLittleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.RightLittleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_RightLittleDistal(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.RightLittleDistal);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getUpperChest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.UpperChest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_UpperChest(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.UpperChest);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int getLastBone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,UnityEngine.HumanBodyBones.LastBone);
			return 2;
		}
		catch(Exception e) {
			return error(l,e);
		}
	}
	[SLua.MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	[UnityEngine.Scripting.Preserve]
	static public int get_LastBone(IntPtr l) {
		try {
			pushValue(l,true);
			pushValue(l,(double)UnityEngine.HumanBodyBones.LastBone);
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
		getTypeTable(l,"UnityEngine.HumanBodyBones");
		addMember(l,"Hips",getHips,null,false);
		addMember(l,"_Hips",get_Hips,null,false);
		addMember(l,"LeftUpperLeg",getLeftUpperLeg,null,false);
		addMember(l,"_LeftUpperLeg",get_LeftUpperLeg,null,false);
		addMember(l,"RightUpperLeg",getRightUpperLeg,null,false);
		addMember(l,"_RightUpperLeg",get_RightUpperLeg,null,false);
		addMember(l,"LeftLowerLeg",getLeftLowerLeg,null,false);
		addMember(l,"_LeftLowerLeg",get_LeftLowerLeg,null,false);
		addMember(l,"RightLowerLeg",getRightLowerLeg,null,false);
		addMember(l,"_RightLowerLeg",get_RightLowerLeg,null,false);
		addMember(l,"LeftFoot",getLeftFoot,null,false);
		addMember(l,"_LeftFoot",get_LeftFoot,null,false);
		addMember(l,"RightFoot",getRightFoot,null,false);
		addMember(l,"_RightFoot",get_RightFoot,null,false);
		addMember(l,"Spine",getSpine,null,false);
		addMember(l,"_Spine",get_Spine,null,false);
		addMember(l,"Chest",getChest,null,false);
		addMember(l,"_Chest",get_Chest,null,false);
		addMember(l,"Neck",getNeck,null,false);
		addMember(l,"_Neck",get_Neck,null,false);
		addMember(l,"Head",getHead,null,false);
		addMember(l,"_Head",get_Head,null,false);
		addMember(l,"LeftShoulder",getLeftShoulder,null,false);
		addMember(l,"_LeftShoulder",get_LeftShoulder,null,false);
		addMember(l,"RightShoulder",getRightShoulder,null,false);
		addMember(l,"_RightShoulder",get_RightShoulder,null,false);
		addMember(l,"LeftUpperArm",getLeftUpperArm,null,false);
		addMember(l,"_LeftUpperArm",get_LeftUpperArm,null,false);
		addMember(l,"RightUpperArm",getRightUpperArm,null,false);
		addMember(l,"_RightUpperArm",get_RightUpperArm,null,false);
		addMember(l,"LeftLowerArm",getLeftLowerArm,null,false);
		addMember(l,"_LeftLowerArm",get_LeftLowerArm,null,false);
		addMember(l,"RightLowerArm",getRightLowerArm,null,false);
		addMember(l,"_RightLowerArm",get_RightLowerArm,null,false);
		addMember(l,"LeftHand",getLeftHand,null,false);
		addMember(l,"_LeftHand",get_LeftHand,null,false);
		addMember(l,"RightHand",getRightHand,null,false);
		addMember(l,"_RightHand",get_RightHand,null,false);
		addMember(l,"LeftToes",getLeftToes,null,false);
		addMember(l,"_LeftToes",get_LeftToes,null,false);
		addMember(l,"RightToes",getRightToes,null,false);
		addMember(l,"_RightToes",get_RightToes,null,false);
		addMember(l,"LeftEye",getLeftEye,null,false);
		addMember(l,"_LeftEye",get_LeftEye,null,false);
		addMember(l,"RightEye",getRightEye,null,false);
		addMember(l,"_RightEye",get_RightEye,null,false);
		addMember(l,"Jaw",getJaw,null,false);
		addMember(l,"_Jaw",get_Jaw,null,false);
		addMember(l,"LeftThumbProximal",getLeftThumbProximal,null,false);
		addMember(l,"_LeftThumbProximal",get_LeftThumbProximal,null,false);
		addMember(l,"LeftThumbIntermediate",getLeftThumbIntermediate,null,false);
		addMember(l,"_LeftThumbIntermediate",get_LeftThumbIntermediate,null,false);
		addMember(l,"LeftThumbDistal",getLeftThumbDistal,null,false);
		addMember(l,"_LeftThumbDistal",get_LeftThumbDistal,null,false);
		addMember(l,"LeftIndexProximal",getLeftIndexProximal,null,false);
		addMember(l,"_LeftIndexProximal",get_LeftIndexProximal,null,false);
		addMember(l,"LeftIndexIntermediate",getLeftIndexIntermediate,null,false);
		addMember(l,"_LeftIndexIntermediate",get_LeftIndexIntermediate,null,false);
		addMember(l,"LeftIndexDistal",getLeftIndexDistal,null,false);
		addMember(l,"_LeftIndexDistal",get_LeftIndexDistal,null,false);
		addMember(l,"LeftMiddleProximal",getLeftMiddleProximal,null,false);
		addMember(l,"_LeftMiddleProximal",get_LeftMiddleProximal,null,false);
		addMember(l,"LeftMiddleIntermediate",getLeftMiddleIntermediate,null,false);
		addMember(l,"_LeftMiddleIntermediate",get_LeftMiddleIntermediate,null,false);
		addMember(l,"LeftMiddleDistal",getLeftMiddleDistal,null,false);
		addMember(l,"_LeftMiddleDistal",get_LeftMiddleDistal,null,false);
		addMember(l,"LeftRingProximal",getLeftRingProximal,null,false);
		addMember(l,"_LeftRingProximal",get_LeftRingProximal,null,false);
		addMember(l,"LeftRingIntermediate",getLeftRingIntermediate,null,false);
		addMember(l,"_LeftRingIntermediate",get_LeftRingIntermediate,null,false);
		addMember(l,"LeftRingDistal",getLeftRingDistal,null,false);
		addMember(l,"_LeftRingDistal",get_LeftRingDistal,null,false);
		addMember(l,"LeftLittleProximal",getLeftLittleProximal,null,false);
		addMember(l,"_LeftLittleProximal",get_LeftLittleProximal,null,false);
		addMember(l,"LeftLittleIntermediate",getLeftLittleIntermediate,null,false);
		addMember(l,"_LeftLittleIntermediate",get_LeftLittleIntermediate,null,false);
		addMember(l,"LeftLittleDistal",getLeftLittleDistal,null,false);
		addMember(l,"_LeftLittleDistal",get_LeftLittleDistal,null,false);
		addMember(l,"RightThumbProximal",getRightThumbProximal,null,false);
		addMember(l,"_RightThumbProximal",get_RightThumbProximal,null,false);
		addMember(l,"RightThumbIntermediate",getRightThumbIntermediate,null,false);
		addMember(l,"_RightThumbIntermediate",get_RightThumbIntermediate,null,false);
		addMember(l,"RightThumbDistal",getRightThumbDistal,null,false);
		addMember(l,"_RightThumbDistal",get_RightThumbDistal,null,false);
		addMember(l,"RightIndexProximal",getRightIndexProximal,null,false);
		addMember(l,"_RightIndexProximal",get_RightIndexProximal,null,false);
		addMember(l,"RightIndexIntermediate",getRightIndexIntermediate,null,false);
		addMember(l,"_RightIndexIntermediate",get_RightIndexIntermediate,null,false);
		addMember(l,"RightIndexDistal",getRightIndexDistal,null,false);
		addMember(l,"_RightIndexDistal",get_RightIndexDistal,null,false);
		addMember(l,"RightMiddleProximal",getRightMiddleProximal,null,false);
		addMember(l,"_RightMiddleProximal",get_RightMiddleProximal,null,false);
		addMember(l,"RightMiddleIntermediate",getRightMiddleIntermediate,null,false);
		addMember(l,"_RightMiddleIntermediate",get_RightMiddleIntermediate,null,false);
		addMember(l,"RightMiddleDistal",getRightMiddleDistal,null,false);
		addMember(l,"_RightMiddleDistal",get_RightMiddleDistal,null,false);
		addMember(l,"RightRingProximal",getRightRingProximal,null,false);
		addMember(l,"_RightRingProximal",get_RightRingProximal,null,false);
		addMember(l,"RightRingIntermediate",getRightRingIntermediate,null,false);
		addMember(l,"_RightRingIntermediate",get_RightRingIntermediate,null,false);
		addMember(l,"RightRingDistal",getRightRingDistal,null,false);
		addMember(l,"_RightRingDistal",get_RightRingDistal,null,false);
		addMember(l,"RightLittleProximal",getRightLittleProximal,null,false);
		addMember(l,"_RightLittleProximal",get_RightLittleProximal,null,false);
		addMember(l,"RightLittleIntermediate",getRightLittleIntermediate,null,false);
		addMember(l,"_RightLittleIntermediate",get_RightLittleIntermediate,null,false);
		addMember(l,"RightLittleDistal",getRightLittleDistal,null,false);
		addMember(l,"_RightLittleDistal",get_RightLittleDistal,null,false);
		addMember(l,"UpperChest",getUpperChest,null,false);
		addMember(l,"_UpperChest",get_UpperChest,null,false);
		addMember(l,"LastBone",getLastBone,null,false);
		addMember(l,"_LastBone",get_LastBone,null,false);
		addMember(l,op_Equality);
		createTypeMetatable(l,null, typeof(UnityEngine.HumanBodyBones));
	}
}
