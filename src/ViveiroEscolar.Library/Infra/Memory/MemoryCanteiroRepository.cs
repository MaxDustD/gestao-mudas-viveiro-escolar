using System;
using System.Collections.Generic;
using ViveiroEscolar.Library.Application.Ports.Repositories;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Infra.Memory;

public sealed class MemoryCanteiroRepository : ICanteiroRepository
{
    private readonly Dictionary<Guid, Canteiro> _store = new();

    public void Add(Canteiro entity)
    {
        if (_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Canteiro com esse identificador já existe.");
        }

        _store[entity.Id] = entity;
    }

    public Canteiro? GetById(Guid id)
    {
        _store.TryGetValue(id, out var canteiro);
        return canteiro;
    }

    public IReadOnlyCollection<Canteiro> ListAll() => _store.Values;

    public void Update(Canteiro entity)
    {
        if (!_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Canteiro não encontrado.");
        }

        _store[entity.Id] = entity;
    }

    public void Remove(Guid id)
    {
        if (!_store.Remove(id))
        {
            throw new DomainException("Canteiro não encontrado.");
        }
    }
}
