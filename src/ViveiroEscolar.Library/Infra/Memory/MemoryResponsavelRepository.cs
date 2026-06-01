using System;
using System.Collections.Generic;
using ViveiroEscolar.Library.Application.Ports.Repositories;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Infra.Memory;

public sealed class MemoryResponsavelRepository : IResponsavelRepository
{
    private readonly Dictionary<Guid, Responsavel> _store = new();

    public void Add(Responsavel entity)
    {
        if (_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Responsável com esse identificador já existe.");
        }

        _store[entity.Id] = entity;
    }

    public Responsavel? GetById(Guid id)
    {
        _store.TryGetValue(id, out var responsavel);
        return responsavel;
    }

    public IReadOnlyCollection<Responsavel> ListAll() => _store.Values;

    public void Update(Responsavel entity)
    {
        if (!_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Responsável não encontrado.");
        }

        _store[entity.Id] = entity;
    }

    public void Remove(Guid id)
    {
        if (!_store.Remove(id))
        {
            throw new DomainException("Responsável não encontrado.");
        }
    }
}
