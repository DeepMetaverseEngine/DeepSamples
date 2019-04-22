using UnityEngine;
namespace Slate.ActionClips{

    [Name("TL Audio Clip")]
    public class TLPlayAudio : PlayAudio
    {
        public string AudioName;

        public delegate float OnSoundVolume(float volume);
        public static OnSoundVolume OnSoundVolumeHandle;
        private AudioTrack track
        {
            get { return (AudioTrack)parent; }
        }
        public override float length
        {
            get { return _length; }
            set { _length = value; }
        }

        public override float blendIn
        {
            get { return _blendIn; }
            set { _blendIn = value; }
        }

        public override float blendOut
        {
            get { return _blendOut; }
            set { _blendOut = value; }
        }
        private AudioSource source
        {
            get { return track.source; }
        }

        protected override void OnUpdate(float time, float previousTime)
        {
            if (source != null)
            {
                var weight = Easing.Ease(EaseType.QuadraticInOut, 0, 1, GetClipWeight(time));
                var totalVolume = weight * volume * track.weight;

                if (OnSoundVolumeHandle != null)
                {
                    totalVolume = Mathf.Min(totalVolume, OnSoundVolumeHandle(totalVolume));
                }

                AudioSampler.Sample(source, audioClip, time - clipOffset, previousTime - clipOffset, totalVolume, track.ignoreTimeScale);
                source.panStereo = Mathf.Clamp01(stereoPan + track.stereoPan);

                if (!string.IsNullOrEmpty(subtitlesText))
                {
                    var lerpColor = subtitlesColor;
                    lerpColor.a = weight;
                    DirectorGUI.UpdateSubtitles(string.Format("{0}{1}", parent is ActorAudioTrack ? (actor.name + ": ") : "", subtitlesText), lerpColor);
                }
            }
        }
    }
}