using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Commedie
{
    public int IdCommedia { get; set; }

    public string Titolo { get; set; } = null!;

    public string Autore { get; set; } = null!;

    public string Trama { get; set; } = null!;

    public string? Locandina { get; set; }

    public string? Regia { get; set; }

    public string? Interpreti { get; set; }

    public string? Extra { get; set; }

    public string? Foto1 { get; set; }

    public string? Foto2 { get; set; }

    public string? Foto3 { get; set; }
}
