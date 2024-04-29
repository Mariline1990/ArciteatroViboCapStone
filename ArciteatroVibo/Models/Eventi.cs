using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Eventi
{
    public int IdEvento { get; set; }

    public string Titolo { get; set; } = null!;

    public string Sottotitolo { get; set; } = null!;
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly? Data { get; set; }

    public string? Luogo { get; set; }

    public bool InCorso { get; set; }

    public string? Testo { get; set; }

    public string? Locandina { get; set; }

    public decimal? BigliettoCosto { get; set; }

    [NotMapped]
    public IFormFile? LocandinaUp { get; set; }
}
