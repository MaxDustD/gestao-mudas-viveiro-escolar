using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViveiroEscolar.Library.Application.Services;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Enums;
using ViveiroEscolar.Library.Domain.Exceptions;
using ViveiroEscolar.Library.Infra.Memory;

Console.OutputEncoding = Encoding.UTF8;

var especieRepository = new MemoryEspecieRepository();
var canteiroRepository = new MemoryCanteiroRepository();
var responsavelRepository = new MemoryResponsavelRepository();
var loteRepository = new MemoryLoteRepository();
var service = new ViveiroApplicationService(
    especieRepository,
    canteiroRepository,
    responsavelRepository,
    loteRepository);

var menuService = new ConsoleMenuService(service);
menuService.Execute();

public class ConsoleMenuService
{
    private readonly ViveiroApplicationService _service;

    public ConsoleMenuService(ViveiroApplicationService service)
    {
        _service = service;
    }

    public void Execute()
    {
        bool executando = true;

        while (executando)
        {
            ExibirMenuPrincipal();
            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    MenuEspecies();
                    break;
                case "2":
                    MenuCanteiros();
                    break;
                case "3":
                    MenuResponsaveis();
                    break;
                case "4":
                    MenuLotes();
                    break;
                case "5":
                    MenuConsultas();
                    break;
                case "0":
                    executando = false;
                    Console.WriteLine("\nAt� logo!");
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (executando)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    private void ExibirMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  SISTEMA DE CONTROLE DE VIVEIRO ESCOLAR�");
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("\n1 - Esp�cies");
        Console.WriteLine("2 - Canteiros");
        Console.WriteLine("3 - Respons�veis");
        Console.WriteLine("4 - Lotes");
        Console.WriteLine("5 - Consultas");
        Console.WriteLine("0 - Sair");
        Console.Write("\nEscolha uma op��o: ");
    }

    private void MenuEspecies()
    {
        bool voltarAoMenu = false;

        while (!voltarAoMenu)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("�  MENU DE ESP�CIES                      �");
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("\n1 - Cadastrar esp�cie");
            Console.WriteLine("2 - Listar esp�cies");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("\nEscolha uma op��o: ");

            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    CadastrarEspecie();
                    break;
                case "2":
                    ListarEspecies();
                    break;
                case "0":
                    voltarAoMenu = true;
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (!voltarAoMenu)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void MenuCanteiros()
    {
        bool voltarAoMenu = false;

        while (!voltarAoMenu)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("�  GERENCIAR CANTEIROS                    �");
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("\n1 - Listar Canteiros");
            Console.WriteLine("2 - Criar Novo Canteiro");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("\nEscolha uma op��o: ");

            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    ListarCanteiros();
                    break;
                case "2":
                    CriarCanteiro();
                    break;
                case "0":
                    voltarAoMenu = true;
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (!voltarAoMenu)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void MenuResponsaveis()
    {
        bool voltarAoMenu = false;

        while (!voltarAoMenu)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("�  GERENCIAR RESPONS�VEIS                �");
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("\n1 - Listar Respons�veis");
            Console.WriteLine("2 - Criar Novo Respons�vel");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("\nEscolha uma op��o: ");

            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    ListarResponsaveis();
                    break;
                case "2":
                    CriarResponsavel();
                    break;
                case "0":
                    voltarAoMenu = true;
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (!voltarAoMenu)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void MenuLotes()
    {
        bool voltarAoMenu = false;

        while (!voltarAoMenu)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("�  GERENCIAR LOTES                       �");
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("\n1 - Criar lote");
            Console.WriteLine("2 - Registrar cuidado");
            Console.WriteLine("3 - Registrar retirada");
            Console.WriteLine("4 - Encerrar lote");
            Console.WriteLine("5 - Listar lotes");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("\nEscolha uma op��o: ");

            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    CriarLote();
                    break;
                case "2":
                    RegistrarCuidado();
                    break;
                case "3":
                    RegistrarRetirada();
                    break;
                case "4":
                    EncerrarLote();
                    break;
                case "5":
                    ListarLotes();
                    break;
                case "0":
                    voltarAoMenu = true;
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (!voltarAoMenu)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void MenuConsultas()
    {
        bool voltarAoMenu = false;

        while (!voltarAoMenu)
        {
            Console.Clear();
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("�  CONSULTAS                             �");
            Console.WriteLine("+----------------------------------------+");
            Console.WriteLine("\n1 - Disponibilidade por esp�cie");
            Console.WriteLine("2 - Lotes ativos");
            Console.WriteLine("0 - Voltar ao menu principal");
            Console.Write("\nEscolha uma op��o: ");

            var opcao = Console.ReadLine() ?? "0";

            switch (opcao)
            {
                case "1":
                    ConsultarDisponibilidade();
                    break;
                case "2":
                    ListarLotesAtivos();
                    break;
                case "0":
                    voltarAoMenu = true;
                    break;
                default:
                    Console.WriteLine("\nOp��o inv�lida. Tente novamente.");
                    break;
            }

            if (!voltarAoMenu)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }

    private void CadastrarEspecie()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  CADASTRAR ESP�CIE                     �");
        Console.WriteLine("+----------------------------------------+");

        Console.Write("\nNome cient�fico: ");
        var nomeCientifico = Console.ReadLine() ?? string.Empty;
        Console.Write("Nome comum: ");
        var nomeComum = Console.ReadLine() ?? string.Empty;
        Console.Write("Observa��es (opcional): ");
        var observacoes = Console.ReadLine();

        try
        {
            _service.AdicionarEspecie(new Especie(Guid.NewGuid(), nomeCientifico, nomeComum, string.IsNullOrWhiteSpace(observacoes) ? null : observacoes));
            Console.WriteLine($"\n? Esp�cie '{nomeComum}' cadastrada com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void ListarEspecies()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  ESP�CIES CADASTRADAS                   �");
        Console.WriteLine("+----------------------------------------+");

        var especies = _service.ListarEspecies().ToList();
        if (!especies.Any())
        {
            Console.WriteLine("\nNenhuma esp�cie cadastrada.");
            return;
        }

        foreach (var especie in especies)
        {
            Console.WriteLine($"\nID: {especie.Id}");
            Console.WriteLine($"Nome comum: {especie.NomeComum}");
            Console.WriteLine($"Nome cient�fico: {especie.NomeCientifico}");
            Console.WriteLine($"Observa��es: {especie.Observacoes ?? "-"}");
        }
    }

    private void CriarCanteiro()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  CRIAR NOVO CANTEIRO                   �");
        Console.WriteLine("+----------------------------------------+");

        Console.Write("\nNome do canteiro: ");
        var nome = Console.ReadLine() ?? string.Empty;
        Console.Write("Descri��o (opcional): ");
        var descricao = Console.ReadLine();
        Console.Write("Localiza��o (opcional): ");
        var localizacao = Console.ReadLine();

        try
        {
            _service.AdicionarCanteiro(new Canteiro(Guid.NewGuid(), nome, string.IsNullOrWhiteSpace(descricao) ? null : descricao, string.IsNullOrWhiteSpace(localizacao) ? null : localizacao));
            Console.WriteLine($"\n? Canteiro '{nome}' criado com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void ListarCanteiros()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  CANTEIROS CADASTRADOS                  �");
        Console.WriteLine("+----------------------------------------+");

        var canteiros = _service.ListarCanteiros().ToList();
        if (!canteiros.Any())
        {
            Console.WriteLine("\nNenhum canteiro cadastrado.");
            return;
        }

        foreach (var canteiro in canteiros)
        {
            Console.WriteLine($"\nID: {canteiro.Id}");
            Console.WriteLine($"Nome: {canteiro.Nome}");
            Console.WriteLine($"Descri��o: {canteiro.Descricao ?? "-"}");
            Console.WriteLine($"Localiza��o: {canteiro.Localizacao ?? "-"}");
        }
    }

    private void CriarResponsavel()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  CRIAR NOVO RESPONS�VEL                �");
        Console.WriteLine("+----------------------------------------+");

        Console.Write("\nNome: ");
        var nome = Console.ReadLine() ?? string.Empty;
        Console.Write("Contato: ");
        var contato = Console.ReadLine() ?? string.Empty;
        Console.Write("Fun��o (opcional): ");
        var funcao = Console.ReadLine();

        try
        {
            _service.AdicionarResponsavel(new Responsavel(Guid.NewGuid(), nome, contato, string.IsNullOrWhiteSpace(funcao) ? null : funcao));
            Console.WriteLine($"\n? Respons�vel '{nome}' cadastrado com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void ListarResponsaveis()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  RESPONS�VEIS CADASTRADOS               �");
        Console.WriteLine("+----------------------------------------+");

        var responsaveis = _service.ListarResponsaveis().ToList();
        if (!responsaveis.Any())
        {
            Console.WriteLine("\nNenhum respons�vel cadastrado.");
            return;
        }

        foreach (var responsavel in responsaveis)
        {
            Console.WriteLine($"\nID: {responsavel.Id}");
            Console.WriteLine($"Nome: {responsavel.Nome}");
            Console.WriteLine($"Contato: {responsavel.Contato}");
            Console.WriteLine($"Fun��o: {responsavel.Funcao ?? "-"}");
        }
    }

    private void CriarLote()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  CRIAR LOTE                             �");
        Console.WriteLine("+----------------------------------------+");

        if (!SelecionarEspecie(out var especieId))
            return;

        if (!SelecionarCanteiro(out var canteiroId))
            return;

        Console.Write("\nQuantidade inicial: ");
        if (!int.TryParse(Console.ReadLine(), out var quantidadeInicial))
        {
            Console.WriteLine("Quantidade inv�lida.");
            return;
        }

        Console.Write("Data de plantio (YYYY-MM-DD): ");
        var dataPlantio = DateTime.TryParse(Console.ReadLine(), out var data) ? data : DateTime.Today;

        var responsavelId = SelecionarResponsavelOpcional();

        try
        {
            _service.CriarLote(Guid.NewGuid(), especieId, canteiroId, quantidadeInicial, dataPlantio, responsavelId);
            Console.WriteLine("\n? Lote criado com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void RegistrarCuidado()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  REGISTRAR CUIDADO                      �");
        Console.WriteLine("+----------------------------------------+");

        if (!SelecionarLote(out var lote))
            return;

        Console.Write("\nTipo de cuidado: ");
        var tipo = Console.ReadLine() ?? string.Empty;
        Console.Write("Descri��o do cuidado: ");
        var descricao = Console.ReadLine() ?? string.Empty;
        Console.Write("Data do cuidado (YYYY-MM-DD): ");
        var data = DateTime.TryParse(Console.ReadLine(), out var dataCuidado) ? dataCuidado : DateTime.Today;

        try
        {
            var responsavelId = SelecionarResponsavelObrigatorio();
            _service.RegistrarCuidado(lote.Id, new Cuidado(Guid.NewGuid(), lote.Id, data, tipo, descricao, responsavelId));
            Console.WriteLine("\n? Cuidado registrado com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void RegistrarRetirada()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  REGISTRAR RETIRADA                     �");
        Console.WriteLine("+----------------------------------------+");

        if (!SelecionarLote(out var lote))
            return;

        Console.Write("\nQuantidade retirada: ");
        if (!int.TryParse(Console.ReadLine(), out var quantidade))
        {
            Console.WriteLine("Quantidade inv�lida.");
            return;
        }

        Console.Write("Motivo: ");
        var motivo = Console.ReadLine() ?? string.Empty;
        var destino = LerDestino();
        var responsavelId = SelecionarResponsavelObrigatorio();
        Console.Write("Observa��es (opcional): ");
        var observacoes = Console.ReadLine();

        try
        {
            _service.RegistrarRetirada(lote.Id, new Retirada(Guid.NewGuid(), lote.Id, DateTime.Now, quantidade, motivo, destino, responsavelId, string.IsNullOrWhiteSpace(observacoes) ? null : observacoes));
            Console.WriteLine("\n? Retirada registrada com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void EncerrarLote()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  ENCERRAR LOTE                          �");
        Console.WriteLine("+----------------------------------------+");

        if (!SelecionarLote(out var lote))
            return;

        string? justificativa = null;
        if (lote.QuantidadeDisponivel > 0)
        {
            Console.Write("Justificativa (obrigat�rio se houver mudas dispon�veis): ");
            justificativa = Console.ReadLine();
        }

        try
        {
            _service.EncerrarLote(lote.Id, justificativa);
            Console.WriteLine("\n? Lote encerrado com sucesso!");
        }
        catch (DomainException e)
        {
            Console.WriteLine($"\nERRO: {e.Message}");
        }
    }

    private void ListarLotes()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  LOTES CADASTRADOS                      �");
        Console.WriteLine("+----------------------------------------+");

        var lotes = _service.ListarLotes().ToList();
        if (!lotes.Any())
        {
            Console.WriteLine("\nNenhum lote cadastrado.");
            return;
        }

        foreach (var lote in lotes)
        {
            Console.WriteLine($"\nID: {lote.Id}");
            Console.WriteLine($"Esp�cie: {lote.EspecieId}");
            Console.WriteLine($"Canteiro: {lote.CanteiroId}");
            Console.WriteLine($"Inicial: {lote.QuantidadeInicial}");
            Console.WriteLine($"Dispon�vel: {lote.QuantidadeDisponivel}");
            Console.WriteLine($"Status: {lote.Status}");
        }
    }

    private void ConsultarDisponibilidade()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  DISPONIBILIDADE POR ESP�CIE           �");
        Console.WriteLine("+----------------------------------------+");

        var disponibilidade = _service.ConsultarDisponibilidadePorEspecie().ToList();
        if (!disponibilidade.Any())
        {
            Console.WriteLine("\nNenhuma esp�cie cadastrada.");
            return;
        }

        foreach (var item in disponibilidade)
        {
            Console.WriteLine($"\n{item.NomeComum} ({item.NomeCientifico}) - Dispon�vel: {item.QuantidadeDisponivel}");
        }
    }

    private void ListarLotesAtivos()
    {
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("�  LOTES ATIVOS                           �");
        Console.WriteLine("+----------------------------------------+");

        var lotes = _service.ListarLotesAtivos().ToList();
        if (!lotes.Any())
        {
            Console.WriteLine("\nNenhum lote ativo.");
            return;
        }

        foreach (var lote in lotes)
        {
            Console.WriteLine($"\nID: {lote.Id}");
            Console.WriteLine($"Esp�cie: {lote.EspecieId}");
            Console.WriteLine($"Canteiro: {lote.CanteiroId}");
            Console.WriteLine($"Dispon�vel: {lote.QuantidadeDisponivel}");
        }
    }

    private bool SelecionarEspecie(out Guid id)
    {
        var especies = _service.ListarEspecies().ToArray();
        if (!especies.Any())
        {
            Console.WriteLine("\nNenhuma esp�cie cadastrada.");
            id = Guid.Empty;
            return false;
        }

        for (var i = 0; i < especies.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {especies[i].NomeComum} ({especies[i].NomeCientifico})");
        }

        Console.Write("\nEscolha uma op��o: ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha < 1 || escolha > especies.Length)
        {
            Console.WriteLine("Op��o inv�lida.");
            id = Guid.Empty;
            return false;
        }

        id = especies[escolha - 1].Id;
        return true;
    }

    private bool SelecionarCanteiro(out Guid id)
    {
        var canteiros = _service.ListarCanteiros().ToArray();
        if (!canteiros.Any())
        {
            Console.WriteLine("\nNenhum canteiro cadastrado.");
            id = Guid.Empty;
            return false;
        }

        for (var i = 0; i < canteiros.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {canteiros[i].Nome}");
        }

        Console.Write("\nEscolha uma op��o: ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha < 1 || escolha > canteiros.Length)
        {
            Console.WriteLine("Op��o inv�lida.");
            id = Guid.Empty;
            return false;
        }

        id = canteiros[escolha - 1].Id;
        return true;
    }

    private bool SelecionarLote(out LoteMudas lote)
    {
        var lotes = _service.ListarLotes().ToArray();
        if (!lotes.Any())
        {
            Console.WriteLine("\nNenhum lote cadastrado.");
            lote = null!;
            return false;
        }

        for (var i = 0; i < lotes.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {lotes[i].Id} | Dispon�vel: {lotes[i].QuantidadeDisponivel} | {lotes[i].Status}");
        }

        Console.Write("\nEscolha uma op��o: ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha < 1 || escolha > lotes.Length)
        {
            Console.WriteLine("Op��o inv�lida.");
            lote = null!;
            return false;
        }

        lote = lotes[escolha - 1];
        return true;
    }

    private Guid SelecionarResponsavelObrigatorio()
    {
        var responsaveis = _service.ListarResponsaveis().ToArray();
        if (!responsaveis.Any())
        {
            throw new DomainException("� necess�rio cadastrar ao menos um respons�vel antes de prosseguir.");
        }

        for (var i = 0; i < responsaveis.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {responsaveis[i].Nome} - {responsaveis[i].Contato}");
        }

        Console.Write("\nEscolha uma op��o: ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha < 1 || escolha > responsaveis.Length)
        {
            throw new DomainException("Respons�vel inv�lido.");
        }

        return responsaveis[escolha - 1].Id;
    }

    private Guid? SelecionarResponsavelOpcional()
    {
        var responsaveis = _service.ListarResponsaveis().ToArray();
        if (!responsaveis.Any())
        {
            return null;
        }

        Console.WriteLine("\n0 - Nenhum respons�vel");
        for (var i = 0; i < responsaveis.Length; i++)
        {
            Console.WriteLine($"{i + 1} - {responsaveis[i].Nome}");
        }

        Console.Write("\nEscolha uma op��o: ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha < 0 || escolha > responsaveis.Length)
        {
            Console.WriteLine("Op��o inv�lida. Nenhum respons�vel atribu�do.");
            return null;
        }

        if (escolha == 0)
            return null;

        return responsaveis[escolha - 1].Id;
    }

    private DestinoRetirada LerDestino()
    {
        Console.WriteLine("\n1 - Doa��o");
        Console.WriteLine("2 - Plantio");
        Console.WriteLine("3 - Outro");
        Console.Write("\nEscolha uma op��o: ");

        var opcao = Console.ReadLine() ?? "3";
        return opcao switch
        {
            "1" => DestinoRetirada.Doacao,
            "2" => DestinoRetirada.Plantio,
            _ => DestinoRetirada.Outro,
        };
    }
}
