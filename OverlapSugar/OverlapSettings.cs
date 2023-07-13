using System;
using UnityEngine;

namespace NTC.OverlapSugar
{
    [Serializable]
    public class OverlapSettings
    {
        [Header("Common")]
        [SerializeField] private LayerMask _searchMask;
        [SerializeField] private Transform _overlapPoint;

        [Header("Overlap Area")]
        [SerializeField] private OverlapType _overlapType;
        [SerializeField] private Vector3 _boxSize = Vector3.one;
        [SerializeField, Min(0f)] private float _sphereRadius = 0.5f;

        [Header("Offset")]
        [SerializeField] private Vector3 _positionOffset;
        
        [Header("Obstacles")] 
        [SerializeField] private bool _considerObstacles;
        [SerializeField] private LayerMask _obstaclesMask;
        
        [Header("Gizmos")]
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private Color _gizmosColor = Color.cyan;
        
        [NonSerialized] public int Size;

        public readonly Collider[] OverlapResults = new Collider[32];
        
        public LayerMask SearchMask => _searchMask;
        public Transform OverlapPoint => _overlapPoint;
        public OverlapType OverlapType => _overlapType;
        public Vector3 Offset => _positionOffset;
        public Vector3 BoxSize => _boxSize;
        public float SphereRadius => _sphereRadius;
        public bool ConsiderObstacles => _considerObstacles;
        public LayerMask ObstaclesMask => _obstaclesMask;

        public void SetOverlapPoint(Transform newOverlapPoint)
        {
            if (newOverlapPoint == null)
                throw new ArgumentNullException(nameof(newOverlapPoint));
            
            _overlapPoint = newOverlapPoint;
        }

        public void SetOverlapType(OverlapType overlapType)
        {
            _overlapType = overlapType;
        }

        public void SetSearchMask(LayerMask newMask)
        {
            _searchMask = newMask;
        }

        public void SetObstaclesMask(LayerMask newMask)
        {
            _obstaclesMask = newMask;
        }

        public void SetOffset(Vector3 offset)
        {
            _positionOffset = offset;
        }
        
        public void SetBoxSize(Vector3 size)
        {
            _boxSize = size;
        }

        public void SetSphereRadius(float radius)
        {
            if (radius < 0f)
                throw new ArgumentOutOfRangeException(nameof(radius));
            
            _sphereRadius = radius;
        }

        public void EnableObstacleConsideration()
        {
            _considerObstacles = true;
        }
        
        public void DisableObstacleConsideration()
        {
            _considerObstacles = false;
        }

        public void SetGizmosActive(bool status)
        {
            _drawGizmos = status;
        }
        
        public void AllowGizmos()
        {
            _drawGizmos = true;
        }

        public void DisallowGizmos()
        {
            _drawGizmos = false;
        }

        public void SetGizmosColor(Color color)
        {
            _gizmosColor = color;
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
                
                default: throw new ArgumentOutOfRangeException(nameof(_overlapType));
            }
        }
    }
}