using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Rassegne
{
    public int IdRassegna { get; set; }

    public string Titolo { get; set; } = null!;

    public string Locandina { get; set; } = null!;

    public string? Testo { get; set; }

    public string? Edizione { get; set; }

    public DateOnly? Data { get; set; }

    public string? Luogo { get; set; }

    public string? Regia { get; set; }

    public string? Interpreti { get; set; }

    public string? Extra { get; set; }

    [NotMapped]
    public IFormFile? LocandinaUp { get; set; }
}
