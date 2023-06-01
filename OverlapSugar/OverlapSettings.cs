using System;
using UnityEngine;

namespace NTC.OverlapSugar
{
    [Serializable]
    public class OverlapSettings
    {
        [Header("Overlap")]
        [SerializeField] private LayerMask _searchMask;
        [SerializeField] private Transform _overlapPoint;
        [SerializeField] private OverlapType _overlapType;
        [SerializeField] private Vector3 _positionOffset;
        
        [Header("Size")]
        [SerializeField] private Vector3 _boxSize;
        [SerializeField, Min(0f)] private float _sphereRadius = 0.8f;

        [Header("Obstacles")] 
        [SerializeField] private bool _considerObstacles;
        [SerializeField] private LayerMask _obstaclesMask;
        
        [Header("Gizmos")]
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private Color _gizmosColor = Color.cyan;
        
        [NonSerialized] public int Size;

        public readonly Collider[] Results = new Collider[32];

        public LayerMask SearchMask => _searchMask;
        public Transform OverlapPoint => _overlapPoint;
        public OverlapType OverlapType => _overlapType;
        public Vector3 Offset => _positionOffset;
        public Vector3 BoxSize => _boxSize;
        public float SphereRadius => _sphereRadius;
        public bool ConsiderObstacles => _considerObstacles;
        public LayerMask ObstaclesMask => _obstaclesMask;

        public void ChangeOverlapPoint(Transform newRoot)
        {
            if (newRoot == null)
                throw new ArgumentNullException(nameof(newRoot));
            
            _overlapPoint = newRoot;
        }

        public void ChangeSearchMask(in LayerMask newMask)
        {
            _searchMask = newMask;
        }

        public void ChangeObstaclesMask(in LayerMask newMask)
        {
            _obstaclesMask = newMask;
        }

        public void ChangeOffset(in Vector3 offset)
        {
            _positionOffset = offset;
        }

        public void SetSphere(in float radius, in Vector3 offset)
        {
            _overlapType = OverlapType.Sphere;
            _sphereRadius = radius;
            _positionOffset = offset;
        }

        public void SetBox(in Vector3 size, in Vector3 offset)
        {
            _overlapType = OverlapType.Box;
            _boxSize = size;
            _positionOffset = offset;
        }

        public void SetGizmos(bool drawGizmos, in Color color)
        {
            _drawGizmos = drawGizmos;
            _gizmosColor = color;
        }

        public void EnableConsiderObstacles()
        {
            _considerObstacles = true;
        }
        
        public void DisableConsiderObstacles()
        {
            _considerObstacles = false;
        }

        public void TryDrawGizmos()
        {
            if (_drawGizmos == false)
                return;
            
            if (_overlapPoint == null)
                return;
            
            Gizmos.matrix = _overlapPoint.localToWorldMatrix;
            Gizmos.color = _gizmosColor;

            switch (_overlapType)
            {
                case OverlapType.Box: Gizmos.DrawCube(_positionOffset, _boxSize); break;
                case OverlapType.Sphere: Gizmos.DrawSphere(_positionOffset, _sphereRadius); break;
                
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}