using System;
using System.Linq;
using UnityEngine;

namespace Core.Animation.Facial
{
    public enum FacialType
    {
        Eyes,
        EyesBrown,
        Nose,
        Mouth,
        Jaw,
    }
    
    public abstract class FacialAnimationBase : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer faceMesh;
        [SerializeField] private FacialPoint[] facialPoints;

        protected SkinnedMeshRenderer FaceMesh => faceMesh;
        protected FacialPoint GetFacialPoints(FacialType type) => 
            facialPoints.FirstOrDefault(v => v.name == type);
    }

    [Serializable]
    public record FacialPoint
    {
        public FacialType name;
        public int index;
    }
}
