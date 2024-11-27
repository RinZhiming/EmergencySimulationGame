using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Ui.Effect
{
    public class UiAudioEffect : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        private UiAudioEffectData audioData;
        private static AudioSource audioSource;
        
        private void Awake()
        {
            if (!audioData)
            {
                audioData = Resources.Load<UiAudioEffectData>("UiAudioEffect");
            }
            
            if (!audioSource)
            {
                audioSource = new GameObject("Effect Audio").AddComponent<AudioSource>();
                audioSource.loop = false;
                audioSource.playOnAwake = false;
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(audioSource.gameObject);
        }

        private void Update()
        {
            if (!audioSource) return;
            
            audioSource.volume = audioData.Volume;
            audioSource.outputAudioMixerGroup = audioData.Mixer;
        }

        private void OnDestroy()
        {
            if (audioSource) Destroy(audioSource.gameObject);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PlayAudio(audioData.HoverSound);
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayAudio(audioData.ClickSound);
        }

        private void PlayAudio(AudioClip audioClip)
        {
            if (!audioData || !audioClip || !audioSource) return;
            
            audioSource.PlayOneShot(audioClip);
        }
    }
}
