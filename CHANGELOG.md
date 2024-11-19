# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [UNRELEASED]

### Added

- Extension methods
	- `GetUniqueRandom`
	- Stack/queue API for `IList<T>`: `Peek`, `Push`, `Pop`, `Enqueue`, `Dequeue`
	- `Shuffle(IList<T>)` to shuffle a list in place
	- `Any(ICollection<T>)` substitution that's more efficient than LINQ
	- `Only(HashSet<T>)`
	- `Slice(IReadOnlyList<T>)`
	- `Vector3 SwizzleXYZ_XZY(Vector3)`
	- `RotateBy(Vector2)`
		- `AsRectTransform(Transform)` and `GetRectTransform(Component)`
		- `GetRandomValue` for `Vector2` and `Vector2Int` ranges
		- `CancelAndDispose(CancellationTokenSource)`
		- `FindInAllChildren(Transform)`
- `GetComponentCached` and `TryGetComponentCached` now also extend `Component` instead of just `GameObject`
- `ScriptableList<TItem>`: A quick way to define a `ScriptableObject` that holds a list of items and implements `IList`
- `TargetGraphicsGroup` and `MultiTargetButton`
	- See https://discussions.unity.com/t/tint-multiple-targets-with-single-button/556754/11
- `Compare` `Min` and `Max`

### Changed

- BREAKING: Rename `Comparers` to `Compare`
- Improved `ForceExpandedPropertyDrawer`

### Fixed

- `RandomElemennt` returns null instead of throwing `IndexOutOfRangeException`

## [1.6.2] - 2024-08-28

### Added

- `WeightedRandomList` default constructor
- `WeightedRandomList` `List<WeightedRandomItem>` constructor

### Changed

- Make `WeightedRandomList.Items` list public so weighted random lists can be edited from code

## [1.6.1] - 2024-07-31

### Fixed

- `DebugFiltered.LogException` only logs when `filterLevel` is exactly `Exception`

### Development

- Added `DebugFiltered.LogException` tests

## [1.6.0] - 2024-06-06

### Added

- Add copy constructor to `WeightedRandomList`

## [1.5.1] - 2024-05-23

### Changed

- Hide `DebugFiltered` and `IUseLogLevel` log methods from Unity call stack

## [1.5.0] - 2024-05-23

### Added

- `IUseLogLevel` interface to make it slightly easier to work with `LogLevel` and `DebugFiltered`

## [1.4.1] - 2024-05-14

### Fixed

- `SceneField` implicit string conversion throws `NullReferenceException` if the `SceneField` is null
- `SceneField.RefreshSceneName()` sets `ScenePath` to the scene's name insead of path

### Development

- Added `SceneField` tests

## [1.4.0] - 2024-05-07

### Added

- `GameObject.GetComponentCached`, `TryGetComponentCached` and `GetOrAddComponentCached` extensions
- GameObject lifecycle callbacks (Enable/Disable/Destroy)
- Trigger Enter/Exit callbacks
- Toolbox that holds stuff
- Util.Random.AlphanumericString
- AsyncFileWriter utility
- `Tools/Editor/Open Application.persistentDataPath` menu option
- LogLevel enum
- DebugFiltered static class for filtered debug logging
- SceneField
- WaitForEvent yield instruction
- `UIFader.OnFadeIn/OutStart` events
- `Util.Math.Wrap` for `int` type
- `Util.Math.Wrap` default overrides for minimum of `0`

### Changed

- `DetectGround` uses rigidbody position and rotation for raycasts if available

### Fixed

- Remove NaughtyAttributes dependency in package.json to prevent Unity errors on import
- Compiler errors in RuntimePlatformFlag.cs in Unity 2022
- Force set fader state on Fade In/Out when object is disabled

### Development

- Added `Util.Math.Wrap` tests

## [1.3.0] - 2023-10-12

### Added

- RuntimePlatformFlag enum
- DisableOnPlatform component

### Changed

- Bump minimum editor version to 2021.3

## [1.2.1] - 2023-03-20

### Fixed

- Compilation errors in StartupInfoLogging

## [1.2.0] - 2023-03-20

### Added

- ForwardTriggerEvents component.
- [UIFader] FadeInCoroutine/FadeOutCoroutine methods that return the Coroutine handle.
- RoundToPrecision math util.
- Timer component.
- Jiggle component.

### Changed

- DetectGround sets IsOnGround to false on disable.
- Send exit events on disable from ForwardTriggerEvents.
- [UIFader] FadeIn/FadeOut no longer return a Coroutine handle (allows use as UnityEvent listener).

### Fixed

- [DetectGround] Update LastHit before sending OnGroundChanged event.

## [1.1.0] - 2023-02-08

### Added

- CollisionSFX component.
- ForwardCollisionEvents component.
- DetectGround component.
- Gizmo component.
- NonAlloc raycast helper methods.
- Easing functions.
- RandomElement extension method for IList.
- Joint get/set world anchor extension method.
- (Editor) Option to clear active selection on play.

### Changed

- Reorganize scripts.

## [1.0.0] - 2023-01-16

### Added

- Import from old package
