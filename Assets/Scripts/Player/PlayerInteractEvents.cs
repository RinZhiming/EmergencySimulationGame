using System;
using UnityEngine;

namespace Player
{
    public static class PlayerInteractEvents
    {
        public static Action<Collider> OnInteract { get; set; }
        public static Action<Collider> OnHit { get; set; }
        public static Action OnLost { get; set; }
    }
}