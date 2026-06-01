using System;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed class Canteiro
{
    public Guid Id { get; }
    public string Nome { get; private set; }
    public string? Descricao { get; private set; }
    public string? Localizacao { get; private set; }

    public Canteiro(Guid id, string nome, string? descricao = null, string? localizacao = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Canteiro deve possuir um identificador válido.") : id;
        Nome = ValidarTexto(nome, nameof(nome));
        Descricao = descricao?.Trim();
        Localizacao = localizacao?.Trim();
    }

    public void AtualizarNome(string nome)
    {
        Nome = ValidarTexto(nome, nameof(nome));
    }

    public void AtualizarDescricao(string? descricao)
    {
        Descricao = descricao?.Trim();
    }

    public void AtualizarLocalizacao(string? localizacao)
    {
        Localizacao = localizacao?.Trim();
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
