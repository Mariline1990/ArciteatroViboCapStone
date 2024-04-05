using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Contatti
{
    public int IdContatto { get; set; }

    public string Sede { get; set; } = null!;

    public int Telefono1 { get; set; }

    public int? Telefono2 { get; set; }

    public string Email { get; set; } = null!;

    public string? Pec { get; set; }
}
