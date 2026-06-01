using System;
using System.Collections.Generic;
using System.Linq;
using ViveiroEscolar.Library.Application.Ports.Repositories;
using ViveiroEscolar.Library.Domain.Entities;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Infra.Memory;

public sealed class MemoryLoteRepository : ILoteRepository
{
    private readonly Dictionary<Guid, LoteMudas> _store = new();

    public void Add(LoteMudas entity)
    {
        if (_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Lote com esse identificador já existe.");
        }

        _store[entity.Id] = entity;
    }

    public LoteMudas? GetById(Guid id)
    {
        _store.TryGetValue(id, out var lote);
        return lote;
    }

    public IReadOnlyCollection<LoteMudas> ListAll() => _store.Values;

    public void Update(LoteMudas entity)
    {
        if (!_store.ContainsKey(entity.Id))
        {
            throw new DomainException("Lote não encontrado.");
        }

        _store[entity.Id] = entity;
    }

    public void Remove(Guid id)
    {
        if (!_store.Remove(id))
        {
            throw new DomainException("Lote não encontrado.");
        }
    }

    public IReadOnlyCollection<LoteMudas> ListByEspecie(Guid especieId)
    {
        return _store.Values.Where(l => l.EspecieId == especieId).ToArray();
    }

    public IReadOnlyCollection<LoteMudas> ListActives()
    {
        return _store.Values.Where(l => l.Status != Domain.Enums.LoteStatus.Encerrado).ToArray();
    }
}
