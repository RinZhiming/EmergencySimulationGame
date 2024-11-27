using System;
using System.Collections.Generic;
using System.Linq;
using Michsky.UI.Heat;
using Ui.Assessment;
using Ui.Menu;
using UnityEngine;
using Utilities;

namespace Ui.Tutorial
{
    public class TutorialViewManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup tutorialCanvasGroup;
        [SerializeField] private PanelButton backButton, completeButton;
        [SerializeField] private HorizontalSelector tutorialSelector;
        [SerializeField] private List<TutorialPanel> tutorialPanels = new();
        public static event Action tutorialCompleteButtonPressed, tutorialBackButtonPressed;

        private void Awake()
        {
            MenuViewManager.startButtonPressed += MenuViewManagerOnStartButtonPressed;
            AssessmentViewManager.assessmentBackButtonPressed += AssessmentViewManagerOnAssessmentBackButtonPressed;
            backButton.onClick.AddListener(BackButton);
            completeButton.onClick.AddListener(CompleteButton);
            
            tutorialCanvasGroup.SetActive(false);

            var currentIndex = 0;
            tutorialPanels.Sort();
            tutorialSelector.items.Sort();
            
            foreach (var item in tutorialSelector.items)
            {
                for (var i = currentIndex; i < tutorialPanels.Count ; i++)
                {
                    tutorialPanels[i].scrollView.SetActive(false);
                    
                    if (item.itemTitle == tutorialPanels[i].nameId)
                    {
                        item.onItemSelect.AddListener(() => SelectTutorial(tutorialPanels[i]));
                        currentIndex = i + 1;
                        break;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var item in tutorialSelector.items)
            {
                item.onItemSelect.RemoveAllListeners();
            }

            MenuViewManager.startButtonPressed -= MenuViewManagerOnStartButtonPressed;
            AssessmentViewManager.assessmentBackButtonPressed -= AssessmentViewManagerOnAssessmentBackButtonPressed;
        }

        private void Start()
        {
            if (tutorialPanels.Count > 0)
            {
                tutorialPanels[0].scrollView.SetActive(true);
            }
            
            tutorialSelector.UpdateUI();
        }

        private void MenuViewManagerOnStartButtonPressed()
        {
            tutorialCanvasGroup.SetActive(true);
        }

        private void AssessmentViewManagerOnAssessmentBackButtonPressed()
        {
            tutorialCanvasGroup.SetActive(true);
        }

        private void SelectTutorial(TutorialPanel tutorialPanel)
        {
            foreach (var panel in tutorialPanels)
            {
                panel.scrollView.SetActive(false);
            }

            tutorialPanel.scrollView.SetActive(true);
        }

        private void CompleteButton()
        {
            tutorialCompleteButtonPressed?.Invoke();
            tutorialCanvasGroup.SetActive(false);
        }

        private void BackButton()
        {
            tutorialBackButtonPressed?.Invoke();
            tutorialCanvasGroup.SetActive(false);
        }
    }

    [Serializable]
    public struct TutorialPanel : IComparable<TutorialPanel>
    {
        public GameObject scrollView;
        public string nameId;

        public int CompareTo(TutorialPanel other)
        {
            return string.Compare(nameId, other.nameId, StringComparison.Ordinal);
        }
    }
}