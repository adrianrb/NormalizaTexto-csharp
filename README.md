# NormalizaTexto

A Windows Forms utility written in C# (.NET Framework) for normalizing text, removing diacritics (accents), and analyzing text statistics in real time.

## Features

- **Diacritic Removal**: Instantly strips accents from text using Unicode normalization (`NormalizationForm.FormD`).
- **Real-Time Statistics**: Tracks lines, words, and character counts as you type.
- **Line Highlighting**: Highlights the active line in both source and destination text boxes for easy navigation.
- **Automatic Versioning**: Automatically increments the patch version (`Z`) on every build via MSBuild targets using `version.txt`.

## Project Structure

- `NormalizaTexto/frmMain.cs`: Main interface logic and diacritic conversion.
- `NormalizaTexto/version.txt`: Stores the current version (`X.Y.Z`).
- `NormalizaTexto/NormalizaTexto.csproj`: C# project file with custom MSBuild targets for versioning.

## License

This project is open source.
