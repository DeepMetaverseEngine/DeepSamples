using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public delegate int PlaySoundHandler(string bundleName, Transform parent, float volume, bool loop = false);

    public delegate int Play3DSoundHadnler(string bundleName,Transform parent, float volume,
        float mindistance, float maxdistance, bool loop = false);
    public delegate void StopSoundHandler(int id);
    public static PlaySoundHandler OnSoundEffectHandler;
    public static Play3DSoundHadnler On3DSoundEffectHandler;
    public static StopSoundHandler OnStopSoundHandler;
    public delegate void PlayBGMHandler(string bundleName, float volume);
    public static PlayBGMHandler OnSoundBGMHandler;
    public string SoundEffectRes;
    public string SoundBGMRes;
    public float EffectVol = 1;
    public float BGMVol = 1;
    public bool IsLoop = true;
    public float Delay = 0;
    public float MinDistance = 0;
    public float MaxDistance = 0;

    private int soundid = 0;

    public bool PlayWithEnable = false;
    // Use this for initialization
    void Start() {
        
        if (!PlayWithEnable)
        {
            Play();
        }
       
    }

    private void Play()
    {
        if (Delay != 0)
        {
            Invoke("PlaySound", Delay);
        }
        else
        {
            PlaySound();
        }
    }
    
    private void Stop()
    {
        if (soundid != 0)
        {
            if (OnStopSoundHandler != null)
            {
                OnStopSoundHandler.Invoke(soundid);
            }
        }
    }

    void OnEnable()
    {
        if (PlayWithEnable)
        {
            Play();
        }
    }

    private void OnDisable()
    {
        if (PlayWithEnable)
        {
            Stop();
        }
    }

    void OnDestroy()
    {
        if (!PlayWithEnable)
        {
            Stop();
        }
    }
    private void PlaySound()
    {
        if (!string.IsNullOrEmpty(SoundEffectRes))
        {
            if (MinDistance == 0 && MaxDistance == 0)
            {
                if (OnSoundEffectHandler != null)
                {
                    soundid = OnSoundEffectHandler.Invoke(SoundEffectRes, null, EffectVol, IsLoop);
                }
            }
            else
            {
                if (On3DSoundEffectHandler != null)
                {
                    soundid = On3DSoundEffectHandler.Invoke(SoundEffectRes, transform, EffectVol, MinDistance,
                        MaxDistance,
                        IsLoop);
                }
            }
        }

        if (OnSoundBGMHandler != null)
        {
            if (!string.IsNullOrEmpty(SoundBGMRes))
            {
                OnSoundBGMHandler.Invoke(SoundBGMRes, BGMVol);
            }
        }
    }
}
