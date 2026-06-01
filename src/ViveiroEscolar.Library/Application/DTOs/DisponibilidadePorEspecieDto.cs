namespace ViveiroEscolar.Library.Application.DTOs;

public sealed record DisponibilidadePorEspecieDto(Guid EspecieId, string NomeComum, string NomeCientifico, int QuantidadeDisponivel);
