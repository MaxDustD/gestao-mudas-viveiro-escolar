using System;
using System.Collections.Generic;
using ViveiroEscolar.Library.Application.Ports.Repositories;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Infra.Memory;

public sealed class MemoryEspecieRepository : IEspecieRepository
{
    private readonly Dictionary<Guid, Especie> _store = new();

    public void Add(Especie entity)
    {
        if (_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Espécie com esse identificador já existe.");
        }

        _store[entity.Id] = entity;
    }

    public Especie? GetById(Guid id)
    {
        _store.TryGetValue(id, out var especie);
        return especie;
    }

    public IReadOnlyCollection<Especie> ListAll() => _store.Values;

    public void Update(Especie entity)
    {
        if (!_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Espécie não encontrada.");
        }

        _store[entity.Id] = entity;
    }

    public void Remove(Guid id)
    {
        if (!_store.Remove(id))
        {
            throw new DomainException("Espécie não encontrada.");
        }
    }
}
