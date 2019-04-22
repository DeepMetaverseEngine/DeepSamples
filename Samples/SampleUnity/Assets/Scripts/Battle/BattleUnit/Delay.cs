using UnityEngine;
using System.Collections;

public class Delay : MonoBehaviour {

    public float delayTime = 1.0f;
    private bool mInActive = false;

    private void OnEnable()
    {
        if (!mInActive)
        {
            gameObject.SetActive(false);
            Invoke("Active", delayTime);
        }
    }

    private void OnDisable()
    {
        mInActive = false;
    }

    private void Active()
    {
        mInActive = true;
        gameObject.SetActive(true);
    }
}
