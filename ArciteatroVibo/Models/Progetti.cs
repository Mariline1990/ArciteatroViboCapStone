using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Progetti
{
    public int IdProgetto { get; set; }

    public string Titolo { get; set; } = null!;

    public string Testo { get; set; } = null!;

    public DateOnly? Data { get; set; }

    public string? Luogo { get; set; }

    public string? Immagine { get; set; }

    public string? Pdf { get; set; }
}
