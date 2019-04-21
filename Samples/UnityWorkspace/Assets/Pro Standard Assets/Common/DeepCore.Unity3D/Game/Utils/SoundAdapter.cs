using UnityEngine;

namespace DeepCore.Unity3D.Utils
{
    public abstract class Audio { }
    public abstract class SoundAdapter
    {
        public abstract void PlayBGM(string name);

        public abstract void PlaySound(string name, Vector3 pos, float distance = 20f);
        public abstract void PlaySound(string name, int timeMS, Vector3 pos, float distance = 20f);
        public abstract Audio PlaySoundLoop(string name, Vector3 pos, float distance = 20f);

        public abstract void PlaySound(string name, GameObject obj, float distance = 20f);
        public abstract void PlaySound(string name, int timeMS, GameObject obj, float distance = 20f);
        public abstract Audio PlaySoundLoop(string name, GameObject obj, float distance = 20f);
        
        public abstract void StopSoundLoop(Audio sound);
        public abstract void StopSound(Audio sound);
        public abstract void StopBGM();

        public abstract void PauseBGM();
        public abstract void UnPauseBGM();

        public abstract void PauseSound(Audio sound);
        public abstract void UnPauseSound(Audio sound);
        public abstract void Clear();
    }

}