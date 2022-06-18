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