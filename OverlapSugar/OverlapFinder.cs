using System.Collections.Generic;
using UnityEngine;
using static NTC.OverlapSugar.CheckComponentShortCuts;

namespace NTC.OverlapSugar
{
    public static class OverlapFinder
    {
        public static bool TryFind<TTarget>(this OverlapSettings overlapSettings, out TTarget target)
        {
            return TryFindSingleTarget(overlapSettings, out target, HasComponent);
        }

        public static bool TryFind<TTarget>(ICollection<TTarget> results, OverlapSettings overlapSettings)
        {
            return TryFindManyTargets(results, overlapSettings, HasComponent);
        }
        
        public static bool TryFindInChildren<TTarget>(ICollection<TTarget> results, OverlapSettings overlapSettings)
        {
            return TryFindManyTargets(results, overlapSettings, HasComponentInChildren);
        }

        private static bool TryFindSingleTarget<TTarget>(this OverlapSettings overlapSettings, out TTarget target, 
            HasComponent<TTarget> hasComponent) 
        {
            overlapSettings.PerformOverlap();

            for (var i = 0; i < overlapSettings.Size; i++)
            {
                if (overlapSettings.ConsiderObstacles)
                {
                    if (HasObstacleOnTheWay(overlapSettings, i))
                    {
                        continue;
                    }
                }
                
                if (hasComponent.Invoke(overlapSettings.Results[i], out target))
                {
                    return true;
                }
            }

            target = default;
            return false;
        }
        
        private static bool TryFindManyTargets<TTarget>(ICollection<TTarget> results, OverlapSettings overlapSettings, 
            HasComponent<TTarget> hasComponent) 
        {
            results.Clear();
            overlapSettings.PerformOverlap();
            
            for (var i = 0; i < overlapSettings.Size; i++)
            {
                if (overlapSettings.ConsiderObstacles)
                {
                    if (HasObstacleOnTheWay(overlapSettings, i))
                    {
                        continue;
                    }
                }
                
                if (hasComponent.Invoke(overlapSettings.Results[i], out var target))
                {
                    results.Add(target);
                }
            }

            return results.Count > 0;
        }

        private static bool HasObstacleOnTheWay(OverlapSettings overlapSettings, int id)
        {
            var startPosition = overlapSettings.OverlapPoint.position;
            var colliderPosition = overlapSettings.Results[id].transform.position;

            return Physics.Linecast(startPosition, colliderPosition, overlapSettings.ObstaclesMask);
        }
    }
}