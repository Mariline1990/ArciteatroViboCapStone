using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Contattaci
{
    public int IdContattici { get; set; }

    public string? Titolo { get; set; }

    public string? Email { get; set; }

    public string? Testo { get; set; }
}
