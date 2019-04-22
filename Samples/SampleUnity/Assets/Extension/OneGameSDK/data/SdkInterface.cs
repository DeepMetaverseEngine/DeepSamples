using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SdkInterface :
#if UNITY_ANDROID && !UNITY_EDITOR
	SDKInterfaceAndroid
#elif UNITY_IOS && !UNITY_EDITOR
	SDKInterfaceIOS
#else
    SDKInterfaceWin
#endif
{
}