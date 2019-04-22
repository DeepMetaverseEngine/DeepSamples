using DeepCore.Unity3D;
using UnityEngine;

public class TLPlaySound : MonoBehaviour {
    public bool CanPlay { get; set; }
    public void PlaySound(string path)
    {
        //Debug.Log("path=" + path);
        if (CanPlay)
        {
           SoundManager.Instance.PlaySoundByKey(path, false);
        }
    }
}


public class DummyPlaySound : MonoBehaviour
{
    public void PlaySound(string path)
    { 
    }
}

