# 🚄 Overlap Sugar
[![License](https://img.shields.io/github/license/meexasik/overlapsugar?color=318CE7&style=flat-square)](LICENSE.md) [![Version](https://img.shields.io/github/package-json/v/MeeXaSiK/OverlapSugar?color=318CE7&style=flat-square)](package.json) [![Unity](https://img.shields.io/badge/Unity-2021.3+-2296F3.svg?color=318CE7&style=flat-square)](https://unity.com/)

Short cuts for Physics.Overlap in Unity by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode)
* User-friendly `OverlapSettings` in inspector
* `PerformOverlap` in one line of code

## 🌐 Navigation

* [Main](#-overlap-sugar)
* [Asset Review](#user-friendly-settings-in-inspector)
  * [User friendly settings in inspector](#user-friendly-settings-in-inspector)
  * [Overlap in one line of code](#overlap-in-one-line-of-code)
    * [For single target](#for-single-target)
    * [For many targets](#for-many-targets)
  * [Examples of using](#examples-of-using)
  * [Draw Gizmos](#draw-gizmos)
  * [Perform Overlap method](#perform-overlap-method)
  * [Custom max results size](#custom-max-results-size)

## User-friendly Settings In Inspector

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
```csharp
_overlapSettings.TryFindInChildren(out IDamageable damageable);
```
#### For Many Targets

```csharp
private readonly List<IDamageable> _detectedDamageables = new List<IDamageable>(32);

_overlapSettings.TryFind(_detectedDamageables);
```
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
## Perform Overlap method

You can also use only `PerformOverlap` without components auto-finding:
```csharp
_overlapSettings.PerformOverlap();
```
Then you can find results here:
```csharp
Collider[] results = _overlapSettings.OverlapResults;
```
> **Warning!** Method `PerformOverlap` is called in `TryFind` methods!

## Custom max results size

You can set the results collection size by `Init` method:

```csharp
int maxResultsSize = 32;
_overlapSettings.Init(maxResultsSize);
```
