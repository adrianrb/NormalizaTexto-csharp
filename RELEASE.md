# Release 1.1.1

Stability and versioning improvements for **NormalizaTexto**.

## What's Changed
- Corrected word and line counters.
- The caret no longer jumps to the start while typing.
- Line/column status follows the caret (1-based).
- Each pane tracks its own highlighted line.
- Ctrl+V pastes at the caret instead of replacing the whole document.
- Splitter restore and pasted caret positions are clamped to valid ranges.
- Removed the unused OpenFileDialog and dead code.
- Retargeted to .NET Framework 4.8.
- Replaced the broken DLL HintPath with a project reference to `LibComponentesCustomizados`.
- The executable version is now stamped from `version.txt`.
- Stopped tracking `bin/`, `obj/` and `.suo`.

## Requirements
- Windows with .NET Framework 4.8.
