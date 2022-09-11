## [0.3.1]

### Changes

- Moved `MXAction` out from `Internal.cs` file into `MXAction.cs` file.
- Implementation of correct and optimized easing functions.

## [0.3.0]

### New Features

- Added more action types:
  - `TMPro` actions (color, alpha)
  - `RectTransform` actions (anchored position)

### Changes

- Changed `Act` delegate to `MXAction` and removed `MXAction` static class, making the delegate independant.
- Changed `MXAction` static class into multiple smaller static "Action" classes. (see New Features).
- Turned all 2 spaces into 4 spaces.
- Major refactoring on private and public variables (use of m_ and Capital Letter).

## [0.2.2]

### Bug Fixes

- Fixed `Holders` is null error when evalulating.

## [0.2.1]

### New Features

- Added "set" type actions.
- Added a whole lot of easing transitions.
- Added `MXTimelineManager` to auto rebuild `PlayableDirector` graph when Unity reloads.

### Changes

- Only add `BUFFER_TIME` to the opposite moving direction when evaulating in `ISeqHolder`.
- Uses `ref` for "play" type actions.
- Added "alpha" to package version.
- Clamp global time to start and end in `ISeqHolder`.
- Scene evalulation now happens at `MXTrackMixer`.
- Ignore evalulation for `ISeqHolder` when `Holders` list is null or has 0 count.
- Create instance of `MXClipBehaviour` before creating playable out of it.

### Bug Fixes

- Prevent initialization when `TimelineEditor.inspectedDirector` is null.

## [0.2.0]

### New Features

- Implementation of interfaces: `IHolder` for single holder like `MXActionHolder` and `ISeqHolder` for multiple holders (of `IHolder`s).
- `ISeqHolder` interface holds definition of `Evaulate` and `InitEvalulation` for generic sequence evalulation.
- Added `MXUtil`.
- Uses `PrevGlobalTime` to check for forward/backward movement of time and only evaulate affected `IHolder`s.
- Moved evaluation from `MXClipBehaviour` to `MXScene`.

### Bug Fixes

- Timeline playable graph now rebuilds whenever there is a change in duration.
- Initialize evaluation is called when Unity reloads.

## [0.1.0]

- Initial setup.