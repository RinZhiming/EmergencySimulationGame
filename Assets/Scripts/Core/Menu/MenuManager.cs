using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Ui.Assessment;
using UnityEngine;
using Utilities;

namespace Core.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] menuMusic;
        private int currentMenuMusic;
        private bool isPlay;

        private void Awake()
        {
            isPlay = false;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            
            AssessmentViewManager.onGameStart += OnGameStart;
        }

        private void OnDestroy()
        {
            AssessmentViewManager.onGameStart -= OnGameStart;
        }

        private void OnGameStart()
        {
            isPlay = true;
            if (audioSource.isPlaying)
            {
                DOVirtual.Float(1, 0, 3f, v =>
                {
                    audioSource.volume = v;
                }).OnComplete(() =>
                {
                    audioSource.Stop();
                    audioSource.clip = null;
                });
            }
        }

        private void Start()
        {
            menuMusic.Shuffle();

            AudioMenu(0);
        }

        private async void AudioMenu(int index)
        {
            if (isPlay)
            {
                Debug.Log("Out");
                return;
            }
            
            if (index >= menuMusic.Length)
            {
                index = 0;
            }
            
            audioSource.clip = menuMusic[index];
            
            audioSource.Play();
            
            await UniTask.WaitForSeconds(menuMusic[index].length + 1f);
            Debug.Log("Next Song");
            index++;
            
            audioSource.Stop();
            AudioMenu(index);
        }
    }
}
