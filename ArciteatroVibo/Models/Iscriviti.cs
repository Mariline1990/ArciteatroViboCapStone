using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Iscriviti
{
    public int IdIscriviti { get; set; }

    public string Titolo { get; set; } = null!;

    public string Testo { get; set; } = null!;

    public string? EMail { get; set; }

    public int? Telefono { get; set; }

    public DateOnly? DataInizio { get; set; }

    public DateOnly? DataFine { get; set; }
}
