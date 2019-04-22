using UnityEngine;
using System.Collections;
using TL.UGUI.Skill;
using UnityEngine.UI;

public class SkillBarEffect : MonoBehaviour 
{

    public GameObject cdEffect;
    public uTools.uTweenValue cdEvent;
    //public uTools.uTweenRotation lightratation;
    //public uTools.uTweenAlpha lightAlpha;
    //public uTools.uTweenScale lightScale;
    //public uTools.uTweenScale circleScale;
    //public uTools.uTweenAlpha circleAlpha;

    public GameObject clickEffect;
    public uTools.uTweenValue clickEvent;
    //public uTools.uTweenScale clickScale;
    //public uTools.uTweenAlpha clickAlpha;
    //public uTools.uTweenPositionX clickEvent;
    private SkillButton sk;

    void Start()
    {
        sk= this.transform.parent.GetComponent<SkillButton>();
    }
    
    public void PlayCDEffect()
    {
        if (cdEffect != null)
        {
            cdEffect.SetActive(true);
            cdEvent.Replay();
            //lightAlpha.Replay();
            //lightScale.Replay();
            //circleScale.Replay();
            //circleAlpha.Replay();
        }
        if (sk!=null && sk.miracleEffect != null)
            sk.miracleEffect.SetActive(true);
    }

    public void PlayClickEffect()
    {
        if (clickEffect != null)
        {
            clickEffect.SetActive(false);
            clickEffect.SetActive(true);
            clickEvent.Replay();
            //clickAlpha.Replay();
            //clickEvent.Replay();
        }
    }

}
