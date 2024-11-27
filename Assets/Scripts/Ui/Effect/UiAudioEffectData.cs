using UnityEngine;
using UnityEngine.Audio;

namespace Ui.Effect
{
    [CreateAssetMenu(fileName = "New Effect Data", menuName = "Core/Ui/EffectData")]
    public class UiAudioEffectData : ScriptableObject
    {
        [SerializeField] private AudioClip hoverSound, clickSound;
        [SerializeField] private AudioMixerGroup mixer;
        [SerializeField, Range(0,1)] private float volume;

        public AudioClip HoverSound => hoverSound;
        public AudioClip ClickSound => clickSound;

        public AudioMixerGroup Mixer => mixer;

        public float Volume => volume;
    }
}
