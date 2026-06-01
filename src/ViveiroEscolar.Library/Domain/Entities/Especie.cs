using System;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed class Especie
{
    public Guid Id { get; }
    public string NomeCientifico { get; private set; }
    public string NomeComum { get; private set; }
    public string? Observacoes { get; private set; }

    public Especie(Guid id, string nomeCientifico, string nomeComum, string? observacoes = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Especie deve possuir um identificador válido.") : id;
        NomeCientifico = ValidarTexto(nomeCientifico, nameof(nomeCientifico));
        NomeComum = ValidarTexto(nomeComum, nameof(nomeComum));
        Observacoes = observacoes?.Trim();
    }

    public void AtualizarNomeCientifico(string nomeCientifico)
    {
        NomeCientifico = ValidarTexto(nomeCientifico, nameof(nomeCientifico));
    }

    public void AtualizarNomeComum(string nomeComum)
    {
        NomeComum = ValidarTexto(nomeComum, nameof(nomeComum));
    }

    public void AtualizarObservacoes(string? observacoes)
    {
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
