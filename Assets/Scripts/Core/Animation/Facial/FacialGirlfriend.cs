using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.Animation.Facial
{
    public class FacialGirlfriend : FacialAnimationBase
    {
        private FacialPoint mouth, jaw, eyes;
        private void Awake()
        {
            mouth = GetFacialPoints(FacialType.Mouth);
            eyes = GetFacialPoints(FacialType.Eyes);
            jaw = GetFacialPoints(FacialType.Jaw);
        }

        private void Start()
        {
            if (mouth == null || eyes == null || jaw == null) return;

            Breathing();
        }

        private async void Breathing()
        {
            while (Application.isPlaying)
            {
                var open = DOVirtual.Float(0, 100, 1.5f, v =>
                {
                    FaceMesh.SetBlendShapeWeight(mouth.index, v);
                    FaceMesh.SetBlendShapeWeight(jaw.index, v);
                });

                await open.AsyncWaitForCompletion();
                
                var close = DOVirtual.Float(100, 0, 1.5f, v =>
                {
                    FaceMesh.SetBlendShapeWeight(mouth.index, v);
                    FaceMesh.SetBlendShapeWeight(jaw.index, v);
                });

                await close.AsyncWaitForCompletion();
            }
        }
    }
}
