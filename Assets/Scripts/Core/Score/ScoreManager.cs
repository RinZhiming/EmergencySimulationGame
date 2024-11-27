using System;
using Ui.Assessment;
using UnityEngine;

namespace Core.Score
{
    public enum ScoreType
    {
        Pre,
        Post,
    }
    public class ScoreManager : MonoBehaviour
    {
        private ScoreData scoreData;
        
        private void Awake()
        {
            AssessmentViewManager.assessmentSubmitButtonPressed += OnAssessmentSubmitButtonPressed;
            
            LoadScore(ref scoreData);
        }

        private void OnDestroy()
        {
            AssessmentViewManager.assessmentSubmitButtonPressed -= OnAssessmentSubmitButtonPressed;
        }

        private bool OnAssessmentSubmitButtonPressed(int score, ScoreType scoreType)
        {
            if (scoreData == null) return false;
            return true;
        }

        private void LoadScore(ref ScoreData data)
        {
            if (data != null) 
                scoreData = null;
            
            var preScore = PlayerPrefs.GetInt("PreScore", -1);
            var postScore = PlayerPrefs.GetInt("PostScore", -1);
            data = new ScoreData
            {
                PreScore = preScore,
                PostScore = postScore, 
            };
        }
    }
    
    public record ScoreData
    {
        public int PreScore { get; set; }
        public int PostScore { get; set; }
    }
}
