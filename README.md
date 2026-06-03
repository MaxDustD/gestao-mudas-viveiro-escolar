# Projeto Viveiro Escolar

Autor: Nilseo Cassol

## Índice

- [Visão Geral](#visão-geral)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Requisitos](#requisitos)
- [Como Executar](#como-executar)
  - [Executar a versão console](#executar-a-versão-console)
  - [Executar a versão web](#executar-a-versão-web)
- [Funcionalidades](#funcionalidades)
  - [Console](#console)
  - [Web](#web)
- [Arquitetura e Código](#arquitetura-e-código)
- [Páginas e Menus](#páginas-e-menus)
- [Observações](#observações)

## Visão Geral

Este projeto implementa um sistema de controle de produção de mudas para um viveiro escolar. O código inclui:

- um aplicativo console com menu em caixa ASCII;
- uma biblioteca de domínio e serviços de aplicação (`ViveiroEscolar.Library`);
- uma interface web Blazor Server para gestão e consulta via navegador.

O foco do sistema é registrar espécies, canteiros, responsáveis e lotes, além de consultar disponibilidade e lotes ativos.

## Estrutura do Projeto

- `Program.cs` - aplicação console principal com menus e interface de texto.
- `src/ViveiroEscolar.Library/` - biblioteca de domínio, serviços e repositórios em memória.
- `src/ViveiroEscolar.Web/` - aplicação Blazor Server com frontend completo.

## Requisitos

- .NET SDK 10.0
- Windows (o projeto já foi testado neste ambiente)

## Como Executar

### Executar a versão console

1. Abra o terminal na raiz do projeto.
2. Execute:

```powershell
dotnet run --project .\ViveiroEscolar.ConsoleApp.csproj
```

3. Use o menu exibido para navegar entre Espécies, Canteiros, Responsáveis, Lotes e Consultas.

### Executar a versão web

1. Abra o terminal na raiz do projeto.
2. Execute:

```powershell
dotnet watch run --project .\src\ViveiroEscolar.Web\ViveiroEscolar.Web.csproj --urls http://localhost:5000
```

3. Abra o navegador em:

```text
http://localhost:5000
```

4. A página carrega automaticamente com hot reload.

## Funcionalidades

### Console

A versão console oferece:

- cadastro de espécies, canteiros e responsáveis;
- criação de lotes com seleção de espécie e canteiro;
- registro de cuidados e retiradas de lote;
- encerramento de lote;
- consultas de disponibilidade por espécie e listagem de lotes ativos.

### Web

A versão web inclui:

- navegação lateral com acesso a páginas:
  - Home
  - Espécies
  - Canteiros
  - Responsáveis
  - Lotes
  - Consultas
- cadastro e listagem de entidades em páginas dedicadas;
- criação de lotes com seleção de espécie, canteiro e responsável;
- visualização de disponibilidade e lotes ativos.

## Arquitetura e Código

O projeto segue uma separação simples entre camadas:

- `Domain.Entities` - classes como `Especie`, `Canteiro`, `Responsavel`, `LoteMudas`, `Cuidado` e `Retirada`.
- `Application.Services` - a classe `ViveiroApplicationService` orquestra regras de negócio e validações.
- `Infra.Memory` - repositórios em memória para persistência durante a execução.
- `Program.cs` (console) - interface de usuário com menus e leitura de entrada.
- `src/ViveiroEscolar.Web` - frontend Blazor que consome `ViveiroApplicationService` via injeção de dependência.

## Páginas e Menus

### Menu principal do console

- `1. Espécies`
- `2. Canteiros`
- `3. Responsáveis`
- `4. Lotes`
- `5. Consultas`
- `0. Sair`

### Submenus do console

- Menu de Espécies: cadastrar e listar.
- Menu de Canteiros: cadastrar e listar.
- Menu de Responsáveis: cadastrar e listar.
- Menu de Lotes: criar lote, registrar cuidado, registrar retirada, encerrar lote, listar lotes.
- Menu de Consultas: disponibilidade por espécie, lotes ativos.

### Páginas web disponíveis

- `Especies` - cadastro e listagem de espécies.
- `Canteiros` - cadastro e listagem de canteiros.
- `Responsaveis` - cadastro e listagem de responsáveis.
- `Lotes` - criação e listagem de lotes com seleção de entidades.
- `Consultas` - consulta de disponibilidade por espécie e listagem de lotes ativos.

## Observações
- **Persistência:** os dados são armazenados em memória; ao fechar o aplicativo, os registros são perdidos.

- **Configuração de portas:** a aplicação web utiliza `launchSettings.json` para configurar `http://localhost:5000` e `https://localhost:5001` por padrão. Se a porta estiver em uso, a aplicação não iniciará. Para resolver:
  - Pare o servidor em execução (no terminal onde está rodando, use Ctrl+C).
  - Encontre o PID que ocupa a porta e finalize-o (PowerShell):

```powershell
netstat -ano | findstr ":5000"
Get-Process -Id <PID> | Select-Object Id,ProcessName,Path
Stop-Process -Id <PID> -Force
```

  - Ou execute em portas alternativas:

```powershell
dotnet run --project src/ViveiroEscolar.Web --urls "http://localhost:5005;https://localhost:5006"
# ou com watch
$env:ASPNETCORE_URLS="http://localhost:5005;https://localhost:5006"; dotnet watch --project src/ViveiroEscolar.Web
```

- **Certificado HTTPS de desenvolvimento:** se o HTTPS local não for confiável, rode:

```powershell
dotnet dev-certs https --trust
```

- **Lock de arquivos durante build/watch:** evite múltiplas instâncias do mesmo projeto. Caso veja erros de "file is being used by another process", finalize o processo que está rodando o app (Ctrl+C ou `Stop-Process`).

- **Script de conveniência:** existe um script `scripts/run-watch.ps1` que mata qualquer processo que esteja usando a porta especificada e inicia `dotnet watch` (padrão `5000`). Exemplo de uso:

```powershell
# roda watcher padrão na porta 5000
.\scripts\run-watch.ps1

# roda watcher em portas alternativas
#.\scripts\run-watch.ps1 -HttpPort 5005 -HttpsPort 5006
```

---
---
