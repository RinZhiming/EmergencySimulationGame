using System;
using System.Collections.Generic;
using Core.Assessment;
using Core.Score;
using Michsky.UI.Heat;
using Ui.Menu;
using Ui.Tutorial;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Ui.Assessment
{
    public enum AssessmentType
    {
        Pre, Post
    }
    
    public class AssessmentViewManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup assessmentCanvasGroup;
        [SerializeField] private PanelButton backButton, submitButton;
        [SerializeField] private Transform problemContainer;
        private List<AssessmentObject> assessmentObjects = new();
        public static event Action assessmentBackButtonPressed, onGameStart;
        public static event Func<int, ScoreType, bool> assessmentSubmitButtonPressed;
        private AssessmentType assessmentType = AssessmentType.Pre;
        private ScoreType scoreType = ScoreType.Pre;
        private void Awake()
        {
            TutorialViewManager.tutorialCompleteButtonPressed += TutorialViewManagerOnTutorialCompleteButtonPressed;
            backButton.onClick.AddListener(BackButton);
            submitButton.onClick.AddListener(SubmitButton);
        }

        private void OnDestroy()
        {
            backButton.onClick.RemoveAllListeners();
            
            TutorialViewManager.tutorialCompleteButtonPressed -= TutorialViewManagerOnTutorialCompleteButtonPressed;
            assessmentObjects.Clear();
        }

        private void Start()
        {
            assessmentCanvasGroup.SetActive(false);
            
            problemContainer.ClearChild();
        }

        private void Update()
        {
            backButton.gameObject.SetActive(assessmentType != AssessmentType.Post);
        }
        
        private void TutorialViewManagerOnTutorialCompleteButtonPressed()
        {
            assessmentObjects.Clear();
            assessmentType = AssessmentType.Pre;
            scoreType = ScoreType.Pre;
            assessmentCanvasGroup.SetActive(true);
            CreateAssessment();
        }

        private void CreateAssessment()
        {
            var problems = AssessmentManager.Problems;
            var problemPrefab = Resources.Load<GameObject>("Problem");

            var left = 0;
            var right = problems.Count - 1;
            while (left < right)
            {
                CreateNewAssessmentObject(problemPrefab, problems[left]);
                CreateNewAssessmentObject(problemPrefab, problems[right]);
                
                left++;
                right--;
            }
            
            if (left == right && (problems.Count - 1) % 2 == 0)
                CreateNewAssessmentObject(problemPrefab, problems[(problems.Count - 1) / 2]);
        }

        private void CreateNewAssessmentObject(GameObject problemPrefab, Problem problem)
        {
            var go = Instantiate(problemPrefab, problemContainer);
            var assessmentObject = go.GetComponent<AssessmentObject>();
                
            assessmentObject.Problem = problem;
            assessmentObject.HeaderText.text = problem.header;
            assessmentObject.ChoiceToggle1.GetComponentInChildren<Text>().text = problem.choices[0];
            assessmentObject.ChoiceToggle2.GetComponentInChildren<Text>().text = problem.choices[1];
            assessmentObject.ChoiceToggle3.GetComponentInChildren<Text>().text = problem.choices[2];
            assessmentObjects.Add(assessmentObject);
        }

        private void SubmitButton()
        {
            var score = 0;
            foreach (var assessment in assessmentObjects)
            {
                if (assessment.IsCorrect) score++;
            }

            var isSuccess = assessmentSubmitButtonPressed?.Invoke(score, scoreType) ?? false;
            if (isSuccess)
            {
                assessmentCanvasGroup.SetActive(false);
                problemContainer.ClearChild();
                onGameStart?.Invoke();
            }
        }

        private void BackButton()
        {
            problemContainer.ClearChild();
            assessmentObjects.Clear();
            assessmentBackButtonPressed?.Invoke();
            assessmentCanvasGroup.SetActive(false);
        }
    }
}