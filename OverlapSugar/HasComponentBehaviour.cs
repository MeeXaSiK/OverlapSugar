namespace NTC.OverlapSugar
{
    internal static class HasComponentBehaviour<T>
    {
        internal static readonly HasComponent<T> Default;
        internal static readonly HasComponent<T> InChildren;
            
        static HasComponentBehaviour()
        {
            Default = CheckForComponentShortCuts.HasComponent;
            InChildren = CheckForComponentShortCuts.HasComponentInChildren;
        }
    }
}