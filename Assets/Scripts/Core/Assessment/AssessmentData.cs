using System;
using UnityEngine;

namespace Core.Assessment
{
    [CreateAssetMenu(fileName = "New Assessment Data", menuName = "Core/Assessment")]
    public class AssessmentData : ScriptableObject
    {
        [SerializeField] private Problem[] problems;

        public Problem[] Problems => problems;
    }

    [Serializable]
    public struct Problem
    {
        public string header;
        public string[] choices;
        public string answer;
    }
}