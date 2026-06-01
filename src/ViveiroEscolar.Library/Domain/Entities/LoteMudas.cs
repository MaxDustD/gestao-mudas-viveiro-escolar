using System;
using System.Collections.Generic;
using ViveiroEscolar.Library.Domain.Enums;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed class LoteMudas
{
    private readonly List<Cuidado> _cuidados = new();
    private readonly List<Retirada> _retiradas = new();

    public Guid Id { get; }
    public Guid EspecieId { get; private set; }
    public Guid CanteiroId { get; private set; }
    public Guid? ResponsavelId { get; private set; }
    public int QuantidadeInicial { get; private set; }
    public int QuantidadeDisponivel { get; private set; }
    public DateTime DataPlantio { get; private set; }
    public LoteStatus Status { get; private set; }
    public string? JustificativaEncerramento { get; private set; }

    public IReadOnlyCollection<Cuidado> Cuidados => _cuidados.AsReadOnly();
    public IReadOnlyCollection<Retirada> Retiradas => _retiradas.AsReadOnly();

    public LoteMudas(Guid id, Guid especieId, Guid canteiroId, int quantidadeInicial, DateTime dataPlantio, Guid? responsavelId = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Lote deve possuir um identificador válido.") : id;
        EspecieId = especieId == Guid.Empty ? throw new DomainException("Lote deve estar associado a uma espécie válida.") : especieId;
        CanteiroId = canteiroId == Guid.Empty ? throw new DomainException("Lote deve estar associado a um canteiro válido.") : canteiroId;
        QuantidadeInicial = quantidadeInicial <= 0 ? throw new DomainException("Quantidade inicial deve ser maior que zero.") : quantidadeInicial;
        DataPlantio = dataPlantio == default ? throw new DomainException("Data de plantio é obrigatória.") : dataPlantio;
        ResponsavelId = responsavelId;
        QuantidadeDisponivel = QuantidadeInicial;
        Status = LoteStatus.Ativo;
    }

    public void RegistrarCuidado(Cuidado cuidado)
    {
        if (cuidado.LoteId != Id)
        {
            throw new DomainException("O cuidado informado não pertence a este lote.");
        }

        if (Status == LoteStatus.Encerrado)
        {
            throw new DomainException("Não é possível registrar cuidado em lote encerrado.");
        }

        _cuidados.Add(cuidado);
    }

    public void RegistrarRetirada(Retirada retirada)
    {
        if (retirada.LoteId != Id)
        {
            throw new DomainException("A retirada informada não pertence a este lote.");
        }

        if (Status == LoteStatus.Encerrado)
        {
            throw new DomainException("Não é possível registrar retirada em lote encerrado.");
        }

        if (retirada.Quantidade > QuantidadeDisponivel)
        {
            throw new DomainException("Quantidade de retirada não pode exceder a quantidade disponível.");
        }

        QuantidadeDisponivel -= retirada.Quantidade;
        _retiradas.Add(retirada);
    }

    public void Encerrar(string? justificativaEncerramento = null)
    {
        if (Status == LoteStatus.Encerrado)
        {
            return;
        }

        if (QuantidadeDisponivel > 0 && string.IsNullOrWhiteSpace(justificativaEncerramento))
        {
            throw new DomainException("Encerramento de lote com quantidade disponível exige justificativa.");
        }

        Status = LoteStatus.Encerrado;
        JustificativaEncerramento = justificativaEncerramento?.Trim();
    }

    public void AtualizarCanteiro(Guid canteiroId)
    {
        if (canteiroId == Guid.Empty)
        {
            throw new DomainException("Canteiro deve ser válido.");
        }

        CanteiroId = canteiroId;
    }

    public void AtualizarResponsavel(Guid? responsavelId)
    {
        ResponsavelId = responsavelId;
    }

    public void AtualizarDataPlantio(DateTime dataPlantio)
    {
        if (dataPlantio == default)
        {
            throw new DomainException("Data de plantio é obrigatória.");
        }

        DataPlantio = dataPlantio;
    }

    public void AtualizarQuantidadeInicial(int quantidadeInicial)
    {
        if (quantidadeInicial <= 0)
        {
            throw new DomainException("Quantidade inicial deve ser maior que zero.");
        }

        var quantidadeRetirada = QuantidadeInicial - QuantidadeDisponivel;
        if (quantidadeInicial < quantidadeRetirada)
        {
            throw new DomainException("Quantidade inicial não pode ser menor que a quantidade já retirada.");
        }

        QuantidadeInicial = quantidadeInicial;
        QuantidadeDisponivel = quantidadeInicial - quantidadeRetirada;
    }
}

