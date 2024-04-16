using System;
using System.Collections.Generic;

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

    public int? Telefono { get; set; }

    public DateOnly DataInizio { get; set; }

    public DateOnly? DataFine { get; set; }

    public virtual ICollection<Richieste> Richiestes { get; set; } = new List<Richieste>();
}
