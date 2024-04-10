using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArciteatroVibo.Models;

public partial class Home1
{
    public int IdHome1 { get; set; }

    public string Foto1 { get; set; } = null!;

    public string? Foto2 { get; set; }

    public string? Foto3 { get; set; }

    public string? Upload { get; set; }

    public string? Video { get; set; }

    [NotMapped]
    public IFormFile updateimm { get; set; }

    [NotMapped]
    public IFormFile updateimmDue { get; set; }

    [NotMapped]
    public IFormFile updateimmTre { get; set; }


    [NotMapped] // Questo campo non verrà mappato nel database
    public IFormFile UploadUp { get; set; } // Campo per il caricamento del file PDF


    [NotMapped] // Questo campo non verrà mappato nel database
    public IFormFile Uploadvideo { get; set; } // Campo per il caricamento del file PDF
}
