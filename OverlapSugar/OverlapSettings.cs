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

        private int _size;
        
        public LayerMask SearchMask => _searchMask;
        
        public Transform OverlapPoint => _overlapPoint;
        
        public OverlapType OverlapType => _overlapType;
        
        public Vector3 Offset => _positionOffset;
        
        public Vector3 BoxSize => _boxSize;
        
        public float SphereRadius => _sphereRadius;
        
        public bool ConsiderObstacles => _considerObstacles;
        
        public LayerMask ObstaclesMask => _obstaclesMask;
        
        public Collider[] OverlapResults { get; private set; }
        
        public bool Initialized { get; private set; }

        public int Size
        {
            get => _size;
            set
            {
                _size = Mathf.Clamp(value, 0, int.MaxValue);
                SizeChanged?.Invoke(_size);
            }
        }
        
        public event Action<int> SizeChanged;
        
        public OverlapSettings Init(int resultsCapacity = 32)
        {
#if DEBUG
            if (resultsCapacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(resultsCapacity));
#endif
            OverlapResults = new Collider[resultsCapacity];
            Initialized = true;
            return this;
        }
        
        public OverlapSettings SetOverlapPoint(Transform newOverlapPoint)
        {
#if DEBUG
            if (newOverlapPoint == null)
                throw new ArgumentNullException(nameof(newOverlapPoint));
#endif
            _overlapPoint = newOverlapPoint;
            return this;
        }

        public OverlapSettings SetOverlapType(OverlapType overlapType)
        {
            _overlapType = overlapType;
            return this;
        }

        public OverlapSettings SetSearchMask(LayerMask newMask)
        {
            _searchMask = newMask;
            return this;
        }

        public OverlapSettings SetObstaclesMask(LayerMask newMask)
        {
            _obstaclesMask = newMask;
            return this;
        }

        public OverlapSettings SetOffset(Vector3 offset)
        {
            _positionOffset = offset;
            return this;
        }
        
        public OverlapSettings SetBoxSize(Vector3 size)
        {
            _boxSize = size;
            return this;
        }

        public OverlapSettings SetSphereRadius(float radius)
        {
#if DEBUG
            if (radius < 0f)
                throw new ArgumentOutOfRangeException(nameof(radius));
#endif
            _sphereRadius = radius;
            return this;
        }

        public OverlapSettings EnableObstacleConsideration()
        {
            _considerObstacles = true;
            return this;
        }
        
        public OverlapSettings DisableObstacleConsideration()
        {
            _considerObstacles = false;
            return this;
        }

        public OverlapSettings SetGizmosActive(bool status)
        {
            _drawGizmos = status;
            return this;
        }
        
        public OverlapSettings AllowGizmos()
        {
            return SetGizmosActive(true);
        }

        public OverlapSettings DisallowGizmos()
        {
            return SetGizmosActive(false);
        }

        public OverlapSettings SetGizmosColor(Color color)
        {
            _gizmosColor = color;
            return this;
        }

        public bool TryDrawGizmos()
        {
            if (_drawGizmos == false)
                return false;
            
            if (_overlapPoint == null)
                return false;
            
            Gizmos.matrix = _overlapPoint.localToWorldMatrix;
            Gizmos.color = _gizmosColor;

            switch (_overlapType)
            {
                case OverlapType.Box: Gizmos.DrawCube(_positionOffset, _boxSize); break;
                case OverlapType.Sphere: Gizmos.DrawSphere(_positionOffset, _sphereRadius); break;
                default: throw new ArgumentOutOfRangeException(nameof(_overlapType));
            }

            return true;
        }
    }
}