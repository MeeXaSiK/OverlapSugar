using UnityEngine;

namespace NTC.OverlapSugar
{
    public delegate bool HasComponent<T>(Component component, out T target);
}