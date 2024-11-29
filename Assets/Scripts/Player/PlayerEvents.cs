using System;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Cutscene,
        Play
    }
    
    public static class PlayerEvents
    {
        public static Action<PlayerState> OnPlayerStateChange { get; set; }
    }
}