using System;
using System.Collections.Generic;
using System.Linq;
using ViveiroEscolar.Library.Application.DTOs;
using ViveiroEscolar.Library.Application.Ports.Repositories;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Application.Services;

public sealed class ViveiroApplicationService
{
    private readonly IEspecieRepository _especieRepository;
    private readonly ICanteiroRepository _canteiroRepository;
    private readonly IResponsavelRepository _responsavelRepository;
    private readonly ILoteRepository _loteRepository;

    public ViveiroApplicationService(
        IEspecieRepository especieRepository,
        ICanteiroRepository canteiroRepository,
        IResponsavelRepository responsavelRepository,
        ILoteRepository loteRepository)
    {
        _especieRepository = especieRepository;
        _canteiroRepository = canteiroRepository;
        _responsavelRepository = responsavelRepository;
        _loteRepository = loteRepository;
    }

    public void AdicionarEspecie(Especie especie)
    {
        VerificarEntidadeNaoNula(especie, nameof(especie));
        _especieRepository.Add(especie);
    }

    public IReadOnlyCollection<Especie> ListarEspecies() => _especieRepository.ListAll();

    public void AdicionarCanteiro(Canteiro canteiro)
    {
        VerificarEntidadeNaoNula(canteiro, nameof(canteiro));
        _canteiroRepository.Add(canteiro);
    }

    public IReadOnlyCollection<Canteiro> ListarCanteiros() => _canteiroRepository.ListAll();

    public void AdicionarResponsavel(Responsavel responsavel)
    {
        VerificarEntidadeNaoNula(responsavel, nameof(responsavel));
        _responsavelRepository.Add(responsavel);
    }

    public IReadOnlyCollection<Responsavel> ListarResponsaveis() => _responsavelRepository.ListAll();

    public void CriarLote(Guid id, Guid especieId, Guid canteiroId, int quantidadeInicial, DateTime dataPlantio, Guid? responsavelId = null)
    {
        if (_especieRepository.GetById(especieId) is null)
        {
            throw new DomainException("Espécie não encontrada.");
        }

        if (_canteiroRepository.GetById(canteiroId) is null)
        {
            throw new DomainException("Canteiro não encontrado.");
        }

        if (responsavelId.HasValue && _responsavelRepository.GetById(responsavelId.Value) is null)
        {
            throw new DomainException("Responsável informado não encontrado.");
        }

        var lote = new LoteMudas(id, especieId, canteiroId, quantidadeInicial, dataPlantio, responsavelId);
        _loteRepository.Add(lote);
    }

    public IReadOnlyCollection<LoteMudas> ListarLotes() => _loteRepository.ListAll();

    public IReadOnlyCollection<LoteMudas> ListarLotesPorEspecie(Guid especieId) => _loteRepository.ListByEspecie(especieId);

    public IReadOnlyCollection<LoteMudas> ListarLotesAtivos() => _loteRepository.ListActives();

    public void RegistrarCuidado(Guid loteId, Cuidado cuidado)
    {
        VerificarEntidadeNaoNula(cuidado, nameof(cuidado));

        var lote = ObterLoteExistente(loteId);
        lote.RegistrarCuidado(cuidado);
        _loteRepository.Update(lote);
    }

    public void RegistrarRetirada(Guid loteId, Retirada retirada)
    {
        VerificarEntidadeNaoNula(retirada, nameof(retirada));

        var lote = ObterLoteExistente(loteId);
        lote.RegistrarRetirada(retirada);
        _loteRepository.Update(lote);
    }

    public void EncerrarLote(Guid loteId, string? justificativaEncerramento = null)
    {
        var lote = ObterLoteExistente(loteId);
        lote.Encerrar(justificativaEncerramento);
        _loteRepository.Update(lote);
    }

    public IReadOnlyCollection<DisponibilidadePorEspecieDto> ConsultarDisponibilidadePorEspecie()
    {
        var lotes = _loteRepository.ListAll();

        return lotes
            .GroupBy(l => l.EspecieId)
            .Select(grupo =>
            {
                var especie = _especieRepository.GetById(grupo.Key);
                return new DisponibilidadePorEspecieDto(
                    grupo.Key,
                    especie?.NomeComum ?? string.Empty,
                    especie?.NomeCientifico ?? string.Empty,
                    grupo.Sum(l => l.QuantidadeDisponivel));
            })
            .ToArray();
    }

    private LoteMudas ObterLoteExistente(Guid loteId)
    {
        var lote = _loteRepository.GetById(loteId);
        return lote ?? throw new DomainException("Lote não encontrado.");
    }

    private static void VerificarEntidadeNaoNula(object entity, string nome)
    {
        if (entity is null)
        {
            throw new DomainException($"{nome} não pode ser nulo.");
        }
    }
}
