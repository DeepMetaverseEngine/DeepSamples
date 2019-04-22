using UnityEngine;
using System.Collections;

public class SoundUtility : MonoBehaviour
{
    private static SoundUtility mInstance;

    private AudioListener mListener;
        
    
    public static SoundUtility GetInstance()
    {
        return mInstance;
    }

    void Awake()
    {
        mInstance = this;
        mListener = this.gameObject.GetComponent<AudioListener>();
        if (mListener == null)
        {
            mListener = this.gameObject.AddComponent<AudioListener>();
        }
        mListener.transform.position = Vector3.zero;
    }
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (mListener != null)
        {
            if (GameSceneMgr.Instance != null && GameSceneMgr.Instance.SceneCamera != null)
            {
                mListener.gameObject.transform.position = GameSceneMgr.Instance.SceneCamera.transform.position;
            }
            else
            {
                mListener.gameObject.transform.position = Vector3.zero;
            }
        }
    }
    void OnDestroy()
    {
        mInstance = null;
        mListener = null;
    }

    public static IEnumerator Clear()
    {
        AudioSource[] srcs = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));

        foreach (AudioSource e in srcs)
        {
            AudioSource s = (AudioSource)e;
            s.Stop();
            s.clip = null;
        }

        yield return 1;
    }

    public static IEnumerator Reload()
    {
        AudioListener[] listen = (AudioListener[])GameObject.FindObjectsOfType(typeof(AudioListener));

        foreach (AudioListener e in listen)
        {
            AudioListener al = e;
            DeepCore.Unity3D.UnityHelper.Destroy(al);
            al = null;

            yield return 1;

        }


    }

    public static void SetListener(AudioListener l)
    {
        if(mInstance != null)
            mInstance.mListener = l;
    }

    public static AudioSource Get2DAudioSource(string name)
    {
        GameObject obj = new GameObject(name);
        AudioSource source = obj.AddComponent<AudioSource>();
        obj.transform.parent = mInstance.gameObject.transform;
        obj.transform.localPosition = Vector3.zero;
        return source;
    }

    public static Vector3 GetListenerVector3()
    {
        if (mInstance.mListener != null)
            return mInstance.mListener.gameObject.transform.position;
        else
            return Vector3.zero;
    }

    /// <summary>
    /// 音效开关.
    /// </summary>
    /// <param name="sound"></param>
    public static void Enable(bool sound)
    {
        if (mInstance.mListener != null)
            mInstance.mListener.enabled = sound;
    }
   
    public AudioListener GetListener()
    {
        return mListener;
    }
}
