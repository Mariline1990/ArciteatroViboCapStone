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
}
