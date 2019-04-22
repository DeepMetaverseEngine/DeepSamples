using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum eScreenEffectState
{
    none = 0,
    lowHp = 1,
    beAttacked = 2,
}

public class FullScreenEffect : MonoBehaviour {
    [HideInInspector]
    public static FullScreenEffect Instance;

    public Image sceneEffect;
    public Image characterEffect;

    public Sprite telescope;

    public RippleEffect rippleEffect;

    public eScreenEffectState LastState;
    // Use this for initialization
    void Start () {
        Instance = this;
	}
	
    void OnDestroy()
    {
        Instance = null;
    }
    
    public void ShowLowHPEffect()
    {
        if (!characterEffect.gameObject.activeSelf)
            characterEffect.gameObject.SetActive(true);
    }

    public void HideLowHPEffect()
    {
        LastState = eScreenEffectState.none;
        if (characterEffect.gameObject.activeSelf)
            characterEffect.gameObject.SetActive(false);
    }

    public void ShowTelescope()
    {
        sceneEffect.sprite = telescope;
        sceneEffect.gameObject.SetActive(true);
    }

    public void HideSceneEffect()
    {
        sceneEffect.gameObject.SetActive(false);
    }

    public void ShowRippleEffect()
    {
        rippleEffect.Emit();
    }

//     void OnGUI()
//     {
//         if (GUI.Button(new Rect(0, 0, 50, 50), "aaa"))
//         {
//             ShowTelescope();
//         }
//         if (GUI.Button(new Rect(55, 0, 50, 50), "bbb"))
//         {
//             HideSceneEffect();
//         }
}
