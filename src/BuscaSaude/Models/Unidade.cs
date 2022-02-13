using System;

namespace BuscaSaude.Models;

public class Unidade
{
    public string? Cnes { get; set; }
    public int? Uf { get; set; }
    public string? Ibge { get; set; }
    public string? Nome { get; set; }
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}