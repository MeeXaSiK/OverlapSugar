using System;
using UnityEngine;

namespace NTC.OverlapSugar
{
    [Serializable]
    public class OverlapSettings
    {
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Transform overlapPoint;
        [SerializeField] private OverlapType overlapType;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 boxSize;
        [SerializeField, Range(0f, 10f), Min(0f)] private float sphereRadius = 0.8f;
        [SerializeField] private Color gizmosColor;
        [SerializeField] private bool drawGizmos;

        [NonSerialized] public int Size;
        
        public readonly Collider[] Results = new Collider[64];

        public LayerMask LayerMask => layerMask;
        public Transform OverlapPoint => overlapPoint;
        public OverlapType OverlapType => overlapType;
        public Vector3 Offset => positionOffset;
        public Vector3 BoxSize => boxSize;
        public float SphereRadius => sphereRadius;
        
        public void ChangeRoot(Transform newRoot)
        {
            if (newRoot == null)
                throw new NullReferenceException(nameof(newRoot), null);
            
            overlapPoint = newRoot;
        }
        
        public void TryDrawGizmos()
        {
            if (drawGizmos == false)
                return;
            
            if (overlapPoint == null)
                return;
            
            Gizmos.matrix = overlapPoint.localToWorldMatrix;
            Gizmos.color = gizmosColor;

            switch (overlapType)
            {
                case OverlapType.Box: Gizmos.DrawCube(positionOffset, boxSize); break;
                case OverlapType.Sphere: Gizmos.DrawSphere(positionOffset, sphereRadius); break;
            }
        }
    }
}