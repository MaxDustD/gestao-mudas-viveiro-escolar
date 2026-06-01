using System;
using System.Collections.Generic;

namespace ViveiroEscolar.Library.Application.Ports.Repositories;

public interface IRepository<T>
{
    void Add(T entity);
    T? GetById(Guid id);
    IReadOnlyCollection<T> ListAll();
    void Update(T entity);
    void Remove(Guid id);
}
