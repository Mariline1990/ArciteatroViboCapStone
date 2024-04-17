using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Eventi
{
    public int IdEvento { get; set; }

    public string Titolo { get; set; } = null!;

    public string Sottotitolo { get; set; } = null!;

    public DateOnly? Data { get; set; }

    public string? Luogo { get; set; }

    public bool InCorso { get; set; }

    public string? Testo { get; set; }

    public string? Locandina { get; set; }

    [NotMapped]
    public IFormFile? LocandinaUp { get; set; }
}