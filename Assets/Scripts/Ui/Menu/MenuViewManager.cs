using System;
using GameSystem;
using Michsky.UI.Heat;
using Ui.Assessment;
using Ui.Tutorial;
using UnityEngine;
using Utilities;

namespace Ui.Menu
{
    public class MenuViewManager : UiBehavior
    {
        [SerializeField] private PanelButton startButton, settingButton, exitButton;
        [SerializeField] private CanvasGroup menuCanvasGroup;
        public static event Action startButtonPressed;
        public static event Action gameInitialized, backButtonPressed;

        private void Awake()
        {
            startButton.onClick.AddListener(StartButton);
            settingButton.onClick.AddListener(SettingButton);
            exitButton.onClick.AddListener(ExitButton);
            
            TutorialViewManager.tutorialBackButtonPressed += TutorialViewManagerOnTutorialBackButtonPressed;
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
            settingButton.onClick.RemoveAllListeners();
            exitButton.onClick.RemoveAllListeners();
            
            TutorialViewManager.tutorialBackButtonPressed -= TutorialViewManagerOnTutorialBackButtonPressed;
        }

        private void Start()
        {
            
        }

        private void StartButton()
        {
            menuCanvasGroup.SetActive(false);
            startButtonPressed?.Invoke();
            gameInitialized?.Invoke();
        }

        private void SettingButton()
        {
        }

        private void ExitButton()
        {
            Application.Quit();            
        }
        
        private void TutorialViewManagerOnTutorialBackButtonPressed()
        {
            menuCanvasGroup.SetActive(true);
        }
    }
}