
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AndroidInterfaces : MonoBehaviour {

#if UNITY_ANDROID
	private static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
	private static AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public static void callOnGetAchievement(ArrayList achievements){
#if UNITY_ANDROID
		string[]	paramList = new string[achievements.Count];
		achievements.CopyTo(paramList);
		activity.Call("onGetAchievement", paramList);
#endif
	}
	
	public static void callOnGetAchievement(string achievementID){
#if UNITY_ANDROID
		string[]	paramList = new string[1];
		paramList[0] = achievementID;
		activity.Call("onGetAchievement", achievementID);
#endif
	}
	
	public static void callOnUpdateScore(int score){
#if UNITY_ANDROID
		activity.Call("onUpdateScore", score);
#endif
	}
	
	public static void callPostFreeMoneyNotification(int freeMoneyInterval) {
		Debugger.Log("PostFreeMoneyNotification: "+freeMoneyInterval);
#if UNITY_ANDROID
		activity.Call("PostFreeMoneyNotification", freeMoneyInterval);
#endif
}
	
	public static void callLogEvent(string eventId) {
#if UNITY_ANDROID
		activity.Call("logEvent", eventId);
#endif
	}
	
	public static void callLogEvent(string eventId, bool timed) {
#if UNITY_ANDROID
		activity.Call("logEvent", eventId, timed);
#endif
	}
	
	public static void callLogEvent(string eventId, Dictionary<string, string> parameters) {
#if UNITY_ANDROID
		using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap")){
	        // Call 'put' via the JNI instead of using helper classes to avoid:
	        //  "JNI: Init'd AndroidJavaObject with null ptr!"
	        IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");       
	        object[] args = new object[2];
	
	        foreach(KeyValuePair<string, string> kvp in parameters)
	        {
	            using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
	            {
	                using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
	                {
	                    args[0] = k;
	                    args[1] = v;
	                    AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(),method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
	                }           
				}
	        }
			activity.Call("logEvent", eventId, obj_HashMap);
		}
#endif
	}
	
	public static void callLogEvent(string eventId, IDictionary<string, string> parameters, bool timed) {
#if UNITY_ANDROID
		using(AndroidJavaObject obj_HashMap = new AndroidJavaObject("java.util.HashMap")){
	        // Call 'put' via the JNI instead of using helper classes to avoid:
	        //  "JNI: Init'd AndroidJavaObject with null ptr!"
	        IntPtr method_Put = AndroidJNIHelper.GetMethodID(obj_HashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");       
	        object[] args = new object[2];
	
	        foreach(KeyValuePair<string, string> kvp in parameters)
	        {
	            using(AndroidJavaObject k = new AndroidJavaObject("java.lang.String", kvp.Key))
	            {
	                using(AndroidJavaObject v = new AndroidJavaObject("java.lang.String", kvp.Value))
	                {
	                    args[0] = k;
	                    args[1] = v;
	                    AndroidJNI.CallObjectMethod(obj_HashMap.GetRawObject(),method_Put, AndroidJNIHelper.CreateJNIArgArray(args));
	                }           
				}
	        }
			activity.Call("logEvent", eventId, obj_HashMap, timed);
		}
#endif
	}
	
	public static void callCheckNetworkStatus(string gameObject, string callBackMethod, string callBackParam) {
#if UNITY_ANDROID
		activity.Call("checkNetWorkStatus", gameObject, callBackMethod, callBackParam);
#endif
	}
	
	public static void callQuitGame() {
#if UNITY_ANDROID
		activity.Call("quitGame");
#endif
	}
	
	public static void callLogin(string asGuest) {
#if UNITY_ANDROID
		activity.Call("callLogin", asGuest);
#endif
	}
	
	public static void callUpdateMoney() {
#if UNITY_ANDROID
		activity.Call("callUpdateMoney");
#endif		
	}
	
	public static void callBuyItem(string itemId) {
#if UNITY_ANDROID
		activity.Call("buyItem", itemId);
#endif
	}
	
	public static void callGetAccount() {
#if UNITY_ANDROID
		activity.Call("GetAccount");
#endif
	}
	
	public static void callShowCasinoHomePage() {
#if UNITY_ANDROID
		activity.Call("showCasinoHomePage");
#endif
	}
}