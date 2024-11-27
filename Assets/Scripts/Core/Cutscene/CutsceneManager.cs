using System;
using Cysharp.Threading.Tasks;
using GameSystem;
using R3;
using UnityEngine;
using UnityEngine.Playables;

namespace Core.Cutscene
{
    public class CutsceneManager : CoreBehavior
    {
        private static CutsceneManager instance;
        private readonly object lockObject = new();
        private PlayableDirector currentCutscene;
        private IDisposable cutsceneChangeSubscription;
        public static event Action<bool> onCutsceneChanged;
        private bool isOtherCamera;

        private void Awake()
        {
            if (!instance)
            {
                lock (lockObject)
                {
                    if (!instance) instance = this;
                }
            }
            
            currentCutscene = null;

            cutsceneChangeSubscription = OnCuteSceneChange();
        }

        private void OnDestroy()
        {
            cutsceneChangeSubscription?.Dispose();
        }

        public static void PlayCutscene(PlayableDirector director, bool isOtherCamera = true)
        {
            if (!instance || !director) 
                return;

            if (instance.currentCutscene &&
                instance.currentCutscene.state == PlayState.Playing &&
                director == instance.currentCutscene)
                return;
            
            if (instance.currentCutscene && 
                instance.currentCutscene.state == PlayState.Playing)
            {
                instance.currentCutscene.Stop();
                instance.currentCutscene = null;
            }
            
            instance.currentCutscene = director;
            instance.isOtherCamera = isOtherCamera;
        }
        
        private IDisposable OnCuteSceneChange()
        {
            return Observable.EveryValueChanged(this, manager => manager.currentCutscene).Subscribe(_ =>
            {
                OnCutScene();
            });
        }

        private async void OnCutScene()
        {
            if (!currentCutscene) return;
            if (isOtherCamera) onCutsceneChanged?.Invoke(true);
            currentCutscene.Play();
            await UniTask.WaitForSeconds((float)currentCutscene.duration);
            currentCutscene.Stop();
            await UniTask.WaitForEndOfFrame(this);
            currentCutscene = null;
            if (isOtherCamera) onCutsceneChanged?.Invoke(false);
        }
    }
}
