using System.Collections.Generic;
using UnityEngine;
using static NTC.OverlapSugar.CheckForComponentShortCuts;

namespace NTC.OverlapSugar
{
    public static class OverlapSettingsExtensions
    {
        public static bool TryFind<TComponent>(this OverlapSettings overlapSettings, out TComponent component)
        {
            return TryFindSingleComponent(overlapSettings, out component, HasComponent);
        }

        public static bool TryFind<TComponent>(this OverlapSettings overlapSettings, ICollection<TComponent> results)
        {
            return TryFindManyComponents(results, overlapSettings, HasComponent);
        }
        
        public static bool TryFindInChildren<TComponent>(this OverlapSettings overlapSettings, ICollection<TComponent> results)
        {
            return TryFindManyComponents(results, overlapSettings, HasComponentInChildren);
        }

        private static bool TryFindSingleComponent<TComponent>(this OverlapSettings overlapSettings, 
            out TComponent target, HasComponent<TComponent> hasComponent)
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
                
                if (hasComponent.Invoke(overlapSettings.OverlapResults[i], out target))
                {
                    return true;
                }
            }

            target = default;
            return false;
        }
        
        private static bool TryFindManyComponents<TComponent>(ICollection<TComponent> results, 
            OverlapSettings overlapSettings, HasComponent<TComponent> hasComponent)
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
                
                if (hasComponent.Invoke(overlapSettings.OverlapResults[i], out var target))
                {
                    results.Add(target);
                }
            }

            return results.Count > 0;
        }

        private static bool HasObstacleOnTheWay(OverlapSettings overlapSettings, int id)
        {
            var startPosition = overlapSettings.OverlapPoint.position;
            var colliderPosition = overlapSettings.OverlapResults[id].transform.position;

            return Physics.Linecast(startPosition, colliderPosition, overlapSettings.ObstaclesMask);
        }
    }
}