# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [UNRELEASED]

### Added

- `GameObject.TryGetComponentCached` and `GetOrAddComponentCached` extensions
- GameObject lifecycle callbacks (Enable/Disable/Destroy)
- Trigger Enter/Exit callbacks
- Util.Random.AlphanumericString
- AsyncFileWriter utility
- `Tools/Editor/Open Application.persistentDataPath` menu option

### Fixed

- Remove NaughtyAttributes dependency in package.json to prevent Unity errors on import
- Compiler errors in RuntimePlatformFlag.cs in Unity 2022

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
