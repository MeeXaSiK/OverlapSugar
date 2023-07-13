using UnityEngine;

namespace NTC.OverlapSugar
{
    public static class CheckForComponentShortCuts
    {
        public static bool HasComponent<T>(Component component, out T target)
        {
            return component.TryGetComponent(out target);
        }
        
        public static bool HasComponentInChildren<T>(Component component, out T target)
        {
            target = component.GetComponentInChildren<T>();

            return target != null;
        }
    }
}