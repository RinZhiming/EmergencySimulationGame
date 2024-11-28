using System;
using UnityEngine;

namespace Player
{
    public static class PlayerInteractEvents
    {
        public static Action<Collider> OnInteract;
        public static Action<Collider> OnHit;
        public static Action<Collider> OnLost;
    }
}