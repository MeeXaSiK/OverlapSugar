# OverlapSugar
 > Short cuts for Physics.Overlap in Unity by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode)
* Friendly Overlap Settings in inspector
* Perform Overlap in one line of code

## Navigation

* [Main](#overlapsugar)
* Asset Review
  * [Friendly settings in inspector](#friendly-settings-in-inspector)
  * [Overlap in one line of code](#overlap-in-one-line-of-code)
    * [For single target](#for-single-target)
    * [For many targets](#for-many-targets)
* [Examples of using](#examples-of-using)
* [Draw Gizmos](#draw-gizmos)

## Friendly Settings In Inspector

```csharp
[SerializeField] private OverlapSettings _overlapSettings;
```

![OverlapSettings Review Screenshot](https://github.com/MeeXaSiK/OverlapSugar/blob/main/README%20Files/OverlapSettingsReview.PNG)

## Overlap In One Line Of Code

#### Using

```csharp
using NTC.OverlapSugar;
```
#### Overlap Settings

```csharp
[SerializeField] private OverlapSettings _overlapSettings;
```

#### For Single Target

```csharp
_overlapSettings.TryFind(out IDamageable damageable);
```
#### For Many Targets

```csharp
private readonly List<IDamageable> _detectedDamageables = new List<IDamageable>(32);

_overlapSettings.TryFind(_detectedDamageables);
```
or
```csharp
private readonly List<IDamageable> _detectedDamageables = new List<IDamageable>(32);

_overlapSettings.TryFindInChildren(_detectedDamageables);
```

## Examples Of Using

`Example For Single Target:`

```csharp
    using NTC.OverlapSugar;
    using UnityEngine;

    public class UnitMeleeAttack : UnitAttack
    {
        [SerializeField] private OverlapSettings _overlapSettings;

        public void TryPerformAttack()
        {
            if (_overlapSettings.TryFind(out IDamageable damageable))
            {
                damageable.ApplyDamage(Damage);
            }
        }
    }
```

`Example For Many Targets:`

```csharp
    using NTC.OverlapSugar;
    using UnityEngine;

    public class UnitMeleeAttack : UnitAttack
    {
        [SerializeField] private OverlapSettings _overlapSettings;

        private readonly List<IDamageable> _detectedDamageables = new List<IDamageable>(32);
        
        public void TryPerformAttack()
        {
            if (_overlapSettings.TryFind(_detectedDamageables))
            {
                _detectedDamageables.ForEach(ApplyDamage);
            }
        }
        
        private void ApplyDamage(IDamageable damageable)
        {
            damageable.ApplyDamage(Damage);
        }
    }
```

## Draw Gizmos

`Also you can draw gizmos of overlap zone:`

```csharp
    public class UnitMeleeAttack : UnitAttack
    {
        [SerializeField] private OverlapSettings _overlapSettings;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            _overlapSettings.TryDrawGizmos();
        }
#endif
    }
```

or

```csharp
    public class UnitMeleeAttack : UnitAttack
    {
        [SerializeField] private OverlapSettings _overlapSettings;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            _overlapSettings.TryDrawGizmos();
        }
#endif
    }
```
