using UnityEngine;

namespace NTC.OverlapSugar
{
    internal static class CheckForComponentShortCuts
    {
        internal static bool HasComponent<T>(Component component, out T target)
        {
            return component.TryGetComponent(out target);
        }
        
        internal static bool HasComponentInChildren<T>(Component component, out T target)
        {
            target = component.GetComponentInChildren<T>();
            return target != null;
        }
    }
}