using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utilities;

namespace Core.Assessment
{
    public class AssessmentManager : MonoBehaviour
    {
        private AssessmentData tensionAssessmentData, cprAssessmentData, chokingAssessmentData;
        public static List<Problem> Problems { get; private set; } = new();

        private async void Awake()
        {
            tensionAssessmentData = Resources.Load<AssessmentData>("TensionAssessmentData");
            cprAssessmentData = Resources.Load<AssessmentData>("CprAssessmentData");
            chokingAssessmentData = Resources.Load<AssessmentData>("ChokingAssessmentData");

            await UniTask.WaitUntil(() => tensionAssessmentData && cprAssessmentData && chokingAssessmentData);
            
            Problems = CreatAssessment();
        }

        private void OnDestroy()
        {
            tensionAssessmentData = null;
            cprAssessmentData = null;
            chokingAssessmentData = null;
        }

        private List<Problem> CreatAssessment()
        {
            var problems = new List<Problem>();
            problems.AddRange(tensionAssessmentData.Problems);
            problems.AddRange(cprAssessmentData.Problems);
            problems.AddRange(chokingAssessmentData.Problems);
            problems.Shuffle();
            return problems;
        }
    }
}
