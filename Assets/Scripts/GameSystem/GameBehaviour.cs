using System;
using UnityEngine;
using UnityEngine.Playables;

namespace GameSystem
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayableDirector cutscene;
        protected PlayableDirector Cutscene => cutscene;
    }
}