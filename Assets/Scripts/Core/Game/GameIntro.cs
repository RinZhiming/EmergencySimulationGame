using System;
using Core.Cutscene;
using Core.Score;
using Cysharp.Threading.Tasks;
using GameSystem;
using Ui.Assessment;

namespace Core.Game
{
    public class GameIntro : GameBehaviour
    {
        private void Awake()
        {
            AssessmentViewManager.assessmentSubmitButtonPressed += OnAssessmentSubmitButtonPressed;
        }
        
        private void OnDestroy()
        {
            AssessmentViewManager.assessmentSubmitButtonPressed -= OnAssessmentSubmitButtonPressed;
        }
        
        private bool OnAssessmentSubmitButtonPressed(int score, ScoreType scoreType)
        {
            if (scoreType == ScoreType.Pre)
            {
                CutsceneManager.PlayCutscene(Cutscene);
                return false;
            }
            return true;
        }
    }
}
