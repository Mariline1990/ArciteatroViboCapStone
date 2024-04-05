using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Card
{
    public int IdCard { get; set; }

    public string Foto { get; set; } = null!;

    public string Titolo { get; set; } = null!;
}
