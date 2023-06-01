using System;
using UnityEngine;

namespace NTC.OverlapSugar
{
    public static class OverlapShortCuts
    {
        public static void PerformOverlap(this OverlapSettings overlapSettings)
        {
            if (overlapSettings.OverlapPoint == null)
                throw new NullReferenceException(nameof(overlapSettings.OverlapPoint), null);
            
            Array.Clear(overlapSettings.Results, 0, overlapSettings.Results.Length);

            var position = overlapSettings.OverlapPoint.TransformPoint(overlapSettings.Offset);

            switch (overlapSettings.OverlapType)
            {
                case OverlapType.Box: OverlapBox(overlapSettings, position); break;
                case OverlapType.Sphere: OverlapSphere(overlapSettings, position); break;
                
                default: 
                    throw new ArgumentOutOfRangeException(nameof(overlapSettings.OverlapType));
            }
        }

        private static void OverlapBox(OverlapSettings overlapSettings, in Vector3 position)
        {
            overlapSettings.Size =
                Physics.OverlapBoxNonAlloc(
                    position, overlapSettings.BoxSize / 2, overlapSettings.Results,
                    overlapSettings.OverlapPoint.rotation, overlapSettings.SearchMask.value);
        }

        private static void OverlapSphere(OverlapSettings overlapSettings, in Vector3 position)
        {
            overlapSettings.Size =
                Physics.OverlapSphereNonAlloc(
                    position, overlapSettings.SphereRadius, 
                    overlapSettings.Results, overlapSettings.SearchMask.value);
        }
    }
}