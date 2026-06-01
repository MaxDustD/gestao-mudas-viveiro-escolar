using System;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed record Cuidado
{
    public Guid Id { get; init; }
    public Guid LoteId { get; init; }
    public DateTime Data { get; init; }
    public string TipoCuidado { get; init; }
    public string Descricao { get; init; }
    public Guid ResponsavelId { get; init; }
    public string? Observacoes { get; init; }

    public Cuidado(Guid id, Guid loteId, DateTime data, string tipoCuidado, string descricao, Guid responsavelId, string? observacoes = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Cuidado deve possuir um identificador válido.") : id;
        LoteId = loteId == Guid.Empty ? throw new DomainException("Cuidado deve estar associado a um lote válido.") : loteId;
        Data = data == default ? throw new DomainException("Data do cuidado é obrigatória.") : data;
        TipoCuidado = ValidarTexto(tipoCuidado, nameof(tipoCuidado));
        Descricao = ValidarTexto(descricao, nameof(descricao));
        ResponsavelId = responsavelId == Guid.Empty ? throw new DomainException("Responsável do cuidado deve ser válido.") : responsavelId;
        Observacoes = observacoes?.Trim();
    }

    private static string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new DomainException($"{campo} é obrigatório e não pode estar vazio.");
        }

        return valor.Trim();
    }
}
