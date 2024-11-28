using System;

namespace Player
{
    public enum PlayerState
    {
        Idle,
        Play
    }
    
    public static class PlayerEvents
    {
        public static Action<PlayerState> OnPlayerStateChange { get; set; }
    }
}