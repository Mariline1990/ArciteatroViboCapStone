using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Card
{
    public int IdCard { get; set; }

    public string Foto { get; set; } = null!;

    public string Titolo { get; set; } = null!;

    public string? link { get; set; }

    [NotMapped]
    public IFormFile FotoCard { get; set; }

    
}
