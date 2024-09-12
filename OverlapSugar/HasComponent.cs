using UnityEngine;

namespace NTC.OverlapSugar
{
    internal delegate bool HasComponent<T>(Component component, out T target);
}