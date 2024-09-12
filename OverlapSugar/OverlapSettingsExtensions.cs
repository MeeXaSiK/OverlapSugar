using System.Collections.Generic;
using UnityEngine;

namespace NTC.OverlapSugar
{
    public static class OverlapSettingsExtensions
    {
        public static bool TryFind<T>(this OverlapSettings overlapSettings, out T component)
        {
            return TryFindSingleComponent(overlapSettings, out component, HasComponentBehaviour<T>.Default);
        }

        public static bool TryFindInChildren<T>(this OverlapSettings overlapSettings, out T component)
        {
            return TryFindSingleComponent(overlapSettings, out component, HasComponentBehaviour<T>.InChildren);
        }
        
        public static bool TryFind<T>(this OverlapSettings overlapSettings, ICollection<T> results)
        {
            return TryFindManyComponents(results, overlapSettings, HasComponentBehaviour<T>.Default);
        }
        
        public static bool TryFindInChildren<T>(this OverlapSettings overlapSettings, ICollection<T> results)
        {
            return TryFindManyComponents(results, overlapSettings, HasComponentBehaviour<T>.InChildren);
        }

        private static bool TryFindSingleComponent<T>(this OverlapSettings overlapSettings, out T target, 
            HasComponent<T> hasComponent)
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
        
        private static bool TryFindManyComponents<T>(ICollection<T> results, OverlapSettings overlapSettings,
            HasComponent<T> hasComponent)
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
                
                if (hasComponent.Invoke(overlapSettings.OverlapResults[i], out T target))
                {
                    results.Add(target);
                }
            }

            return results.Count > 0;
        }

        private static bool HasObstacleOnTheWay(OverlapSettings overlapSettings, int id)
        {
            Vector3 startPosition = overlapSettings.OverlapPoint.position;
            Vector3 colliderPosition = overlapSettings.OverlapResults[id].transform.position;
            return Physics.Linecast(startPosition, colliderPosition, overlapSettings.ObstaclesMask);
        }
    }
}