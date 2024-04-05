using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Album
{
    public int IdFoto { get; set; }

    public string? Nome { get; set; }

    public string Img { get; set; } = null!;
}
