# NormalizaTexto (Português - Brasil)

Utilitário Windows Forms desenvolvido em C# (.NET Framework) para normalizar textos, remover diacriticos (acentos) e analisar estatísticas de texto em tempo real.

## Funcionalidades

- **Remoção de Acentos**: Remove instantaneamente acentos e caracteres diacríticos utilizando normalização Unicode (`NormalizationForm.FormD`).
- **Estatísticas em Tempo Real**: Monitora contagem de linhas, palavras e caracteres à medida que você digita.
- **Destaque de Linha**: Destaca a linha ativa nas caixas de texto de origem e destino para facilitar a navegação.
- **Versionamento Automático**: Incrementa automaticamente a versão de patch (`Z`) a cada compilação (build) via MSBuild utilizando o arquivo `version.txt`.

## Estrutura do Projeto

- `NormalizaTexto/frmMain.cs`: Lógica principal da interface e conversão de acentos.
- `NormalizaTexto/version.txt`: Armazena a versão atual (`X.Y.Z`).
- `NormalizaTexto/NormalizaTexto.csproj`: Arquivo de projeto com regras customizadas do MSBuild para versionamento.

## Licença

Este projeto é de código aberto.
