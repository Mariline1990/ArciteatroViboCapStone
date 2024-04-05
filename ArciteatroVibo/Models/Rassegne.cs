using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Rassegne
{
    public int IdRassegna { get; set; }

    public string Titolo { get; set; } = null!;

    public string? Locandina { get; set; }

    public string Testo { get; set; } = null!;

    public string Edizione { get; set; } = null!;

    public DateOnly Data { get; set; }

    public string Luogo { get; set; } = null!;

    public string? Regia { get; set; }

    public string? Interpreti { get; set; }

    public string? Extra { get; set; }
}
