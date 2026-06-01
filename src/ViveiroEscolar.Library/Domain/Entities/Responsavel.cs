using System;
using ViveiroEscolar.Library.Domain.Exceptions;

namespace ViveiroEscolar.Library.Domain.Entities;

public sealed class Responsavel
{
    public Guid Id { get; }
    public string Nome { get; private set; }
    public string Contato { get; private set; }
    public string? Funcao { get; private set; }

    public Responsavel(Guid id, string nome, string contato, string? funcao = null)
    {
        Id = id == Guid.Empty ? throw new DomainException("Responsável deve possuir um identificador válido.") : id;
        Nome = ValidarTexto(nome, nameof(nome));
        Contato = ValidarTexto(contato, nameof(contato));
        Funcao = funcao?.Trim();
    }

    public void AtualizarNome(string nome)
    {
        Nome = ValidarTexto(nome, nameof(nome));
    }

    public void AtualizarContato(string contato)
    {
        Contato = ValidarTexto(contato, nameof(contato));
    }

    public void AtualizarFuncao(string? funcao)
    {
        Funcao = funcao?.Trim();
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
