using System;
using ViveiroEscolar.Library.Domain.Enums;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed record Retirada
{
    public Guid Id { get; init; }
    public Guid LoteId { get; init; }
    public DateTime Data { get; init; }
    public int Quantidade { get; init; }
    public string Motivo { get; init; }
    public DestinoRetirada Destino { get; init; }
    public Guid ResponsavelId { get; init; }
    public string? Observacoes { get; init; }

    public Retirada(Guid id, Guid loteId, DateTime data, int quantidade, string motivo, DestinoRetirada destino, Guid responsavelId, string? observacoes = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Retirada deve possuir um identificador válido.") : id;
        LoteId = loteId == Guid.Empty ? throw new DomainException("Retirada deve estar associada a um lote válido.") : loteId;
        Data = data == default ? throw new DomainException("Data da retirada é obrigatória.") : data;
        Quantidade = quantidade <= 0 ? throw new DomainException("Quantidade da retirada deve ser maior que zero.") : quantidade;
        Motivo = ValidarTexto(motivo, nameof(motivo));
        Destino = destino;
        ResponsavelId = responsavelId == Guid.Empty ? throw new DomainException("Responsável da retirada deve ser válido.") : responsavelId;
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
