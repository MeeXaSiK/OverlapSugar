using System;
using UnityEngine;

namespace NTC.OverlapSugar
{
    public static class OverlapShortCuts
    {
        public static void PerformOverlap(this OverlapSettings overlapSettings)
        {
#if DEBUG
            if (overlapSettings.OverlapPoint == null)
                throw new NullReferenceException(nameof(overlapSettings.OverlapPoint), null);
#endif
            if (overlapSettings.Initialized == false) 
                overlapSettings.Init();
            
            Array.Clear(overlapSettings.OverlapResults, 0, overlapSettings.OverlapResults.Length);
            Vector3 position = overlapSettings.OverlapPoint.TransformPoint(overlapSettings.Offset);
            
            switch (overlapSettings.OverlapType)
            {
                case OverlapType.Box: OverlapBox(overlapSettings, position); break;
                case OverlapType.Sphere: OverlapSphere(overlapSettings, position); break;
                default: throw new ArgumentOutOfRangeException(nameof(overlapSettings.OverlapType));
            }
        }

        private static void OverlapBox(OverlapSettings overlapSettings, Vector3 position)
        {
            const float half = 0.5f;

            overlapSettings.Size = Physics.OverlapBoxNonAlloc(position,
                overlapSettings.BoxSize * half,
                overlapSettings.OverlapResults,
                overlapSettings.OverlapPoint.rotation,
                overlapSettings.SearchMask.value);
        }

        private static void OverlapSphere(OverlapSettings overlapSettings, Vector3 position)
        {
            overlapSettings.Size = Physics.OverlapSphereNonAlloc(position, 
                overlapSettings.SphereRadius,
                overlapSettings.OverlapResults,
                overlapSettings.SearchMask.value);
        }
    }
}