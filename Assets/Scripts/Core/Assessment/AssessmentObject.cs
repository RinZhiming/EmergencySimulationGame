using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Assessment
{
    public class AssessmentObject : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Toggle choiceToggle1, choiceToggle2, choiceToggle3;
        public Problem Problem { get; set; }
        public bool IsCorrect { get; private set; }

        private void Awake()
        {
            choiceToggle1.onValueChanged.AddListener(v =>
            {
                if (v) 
                    IsCorrect = choiceToggle1.GetComponentInChildren<Text>().text == Problem.answer;
            });
            choiceToggle2.onValueChanged.AddListener(v =>
            {
                if (v) 
                    IsCorrect = choiceToggle2.GetComponentInChildren<Text>().text == Problem.answer;
            });
            choiceToggle3.onValueChanged.AddListener(v =>
            {
                if (v) 
                    IsCorrect = choiceToggle3.GetComponentInChildren<Text>().text == Problem.answer;
            });
        }

        private void Update()
        {
            if (!choiceToggle1.isOn && !choiceToggle2.isOn && !choiceToggle3.isOn)
            {
                IsCorrect = false;
            }
        }

        private void OnDestroy()
        {
            choiceToggle1.onValueChanged.RemoveAllListeners();
            choiceToggle2.onValueChanged.RemoveAllListeners();
            choiceToggle3.onValueChanged.RemoveAllListeners();
        }

        public TextMeshProUGUI HeaderText => headerText;
        public Toggle ChoiceToggle1 => choiceToggle1;
        public Toggle ChoiceToggle2 => choiceToggle2;
        public Toggle ChoiceToggle3 => choiceToggle3;
    }
}