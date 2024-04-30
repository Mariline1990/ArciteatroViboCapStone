using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ArciteatroVibo.Models;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string? Immagine { get; set; }

    public string Titolo { get; set; } = null!;

    public string Testo { get; set; } = null!;

    public int PostiLiberi { get; set; }

    public string? Testo2 { get; set; }

    public string? EMail { get; set; }

    [Column("Telefono")]
    [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
    public string? Telefono { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly DataInizio { get; set; }
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateOnly? DataFine { get; set; }

    public decimal? Costo { get; set; }

    [NotMapped]
    public IFormFile? ImmagineUp { get; set; }

    public virtual ICollection<Richieste> Richiestes { get; set; } = new List<Richieste>();
}
