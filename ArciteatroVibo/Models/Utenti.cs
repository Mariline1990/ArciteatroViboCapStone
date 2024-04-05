using System;
using System.Collections.Generic;

namespace ArciteatroVibo.Models;

public partial class Utenti
{
    public int IdUtente { get; set; }

    public bool Newsletter { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool Ruolo { get; set; }
}
