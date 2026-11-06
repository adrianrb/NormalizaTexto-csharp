# GitHub Releases Documentation

This document contains the release notes for GitHub Releases, provided in both English (EN-US) and Portuguese (PT-BR).

---

## English (EN-US)

### Release v1.1.3
**Standalone executable**

NormalizaTexto no longer requires an external DLL.

#### Highlights:
- The `SyncTextBox` control (keeps both panes scrolled in sync) is now embedded in the application.
- `NormalizaTexto.exe` runs on its own — no `LibComponentesCustomizados.dll` needed alongside it.
- Removed the external project reference from the solution and project file.

#### Requirements:
- Windows with .NET Framework 4.8.

#### Files Included:
- `NormalizaTexto.exe` (standalone executable)
- Source code (ZIP / TAR.GZ)

### Release v1.1.1
**Stability and versioning improvements**

This release modernizes the build, fixes the text statistics and makes editing more predictable.

#### Highlights:
- **Correct text statistics**: the word count no longer counts separators and the total line count no longer adds a phantom line.
- **Caret stays where you type**: editing no longer selects and repositions the whole document on every keystroke.
- **Live line/column**: the status bar follows the caret as you move with the mouse or keyboard (1-based).
- **Per-pane line highlight**: each pane tracks its own highlighted line, keeping source and destination in sync.
- **Safer paste**: Ctrl+V inserts the clipboard text at the caret (or replaces the selection) instead of overwriting the whole document.
- **Version stamped in the executable**: `version.txt` now drives `AssemblyVersion`, `AssemblyFileVersion` and `AssemblyInformationalVersion`, and the patch is bumped after a successful build. Use `/p:BuildVersion=X.Y.Z` to build an exact version.

#### Fixes:
- Corrected word and line counters.
- Removed the caret jump caused by `SelectAll()` on every text change.
- Fixed the highlight state shared between the two text boxes.
- Clamped splitter restore and pasted caret positions to valid ranges.
- Removed the unused `OpenFileDialog` and dead code.

#### Under the hood:
- Retargeted to **.NET Framework 4.8**.
- Replaced the broken DLL `HintPath` with a project reference to `LibComponentesCustomizados`.
- Stopped tracking `bin/`, `obj/` and `.suo`, and normalized line endings with `.gitattributes`.

#### Requirements:
- Windows with .NET Framework 4.8.

#### Files Included:
- `NormalizaTexto-v1.1.1.zip` (executable + required `LibComponentesCustomizados.dll`)
- Source code (ZIP / TAR.GZ)

### Release v1.0.0
**Initial Release**

Welcome to the first official release of **NormalizaTexto**!

#### Highlights:
- **Diacritic Stripping**: Instantly remove accents and special diacritics using robust Unicode normalization (`NormalizationForm.FormD`).
- **Real-Time Text Statistics**: Live counts for characters, words, and lines updated as you type.
- **Active Line Highlighting**: Visual assistance highlighting the current line in both source and destination views.
- **Automated Versioning System**: Integrated MSBuild target that automatically increments the patch version (`Z`) on every build using `version.txt`.

#### Files Included:
- `NormalizaTexto.exe` (Standalone Windows Forms executable)
- Source code (ZIP / TAR.GZ)

---

## Português (PT-BR)

### Lançamento v1.1.3
**Executável independente**

O NormalizaTexto não exige mais uma DLL externa.

#### Destaques:
- O controle `SyncTextBox` (mantém a rolagem dos dois painéis sincronizada) agora está embutido na aplicação.
- O `NormalizaTexto.exe` roda sozinho — sem precisar do `LibComponentesCustomizados.dll` ao lado.
- Referência de projeto externa removida da solução e do arquivo de projeto.

#### Requisitos:
- Windows com .NET Framework 4.8.

#### Arquivos inclusos:
- `NormalizaTexto.exe` (executável independente)
- Código-fonte (ZIP / TAR.GZ)

### Lançamento v1.1.1
**Melhorias de estabilidade e versionamento**

Esta versão moderniza o build, corrige as estatísticas de texto e torna a edição mais previsível.

#### Destaques:
- **Estatísticas corretas**: a contagem de palavras não conta mais separadores e o total de linhas não soma uma linha fantasma.
- **O cursor fica onde você digita**: a edição não seleciona nem reposiciona o documento inteiro a cada tecla.
- **Linha/coluna ao vivo**: a barra de status acompanha o cursor ao mover com o mouse ou teclado (base 1).
- **Destaque por painel**: cada painel controla sua própria linha destacada, mantendo origem e destino sincronizados.
- **Colagem segura**: Ctrl+V insere o texto da área de transferência no cursor (ou substitui a seleção) em vez de sobrescrever o documento inteiro.
- **Versão gravada no executável**: o `version.txt` agora alimenta `AssemblyVersion`, `AssemblyFileVersion` e `AssemblyInformationalVersion`, e o patch é incrementado após um build bem-sucedido. Use `/p:BuildVersion=X.Y.Z` para compilar uma versão exata.

#### Correções:
- Contadores de palavras e linhas corrigidos.
- Removido o salto do cursor causado pelo `SelectAll()` a cada mudança de texto.
- Corrigido o estado de destaque compartilhado entre as duas caixas de texto.
- Restauração do splitter e posição do cursor após colar limitadas a intervalos válidos.
- Removidos o `OpenFileDialog` não utilizado e código morto.

#### Por baixo dos panos:
- Mudança de alvo para **.NET Framework 4.8**.
- `HintPath` quebrado da DLL substituído por uma referência de projeto ao `LibComponentesCustomizados`.
- `bin/`, `obj/` e `.suo` deixaram de ser versionados, e os fins de linha foram normalizados com `.gitattributes`.

#### Requisitos:
- Windows com .NET Framework 4.8.

#### Arquivos inclusos:
- `NormalizaTexto-v1.1.1.zip` (executável + `LibComponentesCustomizados.dll` necessário)
- Código-fonte (ZIP / TAR.GZ)

### Lançamento v1.0.0
**Lançamento Inicial**

Bem-vindo ao primeiro lançamento oficial do **NormalizaTexto**!

#### Destaques:
- **Remoção de Diacríticos**: Remova instantaneamente acentos e caracteres especiais utilizando normalização Unicode robusta (`NormalizationForm.FormD`).
- **Estatísticas de Texto em Tempo Real**: Contadores ao vivo de caracteres, palavras e linhas atualizados à medida que você digita.
- **Destaque de Linha Ativa**: Auxílio visual destacando a linha atual nas visualizações de origem e destino.
- **Sistema de Versionamento Automático**: Alvo do MSBuild integrado que incrementa automaticamente a versão de patch (`Z`) a cada compilação usando o arquivo `version.txt`.

#### Arquivos Inclusos:
- `NormalizaTexto.exe` (Executável autônomo para Windows Forms)
- Código-fonte (ZIP / TAR.GZ)
