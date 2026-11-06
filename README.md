# NormalizaTexto

🇺🇸 **[English (en-US)](README.md)** | 🇧🇷 [Português (pt-BR)](README.pt-BR.md)

A Windows Forms utility written in C# (.NET Framework) for normalizing text, removing diacritics (accents), and analyzing text statistics in real time.

## Features

- **Diacritic Removal**: Instantly strips accents from text using Unicode normalization (`NormalizationForm.FormD`).
- **Real-Time Statistics**: Tracks lines, words, and character counts as you type.
- **Line Highlighting**: Highlights the active line in both source and destination text boxes for easy navigation.
- **Automatic Versioning**: Increments the patch version (`Z`) on every build and stamps it into the assembly metadata (`AssemblyVersion`, `AssemblyFileVersion` and `AssemblyInformationalVersion`) using `version.txt`. To build an exact version (for example a minor/major release), pass `/p:BuildVersion=1.1.0` to MSBuild: the value is used as-is and becomes the new baseline.

## Requirements

- Windows with .NET Framework 4.8
- Visual Studio 2022+ or MSBuild to build from source

## Project Structure

- `NormalizaTexto/frmMain.cs`: Main interface logic, statistics and diacritic conversion.
- `NormalizaTexto/SyncTextBox.cs`: RichTextBox control that keeps the two panes scrolled in sync (embedded in the executable).
- `NormalizaTexto/version.txt`: Stores the current version (`X.Y.Z`).
- `NormalizaTexto/NormalizaTexto.csproj`: C# project file with custom MSBuild targets for versioning.

## Standalone

The executable is self-contained: no extra DLL is required alongside `NormalizaTexto.exe`.

## License

This project is open source.
