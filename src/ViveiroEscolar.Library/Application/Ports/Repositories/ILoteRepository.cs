using System;
using System.Collections.Generic;
using ViveiroEscolar.Library.Domain.Entities;

namespace ViveiroEscolar.Library.Application.Ports.Repositories;

public interface ILoteRepository : IRepository<LoteMudas>
{
    IReadOnlyCollection<LoteMudas> ListByEspecie(Guid especieId);
    IReadOnlyCollection<LoteMudas> ListActives();
}
